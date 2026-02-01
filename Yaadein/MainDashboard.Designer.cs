namespace Yaadein
{
    partial class MainDashboard
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRoutines = new System.Windows.Forms.Button();
            this.btnPeople = new System.Windows.Forms.Button();
            this.btnReminders = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxUpcoming = new System.Windows.Forms.GroupBox();
            this.lstUpcoming = new System.Windows.Forms.ListBox();
            this.groupBoxMotivation = new System.Windows.Forms.GroupBox();
            this.lblMotivation = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.timerReminder = new System.Windows.Forms.Timer(this.components);
            this.panelSidebar.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxUpcoming.SuspendLayout();
            this.groupBoxMotivation.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.panelSidebar.Controls.Add(this.btnExit);
            this.panelSidebar.Controls.Add(this.btnRoutines);
            this.panelSidebar.Controls.Add(this.btnPeople);
            this.panelSidebar.Controls.Add(this.btnReminders);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.lblAppTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(250, 700);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnExit.Location = new System.Drawing.Point(0, 640);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnExit.Size = new System.Drawing.Size(250, 60);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "❌  Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRoutines
            // 
            this.btnRoutines.BackColor = System.Drawing.Color.Transparent;
            this.btnRoutines.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRoutines.FlatAppearance.BorderSize = 0;
            this.btnRoutines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoutines.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoutines.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnRoutines.Location = new System.Drawing.Point(0, 310);
            this.btnRoutines.Name = "btnRoutines";
            this.btnRoutines.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnRoutines.Size = new System.Drawing.Size(250, 60);
            this.btnRoutines.TabIndex = 4;
            this.btnRoutines.Text = "📋  Daily Routines";
            this.btnRoutines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoutines.UseVisualStyleBackColor = false;
            this.btnRoutines.Click += new System.EventHandler(this.btnRoutines_Click);
            // 
            // btnPeople
            // 
            this.btnPeople.BackColor = System.Drawing.Color.Transparent;
            this.btnPeople.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPeople.FlatAppearance.BorderSize = 0;
            this.btnPeople.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPeople.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPeople.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnPeople.Location = new System.Drawing.Point(0, 250);
            this.btnPeople.Name = "btnPeople";
            this.btnPeople.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnPeople.Size = new System.Drawing.Size(250, 60);
            this.btnPeople.TabIndex = 3;
            this.btnPeople.Text = "👥  Important People";
            this.btnPeople.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPeople.UseVisualStyleBackColor = false;
            this.btnPeople.Click += new System.EventHandler(this.btnPeople_Click);
            // 
            // btnReminders
            // 
            this.btnReminders.BackColor = System.Drawing.Color.Transparent;
            this.btnReminders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReminders.FlatAppearance.BorderSize = 0;
            this.btnReminders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReminders.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReminders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnReminders.Location = new System.Drawing.Point(0, 190);
            this.btnReminders.Name = "btnReminders";
            this.btnReminders.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnReminders.Size = new System.Drawing.Size(250, 60);
            this.btnReminders.TabIndex = 2;
            this.btnReminders.Text = "⏰  Reminders";
            this.btnReminders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReminders.UseVisualStyleBackColor = false;
            this.btnReminders.Click += new System.EventHandler(this.btnReminders_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(203)))));
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 130);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(250, 60);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "🏠  Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.lblAppTitle.Location = new System.Drawing.Point(30, 30);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(168, 51);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "Yaadein";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblDate);
            this.panelHeader.Controls.Add(this.lblTime);
            this.panelHeader.Controls.Add(this.lblWelcome);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(250, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(950, 100);
            this.panelHeader.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblDate.Location = new System.Drawing.Point(640, 55);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(243, 25);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Saturday, February 01, 2026";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.lblTime.Location = new System.Drawing.Point(630, 15);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(232, 65);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "12:00 PM";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblWelcome.Location = new System.Drawing.Point(30, 30);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(342, 45);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome Back! 👋";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.groupBoxUpcoming);
            this.panelMain.Controls.Add(this.groupBoxMotivation);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(250, 100);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(30);
            this.panelMain.Size = new System.Drawing.Size(950, 600);
            this.panelMain.TabIndex = 2;
            // 
            // groupBoxUpcoming
            // 
            this.groupBoxUpcoming.Controls.Add(this.lstUpcoming);
            this.groupBoxUpcoming.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxUpcoming.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.groupBoxUpcoming.Location = new System.Drawing.Point(40, 270);
            this.groupBoxUpcoming.Name = "groupBoxUpcoming";
            this.groupBoxUpcoming.Padding = new System.Windows.Forms.Padding(15);
            this.groupBoxUpcoming.Size = new System.Drawing.Size(870, 300);
            this.groupBoxUpcoming.TabIndex = 1;
            this.groupBoxUpcoming.TabStop = false;
            this.groupBoxUpcoming.Text = "📅  Today's Schedule";
            // 
            // lstUpcoming
            // 
            this.lstUpcoming.BackColor = System.Drawing.Color.White;
            this.lstUpcoming.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstUpcoming.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUpcoming.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUpcoming.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lstUpcoming.FormattingEnabled = true;
            this.lstUpcoming.ItemHeight = 25;
            this.lstUpcoming.Location = new System.Drawing.Point(15, 45);
            this.lstUpcoming.Name = "lstUpcoming";
            this.lstUpcoming.Size = new System.Drawing.Size(840, 240);
            this.lstUpcoming.TabIndex = 0;
            // 
            // groupBoxMotivation
            // 
            this.groupBoxMotivation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(245)))));
            this.groupBoxMotivation.Controls.Add(this.lblMotivation);
            this.groupBoxMotivation.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMotivation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(105)))), ((int)(((byte)(180)))));
            this.groupBoxMotivation.Location = new System.Drawing.Point(40, 30);
            this.groupBoxMotivation.Name = "groupBoxMotivation";
            this.groupBoxMotivation.Padding = new System.Windows.Forms.Padding(20);
            this.groupBoxMotivation.Size = new System.Drawing.Size(870, 220);
            this.groupBoxMotivation.TabIndex = 0;
            this.groupBoxMotivation.TabStop = false;
            this.groupBoxMotivation.Text = "💭  Daily Motivation";
            // 
            // lblMotivation
            // 
            this.lblMotivation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMotivation.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblMotivation.Location = new System.Drawing.Point(20, 50);
            this.lblMotivation.Name = "lblMotivation";
            this.lblMotivation.Size = new System.Drawing.Size(830, 150);
            this.lblMotivation.TabIndex = 0;
            this.lblMotivation.Text = "\"Every moment is a fresh beginning.\r\nYou are doing wonderfully today!\"";
            this.lblMotivation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // timerReminder
            // 
            this.timerReminder.Interval = 60000;
            this.timerReminder.Tick += new System.EventHandler(this.timerReminder_Tick);
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yaadein - Supporting Memory, Every Day";
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxUpcoming.ResumeLayout(false);
            this.groupBoxMotivation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnReminders;
        private System.Windows.Forms.Button btnPeople;
        private System.Windows.Forms.Button btnRoutines;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxMotivation;
        private System.Windows.Forms.Label lblMotivation;
        private System.Windows.Forms.GroupBox groupBoxUpcoming;
        private System.Windows.Forms.ListBox lstUpcoming;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.Timer timerReminder;
    }
}