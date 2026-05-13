using ProductivityApp.Database;
using ProductivityApp.Helpers;
using ProductivityApp.Models;

namespace ProductivityApp.Forms
{
    public partial class FocusForm : Form
    {
        // Pomodoro süresi: 25 dakika = 1500 saniye
        private const int TotalSeconds = 25 * 60;
        private int _remainingSeconds = TotalSeconds;
        private bool _isRunning = false;

        // Tamamlanan oturumlar
        private List<FocusSession> _sessions = new List<FocusSession>();
        private FocusSession? _currentSession = null;

        public FocusForm()
        {
            InitializeComponent();
        }

        // ── Form Load ──────────────────────────────────────────────────────
        private void FocusForm_Load(object sender, EventArgs e)
        {
            UpdateTimerLabel();
            UpdateSessionLabel();
        }

        // ── Başlat / Duraklat butonu ───────────────────────────────────────
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (!_isRunning)
            {
                // Başlat
                if (_remainingSeconds == TotalSeconds)
                {
                    _currentSession = new FocusSession
                    {
                        DurationMinutes = 25,
                        StartTime       = DateTime.Now
                    };
                }

                focusTimer.Start();
                _isRunning = true;
                btnStartStop.Text = "⏸   Duraklat";
                btnStartStop.BackColor = AppTheme.AccentOrange;
                btnStartStop.FlatAppearance.MouseOverBackColor = AppTheme.HoverOrange;
                lblStatus.Text = "🎯   Odaklanıyorsun! Konsantrasyonunu koru";
            }
            else
            {
                // Duraklat
                focusTimer.Stop();
                _isRunning = false;
                btnStartStop.Text = "▶   Devam Et";
                btnStartStop.BackColor = AppTheme.AccentBlue;
                btnStartStop.FlatAppearance.MouseOverBackColor = AppTheme.HoverBlue;
                lblStatus.Text = "⏸   Duraklatıldı";
            }
        }

        // ── Sıfırla butonu ────────────────────────────────────────────────
        private void btnReset_Click(object sender, EventArgs e)
        {
            focusTimer.Stop();
            _isRunning = false;
            _remainingSeconds = TotalSeconds;
            _currentSession = null;
            UpdateTimerLabel();
            btnStartStop.Text = "▶   Başlat";
            btnStartStop.BackColor = AppTheme.AccentBlue;
            btnStartStop.FlatAppearance.MouseOverBackColor = AppTheme.HoverBlue;
            lblStatus.Text = "⏱   Hazır — Başlat butonuna bas";
            progressBar.Value = 0;
            progressBar.Invalidate();  // özel Paint tetikle
        }

        // ── Timer Tick — her saniye tetiklenir ────────────────────────────
        private void focusTimer_Tick(object sender, EventArgs e)
        {
            if (_remainingSeconds > 0)
            {
                _remainingSeconds--;
                UpdateTimerLabel();

                // İlerleme çubuğu
                int elapsed = TotalSeconds - _remainingSeconds;
                progressBar.Value = (int)((double)elapsed / TotalSeconds * 100);
            }
            else
            {
                // Süre doldu
                focusTimer.Stop();
                _isRunning = false;
                _remainingSeconds = TotalSeconds;

                if (_currentSession != null)
                {
                    _currentSession.EndTime   = DateTime.Now;
                    _currentSession.Completed = true;
                    _sessions.Add(_currentSession);
                    DatabaseHelper.InsertFocusSession(_currentSession);
                    _currentSession = null;
                }

                UpdateTimerLabel();
                UpdateSessionLabel();
                progressBar.Value = 100;

                btnStartStop.Text = "▶   Başlat";
                btnStartStop.BackColor = AppTheme.AccentBlue;
                lblStatus.Text = "✅   Tebrikler! 25 dakika tamamlandı";

                MessageBox.Show("🎉 25 dakikalık odak seansı tamamlandı! Harikasın!",
                    "Pomodoro Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar.Value = 0;
            }
        }

        // ── Yardımcı: sayacı biçimlendir ──────────────────────────────────
        private void UpdateTimerLabel()
        {
            int m = _remainingSeconds / 60;
            int s = _remainingSeconds % 60;
            lblTimer.Text = $"{m:D2}:{s:D2}";
        }

        private void UpdateSessionLabel()
        {
            lblSessions.Text = $"🍅  Tamamlanan Seans: {_sessions.Count}";
        }

        // ── Geri Dön Butonu ────────────────────────────────────────────────
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
