namespace Personality.Domain.Base
{
    /// <summary>
    /// Сущность
    /// <para>Базовый класс</para>
    /// </summary>
    public abstract class Entity<TId> where TId : IEntityId
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual TId Id { get; protected set; }

        public static bool operator ==(Entity<TId> value1, Entity<TId> value2)
        {
            if ((object)value1 == null)
                return (object)value2 == null;

            return value1.Equals(value2);
        }

        public static bool operator !=(Entity<TId> value1, Entity<TId> value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return CompareValues(this, (Entity<TId>)obj);
        }

        public bool Equals(Entity<TId> obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return CompareValues(this, obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Сравнить значения
        /// </summary>
        /// <param name="value1">Значение 1</param>
        /// <param name="value2">Значение 2</param>
        /// <returns></returns>
        private bool CompareValues(Entity<TId> value1, Entity<TId> value2)
        {
            return value1.Id.Equals(value2.Id);
        }
    }
}
