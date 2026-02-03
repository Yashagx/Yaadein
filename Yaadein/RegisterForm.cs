using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Yaadein.Data;

namespace Yaadein
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
            UpdatePasswordStrength();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            if (DatabaseHelper.UsernameExists(txtUsername.Text.Trim()))
            {
                MessageBox.Show("This username is already taken. Please choose a different username.",
                    "Username Exists",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }

            // Default to patient (true) - change to false if you want default as caregiver
            bool isPatient = true;

            bool success = DatabaseHelper.RegisterUser(
                txtUsername.Text.Trim(),
                txtPassword.Text,
                txtFullName.Text.Trim(),
                txtEmail.Text.Trim(),
                isPatient
            );

            if (success)
            {
                MessageBox.Show(
                    $"Welcome to Yaadein, {txtFullName.Text.Trim()}!\n\n" +
                    $"Your account has been created successfully.\n" +
                    $"You can now login with your credentials.\n\n" +
                    $"Username: {txtUsername.Text.Trim()}",
                    "Registration Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "An error occurred while creating your account.\n\nPlease try again.",
                    "Registration Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            UpdatePasswordStrength();
        }

        private void UpdatePasswordStrength()
        {
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(password))
            {
                lblPasswordStrength.Text = "Password strength: ";
                lblPasswordStrength.ForeColor = Color.Gray;
                return;
            }

            int strength = 0;
            if (password.Length >= 8) strength++;
            if (password.Length >= 12) strength++;
            if (Regex.IsMatch(password, @"[a-z]") && Regex.IsMatch(password, @"[A-Z]")) strength++;
            if (Regex.IsMatch(password, @"\d")) strength++;
            if (Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>?/]")) strength++;

            string strengthText;
            Color strengthColor;

            if (strength <= 1)
            {
                strengthText = "Weak";
                strengthColor = Color.Red;
            }
            else if (strength <= 3)
            {
                strengthText = "Medium";
                strengthColor = Color.Orange;
            }
            else
            {
                strengthText = "Strong";
                strengthColor = Color.Green;
            }

            lblPasswordStrength.Text = $"Password strength: {strengthText}";
            lblPasswordStrength.ForeColor = strengthColor;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Trim().Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Please confirm your password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Please enter your full name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (txtFullName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Please enter a valid full name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter your email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}