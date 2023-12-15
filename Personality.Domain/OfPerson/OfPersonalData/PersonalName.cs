using Personality.Domain.Base;
using System;

namespace Personality.Domain.OfPerson.OfPersonalData
{
    /// <summary>
    /// ФИО
    /// </summary>
    public class PersonalName : ValueObject<PersonalName>
    {
        protected PersonalName()
        {
            LastName = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
        }

        public PersonalName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Фамилия должна быть заполнена");

            LastName = lastName;
            FirstName = string.Empty;
            MiddleName = string.Empty;
        }

        public PersonalName(string lastName, string firstName, string middleName = null)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Фамилия должна быть заполнена");
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("Имя должно быть заполнено");

            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName ?? string.Empty;
        }


        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; private init; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; private init; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; private init; }


        /// <summary>
        /// Значение отсутствует
        /// </summary>
        public static PersonalName Empty => new() { LastName = string.Empty, FirstName = string.Empty, MiddleName = string.Empty };


        protected override int GetValueHashCode()
        {
            return HashCode.Combine(FirstName, LastName, MiddleName);
        }

        protected override bool CompareValues(PersonalName other)
        {
            return (FirstName, MiddleName, LastName) == (other.FirstName, other.MiddleName, other.LastName);
        }
    }
}
