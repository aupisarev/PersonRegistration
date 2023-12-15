using System;
using Personality.Domain.OfPerson.OfPersonalData;

namespace Personality.Domain.OfPerson.OfEvent.Events
{
    /// <summary>
    /// Событие изменения персональных данных лица
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="ChangeDate">Дата применения изменений</param>
    /// <param name="OccurredAt">Дата события</param>
    /// <param name="PersonalData">Персональные данные</param>
    public record PersonalDataChanged(PersonEventId Id, PersonId PersonId, DateTime ChangeDate, DateTime OccurredAt, PersonalData PersonalData)
        : PersonEvent(
            Id,
            PersonId,
            "Изменение персональных данных",
            ChangeDate,
            OccurredAt)
    {
        protected override void Validate()
        {
            base.Validate();

            if (PersonalData == null)
                throw new ArgumentNullException(nameof(PersonalData));
        }
    }
}
