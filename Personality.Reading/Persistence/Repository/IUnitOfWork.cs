using System.Threading;
using System.Threading.Tasks;

namespace Personality.Reading.Persistence.Repository
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
