using ProductivityApp.Helpers;

namespace ProductivityApp.Forms
{
    partial class FocusForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components   = new System.ComponentModel.Container();
            focusTimer   = new System.Windows.Forms.Timer(components);

            pnlHeader    = new Panel();
            lblFormTitle = new Label();
            lblSubtitle  = new Label();

            pnlCenter    = new Panel();
            pnlTimerRing = new Panel();    // timer'ı çevreleyen dekoratif alan
            lblTimer     = new Label();
            lblStatus    = new Label();

            progressBar  = new ProgressBar();

            pnlButtons   = new Panel();
            btnStartStop = AppTheme.MakeButton("▶   Başlat",  AppTheme.AccentBlue,   AppTheme.HoverBlue,   160, 46, new Font("Segoe UI", 12f, FontStyle.Bold));
            btnReset     = AppTheme.MakeButton("↺   Sıfırla", AppTheme.BgOverlay,    AppTheme.HoverGray,   140, 46, new Font("Segoe UI", 12f, FontStyle.Bold));

            pnlFooter    = new Panel();
            lblSessions  = new Label();

            pnlHeader.SuspendLayout();
            pnlCenter.SuspendLayout();
            pnlTimerRing.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── Timer ──────────────────────────────────────────────────────
            focusTimer.Interval = 1000;
            focusTimer.Tick    += focusTimer_Tick;

            // ── Form ──────────────────────────────────────────────────────
            ClientSize      = new Size(900, 600);
            Text            = "Odak Sayacı — Pomodoro";
            StartPosition   = FormStartPosition.CenterScreen;
            BackColor       = AppTheme.BgBase;
            Font            = AppTheme.FontBody;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            MinimumSize     = new Size(900, 600);
            Load           += FocusForm_Load;

            // ── pnlHeader ─────────────────────────────────────────────────
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 68;
            pnlHeader.BackColor = AppTheme.BgDeep;

            btnBack = AppTheme.MakeButton("← Geri", AppTheme.BgDeep, AppTheme.BgOverlay, 80, 36);
            btnBack.Location = new Point(16, 16);
            btnBack.Click += btnBack_Click;

            lblFormTitle.Text      = "⏱️  Odak Sayacı";
            lblFormTitle.Font      = AppTheme.FontH2;
            lblFormTitle.ForeColor = AppTheme.AccentOrange;
            lblFormTitle.Location  = new Point(110, 12);
            lblFormTitle.Size      = new Size(300, 28);

            lblSubtitle.Text      = "Pomodoro tekniği — 25 dakika tam odak";
            lblSubtitle.Font      = AppTheme.FontSmall;
            lblSubtitle.ForeColor = AppTheme.TextMuted;
            lblSubtitle.Location  = new Point(110, 40);
            lblSubtitle.Size      = new Size(340, 18);

            pnlHeader.Controls.AddRange(new Control[] { btnBack, lblFormTitle, lblSubtitle });

            // ── pnlCenter (timer bölgesi) ─────────────────────────────────
            pnlCenter.Dock      = DockStyle.Fill;
            pnlCenter.BackColor = AppTheme.BgBase;

            // Büyük sayaç etiketi
            lblTimer.Text      = "25:00";
            lblTimer.Font      = AppTheme.FontTimer;
            lblTimer.ForeColor = AppTheme.AccentPurple;
            lblTimer.Size      = new Size(360, 100);
            lblTimer.Location  = new Point(270, 70);
            lblTimer.TextAlign = ContentAlignment.MiddleCenter;

            // Durum etiketi
            lblStatus.Text      = "⏱   Hazır — Başlat butonuna bas";
            lblStatus.Font      = new Font("Segoe UI", 10f, FontStyle.Regular);
            lblStatus.ForeColor = AppTheme.TextSecondary;
            lblStatus.Size      = new Size(360, 24);
            lblStatus.Location  = new Point(270, 178);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            // ── ProgressBar ───────────────────────────────────────────────
            progressBar.Location  = new Point(240, 222);
            progressBar.Size      = new Size(420, 10);
            progressBar.Minimum   = 0;
            progressBar.Maximum   = 100;
            progressBar.Value     = 0;
            progressBar.Style     = ProgressBarStyle.Continuous;
            // ProgressBar rengini Paint olayıyla geçersiz kılıyoruz
            progressBar.Paint    += ProgressBar_Paint;

            // ── pnlButtons ────────────────────────────────────────────────
            pnlButtons.BackColor = AppTheme.BgBase;
            pnlButtons.Size      = new Size(360, 60);
            pnlButtons.Location  = new Point(270, 248);

            btnStartStop.Location = new Point(0,   10);
            btnReset.Location     = new Point(172, 10);
            btnReset.ForeColor    = AppTheme.TextSecondary;

            btnStartStop.Click   += btnStartStop_Click;
            btnReset.Click       += btnReset_Click;

            pnlButtons.Controls.AddRange(new Control[] { btnStartStop, btnReset });

            pnlCenter.Controls.AddRange(new Control[]
                { lblTimer, lblStatus, progressBar, pnlButtons });

            // ── pnlFooter ─────────────────────────────────────────────────
            pnlFooter.Dock      = DockStyle.Bottom;
            pnlFooter.Height    = 80;
            pnlFooter.BackColor = AppTheme.BgSurface;

            // Üst çizgi
            var sepLine = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = 1,
                BackColor = AppTheme.BgOverlay
            };

            // Tamamlanan seanslar
            lblSessions.Text      = "🍅  Tamamlanan Seans: 0";
            lblSessions.Font      = new Font("Segoe UI", 13f, FontStyle.Bold);
            lblSessions.ForeColor = AppTheme.AccentOrange;
            lblSessions.Location  = new Point(0, 18);
            lblSessions.Size      = new Size(500, 32);
            lblSessions.TextAlign = ContentAlignment.MiddleCenter;

            var lblFooterNote = new Label
            {
                Text      = "Her tamamlanan seans veritabanına kaydedilir",
                Font      = AppTheme.FontSmall,
                ForeColor = AppTheme.TextMuted,
                Location  = new Point(0, 52),
                Size      = new Size(500, 18),
                TextAlign = ContentAlignment.MiddleCenter
            };

            pnlFooter.Controls.AddRange(new Control[] { sepLine, lblSessions, lblFooterNote });

            Controls.AddRange(new Control[] { pnlCenter, pnlHeader, pnlFooter });

            pnlHeader.ResumeLayout(false);
            pnlCenter.ResumeLayout(false);
            pnlTimerRing.ResumeLayout(false);
            pnlButtons.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── ProgressBar özel renk çizimi ─────────────────────────────────
        private void ProgressBar_Paint(object sender, PaintEventArgs e)
        {
            var pb   = (ProgressBar)sender;
            var g    = e.Graphics;
            var rect = pb.ClientRectangle;

            // Arka plan
            using var bgBrush = new SolidBrush(AppTheme.BgOverlay);
            g.FillRectangle(bgBrush, rect);

            // Dolgu
            if (pb.Value > 0)
            {
                int fillWidth = (int)((double)pb.Value / pb.Maximum * rect.Width);
                using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, Math.Max(fillWidth, 1), rect.Height),
                    AppTheme.AccentBlue,
                    AppTheme.AccentPurple,
                    System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                g.FillRectangle(brush, 0, 0, fillWidth, rect.Height);
            }
        }

        // ── Alan değişkenleri ─────────────────────────────────────────────
        private System.Windows.Forms.Timer focusTimer;
        private Panel       pnlHeader;
        private Panel       pnlCenter;
        private Panel       pnlTimerRing;
        private Panel       pnlButtons;
        private Panel       pnlFooter;
        private Label       lblFormTitle;
        private Label       lblSubtitle;
        private Label       lblTimer;
        private Label       lblStatus;
        private Label       lblSessions;
        private Button      btnStartStop;
        private Button      btnReset;
        private Button      btnBack;
        private ProgressBar progressBar;
    }
}
