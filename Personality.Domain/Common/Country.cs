using Personality.Domain.Base;

namespace Personality.Domain.Common
{
    /// <summary>
    /// Страна
    /// </summary>
    public class Country : ExternalReference<Country, string>
    {
        protected Country()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр страны
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="name">Наименование</param>
        public Country(string value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Значение отсутствует
        /// </summary>
        public static Country Empty => new(string.Empty, string.Empty);

        public override Country GetDefault() => Empty;
    }
}
