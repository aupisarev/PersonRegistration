using Personality.Domain.Common;
using System.Threading.Tasks;

namespace Personality.Writing.Outside
{
    /// <summary>
    /// Получатель стран
    /// </summary>
    public interface ICountryGetter
    {
        /// <summary>
        /// Получить страну по значению
        /// </summary>
        /// <param name="countryValue">Значение страны</param>
        /// <returns>Страна</returns>
        public Task<Country> GetAsync(string countryValue);
    }
}
