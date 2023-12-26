using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class PersonEventBase
    {
        protected PersonEventBase()
        { }

        public PersonEventBase(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt)
        {
            Id = id;
            PersonId = personId;
            StateId = stateId;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Compensatory = false;
            ChangeDate = changeDate;
            OccurredAt = occurredAt;
        }

        /// <summary>
        /// Идентификатор события
        /// </summary>
        public long Id { get; init; }

        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; init; }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public long StateId { get; init; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Компенсирующее событие
        /// </summary>
        public bool Compensatory { get; set; }

        /// <summary>
        /// Дата вступления изменений в силу
        /// </summary>
        public DateTime ChangeDate { get; init; }

        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime OccurredAt { get; init; }
    }
}
