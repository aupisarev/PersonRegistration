using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfState
{
    /// <summary>
    /// Статус состояния
    /// </summary>
    public class StateStatus : InternalReference<StateStatus, string>
    {
        private StateStatus(string value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Финальный статус
        /// </summary>
        public bool IsFinal => this == Applied || this == Canceled;

        /// <summary>
        /// В работе
        /// </summary>
        public static StateStatus Raw => new("raw", "В работе");

        /// <summary>
        /// Согласовано
        /// </summary>
        public static StateStatus Approved => new("approved", "Согласовано");
        
        /// <summary>
        /// Применено
        /// </summary>
        public static StateStatus Applied => new("applied", "Применено");

        /// <summary>
        /// Отменено
        /// </summary>
        public static StateStatus Canceled => new("canceled", "Отменено");
    }
}
