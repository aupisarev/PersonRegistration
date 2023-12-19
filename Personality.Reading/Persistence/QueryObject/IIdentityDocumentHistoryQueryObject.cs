using Personality.Reading.Query.GetIdentityDocumentHistory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personality.Reading.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса истории документов, удостоверяющих личность
    /// </summary>
    public interface IIdentityDocumentHistoryQueryObject
    {
        /// <summary>
        /// Получить список документов, удостоверяющих личность
        /// </summary>
        /// <param name="personId">Идентификатор лица</param>
        /// <returns>Список документов, удостоверяющих личность</returns>
        public Task<IList<IdentityDocumentHistoryView>> GetAsync(long personId);
    }
}
