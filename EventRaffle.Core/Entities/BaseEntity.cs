namespace EventRaffle.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedUtcDate { get; set; } = DateTime.UtcNow;
    }
}
