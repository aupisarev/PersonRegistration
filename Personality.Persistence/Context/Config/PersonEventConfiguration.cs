using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personality.Domain.OfPerson;
using Personality.Domain.OfPerson.OfEvent;
using Personality.Domain.OfPerson.OfState;
using Personality.Persistence.EventStore.Entity;

namespace Personality.Persistence.Context.Config
{
    internal class PersonEventConfiguration : IEntityTypeConfiguration<PersonEventItem>
    {
        public void Configure(EntityTypeBuilder<PersonEventItem> builder)
        {
            builder.ToTable("PersonEvent");

            builder.Property<long>("Id")
                .ValueGeneratedOnAdd();
            builder.HasKey("Id");

            builder.Property(e => e.EventId)
                .IsRequired()
                .HasConversion(v => v.Value, v => new PersonEventId(v));

            builder.Property(e => e.PersonId)
                .IsRequired()
                .HasConversion(v => v.Value, v => new PersonId(v));

            builder.Property(e => e.StateId)
                .IsRequired()
                .HasConversion(v => v.Value, v => new StateId(v));

            builder.Property(e => e.Description);

            builder.Property(e => e.OccurredAt);

            builder.Property(e => e.ClrType)
                .IsRequired();

            builder.Property(e => e.Data)
                .IsRequired();
        }
    }
}
