using System;

namespace Personality.Domain.Base
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// <para>Базовый класс</para>
    /// </summary>
    public abstract class EntityId<T> : ValueObject<T>, IEntityId where T : EntityId<T>
    {
        /// <summary>
        /// Значение
        /// </summary>
        protected long? value;

        /// <summary>
        /// Значение
        /// </summary>
        public long Value
        {
            get => value ?? throw new InvalidOperationException("Отсутствует значение идентификатора сущности");
            protected set => this.value = value;
        }

        public static implicit operator long(EntityId<T> id)
        {
            return id?.Value ?? throw new InvalidCastException("Отсутствует значение идентификатора сущности");
        }


        public static implicit operator long?(EntityId<T> id)
        {
            return id.value;
        }

        protected override bool CompareValues(T other)
        {
            return value == other?.value;
        }

        protected override int GetValueHashCode()
        {
            return value.GetHashCode();
        }

        public bool Equals(IEntityId other)
        {
            return base.Equals(other);
        }
    }
}
