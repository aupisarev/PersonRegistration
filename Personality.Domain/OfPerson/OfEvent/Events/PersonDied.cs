using System;

namespace Personality.Domain.OfPerson.OfEvent.Events
{
    /// <summary>
    /// Событие смерти лица
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="ChangeDate">Дата применения изменений</param>
    /// <param name="OccurredAt">Дата события</param>
    /// <param name="DeathDate">Дата смерти</param>
    public record PersonDied(PersonEventId Id, PersonId PersonId, DateTime ChangeDate, DateTime OccurredAt, DateOnly? DeathDate)
        : PersonEvent(
            Id,
            PersonId,
            "Смерть лица",
            ChangeDate,
            OccurredAt)
    {
    }
}
