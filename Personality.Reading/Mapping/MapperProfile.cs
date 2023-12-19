using AutoMapper;
using Personality.Domain.OfPerson.OfIdentityDocument;
using Personality.Domain.OfPerson.OfPersonalData;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;

namespace Personality.Reading.Mapping
{
    /// <summary>
    /// Маппинг
    /// </summary>
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<int, Sex>()
                .ConvertUsing(s => Sex.Get(s));
            CreateMap<Sex, int>()
                .ConvertUsing(s => s.Value);


            CreateMapHistoryProjection();
        }

        private void CreateMapHistoryProjection()
        {
            CreateMap<PersonalName, PersonalDataHistoryProjection>(MemberList.Source);
            CreateMap<PersonalData, PersonalDataHistoryProjection>(MemberList.Source)
                .IncludeMembers(s => s.PersonalName);

            CreateMap<IdentityDocument, IdentityDocumentHistoryProjection>(MemberList.Source)
                .ForMember(d => d.IdentityDocumentTypeValue, m => m.MapFrom(s => s.DocumentType.Value))
                .ForMember(d => d.IdentityDocumentTypeName, m => m.MapFrom(s => s.DocumentType.Name))
                .ForMember(d => d.WhoIssued, m => m.MapFrom(s => s.WhoIssued.Value))
                .ForSourceMember(s => s.DocumentType, m => m.DoNotValidate())
                .ForSourceMember(s => s.WhoIssued, m => m.DoNotValidate());
        }
    }
}
