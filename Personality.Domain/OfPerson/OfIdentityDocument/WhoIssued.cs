using Personality.Domain.Base;

namespace Personality.Domain.OfPerson.OfIdentityDocument
{
    /// <summary>
    /// Орган, выдавший документ
    /// </summary>
    public class WhoIssued : ValueObject<WhoIssued>
    {
        private WhoIssued()
        { }

        /// <summary>
        /// Инициализирует новый экземпляр органа, выдавшего документ
        /// </summary>
        /// <param name="value">Значение</param>
        public WhoIssued(string value)
        {
            Value = value.ToUpper().Trim();
        }

        /// <summary>
        /// Значение
        /// </summary>
        public string Value { get; private set; }
        

        protected override int GetValueHashCode()
        {
            throw new System.NotImplementedException();
        }

        protected override bool CompareValues(WhoIssued other)
        {
            throw new System.NotImplementedException();
        }
    }
}
