using System;

namespace Personality.Reading.Projecting.PersonHistoryModel
{
    /// <summary>
    /// Проекция модели чтения истории данных лица
    /// </summary>
    public interface IHistoryProjection : IProjection
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Идентификатор состояния, в котором применены данные
        /// </summary>
        public long StateId { get; set; }

        /// <summary>
        /// Значение статуса состояния
        /// </summary>
        public string StateStatusValue { get; set; }

        /// <summary>
        /// Наименование статуса состояния
        /// </summary>
        public string StateStatusName { get; set; }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        DateTime? EndDate { get; set; }
    }
}
