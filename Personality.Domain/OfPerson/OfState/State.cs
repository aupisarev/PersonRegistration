using Personality.Domain.Base.EventSourcing;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfEvent.Events;
using Personality.Domain.OfPerson.OfEvent.Events.StateEvents;
using Personality.Domain.OfPerson.OfIdentityDocument;
using Personality.Domain.OfPerson.OfPersonalData;
using System;

namespace Personality.Domain.OfPerson.OfState
{
    /// <summary>
    /// Состояние
    /// </summary>
    public class State : EventSourcingState<StateId, PersonEventId, PersonEvent, DateTime>
    {
        /// <summary>
        /// Инициализирует новый экземпляр нового состояния
        /// </summary>
        /// <param name="id">Идентификатор состояния</param>
        public State(StateId id)
            : base(id)
        {
            Status = StateStatus.Raw;
        }

        /// <summary>
        /// Инициализирует новый экземпляр нового состояния с данными указанного состояния
        /// </summary>
        /// <param name="id">Идентификатор состояния</param>
        /// <param name="state">Состояние для копирования данных</param>
        public State(StateId id, State state)
            : this(id)
        {
            this.PersonId = state?.PersonId;
            this.PersonalData = state?.PersonalData;
            this.IdentityDocument = state?.IdentityDocument;
        }


        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public PersonId PersonId { get; private set; }

        /// <summary>
        /// Статус
        /// </summary>
        public StateStatus Status { get; private set; }

        /// <summary>
        /// Персональные данные
        /// </summary>
        public PersonalData PersonalData { get; private set; }

        /// <summary>
        /// Документ, удостоверяющий личность
        /// </summary>
        public IdentityDocument IdentityDocument { get; private set; }


        protected override void When(PersonEvent @event)
        {
            switch (@event)
            {
                case PersonCreated personCreated:
                    PersonId = personCreated.PersonId;
                    break;
                case PersonalDataChanged personalDataChanged:
                    PersonalData = personalDataChanged.PersonalData;
                    break;
                case IdentityDocumentChanged identityDocumentChanged:
                    IdentityDocument = identityDocumentChanged.IdentityDocument;
                    break;
                case PersonDied personDied:
                    PersonalData = new PersonalData(
                        PersonalData.PersonalName,
                        PersonalData.Birthday,
                        personDied.DeathDate,
                        PersonalData.Sex,
                        PersonalData.Citizenship
                    );
                    break;

                case StateApproved:
                    Status = StateStatus.Approved;
                    break;
                case StateApplied:
                    Status = StateStatus.Applied;
                    break;
                case StateCanceled:
                    Status = StateStatus.Canceled;
                    break;
            }
        }
    }
}
