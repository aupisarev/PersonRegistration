using Personality.Writing.Base;

namespace Personality.Writing.Command.UpdatePersonModification
{
    /// <summary>
    /// Команда обновления модификации лица
    /// </summary>
    public class UpdatePersonModificationCommand : ICommand<UpdatePersonModificationResponse>
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Значение статуса для установки на текущее состояние изменений
        /// </summary>
        public string StatusValue { get; set; }
    }
}
