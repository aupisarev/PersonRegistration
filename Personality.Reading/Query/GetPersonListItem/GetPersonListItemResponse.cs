using Personality.Reading.Base;
using System.Collections.Generic;

namespace Personality.Reading.Query.GetPersonListItem
{
    /// <summary>
    /// Ответ на запрос получения элементов списка лиц
    /// </summary>
    public class GetPersonListItemResponse : IResponse
    {
        /// <summary>
        /// Список элементов списка лиц
        /// </summary>
        public IList<PersonListItemView> Items { get; set; }
    }
}
