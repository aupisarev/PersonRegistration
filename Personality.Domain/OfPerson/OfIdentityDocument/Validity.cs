using System;
using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfIdentityDocument
{
    /// <summary>
    /// Действие документа
    /// </summary>
    public class Validity : ValueObject<Validity>
    {
        private Validity()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр действия документа
        /// </summary>
        /// <param name="dateStart">Дата начала действия</param>
        /// <param name="dateEnd">Дата окончания действия</param>
        public Validity(DateOnly dateStart, DateOnly? dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateOnly DateStart { get; private set; }

        /// <summary>
        /// Дата окончания действия
        /// </summary>
        public DateOnly? DateEnd { get; private set; }

        protected override bool CompareValues(Validity other)
        {
            return (DateStart, DateEnd) == (other.DateStart, other.DateEnd);
        }

        protected override int GetValueHashCode()
        {
            return HashCode.Combine(DateStart, DateEnd);
        }
    }
}
