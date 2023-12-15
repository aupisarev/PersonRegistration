using Personality.Domain.Base;

namespace Personality.Domain.OfPerson
{
    /// <summary>
    /// Идентификатор лица
    /// </summary>
    public class PersonId : EntityId<PersonId>
    {
        /// <summary>
        /// Инициализирует новый экземпляр идентификатора лица
        /// </summary>
        /// <param name="value">Значение идентификатора</param>
        public PersonId(long value)
        {
            Value = value;
        }
    }
}
