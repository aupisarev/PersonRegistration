using System;
using System.Threading.Tasks;
using Personality.Reading.Base;
using Personality.Reading.Persistence.QueryObject;
using Personality.Reading.Query.GetPersonListItem;

namespace Personality.Reading.QueryHandler.GetPersonListItem
{
    /// <summary>
    /// Обработчик запроса на получение элементов списка лиц
    /// </summary>
    public class GetPersonListItemQueryHandler : IQueryHandler<GetPersonListItemQuery, GetPersonListItemResponse>
    {
        private readonly IPersonListItemQueryObject personListItemQueryObject;

        public GetPersonListItemQueryHandler(IPersonListItemQueryObject personListItemQueryObject)
        {
            this.personListItemQueryObject = personListItemQueryObject ?? throw new ArgumentNullException(nameof(personListItemQueryObject));
        }

        public async Task<GetPersonListItemResponse> HandleAsync(GetPersonListItemQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            //if (query.LightReadParams == null)
            //    throw new ArgumentException($"Некорректный запрос - отсутствуют параметры чтения");

            return new GetPersonListItemResponse
            {
                Items = await personListItemQueryObject.GetAsync( /*query.LightReadParams,*/ query.IncludeWorkState),
            };
        }
    }
}