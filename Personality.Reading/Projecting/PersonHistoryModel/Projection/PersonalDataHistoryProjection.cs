using System;
using Personality.Reading.Projecting.Common.ProjectionBase;

namespace Personality.Reading.Projecting.PersonHistoryModel.Projection
{
    /// <summary>
    /// Проекция персональных данных лица
    /// </summary>
    public class PersonalDataHistoryProjection : PersonalDataProjectionBase, IHistoryProjection
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
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
