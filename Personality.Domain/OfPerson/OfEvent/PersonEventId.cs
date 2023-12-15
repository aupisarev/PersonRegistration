using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfEvent
{
    /// <summary>
    /// Идентификатор события лица
    /// </summary>
    public class PersonEventId : EntityId<PersonEventId>
    {
        /// <summary>
        /// Инициализирует новый экземпляр идентификатора события лица
        /// </summary>
        /// <param name="value">Значение идентификатора</param>
        public PersonEventId(long value)
        {
            Value = value;
        }
    }
}
