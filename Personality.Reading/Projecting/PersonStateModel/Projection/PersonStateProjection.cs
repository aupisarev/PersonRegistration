using System.Collections.Generic;

namespace Personality.Reading.Projecting.PersonStateModel.Projection
{
    /// <summary>
    /// Проекция состояния лица
    /// </summary>
    public class PersonStateProjection : IProjection
    {
        /// <summary>
        /// Идентификатор проекции
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public long StateId { get; set; }

        /// <summary>
        /// Значение статуса
        /// </summary>
        public string StatusValue { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// События состояния
        /// </summary>
        public IList<PersonEventProjection> Events { get; set; } = new List<PersonEventProjection>();
    }
}
