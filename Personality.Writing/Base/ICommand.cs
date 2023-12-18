namespace Personality.Writing.Base
{
    /// <summary>
    /// Команда
    /// </summary>
    public interface ICommand<TResult> where TResult : IResponse
    {
    }
}
