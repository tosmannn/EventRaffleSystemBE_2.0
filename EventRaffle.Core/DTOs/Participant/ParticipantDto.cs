namespace EventRaffle.Core.DTOs.Participant
{
    public class ParticipantDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string? EmployeeId { get; set; }
        public Guid EventId { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime? RegisteredAt { get; set; }
    }
}
