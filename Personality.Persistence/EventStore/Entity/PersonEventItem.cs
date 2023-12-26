using System;
using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfState;

namespace Personality.Persistence.EventStore.Entity
{
    /// <summary>
    /// Событие агрегата лица
    /// </summary>
    public class PersonEventItem
    {
        public PersonEventItem(PersonEventId eventId, PersonId personId, StateId stateId, string description, DateTime occurredAt, string clrType, string data)
        {
            EventId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            PersonId = personId ?? throw new ArgumentNullException(nameof(personId));
            StateId = stateId ?? throw new ArgumentNullException(nameof(stateId));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            OccurredAt = occurredAt;
            ClrType = clrType ?? throw new ArgumentNullException(nameof(clrType));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public PersonEventId EventId { get; }
        public PersonId PersonId { get; }
        public StateId StateId { get; }
        public string Description { get; }
        public DateTime OccurredAt { get; }
        public string ClrType { get; }
        public string Data { get; }
    }
}
