namespace EventRaffle.Core.DTOs.Dashboard
{
    public class DashboardDto
    {
        public string EventName { get; set; } = string.Empty;
        public int TotalParticipants { get; set; }
        public int RegisteredParticipants { get; set; }
    }
}
