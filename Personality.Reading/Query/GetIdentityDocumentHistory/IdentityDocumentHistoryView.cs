using System;

namespace Personality.Reading.Query.GetIdentityDocumentHistory
{
    /// <summary>
    /// Проекция документа, удостоверяющего личность
    /// </summary>
    public class IdentityDocumentHistoryView
    {
        /// <summary>
        /// Идентификатор проекции
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Значение статуса проекции
        /// </summary>
        public string StatusValue { get; set; }

        /// <summary>
        /// Статус проекции
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }

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

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
