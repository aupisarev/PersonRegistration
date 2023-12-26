using System;

namespace Personality.Persistence.EventStore.EventModel.V1
{
    public class PersonalDataChanged : PersonEventBase
    {
        private PersonalDataChanged()
        { }

        public PersonalDataChanged(long id, long personId, long stateId, string description, DateTime changeDate, DateTime occurredAt,
            string lastName, string firstName, string middleName, DateTime birthday, DateTime? deathDate, int sex,
            string citizenshipValue, string citizenshipName)
            : base(id, personId, stateId, description, changeDate, occurredAt)
        {
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            MiddleName = middleName ?? throw new ArgumentNullException(nameof(middleName));
            Birthday = birthday;
            DeathDate = deathDate;
            Sex = sex;
            CitizenshipValue = citizenshipValue ?? throw new ArgumentNullException(nameof(citizenshipValue));
            CitizenshipName = citizenshipName ?? throw new ArgumentNullException(nameof(citizenshipName));
        }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; init; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; init; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; init; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthday { get; init; }

        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime? DeathDate { get; init; }

        /// <summary>
        /// Пол
        /// </summary>
        public int Sex { get; init; }

        /// <summary>
        /// Значение гражданства
        /// </summary>
        public string CitizenshipValue { get; init; }

        /// <summary>
        /// Наименование гражданства
        /// </summary>
        public string CitizenshipName { get; init; }
    }
}
