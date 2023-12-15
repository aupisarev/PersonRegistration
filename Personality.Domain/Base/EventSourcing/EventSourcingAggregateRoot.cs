using System;
using System.Collections.Generic;
using System.Linq;

namespace Personality.Domain.Base.EventSourcing
{
    /// <summary>
    /// Корень агрегата с коллекцией управляемых состояний
    /// </summary>
    /// <typeparam name="TId">Тип идентификатора агрегата</typeparam>
    /// <typeparam name="TStateId">Тип идентификатора состояния</typeparam>
    /// <typeparam name="TState">Тип состояния</typeparam>
    /// <typeparam name="TEventId">Тип идентификатора события</typeparam>
    /// <typeparam name="TEvent">Тип события</typeparam>
    /// <typeparam name="TDateTime">Тип даты/времени</typeparam>
    public abstract class EventSourcingAggregateRoot<TId, TStateId, TState, TEventId, TEvent, TDateTime> : AggregateRoot<TId>
        where TId : IEntityId
        where TStateId : IEntityId
        where TState : EventSourcingState<TStateId, TEventId, TEvent, TDateTime>
        where TEventId : IEntityId
        where TEvent : Event<TEventId, TStateId, TDateTime>
        where TDateTime : IComparable<TDateTime>
    {
        protected readonly List<TState> states = [];

        /// <summary>
        /// Состояния
        /// </summary>
        public IReadOnlyList<TState> States => states.AsReadOnly();

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public TState CurrentState { get; private set; }


        /// <summary>
        /// Создать следующее состояние
        /// </summary>
        /// <param name="stateId">Идентификатор состояния</param>
        protected void CreateNextState(TStateId stateId)
        {
            CurrentState = CreateState(stateId, CurrentState);
            states.Add(CurrentState);
        }

        /// <summary>
        /// Создать новое состояние
        /// </summary>
        /// <param name="stateId">Идентификатор состояния</param>
        /// <param name="previousState">Предыдущее состояние для нового состояния</param>
        /// <returns>Новое состояние</returns>
        protected abstract TState CreateState(TStateId stateId, TState previousState);

        /// <summary>
        /// Применить событие к текущему состоянию агрегата
        /// </summary>
        /// <param name="event">Событие</param>
        protected virtual void Apply(TEvent @event)
        {
            CurrentState.Apply(@event);
            WhenStateChangedOn(@event);
        }

        /// <summary>
        /// Мутация агрегата после изменения состояния
        /// </summary>
        /// <param name="event">Событие</param>
        protected abstract void WhenStateChangedOn(TEvent @event);

        /// <summary>
        /// Загрузить состояние агрегата по коллекции событий
        /// </summary>
        /// <param name="events"></param>
        protected virtual void Load(IEnumerable<TEvent> events)
        {
            foreach (var @event in events.OrderBy(e => e.OccurredAt))
            {
                if (CurrentState == null || !@event.StateId.Equals(CurrentState.Id))
                    CreateNextState(@event.StateId);

                CurrentState!.AddFixed(@event);
                WhenStateChangedOn(@event);
            }
        }
    }
}
