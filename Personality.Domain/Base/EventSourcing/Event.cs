using System;

namespace Personality.Domain.Base.EventSourcing
{
    /// <summary>
    /// Событие предметной области
    /// </summary>
    /// <remarks>
    /// Базовый класс<br/>
    /// EventSourcing
    /// </remarks>
    /// <typeparam name="TId">Тип идентификатора события</typeparam>
    /// <typeparam name="TStateId">Тип идентификатора состояния</typeparam>
    /// <typeparam name="TDateTime">Тип даты/времени</typeparam>
    public abstract record Event<TId, TStateId, TDateTime>
        where TId : IEntityId
        where TStateId : IEntityId
        where TDateTime : IComparable<TDateTime>
    {
        /// <summary>
        /// Инициализирует новый экземпляр события
        /// </summary>
        /// <param name="Id">Идентификатор события</param>
        /// <param name="OccurredAt">Дата события</param>
        protected Event(TId Id, TDateTime OccurredAt)
        {
            this.Id = Id;
            this.OccurredAt = OccurredAt;
            Validate();
        }

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public TId Id { get; init; }

        /// <summary>
        /// Дата события
        /// </summary>
        public TDateTime OccurredAt { get; init; }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public TStateId StateId { get; protected internal set; }

        /// <summary>
        /// Проверить корректность данных
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected virtual void Validate()
        {
            if (Id == null)
                throw new ArgumentNullException(nameof(Id));
            if (OccurredAt == null)
                throw new ArgumentNullException(nameof(OccurredAt));
        }
    }
}
