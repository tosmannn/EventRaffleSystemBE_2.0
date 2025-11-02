using EventRaffle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventRaffle.Infrastracture.Persistence.Configurations
{
    public class RaffleConfiguration : IEntityTypeConfiguration<Raffle>
    {
        public void Configure(EntityTypeBuilder<Raffle> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                 .ValueGeneratedOnAdd();

            builder.Property(r => r.IsSelected)
                   .IsRequired();

            builder
                .HasIndex(r => new { r.ParticipantId, r.EventId })
                .IsUnique();

            builder.HasOne(r => r.Participant)
                .WithOne(p => p.Raffle)
                .HasForeignKey<Raffle>(r => r.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Event)
                .WithMany(e => e.Raffles)
                .HasForeignKey(r => r.EventId);
        }
    }
}
