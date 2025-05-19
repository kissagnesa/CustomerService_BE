namespace Backend.DTOs
{
    public class CallsDto
    {
        public string CustomerName { get; set; } = null!;

        public string AgentName { get; set; } = null!;

        public int PhoneNumber { get; set; }

        public DateTime CallDate { get; set; }

        public TimeSpan CallTime { get; set; }
    }
}
