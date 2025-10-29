using EventRaffle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventRaffle.Infrastracture.Persistence.Configurations
{
    internal class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(100);    

            builder.Property(p => p.EmployeeId)
                   .HasMaxLength(50);

            builder.HasOne(p => p.Event)
                   .WithMany(e => e.Participants)
                   .HasForeignKey(p => p.EventId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}