using System.Threading;
using System.Threading.Tasks;

namespace Personality.Writing.Persistence
{
    /// <summary>
    /// Единица работы с хранилищем
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
