using System.Threading.Tasks;

namespace Personality.Reading.Projecting
{
    /// <summary>
    /// Генератор идентификаторов проекций
    /// </summary>
    public interface IProjectionIdGenerator
    {
        /// <summary>
        /// Получить новый идентификатор
        /// </summary>
        /// <typeparam name="T">Тип проекции</typeparam>
        Task<long> NewIdAsync<T>() where T : IProjection;
    }
}
