using System.Collections.Generic;
using System.Threading.Tasks;
using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfEvent;

namespace Personality.Writing.Persistence
{
    /// <summary>
    /// Хранилище событий для агрегата Person
    /// </summary>
    public interface IPersonEventStore
    {
        /// <summary>
        /// Получить поток событий лица
        /// </summary>
        /// <param name="personId">Идентификатор лица</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns>Список событий</returns>
        public Task<IList<PersonEvent>> GetEventsAsync(PersonId personId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Получить весь поток событий
        /// </summary>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        /// <returns>Список событий</returns>
        public Task<IList<PersonEvent>> GetAllAsync(IUnitOfWork unitOfWork);

        /// <summary>
        /// Добавить события в поток событий
        /// </summary>
        /// <param name="events">Добавляемые события</param>
        /// <param name="unitOfWork">Единица работы с хранилищем</param>
        public Task AddEventsAsync(IList<PersonEvent> events, IUnitOfWork unitOfWork);
    }
}
