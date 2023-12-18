using System;

namespace Personality.Writing.Command.UpdatePerson.Model
{
    /// <summary>
    /// Документ, удостоверяющий личность
    /// </summary>
    public class IdentityDocument
    {
        /// <summary>
        /// Значение типа документа, удостоверяющего личность
        /// </summary>
        public int IdentityDocumentTypeValue { get; set; }

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
    }
}
