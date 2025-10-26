using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using EventRaffle.Core.Entities;

namespace EventRaffle.Infrastracture.Persistence.Configurations
{
    public class EventRaffleConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(u => u.Description).HasMaxLength(500);

        }
    }
}
