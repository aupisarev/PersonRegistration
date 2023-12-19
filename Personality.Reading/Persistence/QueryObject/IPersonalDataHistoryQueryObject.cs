using Personality.Reading.Query.GetPersonalDataHistory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personality.Reading.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса истории персональных данных лица
    /// </summary>
    public interface IPersonalDataHistoryQueryObject
    {
        /// <summary>
        /// Получить список персональных данных
        /// </summary>
        /// <param name="personId">Идентификатор лица</param>
        /// <returns>Список персональных данных</returns>
        public Task<IList<PersonalDataHistoryView>> GetAsync(long personId);
    }
}
