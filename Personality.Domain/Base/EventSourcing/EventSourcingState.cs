using System;
using System.Collections.Generic;

namespace Personality.Domain.Base.EventSourcing
{
    /// <summary>
    /// Состояние агрегата, сформированное набором событий
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора состояния</typeparam>
    /// <typeparam name="TEventId">Тип идентификатора события</typeparam>
    /// <typeparam name="TEvent">Тип события</typeparam>
    /// <typeparam name="TDateTime">Тип даты/времени</typeparam>
    public abstract class EventSourcingState<TId, TEventId, TEvent, TDateTime> : Entity<TId>
        where TId : IEntityId
        where TEventId : IEntityId
        where TEvent : Event<TEventId, TId, TDateTime>
        where TDateTime : IComparable<TDateTime>
    {
        private readonly List<TEvent> fixedEvents = [];
        private readonly List<TEvent> changes = [];

        /// <summary>
        /// Инициализирует новый экземпляр состояния
        /// </summary>
        /// <param name="id">Идентификатор состояния</param>
        protected EventSourcingState(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Зафиксированные события
        /// </summary>
        public IReadOnlyList<TEvent> FixedEvents => fixedEvents.AsReadOnly();

        /// <summary>
        /// Изменения
        /// </summary>
        public IReadOnlyList<TEvent> Changes => changes.AsReadOnly();


        /// <summary>
        /// Применить событие к состоянию
        /// </summary>
        /// <param name="event">Событие</param>
        internal void Apply(TEvent @event)
        {
            @event.StateId = Id;
            changes.Add(@event);
            When(@event);
        }

        /// <summary>
        /// Добавить зафиксированное событие
        /// </summary>
        /// <param name="event">Событие</param>
        internal void AddFixed(TEvent @event)
        {
            fixedEvents.Add(@event);
            When(@event);
        }

        /// <summary>
        /// Мутация состояния по событию
        /// </summary>
        /// <param name="event">Событие</param>
        protected abstract void When(TEvent @event);
    }
}
