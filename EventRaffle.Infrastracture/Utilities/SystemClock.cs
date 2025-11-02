using EventRaffle.Core.Interfaces.Time;

namespace EventRaffle.Infrastracture.Utilities
{
    public class SystemClock : IClock
    {
        public DateTime UtcNow =>  DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
