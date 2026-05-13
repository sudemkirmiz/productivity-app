namespace ProductivityApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly string _username;

        public MainForm(string username)
        {
            _username = username;
            InitializeComponent();
        }

        // ── Form Load ──────────────────────────────────────────────────────
        private void MainForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Hoş geldin, {_username}! 👋";
        }

        // ── Görevler butonu ────────────────────────────────────────────────
        private void btnTasks_Click(object sender, EventArgs e)
        {
            new TaskForm().ShowDialog();
        }

        // ── Alışkanlıklar butonu ───────────────────────────────────────────
        private void btnHabits_Click(object sender, EventArgs e)
        {
            new HabitForm().ShowDialog();
        }

        // ── Odak butonu ────────────────────────────────────────────────────
        private void btnFocus_Click(object sender, EventArgs e)
        {
            new FocusForm().ShowDialog();
        }

        // ── Çıkış butonu ───────────────────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // LoginForm'u tekrar göster, bu formu kapat
            foreach (Form f in Application.OpenForms)
            {
                if (f is LoginForm login)
                {
                    login.Show();
                    break;
                }
            }
            this.Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            // Açık başka form yoksa uygulamayı kapat
            if (Application.OpenForms.Count == 0)
                Application.Exit();
        }
    }
}
