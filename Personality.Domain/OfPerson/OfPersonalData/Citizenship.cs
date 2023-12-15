using Personality.Domain.Base;
using Personality.Domain.Common;

namespace Personality.Domain.OfPerson.OfPersonalData
{
    /// <summary>
    /// Гражданство
    /// </summary>
    public class Citizenship : ExternalReference<Citizenship, string>
    {
        private Citizenship()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр гражданства
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="name">Наименование</param>
        public Citizenship(string value, string name)
        {
            Value = value;
            Name = name;
        }

        public Citizenship(Country country)
        {
            Value = country.Value;
            Name = country.Name;
        }

        /// <summary>
        /// Без гражданства
        /// </summary>
        public static Citizenship Without => new("БГ", "Без гражданства");

        public override Citizenship GetDefault()
        {
            return Without;
        }
    }
}
