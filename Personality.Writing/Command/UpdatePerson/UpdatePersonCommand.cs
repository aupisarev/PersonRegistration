using Personality.Writing.Base;
using Personality.Writing.Command.UpdatePerson.Model;
using System;

namespace Personality.Writing.Command.UpdatePerson
{
    /// <summary>
    /// Команда обновления лица
    /// </summary>
    public class UpdatePersonCommand : ICommand<UpdatePersonResponse>
    {
        /// <summary>
        /// Идентификатор лица
        /// </summary>
        public long PersonId { get; set; }

        /// <summary>
        /// Персональные данные
        /// </summary>
        public PersonalData PersonalData { get; set; }

        /// <summary>
        /// Документ, удостоверяющий личность
        /// </summary>
        public IdentityDocument IdentityDocument { get; set; }

        /// <summary>
        /// Дата применения изменений
        /// </summary>
        /// <remarks>
        /// Учитывается точность до минуты
        /// </remarks>
        public DateTime ChangeDate { get; set; }
    }
}
