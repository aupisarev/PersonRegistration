using AutoMapper;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfEvent.Events;
using Personality.Domain.OfPerson.OfEvent.Events.StateEvents;
using Personality.Domain.OfPerson.OfState;
using Personality.Reading.Persistence.Repository;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;
using Personality.Writing.Outside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Reading.Projecting.PersonHistoryModel
{
    //Модель истории данных лица - набор проекций на каждое свойство лица
    //Строится одна проекция каждого типа на одно состояние
    //При добавлении новой проекции следующего состояния дата окончания старой проекции не меняется до тех пор, пока новая проекция не перейдет в статус "принято"
    //При отмене состояния проекция удаляется (компенсирующие события игнорируются)
    //При принятии проекции нового состояния проекция прошлого состояния закрывается датой изменений или удаляется в случае корректировки данных новым состоянием

    /// <summary>
    /// Проектор событий на модель чтения истории данных
    /// </summary>
    public class PersonHistoryProjector : IProjector
    {
        private readonly IHistoryProjectionRepository historyProjectionRepository;
        private readonly IProjectionIdGenerator idGenerator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonHistoryProjector(
            IHistoryProjectionRepository historyProjectionRepository,
            IProjectionIdGenerator idGenerator,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.historyProjectionRepository = historyProjectionRepository ?? throw new ArgumentNullException(nameof(historyProjectionRepository));
            this.idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Проектировать событие на модель чтения
        /// </summary>
        /// <param name="events">События агрегата лица</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task ProjectAsync(IList<PersonEvent> events)
        {
            var persons = events.ToLookup(e => e.PersonId, e => e);
            foreach (var personEvents in persons)
            {
                foreach (var @event in personEvents.Where(e => !e.Compensatory).OrderBy(e => e.OccurredAt))
                {
                    var task = @event switch
                    {
                        PersonCreated _ => null,

                        PersonalDataChanged personalDataChanged => ProjectDataChanged<PersonalDataChanged, PersonalDataHistoryProjection>(personalDataChanged,
                            (e, lp) => mapper.Map<PersonalDataHistoryProjection>(personalDataChanged.PersonalData)),

                        IdentityDocumentChanged identityDocumentChanged => ProjectDataChanged<IdentityDocumentChanged, IdentityDocumentHistoryProjection>(
                            identityDocumentChanged,
                            (e, lp) => mapper.Map<IdentityDocumentHistoryProjection>(identityDocumentChanged.IdentityDocument)),

                        PersonDied personDied => ProjectDataChanged<PersonDied, PersonalDataHistoryProjection>(personDied,
                            (e, p) => new PersonalDataHistoryProjection
                            {
                                LastName = p.LastName,
                                FirstName = p.FirstName,
                                MiddleName = p.MiddleName,
                                Birthday = p.Birthday,
                                DeathDate = e.DeathDate.HasValue ? new DateTime(e.DeathDate.Value, TimeOnly.MinValue) : null,
                                SexValue = p.SexValue,
                                SexName = p.SexName,
                                CitizenshipValue = p.CitizenshipValue,
                                CitizenshipName = p.CitizenshipName
                            }),

                        StateEvent stateEvent => Project(stateEvent),

                        _ => throw new ArgumentException($"Тип события {@event.GetType().Name} не поддерживается проектором {GetType().Name}")
                    };

                    if (task != null)
                        await task;
                }
            }
        }

        /// <summary>
        /// Проектировать событие изменения данных лица
        /// </summary>
        /// <typeparam name="TEvent">Тип события</typeparam>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="event">Событие</param>
        /// <param name="projectionModifier">Фабрика проекции. Вход - событие, прошлая версия проекции. Выход - новая версия проекции.</param>
        /// <returns></returns>
        private async Task ProjectDataChanged<TEvent, TProjection>(TEvent @event, Func<TEvent, TProjection, TProjection> projectionModifier)
            where TEvent : PersonEvent
            where TProjection : class, IHistoryProjection
        {
            var projection = (await historyProjectionRepository.GetAsync<TProjection>(
                    p => p.PersonId == @event.PersonId.Value && p.StateId == @event.StateId.Value,
                    unitOfWork))
                .FirstOrDefault();

            var projectionId = projection?.Id;

            projection = projectionModifier(@event, projection);
            projection.Id = projectionId ?? await idGenerator.NewIdAsync<TProjection>();
            projection.PersonId = @event.PersonId;
            projection.StateId = @event.StateId;
            projection.StateStatusValue = StateStatus.Raw.Value;
            projection.StateStatusName = StateStatus.Raw.Name;
            projection.StartDate = @event.ChangeDate;

            if (projectionId == null)
                await historyProjectionRepository.InsertAsync(projection, unitOfWork);
            else
                await historyProjectionRepository.UpdateAsync(projection, unitOfWork);
        }

        /// <summary>
        /// Проектировать событие StateEvent
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns></returns>
        private async Task Project(StateEvent @event)
        {
            await ProjectStateChanged<PersonalDataHistoryProjection>(@event);
            await ProjectStateChanged<IdentityDocumentHistoryProjection>(@event);
        }

        /// <summary>
        /// Проектировать событие StateEvent на проекцию
        /// </summary>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="event">Событие</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private async Task ProjectStateChanged<TProjection>(StateEvent @event)
            where TProjection : class, IHistoryProjection
        {
            var projection = (await historyProjectionRepository.GetAsync<TProjection>(
                    p => p.PersonId == @event.PersonId.Value && p.StateId == @event.StateId.Value,
                    unitOfWork))
                .FirstOrDefault();

            if (projection == null)
                return;

            if (@event is StateCanceled)
                await historyProjectionRepository.DeleteAsync(projection, unitOfWork);
            else
            {
                var status = @event switch
                {
                    StateApproved => StateStatus.Approved,
                    StateApplied => StateStatus.Applied,
                    _ => throw new ArgumentException($"Тип события {@event.GetType().Name} не поддерживается проектором {GetType().Name}")
                };

                projection.StateStatusValue = status.Value;
                projection.StateStatusName = status.Name;
                await historyProjectionRepository.UpdateAsync(projection, unitOfWork);

                if (@event is StateApplied)
                {
                    var lastActualProjection = (await historyProjectionRepository.GetAsync<TProjection>(
                            p => p.PersonId == @event.PersonId.Value && p.EndDate == null,
                            unitOfWork))
                        .FirstOrDefault(p => p.StateId != @event.StateId);

                    if (lastActualProjection != null)
                    {
                        lastActualProjection.EndDate = projection.StartDate.AddMinutes(-1);
                        if (lastActualProjection.StartDate >= lastActualProjection.EndDate)
                            await historyProjectionRepository.DeleteAsync(lastActualProjection, unitOfWork);
                        else
                            await historyProjectionRepository.UpdateAsync(lastActualProjection, unitOfWork);
                    }
                }
            }
        }
    }
}
