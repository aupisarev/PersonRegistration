using System;
using Personality.Domain.OfPerson.OfIdentityDocument;

namespace Personality.Domain.OfPerson.OfEvent.Events
{
    /// <summary>
    /// Событие изменения документа, удостоверяющего личность лица
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="ChangeDate">Дата применения изменений</param>
    /// <param name="OccurredAt">Дата события</param>
    /// <param name="IdentityDocument">Документ, удостоверяющий личность</param>
    public record IdentityDocumentChanged(PersonEventId Id, PersonId PersonId, DateTime ChangeDate, DateTime OccurredAt, IdentityDocument IdentityDocument)
        : PersonEvent(
            Id,
            PersonId,
            "Изменение документа УДЛ",
            ChangeDate,
            OccurredAt)
    {
        protected override void Validate()
        {
            base.Validate();

            if (IdentityDocument == null)
                throw new ArgumentNullException(nameof(IdentityDocument));
        }
    }
}
