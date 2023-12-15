using System;

namespace Personality.Domain.OfPerson.OfEvent.Events
{
    /// <summary>
    /// Событие создания лица
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="ChangeDate">Дата применения изменений</param>
    /// <param name="OccurredAt">Дата события</param>
    public record PersonCreated(PersonEventId Id, PersonId PersonId, DateTime ChangeDate, DateTime OccurredAt)
        : PersonEvent(
            Id,
            PersonId,
            "Создание лица",
            ChangeDate,
            OccurredAt);
}
