namespace ProductivityApp.Models
{
    public class FocusSession
    {
        public int Id { get; set; }
        public int DurationMinutes { get; set; } = 25;
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }
        public bool Completed { get; set; } = false;
    }
}
