using Personality.Domain.Base;
using System.Threading.Tasks;

namespace Personality.Domain
{
    /// <summary>
    /// Генератор идентификаторов сущности
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Получить новый идентификатор
        /// </summary>
        /// <typeparam name="TEntityId">Тип идентификатора</typeparam>
        /// <returns>Новый идентификатор</returns>
        public Task<TEntityId> NewIdAsync<TEntityId>() where TEntityId : IEntityId;
    }
}
