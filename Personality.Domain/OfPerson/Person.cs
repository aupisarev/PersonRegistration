using System;
using Personality.Domain.Base.EventSourcing;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfEvent.Events;
using Personality.Domain.OfPerson.OfEvent.Events.StateEvents;
using Personality.Domain.OfPerson.OfIdentityDocument;
using Personality.Domain.OfPerson.OfPersonalData;
using Personality.Domain.OfPerson.OfState;
using System.Collections.Generic;
using System.Linq;

namespace Personality.Domain.OfPerson
{
    /// <summary>
    /// Лицо
    /// </summary>
    public sealed class Person : EventSourcingAggregateRoot<PersonId, StateId, State, PersonEventId, PersonEvent, DateTime>
    {
        /// <summary>
        /// Инициализирует новый экземпляр существующего лица
        /// </summary>
        /// <param name="events">События лица</param>
        /// <exception cref="ArgumentException"></exception>
        public Person(IList<PersonEvent> events)
        {
            if (!events.Any())
                throw new ArgumentException("Невозможно создать агрегат Лица, т.к. список событий пуст");

            Load(events);
            if (CurrentState.Status.IsFinal)
                CreateNextState(NewStateId());
        }

        /// <summary>
        /// Инициализирует новый экземпляр нового лица
        /// </summary>
        /// <param name="id">Идентификатор лица</param>
        /// <param name="personalData">Персональные данные</param>
        /// <param name="identityDocument">Документ, удостоверяющий личность</param>
        /// <param name="changeDate">Дата применения изменений</param>
        /// <param name="occurredAt">Дата события создания лица</param>
        public Person(PersonId id, PersonalData personalData, IdentityDocument identityDocument, DateTime changeDate, DateTime occurredAt)
        {
            CreateNextState(NewStateId());
            Apply(new PersonCreated(NewEventId(), id, changeDate, occurredAt));
            Apply(new PersonalDataChanged(NewEventId(), id, changeDate, occurredAt, personalData));
            Apply(new IdentityDocumentChanged(NewEventId(), id, changeDate, occurredAt, identityDocument));
        }


        /// <summary>
        /// Изменения
        /// </summary>
        public IReadOnlyList<PersonEvent> Changes => States.SelectMany(s => s.Changes).ToList().AsReadOnly();


        /// <summary>
        /// Изменить персональные данные
        /// </summary>
        /// <param name="personalData">Новые персональные данные</param>
        /// <param name="changeDate">Дата изменения</param>
        /// <param name="occurredAt">Дата события</param>
        public void ChangePersonalData(PersonalData personalData, DateTime changeDate, DateTime occurredAt)
        {
            if (CurrentState.PersonalData != personalData)
                Apply(new PersonalDataChanged(NewEventId(), Id, changeDate, occurredAt, personalData));
        }

        /// <summary>
        /// Изменить документ, удостоверяющий личность
        /// </summary>
        /// <param name="identityDocument">Новый документ</param>
        /// <param name="changeDate">Дата изменения</param>
        /// <param name="occurredAt">Дата события</param>
        public void ChangeIdentityDocument(IdentityDocument identityDocument, DateTime changeDate, DateTime occurredAt)
        {
            if (CurrentState.IdentityDocument != identityDocument)
                Apply(new IdentityDocumentChanged(NewEventId(), Id, changeDate, occurredAt, identityDocument));
        }
        
        /// <summary>
        /// Установить смерть
        /// </summary>
        /// <param name="deathDate">Дата смерти</param>
        /// <param name="changeDate">Дата принятия изменений</param>
        /// <param name="occurredAt">Дата события</param>
        public void SetDeath(DateOnly deathDate, DateTime changeDate, DateTime occurredAt)
        {
            if (CurrentState.PersonalData.DeathDate != deathDate)
                Apply(new PersonDied(NewEventId(), Id, changeDate, occurredAt, deathDate));
        }


        /// <summary>
        /// Согласовать состояние
        /// </summary>
        /// <param name="approvedDate">Дата согласования</param>
        /// <param name="occurredAt">Дата события</param>
        public void ApproveState(DateTime approvedDate, DateTime occurredAt)
        {
            EnsureStatusIs(StateStatus.Approved, StateStatus.Raw);
            Apply(new StateApproved(NewEventId(), Id, approvedDate, occurredAt));
        }

        /// <summary>
        /// Применить состояние
        /// </summary>
        /// <param name="appliedDate">Дата применения</param>
        /// <param name="occurredAt">Дата события</param>
        public void ApplyState(DateTime appliedDate, DateTime occurredAt)
        {
            EnsureStatusIs(StateStatus.Applied, StateStatus.Approved);
            Apply(new StateApplied(NewEventId(), Id, appliedDate, occurredAt));
            CreateNextState(NewStateId());
        }

        /// <summary>
        /// Отменить состояние
        /// </summary>
        /// <param name="canceledDate">Дата отмены</param>
        /// <param name="occurredAt">Дата события</param>
        public void CancelState(DateTime canceledDate, DateTime occurredAt)
        {
            EnsureStatusIs(StateStatus.Canceled, StateStatus.Raw, StateStatus.Approved);
            Apply(new StateCanceled(NewEventId(), Id, canceledDate, occurredAt));
            RollbackCurrentState(occurredAt);
            CreateNextState(NewStateId());
        }

        /// <summary>
        /// Убедиться, что статус равен одному из необходимых статусов при установке статуса в присваиваемый статус
        /// </summary>
        /// <param name="assignedStatus">Присваиваемый статус</param>
        /// <param name="needStatuses">Необходимые статусы</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void EnsureStatusIs(StateStatus assignedStatus, params StateStatus[] needStatuses)
        {
            if (!needStatuses.Contains(CurrentState.Status))
                throw new InvalidOperationException($"Невозможно перевести состояние со статусом {CurrentState.Status.Name} в статус {assignedStatus.Name}. " +
                                                    $"Необходим статус {needStatuses.Select(s => s.Name).Aggregate((p, n) => $"{p},{n}")}");
        }


        protected override State CreateState(StateId stateId, State previousState) => new(stateId, previousState);

        /// <summary>
        /// Применить событие к состоянию
        /// </summary>
        /// <param name="event">Событие</param>
        protected override void Apply(PersonEvent @event)
        {
            if (CurrentState.Status != StateStatus.Raw
                && !(@event is StateApproved or StateApplied or StateCanceled)
                && !@event.Compensatory)
                throw new InvalidOperationException($"Невозможно применить событие '{@event.Description}' к состоянию в статусе '{CurrentState.Status.Name}'");

            base.Apply(@event);
        }

        protected override void WhenStateChangedOn(PersonEvent @event)
        {
            switch (@event)
            {
                case PersonCreated:
                    Id = @event.PersonId;
                    break;
            }
        }

        /// <summary>
        /// Откатить текущее состояние
        /// </summary>
        /// <param name="occurredAt">Дата события</param>
        private void RollbackCurrentState(DateTime occurredAt)
        {
            //удаление лица не предусмотрено
            if (CurrentState.FixedEvents.Union(CurrentState.Changes).Any(e => e is PersonCreated))
                throw new InvalidOperationException("Невозможно отменить создание лица");

            var previousState = states[^2];

            foreach (var @event in CurrentState.FixedEvents.Union(CurrentState.Changes).OrderByDescending(e => e.OccurredAt))
            {
                switch (@event)
                {
                    case PersonalDataChanged:
                        Apply(new PersonalDataChanged(NewEventId(), Id, @event.ChangeDate, occurredAt, previousState.PersonalData) { Compensatory = true });
                        break;
                    case IdentityDocumentChanged:
                        Apply(new IdentityDocumentChanged(NewEventId(), Id, @event.ChangeDate, occurredAt, previousState.IdentityDocument) { Compensatory = true });
                        break;
                    case PersonDied:
                        Apply(new PersonDied(NewEventId(), Id, @event.ChangeDate, occurredAt, previousState.PersonalData.DeathDate) { Compensatory = true });
                        break;
                }
            }
        }


        /// <summary>
        /// Создать новый идентификатор состояния
        /// </summary>
        /// <returns>Новый идентификатор состояния</returns>
        private StateId NewStateId() => !States.Any() ? new StateId(1) : new StateId(States.Max(x => x.Id.Value) + 1);

        /// <summary>
        /// Создать новый идентификатор события
        /// </summary>
        /// <returns>Новый идентификатор события</returns>
        internal PersonEventId NewEventId() => new (States.Sum(s => s.FixedEvents.Count + s.Changes.Count) + 1);
    }
}
