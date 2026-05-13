using ProductivityApp.Database;
using ProductivityApp.Forms;

namespace ProductivityApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Windows Forms görsel stilleri ve metin render'ını etkinleştir
            ApplicationConfiguration.Initialize();

            // SQLite tablolarını oluştur (yoksa)
            DatabaseHelper.InitializeDatabase();

            // Uygulama LoginForm ile başlar
            Application.Run(new LoginForm());
        }
    }
}