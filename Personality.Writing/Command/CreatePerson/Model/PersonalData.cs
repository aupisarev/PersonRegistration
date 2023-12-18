using System;

namespace Personality.Writing.Command.CreatePerson.Model
{
    /// <summary>
    /// Персональные данные
    /// </summary>
    public class PersonalData
    {
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
        /// Значение гражданства
        /// </summary>
        public string CitizenshipValue { get; set; }
    }
}
