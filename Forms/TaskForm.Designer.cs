using ProductivityApp.Helpers;

namespace ProductivityApp.Forms
{
    partial class TaskForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ── Header ────────────────────────────────────────────────────
            pnlHeader    = new Panel();
            lblFormTitle = new Label();
            lblSubtitle  = new Label();

            // ── Sol panel (form giriş alanları) ───────────────────────────
            pnlLeft      = new Panel();
            lblTaskTitle = AppTheme.MakeLabel("GÖREV BAŞLIĞI", AppTheme.TextMuted, new Font("Segoe UI", 7.5f, FontStyle.Bold));
            txtTitle     = new TextBox();
            lblPriority  = AppTheme.MakeLabel("ÖNCELİK",      AppTheme.TextMuted, new Font("Segoe UI", 7.5f, FontStyle.Bold));
            cmbPriority  = new ComboBox();
            lblStatus    = AppTheme.MakeLabel("DURUM",         AppTheme.TextMuted, new Font("Segoe UI", 7.5f, FontStyle.Bold));
            cmbStatus    = new ComboBox();
            lblDueDate   = AppTheme.MakeLabel("SON TARİH",     AppTheme.TextMuted, new Font("Segoe UI", 7.5f, FontStyle.Bold));
            dtpDueDate   = new DateTimePicker();
            btnAdd       = AppTheme.MakeButton("➕  Görev Ekle", AppTheme.AccentGreen,  AppTheme.HoverGreen,  220, 40);
            btnDelete    = AppTheme.MakeButton("🗑  Seçili Sil",  AppTheme.AccentRed,    AppTheme.HoverRed,    220, 40);

            // ── Sağ panel (DataGridView) ───────────────────────────────────
            pnlRight     = new Panel();
            dgvTasks     = new DataGridView();
            lblSelected  = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
            pnlHeader.SuspendLayout();
            pnlLeft.SuspendLayout();
            pnlRight.SuspendLayout();
            SuspendLayout();

            // ── Form ──────────────────────────────────────────────────────
            ClientSize      = new Size(900, 600);
            Text            = "Görev Yönetimi";
            StartPosition   = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox     = false;
            BackColor       = AppTheme.BgBase;
            Font            = AppTheme.FontBody;
            MinimumSize     = new Size(900, 600);
            Load           += TaskForm_Load;

            // ── pnlHeader ─────────────────────────────────────────────────
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 68;
            pnlHeader.BackColor = AppTheme.BgDeep;
            pnlHeader.Padding   = new Padding(24, 0, 0, 0);

            btnBack = AppTheme.MakeButton("← Geri", AppTheme.BgDeep, AppTheme.BgOverlay, 80, 36);
            btnBack.Location = new Point(16, 16);
            btnBack.Click += btnBack_Click;

            lblFormTitle.Text      = "📋  Görev Yönetimi";
            lblFormTitle.Font      = AppTheme.FontH2;
            lblFormTitle.ForeColor = AppTheme.AccentBlue;
            lblFormTitle.Location  = new Point(110, 12);
            lblFormTitle.Size      = new Size(320, 28);

            lblSubtitle.Text      = "Görev ekle, önceliklendir ve takip et";
            lblSubtitle.Font      = AppTheme.FontSmall;
            lblSubtitle.ForeColor = AppTheme.TextMuted;
            lblSubtitle.Location  = new Point(110, 40);
            lblSubtitle.Size      = new Size(320, 18);

            pnlHeader.Controls.AddRange(new Control[] { btnBack, lblFormTitle, lblSubtitle });

            // ── pnlLeft (270px sabit genişlik, sol taraf) ─────────────────
            pnlLeft.Dock      = DockStyle.Left;
            pnlLeft.Width     = 270;
            pnlLeft.BackColor = AppTheme.BgSurface;
            pnlLeft.Padding   = new Padding(20, 20, 20, 20);

            // Input'lar sol panelin iç koordinatlarında konumlandırılır
            lblTaskTitle.Location = new Point(20, 24);
            txtTitle.Location     = new Point(20, 44);
            txtTitle.Size         = new Size(230, 28);
            AppTheme.StyleTextBox(txtTitle);

            lblPriority.Location  = new Point(20, 90);
            cmbPriority.Location  = new Point(20, 110);
            cmbPriority.Size      = new Size(230, 28);
            AppTheme.StyleComboBox(cmbPriority, AppTheme.AccentBlue);
            cmbPriority.SelectedIndexChanged += cmbPriority_SelectedIndexChanged;

            lblStatus.Location    = new Point(20, 156);
            cmbStatus.Location    = new Point(20, 176);
            cmbStatus.Size        = new Size(230, 28);
            AppTheme.StyleComboBox(cmbStatus, AppTheme.BgOverlay);
            cmbStatus.ForeColor   = AppTheme.TextPrimary;

            lblDueDate.Location   = new Point(20, 222);
            dtpDueDate.Location   = new Point(20, 242);
            dtpDueDate.Size       = new Size(230, 28);
            dtpDueDate.Format     = DateTimePickerFormat.Short;
            dtpDueDate.BackColor  = AppTheme.BgOverlay;
            dtpDueDate.ForeColor  = AppTheme.TextPrimary;
            dtpDueDate.Font       = AppTheme.FontBody;
            dtpDueDate.CalendarMonthBackground = AppTheme.BgSurface;
            dtpDueDate.CalendarForeColor       = AppTheme.TextPrimary;
            dtpDueDate.CalendarTitleBackColor  = AppTheme.BgDeep;

            btnAdd.Location    = new Point(20, 298);
            btnAdd.Size        = new Size(230, 40);
            btnAdd.Click      += btnAdd_Click;

            btnDelete.Location = new Point(20, 350);
            btnDelete.Size     = new Size(230, 40);
            btnDelete.Click   += btnDelete_Click;

            // Separator line
            var pnlSep = new Panel
            {
                BackColor = AppTheme.BgOverlay,
                Location  = new Point(20, 282),
                Size      = new Size(230, 1)
            };

            pnlLeft.Controls.AddRange(new Control[]
            {
                lblTaskTitle, txtTitle,
                lblPriority,  cmbPriority,
                lblStatus,    cmbStatus,
                lblDueDate,   dtpDueDate,
                pnlSep, btnAdd, btnDelete
            });

            // ── pnlRight (geriye kalan genişlik) ──────────────────────────
            pnlRight.Dock      = DockStyle.Fill;
            pnlRight.BackColor = AppTheme.BgBase;
            pnlRight.Padding   = new Padding(16, 16, 16, 40);

            // Grid başlık etiketi
            var lblGridTitle = new Label
            {
                Text      = "GÖREV LİSTESİ",
                Font      = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                ForeColor = AppTheme.TextMuted,
                Location  = new Point(16, 16),
                Size      = new Size(200, 16)
            };

            dgvTasks.Location = new Point(16, 40);
            dgvTasks.Anchor   = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTasks.Size     = new Size(588, 448);
            AppTheme.StyleGrid(dgvTasks, AppTheme.AccentBlue);
            dgvTasks.CellClick += dgvTasks_CellClick;

            lblSelected.Text      = "↑  Satıra tıklayarak seçin";
            lblSelected.Font      = AppTheme.FontSmall;
            lblSelected.ForeColor = AppTheme.TextMuted;
            lblSelected.Location  = new Point(16, 496);
            lblSelected.Size      = new Size(580, 18);
            lblSelected.Anchor    = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            pnlRight.Controls.AddRange(new Control[] { lblGridTitle, dgvTasks, lblSelected });

            // ── Splitter (sol-sağ ayırıcı) ────────────────────────────────
            var splitter = new Splitter { Dock = DockStyle.Left, Width = 1, BackColor = AppTheme.BgOverlay };

            Controls.AddRange(new Control[] { pnlRight, splitter, pnlLeft, pnlHeader });

            pnlHeader.ResumeLayout(false);
            pnlLeft.ResumeLayout(false);
            pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
            ResumeLayout(false);
        }

        // ── Alan değişkenleri ─────────────────────────────────────────────
        private Panel            pnlHeader;
        private Panel            pnlLeft;
        private Panel            pnlRight;
        private Label            lblFormTitle;
        private Label            lblSubtitle;
        private Label            lblTaskTitle;
        private Label            lblPriority;
        private Label            lblStatus;
        private Label            lblDueDate;
        private Label            lblSelected;
        private TextBox          txtTitle;
        private ComboBox         cmbPriority;
        private ComboBox         cmbStatus;
        private DateTimePicker   dtpDueDate;
        private Button           btnAdd;
        private Button           btnDelete;
        private Button           btnBack;
        private DataGridView     dgvTasks;
    }
}
