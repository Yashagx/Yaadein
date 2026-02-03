namespace Yaadein
{
    partial class LoginForm
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
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblNoAccount = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
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
            this.panelLeft.Size = new System.Drawing.Size(400, 600);
            this.panelLeft.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblWelcome.ForeColor = System.Drawing.Color.White;
            this.lblWelcome.Location = new System.Drawing.Point(30, 380);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(340, 150);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Supporting today,\r\nwhen memories begin to fade.\r\n\r\nYour trusted companion for\r\nmemor" +
    "y care and daily support.";
            // 
            // lblTagline
            // 
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Italic);
            this.lblTagline.ForeColor = System.Drawing.Color.White;
            this.lblTagline.Location = new System.Drawing.Point(30, 320);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(340, 40);
            this.lblTagline.TabIndex = 1;
            this.lblTagline.Text = "Remember Together";
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
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.btnRegister);
            this.panelRight.Controls.Add(this.lblNoAccount);
            this.panelRight.Controls.Add(this.btnLogin);
            this.panelRight.Controls.Add(this.chkRememberMe);
            this.panelRight.Controls.Add(this.txtPassword);
            this.panelRight.Controls.Add(this.lblPassword);
            this.panelRight.Controls.Add(this.txtUsername);
            this.panelRight.Controls.Add(this.lblUsername);
            this.panelRight.Controls.Add(this.lblLoginTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(400, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(500, 600);
            this.panelRight.TabIndex = 1;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.White;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(203)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.btnRegister.Location = new System.Drawing.Point(80, 510);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(340, 50);
            this.btnRegister.TabIndex = 8;
            this.btnRegister.Text = "Create New Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblNoAccount
            // 
            this.lblNoAccount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNoAccount.ForeColor = System.Drawing.Color.Gray;
            this.lblNoAccount.Location = new System.Drawing.Point(80, 475);
            this.lblNoAccount.Name = "lblNoAccount";
            this.lblNoAccount.Size = new System.Drawing.Size(340, 25);
            this.lblNoAccount.TabIndex = 7;
            this.lblNoAccount.Text = "Don\'t have an account?";
            this.lblNoAccount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(182)))), ((int)(((byte)(193)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(80, 380);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(340, 55);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRememberMe.Location = new System.Drawing.Point(80, 330);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(119, 23);
            this.chkRememberMe.TabIndex = 5;
            this.chkRememberMe.Text = "Remember me";
            this.chkRememberMe.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtPassword.Location = new System.Drawing.Point(80, 280);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(340, 31);
            this.txtPassword.TabIndex = 4;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblPassword.Location = new System.Drawing.Point(80, 250);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 20);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.txtUsername.Location = new System.Drawing.Point(80, 200);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(340, 31);
            this.txtUsername.TabIndex = 2;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblUsername.Location = new System.Drawing.Point(80, 170);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(80, 20);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblLoginTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.lblLoginTitle.Location = new System.Drawing.Point(80, 80);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(340, 60);
            this.lblLoginTitle.TabIndex = 0;
            this.lblLoginTitle.Text = "Welcome Back";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yaadein - Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblNoAccount;
        private System.Windows.Forms.Button btnRegister;
    }
}