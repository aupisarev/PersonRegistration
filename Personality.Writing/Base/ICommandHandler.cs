using System.Threading.Tasks;

namespace Personality.Writing.Base
{
    /// <summary>
    /// Обработчик команды
    /// </summary>
    /// <typeparam name="TCommand">Команда</typeparam>
    /// <typeparam name="TResponse">Ответ</typeparam>
    public interface ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="command">Команда</param>
        /// <returns>Ответ</returns>
        Task<TResponse> HandleAsync(TCommand command);
    }
}
