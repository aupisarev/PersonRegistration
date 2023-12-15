using System;

namespace Personality.Domain.Base
{
    /// <summary>
    /// Справочный элемент из внешнего контекста
    /// <para>Базовый класс</para>
    /// </summary>
    /// <typeparam name="TElement">Тип элемента справочника</typeparam>
    /// <typeparam name="TValue">Тип значения элемента</typeparam>
    public abstract class ExternalReference<TElement, TValue> : ValueObject<TElement>
        where TElement : ExternalReference<TElement, TValue>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; protected set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Значение некорректно
        /// </summary>
        /// <remarks>
        /// Значение не содержится в справочнике
        /// </remarks>
        public bool IsIncorrect { get; private set; }

        /// <summary>
        /// Заменить элемент на некорректный со значением value, если элемент является дефолтным для данного типа
        /// </summary>
        /// <param name="value">Некорректное значение. Если null, замена не происходит</param>
        public TElement TryReplaceDefaultWithIncorrect(TValue value)
        {
            if (this == GetDefault() && value != null)
            {
                Value = value;
                Name = "Некорректное значение";
                IsIncorrect = true;
            }
            return (TElement)this;
        }

        /// <summary>
        /// Получить значение по умолчанию для данного типа
        /// </summary>
        /// <returns></returns>
        public abstract TElement GetDefault();

        protected override bool CompareValues(TElement other)
        {
            return Value.Equals(other.Value);
        }

        protected override int GetValueHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
