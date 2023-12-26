using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personality.Reading.Projecting.PersonHistoryModel;
using Personality.Reading.Projecting.PersonHistoryModel.Projection;

namespace Personality.Persistence.Context.Config
{
    internal class HistoryProjectionConfiguration
        : IEntityTypeConfiguration<PersonalDataHistoryProjection>,
          IEntityTypeConfiguration<IdentityDocumentHistoryProjection>
    {
        public void Configure(EntityTypeBuilder<PersonalDataHistoryProjection> builder)
        {
            builder.UseTpcMappingStrategy().ToTable("ProjectionHistoryPersonalData");

            BuildIHistoryProjection(builder);
        }

        public void Configure(EntityTypeBuilder<IdentityDocumentHistoryProjection> builder)
        {
            builder.UseTpcMappingStrategy().ToTable("ProjectionHistoryIdentityDocument");

            BuildIHistoryProjection(builder);
        }

        private void BuildIHistoryProjection<TProjection>(EntityTypeBuilder<TProjection> builder)
            where TProjection : class, IHistoryProjection
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();
        }
    }
}
