using Personality.Reading.Base;

namespace Personality.Reading.Query.GetPersonalDataHistory
{
    /// <summary>
    /// Запрос истории персональных данных лица
    /// </summary>
    public class GetPersonalDataHistoryQuery : IQuery<GetPersonalDataHistoryResponse>
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }
    }
}
