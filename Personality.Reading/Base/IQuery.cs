namespace Personality.Reading.Base
{
    /// <summary>
    /// Запрос
    /// </summary>
    /// <typeparam name="TResult">Тип результата</typeparam>
    public interface IQuery<TResult> where TResult : IResponse
    {
    }
}
