using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Persistence.Context;
using Personality.Persistence.EventStore.Entity;
using Personality.Persistence.EventStore.EventModel.V1;
using Personality.Writing.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Personality.Persistence.EventStore
{
    /// <summary>
    /// <inheritdoc cref="IPersonEventStore"/>
    /// </summary>
    public class PersonEventStore : IPersonEventStore
    {
        private readonly JsonSerializerOptions serializerOptions;
        private readonly DomainEventMapper domainEventMapper;

        public PersonEventStore(DomainEventMapper domainEventMapper)
        {
            this.domainEventMapper = domainEventMapper ?? throw new ArgumentNullException(nameof(domainEventMapper));

            serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

        public Task<IList<PersonEvent>> GetEventsAsync(PersonId personId, IUnitOfWork unitOfWork)
        {
            var ctx = GetDbContext(unitOfWork);
            return Task.FromResult(GetPersonEventsAsync(ctx.PersonEvents.Where(e => e.PersonId == personId)));
        }

        public Task<IList<PersonEvent>> GetAllAsync(IUnitOfWork unitOfWork)
        {
            var ctx = GetDbContext(unitOfWork);
            return Task.FromResult(GetPersonEventsAsync(ctx.PersonEvents));
        }

        private IList<PersonEvent> GetPersonEventsAsync(IQueryable<PersonEventItem> eventsSource)
        {
            IList<PersonEvent> events = new List<PersonEvent>();

            foreach (var eventItem in eventsSource)
            {
                var eventModelType = Assembly.GetAssembly(typeof(PersonEventBase))?.GetType(eventItem.ClrType);
                var eventModel = JsonSerializer.Deserialize(eventItem.Data, eventModelType, serializerOptions);
                var @event = domainEventMapper.ToEvent(eventModel as PersonEventBase);
                events.Add(@event as PersonEvent);
            }

            return events;
        }

        public async Task AddEventsAsync(IList<PersonEvent> events, IUnitOfWork unitOfWork)
        {
            var ctx = GetDbContext(unitOfWork);

            foreach (var @event in events)
            {
                var eventModel = domainEventMapper.ToModel(@event);

                var eventItem = new PersonEventItem(
                    @event.Id,
                    @event.PersonId,
                    @event.StateId,
                    @event.Description,
                    @event.OccurredAt,
                    eventModel.GetType().FullName,
                    JsonSerializer.Serialize(eventModel, eventModel.GetType(), serializerOptions));
                await ctx.PersonEvents.AddAsync(eventItem);
            }
        }

        private PersonalityContext GetDbContext(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is not PersonalityContext ctx)
                throw new InvalidCastException("Объект unitOfWork не соответствует реализации репозитория. Ожидается объект типа PersonalityContext");

            return ctx;
        }
    }
}
