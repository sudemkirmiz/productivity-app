using ProductivityApp.Helpers;

namespace ProductivityApp.Forms
{
    partial class HabitForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader    = new Panel();
            lblFormTitle = new Label();
            lblSubtitle  = new Label();

            pnlInputRow  = new Panel();
            lblHabitName = AppTheme.MakeLabel("YENİ ALIŞKANLIK", AppTheme.TextMuted, new Font("Segoe UI", 7.5f, FontStyle.Bold));
            txtHabitName = new TextBox();
            btnAdd       = AppTheme.MakeButton("➕  Ekle",     AppTheme.AccentGreen,  AppTheme.HoverGreen,  120, 38);
            btnDelete    = AppTheme.MakeButton("🗑  Sil",      AppTheme.AccentRed,    AppTheme.HoverRed,    110, 38);
            btnStreak    = AppTheme.MakeButton("🔥  +1 Seri", AppTheme.AccentOrange, AppTheme.HoverOrange, 120, 38);

            pnlStats    = new Panel();
            lblStatLine = new Label();

            pnlGrid     = new Panel();
            dgvHabits   = new DataGridView();
            lblSelected = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvHabits).BeginInit();
            pnlHeader.SuspendLayout();
            pnlInputRow.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlGrid.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────
            ClientSize      = new Size(900, 600);
            Text            = "Alışkanlık Takibi";
            StartPosition   = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            BackColor       = AppTheme.BgBase;
            Font            = AppTheme.FontBody;
            MinimumSize     = new Size(900, 600);
            Load           += HabitForm_Load;

            // ── pnlHeader ─────────────────────────────────────────────────
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 68;
            pnlHeader.BackColor = AppTheme.BgDeep;

            btnBack = AppTheme.MakeButton("← Geri", AppTheme.BgDeep, AppTheme.BgOverlay, 80, 36);
            btnBack.Location = new Point(16, 16);
            btnBack.Click += btnBack_Click;

            lblFormTitle.Text      = "🔥  Alışkanlık Takibi";
            lblFormTitle.Font      = AppTheme.FontH2;
            lblFormTitle.ForeColor = AppTheme.AccentGreen;
            lblFormTitle.Location  = new Point(110, 12);
            lblFormTitle.Size      = new Size(340, 28);

            lblSubtitle.Text      = "Günlük alışkanlıklarını oluştur ve serini kır";
            lblSubtitle.Font      = AppTheme.FontSmall;
            lblSubtitle.ForeColor = AppTheme.TextMuted;
            lblSubtitle.Location  = new Point(110, 40);
            lblSubtitle.Size      = new Size(340, 18);

            pnlHeader.Controls.AddRange(new Control[] { btnBack, lblFormTitle, lblSubtitle });

            // ── pnlInputRow (giriş satırı) ────────────────────────────────
            pnlInputRow.Dock      = DockStyle.Top;
            pnlInputRow.Height    = 88;
            pnlInputRow.BackColor = AppTheme.BgSurface;
            pnlInputRow.Padding   = new Padding(24, 0, 24, 0);

            lblHabitName.Location = new Point(24, 16);
            lblHabitName.Size     = new Size(200, 16);

            txtHabitName.Location    = new Point(24, 36);
            txtHabitName.Size        = new Size(260, 28);
            AppTheme.StyleTextBox(txtHabitName);

            btnAdd.Location    = new Point(300, 33);
            btnDelete.Location = new Point(428, 33);
            btnStreak.Location = new Point(546, 33);

            btnAdd.Click    += btnAdd_Click;
            btnDelete.Click += btnDelete_Click;
            btnStreak.Click += btnStreak_Click;

            pnlInputRow.Controls.AddRange(new Control[]
                { lblHabitName, txtHabitName, btnAdd, btnDelete, btnStreak });

            // ── pnlStats (özet istatistik şeridi) ─────────────────────────
            pnlStats.Dock      = DockStyle.Top;
            pnlStats.Height    = 50;
            pnlStats.BackColor = AppTheme.BgBase;
            pnlStats.Padding   = new Padding(24, 0, 24, 0);

            lblStatLine.Text      = "📊  Alışkanlıklarınız aşağıda listelenmektedir. Bir satıra tıklayın ve +1 Seri butonuyla bugünkü serinizi artırın.";
            lblStatLine.Font      = AppTheme.FontSmall;
            lblStatLine.ForeColor = AppTheme.TextSecondary;
            lblStatLine.Location  = new Point(24, 16);
            lblStatLine.Size      = new Size(640, 18);

            pnlStats.Controls.Add(lblStatLine);

            // ── pnlGrid ───────────────────────────────────────────────────
            pnlGrid.Dock      = DockStyle.Fill;
            pnlGrid.BackColor = AppTheme.BgBase;
            pnlGrid.Padding   = new Padding(24, 10, 24, 38);

            dgvHabits.Dock    = DockStyle.Fill;
            AppTheme.StyleGrid(dgvHabits, AppTheme.AccentGreen);
            dgvHabits.CellClick       += dgvHabits_CellClick;
            dgvHabits.CellFormatting  += dgvHabits_CellFormatting;

            lblSelected.Text      = "↑  Bir alışkanlığa tıklayın";
            lblSelected.Font      = AppTheme.FontSmall;
            lblSelected.ForeColor = AppTheme.TextMuted;
            lblSelected.Dock      = DockStyle.Bottom;
            lblSelected.Height    = 22;
            lblSelected.TextAlign = ContentAlignment.MiddleLeft;

            pnlGrid.Controls.AddRange(new Control[] { dgvHabits, lblSelected });

            // ── Çizgi ayırıcı ─────────────────────────────────────────────
            var sep1 = new Panel { Dock = DockStyle.Top, Height = 1, BackColor = AppTheme.BgOverlay };
            var sep2 = new Panel { Dock = DockStyle.Top, Height = 1, BackColor = AppTheme.BgOverlay };

            Controls.AddRange(new Control[] { pnlGrid, sep2, pnlStats, sep1, pnlInputRow, pnlHeader });

            pnlHeader.ResumeLayout(false);
            pnlInputRow.ResumeLayout(false);
            pnlStats.ResumeLayout(false);
            pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHabits).EndInit();
            ResumeLayout(false);
        }

        // ── StreakDays hücresini renklendir ───────────────────────────────
        private void dgvHabits_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHabits.Columns[e.ColumnIndex].Name != "colStreak") return;
            if (e.Value is int streak)
            {
                e.CellStyle.ForeColor = streak >= 7
                    ? AppTheme.AccentOrange
                    : streak >= 3
                        ? AppTheme.AccentGreen
                        : AppTheme.TextSecondary;
                e.CellStyle.Font = streak >= 3
                    ? new Font("Segoe UI", 10f, FontStyle.Bold)
                    : AppTheme.FontBody;
            }
        }

        // ── Alan değişkenleri ─────────────────────────────────────────────
        private Panel        pnlHeader;
        private Panel        pnlInputRow;
        private Panel        pnlStats;
        private Panel        pnlGrid;
        private Label        lblFormTitle;
        private Label        lblSubtitle;
        private Label        lblHabitName;
        private Label        lblStatLine;
        private Label        lblSelected;
        private TextBox      txtHabitName;
        private Button       btnAdd;
        private Button       btnDelete;
        private Button       btnStreak;
        private Button       btnBack;
        private DataGridView dgvHabits;
    }
}
