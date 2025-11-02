namespace EventRaffle.Core.DTOs.Raffle
{
    public class RaffleDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();

        public int Count { get; set; }
    }
}
