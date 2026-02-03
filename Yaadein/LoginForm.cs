using System;
using System.Windows.Forms;
using Yaadein.Data;

namespace Yaadein
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter your username.", "Login Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter your password.", "Password Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (DatabaseHelper.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text))
            {
                MessageBox.Show($"Welcome back, {DatabaseHelper.CurrentUserName}!", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainDashboard dashboard = new MainDashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password.\n\nPlease check your credentials and try again.\n\nIf you don't have an account, click 'Create New Account'.",
                    "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            if (registerForm.ShowDialog() == DialogResult.OK)
            {
                txtUsername.Clear();
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}