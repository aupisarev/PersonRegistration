using Personality.Domain.OfPerson.OfEvent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personality.Writing.Outside
{
    /// <summary>
    /// Проектор событий на модель чтения
    /// </summary>
    public interface IProjector
    {
        /// <summary>
        /// Проектировать событие на модель чтения
        /// </summary>
        public Task ProjectAsync(IList<PersonEvent> events);
    }
}
