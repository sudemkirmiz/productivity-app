using ProductivityApp.Helpers;

namespace ProductivityApp.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Kontroller ────────────────────────────────────────────────
            pnlBackground = new Panel();
            pnlCard       = new Panel();
            lblAppName    = new Label();
            lblTagline    = new Label();
            lblDivider    = new Label();
            lblUsername   = new Label();
            txtUsername   = new TextBox();
            lblPassword   = new Label();
            txtPassword   = new TextBox();
            btnLogin      = AppTheme.MakeButton("  Giriş Yap", AppTheme.AccentBlue, AppTheme.HoverBlue, 280, 46, AppTheme.FontH2);
            lblHint       = new Label();

            pnlBackground.SuspendLayout();
            pnlCard.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────
            ClientSize             = new Size(480, 520);
            Text                   = "ProductivityApp — Giriş";
            StartPosition          = FormStartPosition.CenterScreen;
            FormBorderStyle        = FormBorderStyle.FixedSingle;
            MaximizeBox            = false;
            BackColor              = AppTheme.BgBase;
            Font                   = AppTheme.FontBody;
            Load                  += LoginForm_Load;

            // ── pnlBackground (gradient zemin) ───────────────────────────
            pnlBackground.Dock      = DockStyle.Fill;
            pnlBackground.BackColor = AppTheme.BgBase;
            pnlBackground.Paint    += (s, e) =>
            {
                using var brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    pnlBackground.ClientRectangle,
                    AppTheme.BgDeep,
                    Color.FromArgb(30, 30, 55),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, pnlBackground.ClientRectangle);
            };

            // ── pnlCard (ortalanmış login kutusu) ─────────────────────────
            pnlCard.BackColor  = AppTheme.BgSurface;
            pnlCard.Size       = new Size(360, 400);
            pnlCard.Location   = new Point(60, 58);

            // ── lblAppName ────────────────────────────────────────────────
            lblAppName.Text      = "🎯 ProductivityApp";
            lblAppName.Font      = AppTheme.FontTitle;
            lblAppName.ForeColor = AppTheme.AccentPurple;
            lblAppName.Location  = new Point(30, 30);
            lblAppName.Size      = new Size(300, 36);
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;

            // ── lblTagline ────────────────────────────────────────────────
            lblTagline.Text      = "Verimli günler, mutlu hayatlar";
            lblTagline.Font      = AppTheme.FontSmall;
            lblTagline.ForeColor = AppTheme.TextMuted;
            lblTagline.Location  = new Point(30, 70);
            lblTagline.Size      = new Size(300, 20);
            lblTagline.TextAlign = ContentAlignment.MiddleCenter;

            // ── Ayırıcı çizgi ─────────────────────────────────────────────
            lblDivider.BackColor = AppTheme.BgOverlay;
            lblDivider.Location  = new Point(30, 102);
            lblDivider.Size      = new Size(300, 1);

            // ── lblUsername + txtUsername ─────────────────────────────────
            lblUsername.Text      = "KULLANICI ADI";
            lblUsername.Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            lblUsername.ForeColor = AppTheme.TextMuted;
            lblUsername.Location  = new Point(40, 126);
            lblUsername.Size      = new Size(160, 16);

            txtUsername.Location    = new Point(40, 146);
            txtUsername.Size        = new Size(280, 28);
            AppTheme.StyleTextBox(txtUsername);
            txtUsername.Font        = AppTheme.FontBody;

            // ── lblPassword + txtPassword ─────────────────────────────────
            lblPassword.Text      = "ŞİFRE";
            lblPassword.Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold);
            lblPassword.ForeColor = AppTheme.TextMuted;
            lblPassword.Location  = new Point(40, 192);
            lblPassword.Size      = new Size(160, 16);

            txtPassword.Location     = new Point(40, 212);
            txtPassword.Size         = new Size(280, 28);
            txtPassword.PasswordChar = '●';
            AppTheme.StyleTextBox(txtPassword);
            txtPassword.Font         = AppTheme.FontBody;
            txtPassword.KeyDown     += txtPassword_KeyDown;

            // ── btnLogin ──────────────────────────────────────────────────
            btnLogin.Location = new Point(40, 268);
            btnLogin.Click   += btnLogin_Click;

            // ── lblHint ───────────────────────────────────────────────────
            lblHint.Text      = "Demo: admin / 1234";
            lblHint.Font      = AppTheme.FontSmall;
            lblHint.ForeColor = AppTheme.TextMuted;
            lblHint.Location  = new Point(40, 330);
            lblHint.Size      = new Size(280, 18);
            lblHint.TextAlign = ContentAlignment.MiddleCenter;

            // ── Karta ekle ────────────────────────────────────────────────
            pnlCard.Controls.AddRange(new Control[]
            {
                lblAppName, lblTagline, lblDivider,
                lblUsername, txtUsername,
                lblPassword, txtPassword,
                btnLogin, lblHint
            });

            pnlBackground.Controls.Add(pnlCard);
            Controls.Add(pnlBackground);

            pnlCard.ResumeLayout(false);
            pnlBackground.ResumeLayout(false);
            ResumeLayout(false);
        }

        // ── Alan değişkenleri ─────────────────────────────────────────────
        private Panel   pnlBackground;
        private Panel   pnlCard;
        private Label   lblAppName;
        private Label   lblTagline;
        private Label   lblDivider;
        private Label   lblUsername;
        private Label   lblPassword;
        private Label   lblHint;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button  btnLogin;
    }
}
