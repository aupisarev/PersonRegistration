using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfEvent.Events;
using Personality.Domain.OfPerson.OfEvent.Events.StateEvents;
using Personality.Domain.OfPerson.OfState;
using Personality.Reading.Persistence.Repository;
using Personality.Reading.Projecting.PersonStateModel.Projection;
using Personality.Writing.Outside;

namespace Personality.Reading.Projecting.PersonStateModel
{
    //Модель состояний лица
    //Строится проекция каждого состояния с вложенными событиями

    /// <summary>
    /// Проектор событий на модель чтения состояний лица
    /// </summary>
    public class PersonStateProjector : IProjector
    {
        private readonly IPersonStateProjectionRepository personStateProjectionRepository;
        private readonly IProjectionIdGenerator idGenerator;
        private readonly IUnitOfWork unitOfWork;

        public PersonStateProjector(IPersonStateProjectionRepository personStateProjectionRepository,
            IProjectionIdGenerator idGenerator,
            IUnitOfWork unitOfWork)
        {
            this.personStateProjectionRepository = personStateProjectionRepository ?? throw new ArgumentNullException(nameof(personStateProjectionRepository));
            this.idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task ProjectAsync(IList<PersonEvent> events)
        {
            var states = events.ToLookup(e => new { e.PersonId, e.StateId }, e => e);
            foreach (var state in states)
            {
                var projectionNeedAdd = false;

                var projection = (await personStateProjectionRepository.GetAsync(
                    p=>p.PersonId == state.Key.PersonId.Value && p.StateId == state.Key.StateId.Value,
                    unitOfWork)).FirstOrDefault();

                if (projection == null)
                {
                    projectionNeedAdd = true;
                    projection = new PersonStateProjection
                    {
                        Id = await idGenerator.NewIdAsync<PersonStateProjection>(),
                        PersonId = state.Key.PersonId,
                        StateId = state.Key.StateId,
                        StatusValue = StateStatus.Raw.Value,
                        StatusName = StateStatus.Raw.Name,
                    };
                }

                projection = GetProjection(state, projection);

                if (projectionNeedAdd)
                    await personStateProjectionRepository.InsertAsync(projection, unitOfWork);
                else
                    await personStateProjectionRepository.UpdateAsync(projection, unitOfWork);
            }
        }

        private PersonStateProjection GetProjection(IEnumerable<PersonEvent> events, PersonStateProjection projection)
        {
            foreach (var @event in events.OrderBy(e => e.OccurredAt))
            {
                if (@event is StateEvent)
                {
                    var status = @event switch
                    {
                        StateApproved => StateStatus.Approved,
                        StateApplied => StateStatus.Applied,
                        StateCanceled => StateStatus.Canceled,
                        _ => throw new ArgumentException($"Тип события {@event.GetType().Name} не поддерживается проектором {GetType().Name}")
                    };

                    projection.StatusValue = status.Value;
                    projection.StatusName = status.Name;
                }

                projection.Events.Add(new PersonEventProjection
                {
                    EventId = @event.Id,
                    Compensatory = @event.Compensatory,
                    Description = @event.Description,
                    ChangeDate = @event.ChangeDate,
                    OccurredAt = @event.OccurredAt
                });
            }

            return projection;
        }
    }
}
