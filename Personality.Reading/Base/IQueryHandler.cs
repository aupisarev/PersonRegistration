using System.Threading.Tasks;

namespace Personality.Reading.Base
{
    /// <summary>
    /// Обработчик запросов
    /// </summary>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IResponse
    {
        /// <summary>
        /// Выполнить запрос
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Результат</returns>
        Task<TResult> HandleAsync(TQuery query);
    }
}
