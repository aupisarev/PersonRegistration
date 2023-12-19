using System.Collections.Generic;
using Personality.Reading.Base;

namespace Personality.Reading.Query.GetPersonalDataHistory
{
    /// <summary>
    /// Ответ на запрос истории персональных данных лица
    /// </summary>
    public class GetPersonalDataHistoryResponse : IResponse
    {
        /// <summary>
        /// Список персональных данных лица
        /// </summary>
        public IList<PersonalDataHistoryView> Items { get; set; }
    }
}
