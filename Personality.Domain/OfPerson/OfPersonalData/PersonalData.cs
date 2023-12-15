using Personality.Domain.Base;
using System;

namespace Personality.Domain.OfPerson.OfPersonalData
{
    /// <summary>
    /// Персональные данные
    /// </summary>
    public class PersonalData : ValueObject<PersonalData>
    {
        private PersonalData()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр персональных данных
        /// </summary>
        /// <param name="personalName">ФИО</param>
        /// <param name="birthday">Дата рождения</param>
        /// <param name="deathDate">Дата смерти</param>
        /// <param name="sex">Пол</param>
        /// <param name="citizenship">Гражданство</param>
        /// <exception cref="ArgumentNullException"></exception>
        public PersonalData(PersonalName personalName, DateOnly birthday, DateOnly? deathDate, Sex sex, Citizenship citizenship)
        {
            PersonalName = personalName ?? throw new ArgumentNullException(nameof(personalName));
            Birthday = birthday;
            DeathDate = deathDate;
            Sex = sex ?? throw new ArgumentNullException(nameof(sex));
            Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
        }

        /// <summary>
        /// ФИО
        /// </summary>
        public PersonalName PersonalName { get; private set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateOnly Birthday { get; private set; }

        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateOnly? DeathDate { get; private set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex { get; private set; }

        /// <summary>
        /// Гражданство
        /// </summary>
        public Citizenship Citizenship { get; private set; }

        protected override bool CompareValues(PersonalData other)
        {
            return (PersonalName, Birthday, DeathDate, Sex, Citizenship) == (other.PersonalName, other.Birthday, other.DeathDate, other.Sex, other.Citizenship);
        }

        protected override int GetValueHashCode()
        {
            return HashCode.Combine(PersonalName, Birthday, DeathDate, Sex, Citizenship);
        }
    }
}
