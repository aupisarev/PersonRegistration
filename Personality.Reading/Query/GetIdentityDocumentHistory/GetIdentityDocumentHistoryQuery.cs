using Personality.Reading.Base;

namespace Personality.Reading.Query.GetIdentityDocumentHistory
{
    /// <summary>
    /// Запрос истории документа, удостоверяющего личность
    /// </summary>
    public class GetIdentityDocumentHistoryQuery : IQuery<GetIdentityDocumentHistoryResponse>
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }
    }
}
