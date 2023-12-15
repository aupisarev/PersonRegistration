using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfState
{
    /// <summary>
    /// Идентификатор состояния
    /// </summary>
    public class StateId : EntityId<StateId>
    {
        /// <summary>
        /// Инициализирует новый экземпляр идентификатора состояния
        /// </summary>
        /// <param name="value">Значение идентификатора</param>
        public StateId(long value)
        {
            Value = value;
        }
    }
}
