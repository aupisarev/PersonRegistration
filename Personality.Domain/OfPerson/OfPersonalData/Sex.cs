using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfPersonalData
{
    /// <summary>
    /// Пол
    /// </summary>
    public class Sex : InternalReference<Sex, int>
    {
        private Sex(int value, string name)
        {
            Value = value;
            Name = name;
        }

        /// <summary>
        /// Мужской
        /// </summary>
        public static Sex Male => new(1, "Мужской");

        /// <summary>
        /// Женский
        /// </summary>
        public static Sex Female => new(2, "Женский");
    }
}
