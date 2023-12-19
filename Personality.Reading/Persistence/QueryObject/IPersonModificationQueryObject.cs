using System.Threading.Tasks;
using Personality.Reading.Query.GetPersonModification;

namespace Personality.Reading.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса модификации лица
    /// </summary>
    public interface IPersonModificationQueryObject
    {
        /// <summary>
        /// Получить модификацию лица
        /// </summary>
        /// <param name="personId">Идентификатор лица</param>
        /// <returns></returns>
        public Task<GetPersonModificationResponse> GetAsync(long personId);
    }
}
