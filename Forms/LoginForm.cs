using ProductivityApp.Database;

namespace ProductivityApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // ── Form Load ──────────────────────────────────────────────────────
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Geliştirme kolaylığı: alanları doldur
            txtUsername.Text = "admin";
            txtPassword.Text = "1234";
            txtUsername.Focus();
        }

        // ── Giriş Butonu ──────────────────────────────────────────────────
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseHelper.ValidateUser(username, password))
            {
                var mainForm = new MainForm(username);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı.",
                    "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        // ── Enter tuşuyla giriş ────────────────────────────────────────────
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }
    }
}
