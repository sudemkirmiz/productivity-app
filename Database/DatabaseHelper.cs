using Microsoft.Data.Sqlite;

namespace ProductivityApp.Database
{
    /// <summary>
    /// SQLite veritabanı yardımcı sınıfı.
    /// Tabloları oluşturur ve temel CRUD işlemlerini sağlar.
    /// </summary>
    public static class DatabaseHelper
    {
        // Veritabanı dosyası uygulamanın çalışma dizinine kaydedilir.
        private static readonly string DbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "productivity.db");

        private static string ConnectionString => $"Data Source={DbPath}";

        /// <summary>
        /// Uygulama başlangıcında çağrılır — tablolar yoksa oluşturur.
        /// </summary>
        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            // Users tablosu
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    FullName TEXT
                );
                -- Varsayılan kullanıcı (demo)
                INSERT OR IGNORE INTO Users (Username, Password, FullName)
                VALUES ('admin', '1234', 'Admin Kullanıcı');
            ";
            cmd.ExecuteNonQuery();

            // Tasks tablosu
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title     TEXT    NOT NULL,
                    Priority  TEXT    DEFAULT 'Normal',
                    Status    TEXT    DEFAULT 'Bekliyor',
                    DueDate   TEXT,
                    CreatedAt TEXT
                );
            ";
            cmd.ExecuteNonQuery();

            // Habits tablosu
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Habits (
                    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name        TEXT NOT NULL,
                    StreakDays  INTEGER DEFAULT 0,
                    LastChecked TEXT,
                    CreatedAt   TEXT
                );
            ";
            cmd.ExecuteNonQuery();

            // FocusSessions tablosu
            cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS FocusSessions (
                    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    DurationMinutes INTEGER DEFAULT 25,
                    StartTime       TEXT,
                    EndTime         TEXT,
                    Completed       INTEGER DEFAULT 0
                );
            ";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Kullanıcı adı ve şifreyle giriş doğrulaması yapar.
        /// </summary>
        public static bool ValidateUser(string username, string password)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            long count = (long)(cmd.ExecuteScalar() ?? 0L);
            return count > 0;
        }

        // ──────────────────────────────────────
        // TASK CRUD
        // ──────────────────────────────────────

        public static void InsertTask(Models.TaskItem task)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Tasks (Title, Priority, Status, DueDate, CreatedAt)
                VALUES (@title, @priority, @status, @due, @created);
            ";
            cmd.Parameters.AddWithValue("@title",    task.Title);
            cmd.Parameters.AddWithValue("@priority", task.Priority);
            cmd.Parameters.AddWithValue("@status",   task.Status);
            cmd.Parameters.AddWithValue("@due",      task.DueDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@created",  task.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public static void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Tasks WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ──────────────────────────────────────
        // HABIT CRUD
        // ──────────────────────────────────────

        public static void InsertHabit(Models.Habit habit)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Habits (Name, StreakDays, LastChecked, CreatedAt)
                VALUES (@name, @streak, @last, @created);
            ";
            cmd.Parameters.AddWithValue("@name",    habit.Name);
            cmd.Parameters.AddWithValue("@streak",  habit.StreakDays);
            cmd.Parameters.AddWithValue("@last",    habit.LastChecked.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@created", habit.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.ExecuteNonQuery();
        }

        public static void DeleteHabit(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Habits WHERE Id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // ──────────────────────────────────────
        // FOCUS SESSION
        // ──────────────────────────────────────

        public static void InsertFocusSession(Models.FocusSession session)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO FocusSessions (DurationMinutes, StartTime, EndTime, Completed)
                VALUES (@dur, @start, @end, @completed);
            ";
            cmd.Parameters.AddWithValue("@dur",       session.DurationMinutes);
            cmd.Parameters.AddWithValue("@start",     session.StartTime.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters.AddWithValue("@end",       session.EndTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@completed", session.Completed ? 1 : 0);
            cmd.ExecuteNonQuery();
        }
    }
}
