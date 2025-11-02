namespace EventRaffle.Core.Entities
{
    public class Event : AuditableFieldsEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
        public bool IsActive { get; set; }

        #region Navigation Properties
        public ICollection<Participant> Participants { get; set; } = new List<Participant>();
        public ICollection<Raffle> Raffles { get; set; } = new List<Raffle>();
        #endregion
    }
}
