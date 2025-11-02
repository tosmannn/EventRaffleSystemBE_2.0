namespace EventRaffle.Core.Entities
{
    public class Raffle : AuditableFieldsEntity
    {
        public Guid ParticipantId { get; set; }
        public bool IsSelected { get; set; }
        public Guid EventId { get; set; }

        #region Navigation Property
        public Participant Participant { get; set; } = null!;
        public Event Event { get; set; } = null!;
        #endregion
    }
}
