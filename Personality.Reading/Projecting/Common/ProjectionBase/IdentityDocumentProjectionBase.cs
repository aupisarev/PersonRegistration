using System;

namespace Personality.Reading.Projecting.Common.ProjectionBase
{
    /// <summary>
    /// Проекция документа, удостоверяющего личность
    /// </summary>
    public abstract class IdentityDocumentProjectionBase
    {
        protected IdentityDocumentProjectionBase() { }

        protected IdentityDocumentProjectionBase(IdentityDocumentProjectionBase projection)
        {
            IdentityDocumentTypeValue = projection.IdentityDocumentTypeValue;
            IdentityDocumentTypeName = projection.IdentityDocumentTypeName;
            Serial = projection.Serial;
            Number = projection.Number;
            ValidityDateStart = projection.ValidityDateStart;
            ValidityDateEnd = projection.ValidityDateEnd;
            WhoIssued = projection.WhoIssued;
            IssuedCountryValue = projection.IssuedCountryValue;
            IssuedCountryName = projection.IssuedCountryName;
        }

        /// <summary>
        /// Значение типа документа, удостоверяющего личность
        /// </summary>
        public int IdentityDocumentTypeValue { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        public string IdentityDocumentTypeName { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateTime ValidityDateStart { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime? ValidityDateEnd { get; set; }

        /// <summary>
        /// Орган, выдавший документ
        /// </summary>
        public string WhoIssued { get; set; }

        /// <summary>
        /// Значение страны выдачи документа
        /// </summary>
        public string IssuedCountryValue { get; set; }

        /// <summary>
        /// Страна выдачи документа
        /// </summary>
        public string IssuedCountryName { get; set; }
    }
}
