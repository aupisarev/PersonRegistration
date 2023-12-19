using System.Collections.Generic;
using Personality.Reading.Base;

namespace Personality.Reading.Query.GetPersonModification
{
    /// <summary>
    /// Ответ на запрос модификации лица
    /// </summary>
    public class GetPersonModificationResponse : IResponse
    {
        /// <summary>
        /// Значение текущего статуса
        /// </summary>
        public string CurrentStatusValue { get; set; }

        /// <summary>
        /// Текущий статус
        /// </summary>
        public string CurrentStatusName { get; set; }

        /// <summary>
        /// Рабочие события
        /// </summary>
        public IList<PersonEventView> WorkEvents { get; set; }

        /// <summary>
        /// Исторические события
        /// </summary>
        public IList<PersonEventView> HistoryEvents { get; set; }
    }
}
