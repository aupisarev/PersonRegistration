using Personality.Reading.Base;

namespace Personality.Reading.Query.GetPersonListItem
{
    /// <summary>
    /// Запрос на получение элементов списка лиц
    /// </summary>
    public class GetPersonListItemQuery : IQuery<GetPersonListItemResponse>
    {
        ///// <summary>
        ///// Параметры чтения
        ///// </summary>
        //public LightReadParams<PersonListItemView> LightReadParams { get; set; }

        /// <summary>
        /// Включать рабочие состояния лиц
        /// </summary>
        public bool IncludeWorkState { get; set; } = false;
    }
}
