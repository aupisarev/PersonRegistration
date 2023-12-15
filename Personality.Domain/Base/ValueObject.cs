namespace Personality.Domain.Base
{
    /// <summary>
    /// Объект-значение
    /// <para>Базовый класс</para>
    /// </summary>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> value1, ValueObject<T> value2)
        {
            if ((object)value1 == null)
                return (object)value2 == null;

            return value1.Equals(value2);
        }

        public static bool operator !=(ValueObject<T> value1, ValueObject<T> value2)
        {
            return !(value1 == value2);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return CompareValues((T)obj);
        }

        public override int GetHashCode()
        {
            return GetValueHashCode();
        }

        /// <summary>
        /// Получить HashCode
        /// </summary>
        /// <returns></returns>
        protected abstract int GetValueHashCode();

        /// <summary>
        /// Сравнить значения
        /// </summary>
        /// <returns>true при равенстве объектов-значений, иначе false</returns>
        protected abstract bool CompareValues(T other);
    }
}
