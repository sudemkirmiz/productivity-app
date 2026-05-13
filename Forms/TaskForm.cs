using ProductivityApp.Database;
using ProductivityApp.Models;

namespace ProductivityApp.Forms
{
    public partial class TaskForm : Form
    {
        // Bellekteki görev listesi
        private List<TaskItem> _tasks = new List<TaskItem>();

        // Seçili satırın Id'si (silme için)
        private int _selectedTaskId = -1;

        public TaskForm()
        {
            InitializeComponent();
        }

        // ── Form Load ──────────────────────────────────────────────────────
        private void TaskForm_Load(object sender, EventArgs e)
        {
            // ComboBox'ları doldur
            cmbPriority.Items.AddRange(new[] { "Düşük", "Normal", "Yüksek" });
            cmbPriority.SelectedIndex = 1; // Normal

            cmbStatus.Items.AddRange(new[] { "Bekliyor", "Devam Ediyor", "Tamamlandı" });
            cmbStatus.SelectedIndex = 0;

            dtpDueDate.Value = DateTime.Today;

            // Sütunları hazırla
            SetupGrid();

            // Örnek görev ekle (liste boşken)
            _tasks.Add(new TaskItem
            {
                Id       = 1,
                Title    = "Rapor yaz",
                Priority = "Yüksek",
                Status   = "Bekliyor",
                DueDate  = DateTime.Today.AddDays(2)
            });
            RefreshGrid();
        }

        // ── Grid sütun tanımları ───────────────────────────────────────────
        private void SetupGrid()
        {
            dgvTasks.Columns.Clear();
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colId",       HeaderText = "ID",       DataPropertyName = "Id",       Width = 40  });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colTitle",    HeaderText = "Görev",    DataPropertyName = "Title",    Width = 180 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colPriority", HeaderText = "Öncelik",  DataPropertyName = "Priority", Width = 80  });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colStatus",   HeaderText = "Durum",    DataPropertyName = "Status",   Width = 110 });
            dgvTasks.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colDueDate",  HeaderText = "Son Tarih",DataPropertyName = "DueDate",  Width = 100 });
        }

        // ── Grid yenile ────────────────────────────────────────────────────
        private void RefreshGrid()
        {
            dgvTasks.DataSource = null;
            dgvTasks.DataSource = new BindingSource(_tasks, null);
        }

        // ── Görev Ekle ─────────────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Görev başlığı boş bırakılamaz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var task = new TaskItem
            {
                Id       = _tasks.Count > 0 ? _tasks[^1].Id + 1 : 1,
                Title    = txtTitle.Text.Trim(),
                Priority = cmbPriority.SelectedItem?.ToString() ?? "Normal",
                Status   = cmbStatus.SelectedItem?.ToString()   ?? "Bekliyor",
                DueDate  = dtpDueDate.Value.Date,
                CreatedAt= DateTime.Now
            };

            _tasks.Add(task);

            // Veritabanına da kaydet
            DatabaseHelper.InsertTask(task);

            RefreshGrid();
            txtTitle.Clear();
            txtTitle.Focus();
        }

        // ── Görev Sil ──────────────────────────────────────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedTaskId < 0)
            {
                MessageBox.Show("Lütfen silinecek görevi seçin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _tasks.RemoveAll(t => t.Id == _selectedTaskId);
            DatabaseHelper.DeleteTask(_selectedTaskId);

            _selectedTaskId = -1;
            RefreshGrid();
        }

        // ── DataGridView CellClick (seçim) ────────────────────────────────
        private void dgvTasks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTasks.Rows[e.RowIndex];
            if (row.DataBoundItem is TaskItem task)
            {
                _selectedTaskId  = task.Id;
                lblSelected.Text = $"Seçili: [{task.Id}] {task.Title}";
            }
        }

        // ── ComboBox SelectedIndexChanged örneği ──────────────────────────
        private void cmbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Önceliğe göre border rengini değiştir (görsel geri bildirim)
            string selected = cmbPriority.SelectedItem?.ToString() ?? "";
            cmbPriority.BackColor = selected switch
            {
                "Yüksek" => Color.FromArgb(243, 139, 168),  // kırmızımsı
                "Düşük"  => Color.FromArgb(166, 227, 161),  // yeşil
                _        => Color.FromArgb(137, 180, 250)   // mavi (Normal)
            };
        }

        // ── Geri Dön Butonu ────────────────────────────────────────────────
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
