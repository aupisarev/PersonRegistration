using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfIdentityDocument;
using Personality.Domain.OfPerson.OfPersonalData;
using Personality.Writing.Base;
using Personality.Writing.Command.UpdatePerson;
using Personality.Writing.Outside;
using Personality.Writing.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Personality.Writing.CommandHandler.UpdatePerson
{
    /// <summary>
    /// Обработчик команды обновления лица
    /// </summary>
    public class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, UpdatePersonResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPersonEventStore personEventStore;
        private readonly ICountryGetter countryGetter;
        private readonly TimeProvider dateTimeProvider;
        private readonly IProjector projector;

        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork,
            IPersonEventStore personEventStore,
            ICountryGetter countryGetter,
            TimeProvider dateTimeProvider,
            IProjector projector)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.personEventStore = personEventStore ?? throw new ArgumentNullException(nameof(personEventStore));
            this.countryGetter = countryGetter ?? throw new ArgumentNullException(nameof(countryGetter));
            this.dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            this.projector = projector ?? throw new ArgumentNullException(nameof(projector));
        }

        public async Task<UpdatePersonResponse> HandleAsync(UpdatePersonCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var personId = new PersonId(command.PersonId);
            var events = await personEventStore.GetEventsAsync(personId, unitOfWork);
            if (!events.Any())
                throw new ArgumentException($"Не удалось найти лицо с идентификатором {command.PersonId}");

            var person = new Person(events);

            //персональные данные
            if (command.PersonalData != null)
            {
                var citizenship = await countryGetter.GetAsync(command.PersonalData.CitizenshipValue);
                if (citizenship == null)
                    throw new ArgumentException($"Не удалось найти страну гражданства с кодом {command.PersonalData.CitizenshipValue}");

                var personalData = new PersonalData(
                    new PersonalName(command.PersonalData.LastName, command.PersonalData.FirstName, command.PersonalData.MiddleName),
                    DateOnly.FromDateTime(command.PersonalData.Birthday),
                    command.PersonalData.DeathDate.HasValue ? DateOnly.FromDateTime(command.PersonalData.DeathDate.Value) : null,
                    Sex.Get(command.PersonalData.SexValue),
                    new Citizenship(citizenship));

                person.ChangePersonalData(personalData, command.ChangeDate, dateTimeProvider.GetUtcNow().DateTime);
            }

            if (command.IdentityDocument != null)
            {
                var identityDocumentType = IdentityDocumentType.Get(command.IdentityDocument.IdentityDocumentTypeValue);

                var issuedCountry = await countryGetter.GetAsync(command.IdentityDocument.IssuedCountryValue);
                if (issuedCountry == null)
                    throw new ArgumentException($"Не удалось найти страну выдачи документа с кодом {command.IdentityDocument.IssuedCountryValue}");

                var identityDocument = new IdentityDocument(
                    identityDocumentType,
                    command.IdentityDocument.Serial,
                    command.IdentityDocument.Number,
                    new Validity(
                        DateOnly.FromDateTime(command.IdentityDocument.ValidityDateStart),
                        command.IdentityDocument.ValidityDateEnd.HasValue ? DateOnly.FromDateTime(command.IdentityDocument.ValidityDateEnd.Value) : null),
                    new WhoIssued(command.IdentityDocument.WhoIssued),
                    issuedCountry);

                person.ChangeIdentityDocument(identityDocument, command.ChangeDate, dateTimeProvider.GetUtcNow().DateTime);
            }

            //сохранение изменений write-стека
            await personEventStore.AddEventsAsync(person.Changes.ToList(), unitOfWork);

            //изменение read-стека
            await projector.ProjectAsync(person.Changes.ToList());

            //фиксация состояния в хранилище
            await unitOfWork.CommitAsync();

            return new UpdatePersonResponse();
        }
    }
}
