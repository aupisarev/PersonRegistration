using System;

namespace Personality.Reading.Query.GetPersonListItem
{
    /// <summary>
    /// Проекция элемента списка лиц
    /// </summary>
    public class PersonListItemView
    {
        /// <summary>
        /// Идентификатор проекции
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор последнего события, спроецированного на проекцию
        /// </summary>
        public long LastEventId { get; set; }

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
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime? DeathDate { get; set; }

        /// <summary>
        /// Значение пола
        /// </summary>
        public int SexValue { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public string SexName { get; set; }

        /// <summary>
        /// Значение гражданства
        /// </summary>
        public string CitizenshipValue { get; set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        public string CitizenshipName { get; set; }

        /// <summary>
        /// Значение типа документа, удостоверяющего личность
        /// </summary>
        public int IdentityDocumentTypeValue { get; set; }

        /// <summary>
        /// Тип документа, удостоверяющего личность
        /// </summary>
        public string IdentityDocumentTypeName { get; set; }

        /// <summary>
        /// Серия документа, удостоверяющего личность
        /// </summary>
        public string IdentityDocumentSerial { get; set; }

        /// <summary>
        /// Номер документа, удостоверяющего личность
        /// </summary>
        public string IdentityDocumentNumber { get; set; }
    }
}
