using System;

namespace Personality.Reading.Projecting.PersonStateModel.Projection
{
    /// <summary>
    /// Проекция события лица
    /// </summary>
    public class PersonEventProjection
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public long EventId { get; set; }

        /// <summary>
        /// Компенсационное событие
        /// </summary>
        public bool Compensatory { get; set; }

        /// <summary>
        /// Описание события
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата принятия изменений
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// Дата события
        /// </summary>
        public DateTime OccurredAt { get; set; }
    }
}
