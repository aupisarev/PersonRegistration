using System;
using System.Threading.Tasks;
using Personality.Reading.Base;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonModification;

namespace Personality.Reading.QueryHandler.GetPersonModification
{
    /// <summary>
    /// Обработчик запроса модификации лица
    /// </summary>
    public class GetPersonModificationQueryHandler : IQueryHandler<GetPersonModificationQuery, GetPersonModificationResponse>
    {
        private readonly IPersonModificationQueryObject personModificationQueryObject;

        public GetPersonModificationQueryHandler(IPersonModificationQueryObject personModificationQueryObject)
        {
            this.personModificationQueryObject = personModificationQueryObject ?? throw new ArgumentNullException(nameof(personModificationQueryObject));
        }

        public async Task<GetPersonModificationResponse> HandleAsync(GetPersonModificationQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            return await personModificationQueryObject.GetAsync(query.PersonId);
        }
    }
}
