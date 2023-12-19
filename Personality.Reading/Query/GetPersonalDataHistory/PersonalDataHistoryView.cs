using System;

namespace Personality.Reading.Query.GetPersonalDataHistory
{
    /// <summary>
    /// Проекция персональных данных лица
    /// </summary>
    public class PersonalDataHistoryView
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
        /// Дата начала действия
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
