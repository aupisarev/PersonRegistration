﻿namespace Personality.Writing.Base
{
    /// <summary>
    /// Команда
    /// </summary>
    /// <typeparam name="TResult">Тип результата</typeparam>
    public interface ICommand<TResult> where TResult : IResponse
    {
    }
}
