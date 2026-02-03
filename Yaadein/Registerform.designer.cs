namespace Yaadein
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblTagline = new System.Windows.Forms.Label();
            this.lblAppName = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.btnBackToLogin = new System.Windows.Forms.Button();
            this.lblHaveAccount = new System.Windows.Forms.Label();
            this.btnRegister = new System.Windows.Forms.Button();
            this.panelUserType = new System.Windows.Forms.Panel();
            this.rbCaregiver = new System.Windows.Forms.RadioButton();
            this.rbPatient = new System.Windows.Forms.RadioButton();
            this.lblUserType = new System.Windows.Forms.Label();
            this.dtpDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.lblDateOfBirth = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPasswordStrength = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblRegisterTitle = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelUserType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(203)))));
            this.panelLeft.Controls.Add(this.lblWelcome);
            this.panelLeft.Controls.Add(this.lblTagline);
            this.panelLeft.Controls.Add(this.lblAppName);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(400, 700);
            this.panelLeft.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(30, 400);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(340, 180);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Join our caring community.\r\n\r\nCreate your account to start\r\nyour journey with Yaadein -\r\nwhere memories are cherished\r\nand support is always present.";
            // 
            // lblTagline
            // 
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Italic);
            this.lblTagline.ForeColor = System.Drawing.Color.White;
            this.lblTagline.Location = new System.Drawing.Point(30, 320);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(340, 40);
            this.lblTagline.TabIndex = 1;
            this.lblTagline.Text = "Begin Together";
            // 
            // lblAppName
            // 
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 56F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(30, 200);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(340, 100);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "Yaadein";
            // 
            // panelRight
            // 
            this.panelRight.AutoScroll = true;
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.btnBackToLogin);
            this.panelRight.Controls.Add(this.lblHaveAccount);
            this.panelRight.Controls.Add(this.btnRegister);
            this.panelRight.Controls.Add(this.panelUserType);
            this.panelRight.Controls.Add(this.lblUserType);
            this.panelRight.Controls.Add(this.dtpDateOfBirth);
            this.panelRight.Controls.Add(this.lblDateOfBirth);
            this.panelRight.Controls.Add(this.txtPhone);
            this.panelRight.Controls.Add(this.lblPhone);
            this.panelRight.Controls.Add(this.txtEmail);
            this.panelRight.Controls.Add(this.lblEmail);
            this.panelRight.Controls.Add(this.txtFullName);
            this.panelRight.Controls.Add(this.lblFullName);
            this.panelRight.Controls.Add(this.lblPasswordStrength);
            this.panelRight.Controls.Add(this.txtConfirmPassword);
            this.panelRight.Controls.Add(this.lblConfirmPassword);
            this.panelRight.Controls.Add(this.txtPassword);
            this.panelRight.Controls.Add(this.lblPassword);
            this.panelRight.Controls.Add(this.txtUsername);
            this.panelRight.Controls.Add(this.lblUsername);
            this.panelRight.Controls.Add(this.lblRegisterTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(400, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(500, 700);
            this.panelRight.TabIndex = 1;
            // 
            // btnBackToLogin
            // 
            this.btnBackToLogin.BackColor = System.Drawing.Color.White;
            this.btnBackToLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackToLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(203)))));
            this.btnBackToLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToLogin.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBackToLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.btnBackToLogin.Location = new System.Drawing.Point(50, 1030);
            this.btnBackToLogin.Name = "btnBackToLogin";
            this.btnBackToLogin.Size = new System.Drawing.Size(400, 45);
            this.btnBackToLogin.TabIndex = 20;
            this.btnBackToLogin.Text = "Back to Login";
            this.btnBackToLogin.UseVisualStyleBackColor = false;
            this.btnBackToLogin.Click += new System.EventHandler(this.btnBackToLogin_Click);
            // 
            // lblHaveAccount
            // 
            this.lblHaveAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHaveAccount.ForeColor = System.Drawing.Color.Gray;
            this.lblHaveAccount.Location = new System.Drawing.Point(50, 1000);
            this.lblHaveAccount.Name = "lblHaveAccount";
            this.lblHaveAccount.Size = new System.Drawing.Size(400, 25);
            this.lblHaveAccount.TabIndex = 19;
            this.lblHaveAccount.Text = "Already have an account?";
            this.lblHaveAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(182)))), ((int)(((byte)(193)))));
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(50, 930);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(400, 55);
            this.btnRegister.TabIndex = 18;
            this.btnRegister.Text = "Create Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // panelUserType
            // 
            this.panelUserType.Controls.Add(this.rbCaregiver);
            this.panelUserType.Controls.Add(this.rbPatient);
            this.panelUserType.Location = new System.Drawing.Point(50, 850);
            this.panelUserType.Name = "panelUserType";
            this.panelUserType.Size = new System.Drawing.Size(400, 40);
            this.panelUserType.TabIndex = 17;
            // 
            // rbCaregiver
            // 
            this.rbCaregiver.AutoSize = true;
            this.rbCaregiver.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rbCaregiver.Location = new System.Drawing.Point(220, 8);
            this.rbCaregiver.Name = "rbCaregiver";
            this.rbCaregiver.Size = new System.Drawing.Size(151, 24);
            this.rbCaregiver.TabIndex = 1;
            this.rbCaregiver.Text = "I am a Caregiver";
            this.rbCaregiver.UseVisualStyleBackColor = true;
            // 
            // rbPatient
            // 
            this.rbPatient.AutoSize = true;
            this.rbPatient.Checked = true;
            this.rbPatient.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rbPatient.Location = new System.Drawing.Point(10, 8);
            this.rbPatient.Name = "rbPatient";
            this.rbPatient.Size = new System.Drawing.Size(130, 24);
            this.rbPatient.TabIndex = 0;
            this.rbPatient.TabStop = true;
            this.rbPatient.Text = "I am a Patient";
            this.rbPatient.UseVisualStyleBackColor = true;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUserType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUserType.Location = new System.Drawing.Point(50, 820);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(104, 20);
            this.lblUserType.TabIndex = 16;
            this.lblUserType.Text = "Account Type";
            // 
            // dtpDateOfBirth
            // 
            this.dtpDateOfBirth.CalendarFont = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpDateOfBirth.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Location = new System.Drawing.Point(50, 770);
            this.dtpDateOfBirth.MaxDate = new System.DateTime(2026, 12, 31, 0, 0, 0, 0);
            this.dtpDateOfBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpDateOfBirth.Name = "dtpDateOfBirth";
            this.dtpDateOfBirth.Size = new System.Drawing.Size(400, 29);
            this.dtpDateOfBirth.TabIndex = 15;
            this.dtpDateOfBirth.Value = new System.DateTime(1950, 1, 1, 0, 0, 0, 0);
            // 
            // lblDateOfBirth
            // 
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDateOfBirth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDateOfBirth.Location = new System.Drawing.Point(50, 740);
            this.lblDateOfBirth.Name = "lblDateOfBirth";
            this.lblDateOfBirth.Size = new System.Drawing.Size(104, 20);
            this.lblDateOfBirth.TabIndex = 14;
            this.lblDateOfBirth.Text = "Date of Birth";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtPhone.Location = new System.Drawing.Point(50, 690);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(400, 31);
            this.txtPhone.TabIndex = 13;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblPhone.ForeColor = System.Drawing.Color.Gray;
            this.lblPhone.Location = new System.Drawing.Point(50, 660);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(155, 20);
            this.lblPhone.TabIndex = 12;
            this.lblPhone.Text = "Phone Number (optional)";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtEmail.Location = new System.Drawing.Point(50, 610);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 31);
            this.txtEmail.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblEmail.Location = new System.Drawing.Point(50, 580);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(116, 20);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email Address";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtFullName.Location = new System.Drawing.Point(50, 530);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(400, 31);
            this.txtFullName.TabIndex = 9;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblFullName.Location = new System.Drawing.Point(50, 500);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(82, 20);
            this.lblFullName.TabIndex = 8;
            this.lblFullName.Text = "Full Name";
            // 
            // lblPasswordStrength
            // 
            this.lblPasswordStrength.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPasswordStrength.ForeColor = System.Drawing.Color.Gray;
            this.lblPasswordStrength.Location = new System.Drawing.Point(50, 401);
            this.lblPasswordStrength.Name = "lblPasswordStrength";
            this.lblPasswordStrength.Size = new System.Drawing.Size(400, 20);
            this.lblPasswordStrength.TabIndex = 7;
            this.lblPasswordStrength.Text = "Password strength: ";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(50, 450);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(400, 31);
            this.txtConfirmPassword.TabIndex = 6;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(50, 420);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(141, 20);
            this.lblConfirmPassword.TabIndex = 5;
            this.lblConfirmPassword.Text = "Confirm Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtPassword.Location = new System.Drawing.Point(50, 290);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(400, 31);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPassword.Location = new System.Drawing.Point(50, 260);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 20);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtUsername.Location = new System.Drawing.Point(50, 210);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(400, 31);
            this.txtUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUsername.Location = new System.Drawing.Point(50, 180);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(80, 20);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // lblRegisterTitle
            // 
            this.lblRegisterTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblRegisterTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.lblRegisterTitle.Location = new System.Drawing.Point(50, 80);
            this.lblRegisterTitle.Name = "lblRegisterTitle";
            this.lblRegisterTitle.Size = new System.Drawing.Size(400, 60);
            this.lblRegisterTitle.TabIndex = 0;
            this.lblRegisterTitle.Text = "Create Account";
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RegisterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yaadein - Create Account";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelUserType.ResumeLayout(false);
            this.panelUserType.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblRegisterTitle;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.Label lblPasswordStrength;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label lblDateOfBirth;
        private System.Windows.Forms.Panel panelUserType;
        private System.Windows.Forms.RadioButton rbCaregiver;
        private System.Windows.Forms.RadioButton rbPatient;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblHaveAccount;
        private System.Windows.Forms.Button btnBackToLogin;
    }
}