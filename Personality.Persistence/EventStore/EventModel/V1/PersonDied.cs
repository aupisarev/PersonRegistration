using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class PersonDied : PersonEventBase
    {
        private PersonDied()
        { }

        public PersonDied(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt,
            DateTime? deathDate)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
            DeathDate = deathDate;
        }


        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime? DeathDate { get; init; }
    }
}
