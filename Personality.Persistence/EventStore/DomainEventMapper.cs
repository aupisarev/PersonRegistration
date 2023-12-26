using AutoMapper;
using AutoMapper.Internal;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Persistence.EventStore.EventModel.V1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Personality.Persistence.EventStore
{
    public class DomainEventMapper
    {
        private readonly IMapper mapper;
        private readonly Dictionary<Type, Type> toModelMapping;
        private readonly Dictionary<Type, Type> toEventMapping;

        public DomainEventMapper(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            var mc = new MapperConfiguration(c => c.AddProfile(new MapperProfile()));
            toModelMapping = mc.Internal().GetAllTypeMaps().Where(tm => tm.DestinationType.InheritsFrom(typeof(PersonEvent))).ToDictionary(v => v.DestinationType, k => k.SourceType);
            toEventMapping = toModelMapping.ToDictionary(v => v.Value, v => v.Key);
        }

        public object ToModel<TEvent>(TEvent @event) where TEvent : PersonEvent
        {
            if (!toModelMapping.TryGetValue(@event.GetType(), out var modelType))
                throw new ArgumentException($"Тип события {@event.GetType().Name} не поддерживается хранилищем событий");

            return mapper.Map(@event, @event.GetType(), modelType);
        }

        public object ToEvent<TModel>(TModel model) where TModel : PersonEventBase
        {
            if (!toEventMapping.TryGetValue(model.GetType(), out var eventType))
                throw new ArgumentException($"Тип модели хранения события {model.GetType().Name} не поддерживается хранилищем событий");

            return mapper.Map(model, model.GetType(), eventType);
        }
    }
}
