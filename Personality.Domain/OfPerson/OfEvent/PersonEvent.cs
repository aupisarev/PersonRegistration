using System;
using Personality.Domain.Base.EventSourcing;
using Personality.Domain.OfPerson.OfState;

namespace Personality.Domain.OfPerson.OfEvent
{
    /// <summary>
    /// Событие агрегата лица
    /// </summary>
    /// <param name="Id">Идентификатор события</param>
    /// <param name="PersonId">Идентификатор лица</param>
    /// <param name="Description">Описание</param>
    /// <param name="OccurredAt">Дата события</param>
    /// <param name="ChangeDate">Дата применения изменений</param>
    public abstract record PersonEvent(PersonEventId Id, PersonId PersonId, string Description, DateTime ChangeDate, DateTime OccurredAt)
        : Event<PersonEventId, StateId, DateTime>(Id, OccurredAt)
    {
        /// <summary>
        /// Компенсирующее событие
        /// </summary>
        public bool Compensatory { get; internal set; } = false;

        protected override void Validate()
        {
            base.Validate();

            if (PersonId == null)
                throw new ArgumentNullException(nameof(PersonId));
            if (Description == null)
                throw new ArgumentNullException(nameof(Description));
        }

        /// <summary>
        /// Установить идентификатор состояния
        /// </summary>
        /// <param name="stateId">Идентификатор состояния</param>
        internal void SetStateId(StateId stateId) => StateId = stateId;
    }
}
