using Personality.Writing.Base;

namespace Personality.Writing.Command.CreatePerson
{
    /// <summary>
    /// Результат обработки команды создания лица
    /// </summary>
    public class CreatePersonResponse : IResponse
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }
    }
}
