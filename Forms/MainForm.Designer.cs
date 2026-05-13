using ProductivityApp.Helpers;

namespace ProductivityApp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader  = new Panel();
            lblAppLogo = new Label();
            lblWelcome = new Label();
            lblDate    = new Label();
            pnlBody    = new Panel();
            pnlCards   = new Panel();

            // Kart butonlar
            btnTasks  = MakeNavCard("📋", "Görevler",      "Yapılacakları planla",   AppTheme.AccentBlue,   AppTheme.HoverBlue);
            btnHabits = MakeNavCard("🔥", "Alışkanlıklar", "Serileri koru ve büyüt", AppTheme.AccentGreen,  AppTheme.HoverGreen);
            btnFocus  = MakeNavCard("⏱️", "Odak Sayacı",   "25 dakika tam konsantre", AppTheme.AccentOrange, AppTheme.HoverOrange);

            pnlFooter  = new Panel();
            btnLogout  = AppTheme.MakeButton("  Çıkış", AppTheme.AccentRed, AppTheme.HoverRed, 110, 36);

            pnlHeader.SuspendLayout();
            pnlBody.SuspendLayout();
            pnlCards.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────
            ClientSize      = new Size(900, 600);
            MinimumSize     = new Size(900, 600);
            Text            = "ProductivityApp — Dashboard";
            StartPosition   = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            BackColor       = AppTheme.BgBase;
            Font            = AppTheme.FontBody;
            Load           += MainForm_Load;

            // ── pnlHeader ────────────────────────────────────────────────
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 110;
            pnlHeader.BackColor = AppTheme.BgDeep;
            pnlHeader.Padding   = new Padding(24, 0, 24, 0);
            pnlHeader.Paint    += (s, e) =>
            {
                using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    pnlHeader.ClientRectangle,
                    Color.FromArgb(15, 15, 40),
                    AppTheme.BgDeep,
                    System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                e.Graphics.FillRectangle(brush, pnlHeader.ClientRectangle);
            };

            lblAppLogo.Text      = "🎯 ProductivityApp";
            lblAppLogo.Font      = AppTheme.FontTitle;
            lblAppLogo.ForeColor = AppTheme.AccentPurple;
            lblAppLogo.Location  = new Point(24, 16);
            lblAppLogo.Size      = new Size(340, 36);

            lblWelcome.Text      = "Hoş geldin!";   // Load'da doldurulacak
            lblWelcome.Font      = new Font("Segoe UI", 11f, FontStyle.Regular);
            lblWelcome.ForeColor = AppTheme.TextPrimary;
            lblWelcome.Location  = new Point(24, 56);
            lblWelcome.Size      = new Size(400, 24);

            lblDate.Text      = DateTime.Now.ToString("dddd, dd MMMM yyyy",
                                    new System.Globalization.CultureInfo("tr-TR"));
            lblDate.Font      = AppTheme.FontSmall;
            lblDate.ForeColor = AppTheme.TextMuted;
            lblDate.Location  = new Point(24, 82);
            lblDate.Size      = new Size(300, 18);

            pnlHeader.Controls.AddRange(new Control[] { lblAppLogo, lblWelcome, lblDate });

            // ── pnlBody ───────────────────────────────────────────────────
            pnlBody.Dock      = DockStyle.Fill;
            pnlBody.BackColor = AppTheme.BgBase;
            pnlBody.Padding   = new Padding(24, 24, 24, 0);

            // Subtitle
            var lblSection = new Label
            {
                Text      = "MODÜLLER",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                ForeColor = AppTheme.TextMuted,
                Location  = new Point(24, 24),
                Size      = new Size(200, 16)
            };

            // ── pnlCards (kart butonları dikey sıralar) ───────────────────
            pnlCards.BackColor = AppTheme.BgBase;
            pnlCards.Location  = new Point(24, 46);
            pnlCards.Size      = new Size(512, 336);

            btnTasks.Location  = new Point(0,   0);
            btnHabits.Location = new Point(0, 110);
            btnFocus.Location  = new Point(0, 220);

            // ── Click event bağlamaları ────────────────────────────────────
            btnTasks.Click  += btnTasks_Click;
            btnHabits.Click += btnHabits_Click;
            btnFocus.Click  += btnFocus_Click;

            pnlCards.Controls.AddRange(new Control[] { btnTasks, btnHabits, btnFocus });

            pnlBody.Controls.AddRange(new Control[] { lblSection, pnlCards });

            // ── pnlFooter ─────────────────────────────────────────────────
            pnlFooter.Dock      = DockStyle.Bottom;
            pnlFooter.Height    = 56;
            pnlFooter.BackColor = AppTheme.BgDeep;

            btnLogout.Location = new Point(430, 10);
            btnLogout.Click   += btnLogout_Click;
            pnlFooter.Controls.Add(btnLogout);

            var lblVersion = new Label
            {
                Text      = "v1.0  •  Kişisel Verimlilik Uygulaması",
                Font      = AppTheme.FontSmall,
                ForeColor = AppTheme.TextMuted,
                Location  = new Point(24, 18),
                Size      = new Size(300, 18)
            };
            pnlFooter.Controls.Add(lblVersion);

            Controls.AddRange(new Control[] { pnlBody, pnlHeader, pnlFooter });

            pnlHeader.ResumeLayout(false);
            pnlBody.ResumeLayout(false);
            pnlCards.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        /// <summary>
        /// Büyük navigasyon kart butonu oluşturur.
        /// </summary>
        private static Button MakeNavCard(
            string emoji, string title, string subtitle,
            Color accent, Color hoverColor)
        {
            var btn = new Button
            {
                Size      = new Size(512, 96),
                BackColor = AppTheme.BgSurface,
                ForeColor = AppTheme.TextPrimary,
                FlatStyle = FlatStyle.Flat,
                Cursor    = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleLeft,
                Text      = $"  {emoji}   {title}\n       {subtitle}",
                Font      = new Font("Segoe UI", 11f, FontStyle.Bold),
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize         = 0;
            btn.FlatAppearance.BorderColor        = accent;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Min(AppTheme.BgSurface.R + 14, 255),
                Math.Min(AppTheme.BgSurface.G + 14, 255),
                Math.Min(AppTheme.BgSurface.B + 24, 255));

            // Sol kenar şerit (accent rengi)
            btn.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Sol accent çubuğu
                using var accentBrush = new SolidBrush(accent);
                g.FillRectangle(accentBrush, new Rectangle(0, 0, 5, btn.Height));

                // Emoji + başlık
                using var titleFont    = new Font("Segoe UI", 13f, FontStyle.Bold);
                using var subFont      = new Font("Segoe UI", 9f, FontStyle.Regular);
                using var titleBrush   = new SolidBrush(AppTheme.TextPrimary);
                using var subBrush     = new SolidBrush(AppTheme.TextSecondary);
                using var accentBrush2 = new SolidBrush(accent);

                string emojiText = emoji;
                string titleText = title;
                string subText   = subtitle;

                g.DrawString(emojiText, new Font("Segoe UI", 22f), accentBrush2, new PointF(22, 20));
                g.DrawString(titleText, titleFont,  titleBrush, new PointF(80, 22));
                g.DrawString(subText,   subFont,    subBrush,   new PointF(80, 48));

                // Sağ ok oku
                using var arrowBrush = new SolidBrush(AppTheme.TextMuted);
                g.DrawString("›", new Font("Segoe UI", 26f, FontStyle.Bold), arrowBrush,
                    new PointF(btn.Width - 36, 28));

                // Alt ince çizgi
                using var linePen = new Pen(AppTheme.BgOverlay, 1f);
                g.DrawLine(linePen, 5, btn.Height - 1, btn.Width, btn.Height - 1);
            };

            // Text'i gizle, Paint ile çiziyoruz
            btn.Text = "";
            return btn;
        }

        // ── Alan değişkenleri ─────────────────────────────────────────────
        private Panel  pnlHeader;
        private Panel  pnlBody;
        private Panel  pnlCards;
        private Panel  pnlFooter;
        private Label  lblAppLogo;
        private Label  lblWelcome;
        private Label  lblDate;
        private Button btnTasks;
        private Button btnHabits;
        private Button btnFocus;
        private Button btnLogout;
    }
}
