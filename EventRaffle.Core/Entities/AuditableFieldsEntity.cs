namespace EventRaffle.Core.Entities
{
    public abstract class AuditableFieldsEntity : BaseEntity
    {
        public DateTime? ModifiedUtcDate { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? DeletedUtcDate { get; set; }
        public string? DeletedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
