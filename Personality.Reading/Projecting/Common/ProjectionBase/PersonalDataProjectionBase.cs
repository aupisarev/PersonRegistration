using System;

namespace Personality.Reading.Projecting.Common.ProjectionBase
{
    /// <summary>
    /// Проекция персональных данных лица
    /// </summary>
    public abstract class PersonalDataProjectionBase
    {
        protected PersonalDataProjectionBase() { }

        protected PersonalDataProjectionBase(PersonalDataProjectionBase projection)
        {
            LastName = projection.LastName;
            FirstName = projection.FirstName;
            MiddleName = projection.MiddleName;
            Birthday = projection.Birthday;
            DeathDate = projection.DeathDate;
            SexValue = projection.SexValue;
            SexName = projection.SexName;
            CitizenshipValue = projection.CitizenshipValue;
            CitizenshipName = projection.CitizenshipName;
        }

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
    }
}
