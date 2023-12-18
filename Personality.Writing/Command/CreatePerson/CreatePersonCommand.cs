using Personality.Writing.Base;
using Personality.Writing.Command.CreatePerson.Model;
using System;

namespace Personality.Writing.Command.CreatePerson
{
    /// <summary>
    /// Команда создания лица
    /// </summary>
    public class CreatePersonCommand : ICommand<CreatePersonResponse>
    {
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
