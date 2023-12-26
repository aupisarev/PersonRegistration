namespace Personality.Persistence.Context
{
    /// <summary>
    /// Наблюдатель за контекстом
    /// </summary>
    public interface IContextObserver
    {
        /// <summary>
        /// Обработать событие Commit контекста
        /// </summary>
        void HandleCommit();
    }
}
