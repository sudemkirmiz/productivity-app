namespace ProductivityApp.Helpers
{
    /// <summary>
    /// Uygulama genelinde kullanılan renk ve font sabitleri.
    /// Tüm formlar bu sınıftan renk alır — tek yerden değiştirilebilir.
    /// </summary>
    public static class AppTheme
    {
        // ── Arka plan renkleri ────────────────────────────────────────────
        public static readonly Color BgDeep    = Color.FromArgb(15,  15,  26);   // en koyu zemin
        public static readonly Color BgBase    = Color.FromArgb(24,  24,  37);   // form zemini
        public static readonly Color BgSurface = Color.FromArgb(36,  37,  54);   // kart/panel
        public static readonly Color BgOverlay = Color.FromArgb(49,  50,  68);   // input alanları

        // ── Yazı renkleri ──────────────────────────────────────────────────
        public static readonly Color TextPrimary   = Color.FromArgb(205, 214, 244); // ana metin
        public static readonly Color TextSecondary = Color.FromArgb(147, 153, 178); // ikincil metin
        public static readonly Color TextMuted     = Color.FromArgb(108, 112, 134); // soluk metin

        // ── Vurgu renkleri ─────────────────────────────────────────────────
        public static readonly Color AccentBlue   = Color.FromArgb(137, 180, 250); // mavi (görevler)
        public static readonly Color AccentPurple = Color.FromArgb(203, 166, 247); // mor (başlık)
        public static readonly Color AccentGreen  = Color.FromArgb(166, 227, 161); // yeşil (alışkanlık)
        public static readonly Color AccentOrange = Color.FromArgb(250, 179, 135); // turuncu (odak)
        public static readonly Color AccentRed    = Color.FromArgb(243, 139, 168); // kırmızı (sil/hata)
        public static readonly Color AccentTeal   = Color.FromArgb(148, 226, 213); // teal (vurgu)

        // ── Hover renkleri (biraz daha açık) ──────────────────────────────
        public static readonly Color HoverBlue   = Color.FromArgb(168, 199, 251);
        public static readonly Color HoverGreen  = Color.FromArgb(191, 237, 186);
        public static readonly Color HoverOrange = Color.FromArgb(252, 200, 165);
        public static readonly Color HoverRed    = Color.FromArgb(246, 165, 186);
        public static readonly Color HoverGray   = Color.FromArgb(108, 112, 134);

        // ── Fontlar ────────────────────────────────────────────────────────
        public static readonly Font FontTitle   = new Font("Segoe UI", 18f, FontStyle.Bold);
        public static readonly Font FontH2      = new Font("Segoe UI", 13f, FontStyle.Bold);
        public static readonly Font FontBody    = new Font("Segoe UI", 10f, FontStyle.Regular);
        public static readonly Font FontSmall   = new Font("Segoe UI",  8.5f, FontStyle.Regular);
        public static readonly Font FontBold    = new Font("Segoe UI", 10f, FontStyle.Bold);
        public static readonly Font FontTimer   = new Font("Segoe UI Semibold", 54f, FontStyle.Bold);
        public static readonly Font FontGridHdr = new Font("Segoe UI", 9.5f, FontStyle.Bold);

        // ── Ortak DataGridView stili uygulama ─────────────────────────────
        public static void StyleGrid(DataGridView dgv, Color selectionColor)
        {
            dgv.BackgroundColor = BgBase;
            dgv.GridColor       = BgOverlay;
            dgv.BorderStyle     = BorderStyle.None;

            dgv.DefaultCellStyle.BackColor          = BgSurface;
            dgv.DefaultCellStyle.ForeColor          = TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = selectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = BgDeep;
            dgv.DefaultCellStyle.Font               = FontBody;
            dgv.DefaultCellStyle.Padding            = new Padding(6, 4, 6, 4);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = BgOverlay;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextPrimary;

            dgv.ColumnHeadersDefaultCellStyle.BackColor  = BgDeep;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor  = selectionColor;
            dgv.ColumnHeadersDefaultCellStyle.Font       = FontGridHdr;
            dgv.ColumnHeadersDefaultCellStyle.Padding    = new Padding(8, 0, 0, 0);
            dgv.ColumnHeadersBorderStyle                 = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeight                      = 36;
            dgv.EnableHeadersVisualStyles               = false;

            dgv.RowTemplate.Height  = 34;
            dgv.RowHeadersVisible   = false;
            dgv.AllowUserToAddRows  = false;
            dgv.ReadOnly            = true;
            dgv.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.CellBorderStyle     = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        // ── Ortak buton stili ─────────────────────────────────────────────
        public static Button MakeButton(
            string text, Color backColor, Color hoverColor,
            int width = 130, int height = 40, Font? font = null)
        {
            var btn = new Button
            {
                Text      = text,
                BackColor = backColor,
                ForeColor = BgDeep,
                FlatStyle = FlatStyle.Flat,
                Font      = font ?? FontBold,
                Cursor    = Cursors.Hand,
                Size      = new Size(width, height),
                TextAlign = ContentAlignment.MiddleCenter,
                UseVisualStyleBackColor = false
            };
            btn.FlatAppearance.BorderSize    = 0;
            btn.FlatAppearance.MouseOverBackColor  = hoverColor;
            btn.FlatAppearance.MouseDownBackColor  = backColor;
            return btn;
        }

        // ── TextBox stili ─────────────────────────────────────────────────
        public static void StyleTextBox(TextBox tb)
        {
            tb.BackColor   = BgOverlay;
            tb.ForeColor   = TextPrimary;
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.Font        = FontBody;
        }

        // ── ComboBox stili ────────────────────────────────────────────────
        public static void StyleComboBox(ComboBox cmb, Color accent)
        {
            cmb.BackColor     = accent;
            cmb.ForeColor     = BgDeep;
            cmb.FlatStyle     = FlatStyle.Flat;
            cmb.Font          = FontBold;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ── Label factory ─────────────────────────────────────────────────
        public static Label MakeLabel(string text, Color? color = null, Font? font = null)
            => new Label
            {
                Text      = text,
                ForeColor = color ?? TextSecondary,
                Font      = font  ?? FontSmall,
                AutoSize  = true
            };
    }
}
