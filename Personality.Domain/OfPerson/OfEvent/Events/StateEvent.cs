using System;

namespace Personality.Domain.OfPerson.OfEvent.Events
{
    /// <summary>
    /// Событие изменения состояния лица
    /// </summary>
    public abstract record StateEvent(PersonEventId Id, PersonId PersonId, string Description, DateTime ChangeDate, DateTime OccurredAt)
        : PersonEvent(Id, PersonId, Description, ChangeDate, OccurredAt);
}
