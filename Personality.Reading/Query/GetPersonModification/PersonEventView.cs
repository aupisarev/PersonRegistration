using System;

namespace Personality.Reading.Query.GetPersonModification
{
    /// <summary>
    /// Проекция события изменения лица
    /// </summary>
    public class PersonEventView
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Компенсационное событие
        /// </summary>
        public bool Compensatory { get; set; }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public long StateId { get; set; }

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
