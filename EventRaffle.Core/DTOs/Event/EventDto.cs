namespace EventRaffle.Core.DTOs.Event
{
    public class EventDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateUtc { get; set; }
        public DateTime EndDateUtc { get; set; }
        public bool IsActive { get; set; }
    }
}
