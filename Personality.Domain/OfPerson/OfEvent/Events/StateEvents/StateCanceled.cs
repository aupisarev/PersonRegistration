using System;

namespace Personality.Domain.OfPerson.OfEvent.Events.StateEvents
{
    /// <summary>
    /// Состояние отменено
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="ChangeDate">Дата вступления в силу изменений</param>
    /// <param name="OccurredAt">Дата события</param>
    public record StateCanceled(PersonEventId Id, PersonId PersonId, DateTime ChangeDate, DateTime OccurredAt)
        : StateEvent(
            Id,
            PersonId,
            "Изменения отменены",
            ChangeDate,
            OccurredAt);
}
