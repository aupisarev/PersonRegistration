using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personality.Reading.Projecting.PersonStateModel.Projection;

namespace Personality.Persistence.Context.Config
{
    internal class PersonStateProjectionConfiguration : IEntityTypeConfiguration<PersonStateProjection>
    {
        public void Configure(EntityTypeBuilder<PersonStateProjection> builder)
        {
            builder.UseTpcMappingStrategy().ToTable("ProjectionPersonState");

            builder.Property(e => e.Id)
                .ValueGeneratedNever();
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.PersonId);

            builder.OwnsMany(e => e.Events, b =>
            {
                b.ToTable("ProjectionPersonStateEvent");
            });
        }
    }
}
