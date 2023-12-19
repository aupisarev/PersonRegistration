using System;
using System.Threading.Tasks;
using Personality.Reading.Base;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetIdentityDocumentHistory;

namespace Personality.Reading.QueryHandler.GetIdentityDocumentHistory
{
    /// <summary>
    /// Обработчик запроса истории документа, удостоверяющего личность
    /// </summary>
    public class GetIdentityDocumentHistoryQueryHandler : IQueryHandler<GetIdentityDocumentHistoryQuery, GetIdentityDocumentHistoryResponse>
    {
        private readonly IIdentityDocumentHistoryQueryObject identityDocumentHistoryQueryObject;

        public GetIdentityDocumentHistoryQueryHandler(IIdentityDocumentHistoryQueryObject identityDocumentHistoryQueryObject)
        {
            this.identityDocumentHistoryQueryObject = identityDocumentHistoryQueryObject ?? throw new ArgumentNullException(nameof(identityDocumentHistoryQueryObject));
        }

        public async Task<GetIdentityDocumentHistoryResponse> HandleAsync(GetIdentityDocumentHistoryQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            return new GetIdentityDocumentHistoryResponse
            {
                Items = await identityDocumentHistoryQueryObject.GetAsync(query.PersonId)
            };
        }
    }
}
