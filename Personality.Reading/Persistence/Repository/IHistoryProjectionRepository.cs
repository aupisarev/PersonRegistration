using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Personality.Reading.Projecting.PersonHistoryModel;

namespace Personality.Reading.Persistence.Repository
{
    /// <summary>
    /// Репозиторий проекций модели чтения истории данных лица
    /// </summary>
    public interface IHistoryProjectionRepository
    {
        /// <summary>
        /// Получить список проекций модели чтения истории данных лица
        /// </summary>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="condition">Условие поиска</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task<IList<TProjection>> GetAsync<TProjection>(Expression<Func<TProjection, bool>> condition, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection;

        /// <summary>
        /// Добавить проекцию модели чтения истории данных лица
        /// </summary>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task InsertAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection;

        /// <summary>
        /// Изменить проекцию модели чтения истории данных лица
        /// </summary>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task UpdateAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection;

        /// <summary>
        /// Удалить проекцию модели чтения истории данных лица
        /// </summary>
        /// <typeparam name="TProjection">Тип проекции</typeparam>
        /// <param name="projection">Проекция</param>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task DeleteAsync<TProjection>(TProjection projection, IUnitOfWork unitOfWork) where TProjection : class, IHistoryProjection;

        /// <summary>
        /// Очистить репозиторий
        /// </summary>
        /// <param name="unitOfWork">Единица работы</param>
        /// <returns></returns>
        public Task ClearAsync(IUnitOfWork unitOfWork);
    }
}
