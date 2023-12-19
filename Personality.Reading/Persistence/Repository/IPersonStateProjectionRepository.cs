using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Personality.Reading.Projecting.PersonStateModel.Projection;

namespace Personality.Reading.Persistence.Repository
{
    /// <summary>
    /// Репозиторий проекций модели чтения состояний лица
    /// </summary>
    public interface IPersonStateProjectionRepository
    {
        /// <summary>
        /// Получить список проекций модели чтения состояний лица
        /// </summary>
        /// <param name="condition">Условие поиска</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task<IList<PersonStateProjection>> GetAsync(Expression<Func<PersonStateProjection, bool>> condition, IUnitOfWork unitOfWork);

        /// <summary>
        /// Вставить проекцию модели чтения состояний лица
        /// </summary>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task InsertAsync(PersonStateProjection projection, IUnitOfWork unitOfWork);

        /// <summary>
        /// Обновить проекцию модели чтения состояний лица
        /// </summary>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task UpdateAsync(PersonStateProjection projection, IUnitOfWork unitOfWork);

        /// <summary>
        /// Удалить проекцию модели чтения состояний лица
        /// </summary>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task DeleteAsync(PersonStateProjection projection, IUnitOfWork unitOfWork);

        /// <summary>
        /// Очистить репозиторий
        /// </summary>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task ClearAsync(IUnitOfWork unitOfWork);
    }
}
