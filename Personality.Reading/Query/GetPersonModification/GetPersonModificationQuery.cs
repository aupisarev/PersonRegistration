using Personality.Reading.Base;

namespace Personality.Reading.Query.GetPersonModification
{
    /// <summary>
    /// Запрос модификации лица
    /// </summary>
    public class GetPersonModificationQuery : IQuery<GetPersonModificationResponse>
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }
    }
}
