namespace ProductivityApp.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StreakDays { get; set; } = 0;
        public DateTime LastChecked { get; set; } = DateTime.Today;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
