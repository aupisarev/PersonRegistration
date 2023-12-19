using System;
using System.Threading.Tasks;
using Personality.Reading.Base;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonalDataHistory;

namespace Personality.Reading.QueryHandler.GetPersonalDataHistory
{
    /// <summary>
    /// Обработчик запроса истории персональных данных лица
    /// </summary>
    public class GetPersonalDataHistoryQueryHandler : IQueryHandler<GetPersonalDataHistoryQuery, GetPersonalDataHistoryResponse>
    {
        private readonly IPersonalDataHistoryQueryObject personalDataHistoryQueryObject;

        public GetPersonalDataHistoryQueryHandler(IPersonalDataHistoryQueryObject personalDataHistoryQueryObject)
        {
            this.personalDataHistoryQueryObject = personalDataHistoryQueryObject ?? throw new ArgumentNullException(nameof(personalDataHistoryQueryObject));
        }

        public async Task<GetPersonalDataHistoryResponse> HandleAsync(GetPersonalDataHistoryQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            return new GetPersonalDataHistoryResponse
            {
                Items = await personalDataHistoryQueryObject.GetAsync(query.PersonId)
            };
        }
    }
}
