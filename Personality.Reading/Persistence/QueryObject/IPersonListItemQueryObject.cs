using Personality.Reading.Query.GetPersonListItem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personality.Reading.Persistence.QueryObject
{
    /// <summary>
    /// Объект запроса элементов списка лиц
    /// </summary>
    public interface IPersonListItemQueryObject
    {
        /// <summary>
        /// Получить элементы спика лиц
        /// </summary>
        /// <param name="lightReadParams">Параметры чтения</param>
        /// <param name="includeWorkState">Включать рабочие состояния</param>
        /// <returns>Список элементов</returns>
        Task<IList<PersonListItemView>> GetAsync(/*LightReadParams<PersonListItemView> lightReadParams,*/ bool includeWorkState);
    }
}
