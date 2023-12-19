using System.Collections.Generic;
using Personality.Reading.Base;

namespace Personality.Reading.Query.GetIdentityDocumentHistory
{
    /// <summary>
    /// Ответа на запрос истории документа, удостоверяющего личность
    /// </summary>
    public class GetIdentityDocumentHistoryResponse : IResponse
    {
        /// <summary>
        /// Список документов, удостоверяющих личность
        /// </summary>
        public IList<IdentityDocumentHistoryView> Items { get; set; }
    }
}
