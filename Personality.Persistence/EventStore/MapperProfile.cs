using System;
using AutoMapper;
using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfEvent.Events;
using Personality.Domain.OfPerson.OfEvent.Events.StateEvents;
using Personality.Domain.OfPerson.OfIdentityDocument;
using Personality.Domain.OfPerson.OfPersonalData;
using Personality.Domain.OfPerson.OfState;

namespace Personality.Persistence.EventStore
{
    /// <summary>
    /// Профиль автомаппера
    /// </summary>
    /// <remarks>
    /// Выполняет роль EventUpcaster:<br/>
    /// после изменений домена при невозможности привести события домена к существующим моделям событий, необходимо создать новые версии моделей событий и настроить маппинг
    /// </remarks>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<long, PersonEventId>()
               .ConvertUsing(s => new PersonEventId(s));
            CreateMap<PersonEventId, long>()
                .ConvertUsing(d => d.Value);

            CreateMap<long, PersonId>()
                .ConvertUsing(s => new PersonId(s));
            CreateMap<PersonId, long>()
                .ConvertUsing(d => d.Value);

            CreateMap<long, StateId>()
               .ConvertUsing(s => new StateId(s));
            CreateMap<StateId, long>()
               .ConvertUsing(d => d.Value);

            CreateMap<DateTime, DateOnly>()
                .ConvertUsing(s => DateOnly.FromDateTime(s));
            CreateMap<DateOnly, DateTime>()
                .ConvertUsing(s => s.ToDateTime(TimeOnly.MinValue));

            CreateMapForStateApproved();
            CreateMapForStateApplied();
            CreateMapForStateCanceled();

            CreateMapForPersonCreated();
            CreateMapForPersonalDataChanged();
            CreateMapForIdentityDocumentChanged();
            CreateMapForPersonDied();
        }

        private void CreateMapForStateApproved()
        {
            CreateMap<StateApproved, EventModel.V1.StateApproved>()
                .ReverseMap();
        }

        private void CreateMapForStateApplied()
        {
            CreateMap<StateApplied, EventModel.V1.StateApplied>()
                .ReverseMap();
        }

        private void CreateMapForStateCanceled()
        {
            CreateMap<StateCanceled, EventModel.V1.StateCanceled>()
                .ReverseMap();
        }

        private void CreateMapForPersonCreated()
        {
            CreateMap<PersonCreated, EventModel.V1.PersonCreated>()
               .ReverseMap();
        }

        private void CreateMapForPersonalDataChanged()
        {
            CreateMap<int, Sex>()
                .ConvertUsing(s => Sex.Get(s));
            CreateMap<Sex, int>()
                .ConvertUsing(s => s.Value);

            CreateMap<PersonalName, EventModel.V1.PersonalDataChanged>(MemberList.Source)
                .ReverseMap();

            CreateMap<PersonalData, EventModel.V1.PersonalDataChanged>(MemberList.Source)
                .IncludeMembers(s => s.PersonalName)
                .ReverseMap();

            CreateMap<PersonalDataChanged, EventModel.V1.PersonalDataChanged>()
                .IncludeMembers(s => s.PersonalData)
                .ReverseMap()
                .ForCtorParam(nameof(PersonalDataChanged.PersonalData), m => m.MapFrom(s => s));
        }

        private void CreateMapForIdentityDocumentChanged()
        {
            CreateMap<int, IdentityDocumentType>()
                .ConvertUsing(s => IdentityDocumentType.Get(s));
            CreateMap<IdentityDocumentType, int>()
                .ConvertUsing(s => s.Value);

            CreateMap<WhoIssued, string>()
                .ConvertUsing(s => s.Value);
            CreateMap<string, WhoIssued>()
                .ConvertUsing(s => new WhoIssued(s));

            CreateMap<IdentityDocument, EventModel.V1.IdentityDocumentChanged>(MemberList.Source)
                .ForMember(d => d.IdentityDocumentTypeValue, m => m.MapFrom(s => s.DocumentType))
                .ForMember(d => d.WhoIssued, m => m.MapFrom(s => s.WhoIssued))
                .ReverseMap();

            CreateMap<IdentityDocumentChanged, EventModel.V1.IdentityDocumentChanged>()
                .IncludeMembers(s => s.IdentityDocument)
                .ReverseMap()
                .ForCtorParam(nameof(IdentityDocumentChanged.IdentityDocument), m => m.MapFrom(s => s));
        }

        private void CreateMapForPersonDied()
        {
            CreateMap<PersonDied, EventModel.V1.PersonDied>()
                .ReverseMap();
        }
    }
}
