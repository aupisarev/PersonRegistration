using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfState;
using Personality.Writing.Base;
using Personality.Writing.Command.UpdatePersonModification;
using Personality.Writing.Outside;
using Personality.Writing.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Writing.CommandHandler.UpdatePersonModification
{
    /// <summary>
    /// Обработчик команды обновления модификации лица
    /// </summary>
    public class UpdatePersonModificationCommandHandler : ICommandHandler<UpdatePersonModificationCommand, UpdatePersonModificationResponse>
    {
        private readonly IPersonEventStore personEventStore;
        private readonly IUnitOfWork unitOfWork;
        private readonly TimeProvider dateTimeProvider;
        private readonly IProjector projector;

        public UpdatePersonModificationCommandHandler(
            IPersonEventStore personEventStore,
            IUnitOfWork unitOfWork,
            TimeProvider dateTimeProvider,
            IProjector projector)
        {
            this.personEventStore = personEventStore ?? throw new ArgumentNullException(nameof(personEventStore));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this.projector = projector ?? throw new ArgumentNullException(nameof(projector));
        }

        public async Task<UpdatePersonModificationResponse> HandleAsync(UpdatePersonModificationCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var personId = new PersonId(command.PersonId);
            var events = await personEventStore.GetEventsAsync(personId, unitOfWork);
            if (!events.Any())
                throw new ArgumentException($"Не удалось найти лицо с идентификатором {command.PersonId}");

            var person = new Person(events);

            var stateStatus = StateStatus.Get(command.StatusValue);

            switch (stateStatus)
            {
                case var s when s == StateStatus.Approved:
                    person.ApproveState(dateTimeProvider.GetUtcNow().DateTime, dateTimeProvider.GetUtcNow().DateTime);
                    break;
                case var s when s == StateStatus.Applied:
                    person.ApplyState(dateTimeProvider.GetUtcNow().DateTime, dateTimeProvider.GetUtcNow().DateTime);
                    break;
                case var s when s == StateStatus.Canceled:
                    person.CancelState(dateTimeProvider.GetUtcNow().DateTime, dateTimeProvider.GetUtcNow().DateTime);
                    break;
            }

            //сохранение изменений write-стека
            await personEventStore.AddEventsAsync(person.Changes.ToList(), unitOfWork);

            //изменение read-стека
            await projector.ProjectAsync(person.Changes.ToList());

            //фиксация состояния в хранилище
            await unitOfWork.CommitAsync();

            return new UpdatePersonModificationResponse();
        }
    }
}
