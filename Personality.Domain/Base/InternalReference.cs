using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Personality.Domain.Base
{
    /// <summary>
    /// Справочный элемент из внутреннего контекста с встроенным в тип статическим перечислением возможных элементов
    /// <para>Базовый класс</para>
    /// </summary>
    /// <typeparam name="TElement">Тип элемента справочника</typeparam>
    /// <typeparam name="TValue">Тип значения элемента</typeparam>
    public abstract class InternalReference<TElement, TValue> : ValueObject<TElement>
        where TElement : InternalReference<TElement, TValue>
        where TValue : IEquatable<TValue>
    {
        /// <summary>
        /// Список элементов справочника
        /// </summary>
        private static Dictionary<TValue, PropertyInfo> values;

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; protected set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Получить коллекцию значений
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TElement> GetAll() => GetValues().Values.Select(v => (TElement)v.GetValue(null));

        /// <summary>
        /// Получить элемент по значению
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Найденный элемент</returns>
        public static TElement Get(TValue value)
        {
            if (!GetValues().TryGetValue(value, out var t))
                throw new ArgumentException($"Значение {value} не найдено в справочнике {typeof(TElement).Name}", nameof(value));

            return (TElement)t.GetValue(null);
        }

        /// <summary>
        /// Получить элемент по значению
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="defaultValue">Элемент по умолчанию</param>
        /// <returns>Найденный элемент, или элемент по умолчанию, если значение отсутствует в справочнике</returns>
        public static TElement Get(TValue value, TElement defaultValue)
        {
            if (!GetValues().TryGetValue(value, out var t))
                return defaultValue;

            return (TElement)t.GetValue(null);
        }

        private static Dictionary<TValue, PropertyInfo> GetValues()
        {
            return values ??= typeof(TElement).GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .ToDictionary(k => ((TElement)k.GetValue(null)!).Value, v => v);
        }

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
