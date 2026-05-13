namespace ProductivityApp.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Priority { get; set; } = "Normal";  // Düşük / Normal / Yüksek
        public string Status { get; set; } = "Bekliyor";  // Bekliyor / Devam Ediyor / Tamamlandı
        public DateTime DueDate { get; set; } = DateTime.Today;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
