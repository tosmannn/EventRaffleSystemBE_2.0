namespace EventRaffle.Core.Entities
{
    public class Participant: AuditableFieldsEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string? EmployeeId { get; set; }
        public Guid EventId { get; set; }
        public bool IsRegistered { get; set; }
        public DateTime? RegisteredAt { get; set; }

        #region Navigation Properties
        public Event Event { get; set; }
        #endregion

    }
}
