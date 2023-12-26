using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class IdentityDocumentChanged : PersonEventBase
    {
        private IdentityDocumentChanged()
        { }

        public IdentityDocumentChanged(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt,
            int identityDocumentTypeValue, string serial, string number, DateTime validityDateStart, DateTime? validityDateEnd,
            string whoIssued, string issuedCountryValue, string issuedCountryName)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
            IdentityDocumentTypeValue = identityDocumentTypeValue;
            Serial = serial ?? throw new ArgumentNullException(nameof(serial));
            Number = number ?? throw new ArgumentNullException(nameof(number));
            ValidityDateStart = validityDateStart;
            ValidityDateEnd = validityDateEnd;
            WhoIssued = whoIssued ?? throw new ArgumentNullException(nameof(whoIssued));
            IssuedCountryValue = issuedCountryValue ?? throw new ArgumentNullException(nameof(issuedCountryValue));
            IssuedCountryName = issuedCountryName ?? throw new ArgumentNullException(nameof(issuedCountryName));
        }

        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        public int IdentityDocumentTypeValue { get; init; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Serial { get; init; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; init; }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateTime ValidityDateStart { get; init; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime? ValidityDateEnd { get; init; }

        /// <summary>
        /// Орган, выдавший документ
        /// </summary>
        public string WhoIssued { get; init; }

        /// <summary>
        /// Значение страны выдачи документа
        /// </summary>
        public string IssuedCountryValue { get; init; }

        /// <summary>
        /// Наименование страны выдачи документа
        /// </summary>
        public string IssuedCountryName { get; init; }
    }
}
