using System;
using System.Text.RegularExpressions;
using Personality.Domain.Base;
using Personality.Domain.Common;

namespace Personality.Domain.OfPerson.OfIdentityDocument
{
    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    public class IdentityDocument : ValueObject<IdentityDocument>
    {
        private IdentityDocument()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр документа, удостоверяющего личность
        /// </summary>
        /// <param name="documentType">Тип документа</param>
        /// <param name="serial">Серия</param>
        /// <param name="number">Номер</param>
        /// <param name="validity">Срок действия</param>
        /// <param name="whoIssued">Орган, выдавший документ</param>
        /// <param name="issuedCountry">Страна выдачи документа</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public IdentityDocument(IdentityDocumentType documentType, string serial, string number, Validity validity, WhoIssued whoIssued, Country issuedCountry)
        {
            DocumentType = documentType ?? throw new ArgumentNullException(nameof(documentType));
            Validity = validity ?? throw new ArgumentNullException(nameof(validity));
            WhoIssued = whoIssued ?? throw new ArgumentNullException(nameof(whoIssued));
            IssuedCountry = issuedCountry ?? throw new ArgumentNullException(nameof(issuedCountry));

            Serial = serial ?? throw new ArgumentNullException(nameof(serial));
            Number = number ?? throw new ArgumentNullException(nameof(number));

            if (!Regex.IsMatch(Serial, documentType.SerialPattern))
                throw new ArgumentException($"Серия документа '{Serial}' не соответствует формату заполнения типа документа '{documentType.Name}' - {documentType.SerialRule}");
            if (!Regex.IsMatch(Number, documentType.NumberPattern))
                throw new ArgumentException($"Номер документа '{Number}' не соответствует формату заполнения типа документа '{documentType.Name}' - {documentType.NumberRule}");
        }

        /// <summary>
        /// Тип документа
        /// </summary>
        public IdentityDocumentType DocumentType { get; private set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Serial { get; private set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; private set; }

        /// <summary>
        /// Срок действия
        /// </summary>
        public Validity Validity { get; private set; }

        /// <summary>
        /// Орган, выдавший документ
        /// </summary>
        public WhoIssued WhoIssued { get; private set; }

        /// <summary>
        /// Страна выдачи документа
        /// </summary>
        public Country IssuedCountry { get; private set; }

        protected override bool CompareValues(IdentityDocument other)
        {
            return (DocumentType, Serial, Number, Validity, WhoIssued, IssuedCountry) == (other.DocumentType, other.Serial, other.Number, other.Validity, other.WhoIssued, other.IssuedCountry);
        }

        protected override int GetValueHashCode()
        {
            return HashCode.Combine(DocumentType, Serial, Number, Validity, WhoIssued, IssuedCountry);
        }
    }
}
