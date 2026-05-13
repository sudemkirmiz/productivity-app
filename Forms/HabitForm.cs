using ProductivityApp.Database;
using ProductivityApp.Models;

namespace ProductivityApp.Forms
{
    public partial class HabitForm : Form
    {
        private List<Habit> _habits = new List<Habit>();
        private int _selectedHabitId = -1;

        public HabitForm()
        {
            InitializeComponent();
        }

        // ── Form Load ──────────────────────────────────────────────────────
        private void HabitForm_Load(object sender, EventArgs e)
        {
            SetupGrid();

            // Örnek alışkanlıklar
            _habits.Add(new Habit { Id = 1, Name = "Sabah egzersizi", StreakDays = 5 });
            _habits.Add(new Habit { Id = 2, Name = "Kitap okuma",      StreakDays = 12 });
            RefreshGrid();
        }

        // ── Grid sütunları ─────────────────────────────────────────────────
        private void SetupGrid()
        {
            dgvHabits.Columns.Clear();
            dgvHabits.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colId",     HeaderText = "ID",          DataPropertyName = "Id",         Width = 40  });
            dgvHabits.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colName",   HeaderText = "Alışkanlık",  DataPropertyName = "Name",       Width = 220 });
            dgvHabits.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colStreak", HeaderText = "🔥 Seri (gün)",DataPropertyName = "StreakDays", Width = 100 });
            dgvHabits.Columns.Add(new DataGridViewTextBoxColumn
                { Name = "colLast",   HeaderText = "Son Kontrol", DataPropertyName = "LastChecked",Width = 120 });
        }

        // ── Grid yenile ────────────────────────────────────────────────────
        private void RefreshGrid()
        {
            dgvHabits.DataSource = null;
            dgvHabits.DataSource = new BindingSource(_habits, null);
        }

        // ── Alışkanlık Ekle ────────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHabitName.Text))
            {
                MessageBox.Show("Alışkanlık adı boş bırakılamaz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var habit = new Habit
            {
                Id          = _habits.Count > 0 ? _habits[^1].Id + 1 : 1,
                Name        = txtHabitName.Text.Trim(),
                StreakDays  = 0,
                LastChecked = DateTime.Today,
                CreatedAt   = DateTime.Now
            };

            _habits.Add(habit);
            DatabaseHelper.InsertHabit(habit);

            RefreshGrid();
            txtHabitName.Clear();
            txtHabitName.Focus();
        }

        // ── Alışkanlık Sil ────────────────────────────────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedHabitId < 0)
            {
                MessageBox.Show("Lütfen silinecek alışkanlığı seçin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _habits.RemoveAll(h => h.Id == _selectedHabitId);
            DatabaseHelper.DeleteHabit(_selectedHabitId);

            _selectedHabitId = -1;
            lblSelected.Text = "Bir alışkanlık seçin...";
            RefreshGrid();
        }

        // ── Seriyi artır butonu ────────────────────────────────────────────
        private void btnStreak_Click(object sender, EventArgs e)
        {
            if (_selectedHabitId < 0)
            {
                MessageBox.Show("Lütfen bir alışkanlık seçin.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var habit = _habits.Find(h => h.Id == _selectedHabitId);
            if (habit != null)
            {
                habit.StreakDays++;
                habit.LastChecked = DateTime.Today;
            }

            RefreshGrid();
        }

        // ── DataGridView CellClick ─────────────────────────────────────────
        private void dgvHabits_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvHabits.Rows[e.RowIndex].DataBoundItem is Habit habit)
            {
                _selectedHabitId = habit.Id;
                lblSelected.Text = $"Seçili: [{habit.Id}] {habit.Name}  🔥 {habit.StreakDays} gün";
            }
        }

        // ── Geri Dön Butonu ────────────────────────────────────────────────
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
