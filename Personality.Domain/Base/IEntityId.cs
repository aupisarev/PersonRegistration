using System;

namespace Personality.Domain.Base
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    public interface IEntityId : IEquatable<IEntityId>
    {
        /// <summary>
        /// Значение
        /// </summary>
        public long Value { get; }
    }
}
