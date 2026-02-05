using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Yaadein.Data;

namespace Yaadein
{
    public partial class LoginForm : Form
    {
        private bool isPasswordVisible = false;

        public LoginForm()
        {
            InitializeComponent();
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            // Round the corners of containers (visual effect)
            panelUsernameContainer.Paint += PanelContainer_Paint;
            panelPasswordContainer.Paint += PanelContainer_Paint;

            // Create user icon for username field
            CreateUserIcon();

            // Load hero image - this will ALWAYS show something
            LoadHeroImage();
        }

        private void CreateUserIcon()
        {
            // Create a simple user icon using graphics
            Bitmap userIcon = new Bitmap(25, 25);
            using (Graphics g = Graphics.FromImage(userIcon))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Draw user icon circle
                using (Pen pen = new Pen(Color.FromArgb(147, 51, 234), 2))
                {
                    g.DrawEllipse(pen, 5, 3, 15, 15);
                    g.DrawArc(pen, 2, 15, 21, 15, 0, 180);
                }
            }
            picUsername.Image = userIcon;
        }

        private void LoadHeroImage()
        {
            bool imageLoaded = false;

            try
            {
                // Get the project base directory
                string projectPath = AppDomain.CurrentDomain.BaseDirectory;

                // Try multiple possible locations
                string[] possiblePaths = new string[]
                {
                    // Specific path you mentioned
                    @"C:\Users\Win11\OneDrive\Desktop\VPP\Yaadein\Resources\hero-image.png",
                    // Relative to executable
                    Path.Combine(projectPath, @"..\..\..\Resources\hero-image.png"),
                    Path.Combine(projectPath, @"..\..\Resources\hero-image.png"),
                    Path.Combine(projectPath, "Resources", "hero-image.png"),
                    Path.Combine(Application.StartupPath, "Resources", "hero-image.png"),
                    Path.Combine(Application.StartupPath, "Resources", "hero-image.jpg"),
                    // Other formats
                    @"C:\Users\Win11\OneDrive\Desktop\VPP\Yaadein\Resources\hero-image.jpg",
                    Path.Combine(projectPath, @"..\..\..\Resources\hero-image.jpg")
                };

                foreach (string imagePath in possiblePaths)
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(imagePath);

                        if (File.Exists(fullPath))
                        {
                            // Load image using FileStream to avoid file locking
                            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            {
                                picBoxHero.Image = Image.FromStream(fs);
                            }
                            imageLoaded = true;

                            // Set proper display mode
                            picBoxHero.SizeMode = PictureBoxSizeMode.Zoom;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Try next path
                        System.Diagnostics.Debug.WriteLine($"Failed to load from {imagePath}: {ex.Message}");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadHeroImage: {ex.Message}");
            }

            // Always create a beautiful placeholder if no image loaded
            if (!imageLoaded)
            {
                CreatePlaceholderImage();
            }
        }

        private void CreatePlaceholderImage()
        {
            // Create a beautiful gradient placeholder with memory-themed graphics
            Bitmap placeholder = new Bitmap(300, 200);
            using (Graphics g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Create gradient background
                using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(
                        new Rectangle(0, 0, 300, 200),
                        Color.FromArgb(147, 51, 234),
                        Color.FromArgb(200, 100, 250),
                        45f))
                {
                    g.FillRectangle(brush, 0, 0, 300, 200);
                }

                // Add decorative circles/bubbles
                using (SolidBrush whiteBrush = new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillEllipse(whiteBrush, 20, 30, 60, 60);
                    g.FillEllipse(whiteBrush, 180, 100, 80, 80);
                    g.FillEllipse(whiteBrush, 100, 10, 40, 40);
                    g.FillEllipse(whiteBrush, 240, 40, 30, 30);
                }

                // Draw hearts for memory/care theme
                DrawHeart(g, new Point(80, 60), 30, Color.FromArgb(200, 255, 255, 255));
                DrawHeart(g, new Point(220, 140), 25, Color.FromArgb(180, 255, 255, 255));
                DrawHeart(g, new Point(150, 40), 20, Color.FromArgb(160, 255, 255, 255));

                // Draw brain/memory icon
                DrawMemoryIcon(g, new Point(150, 100), 40, Color.White);

                // Add text
                using (Font font = new Font("Segoe UI", 14, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(Color.White))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString("Yaadein", font, textBrush, new RectangleF(0, 160, 300, 30), sf);
                }
            }

            picBoxHero.Image = placeholder;
            picBoxHero.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void DrawHeart(Graphics g, Point center, int size, Color color)
        {
            using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                // Simple heart shape
                int x = center.X - size / 2;
                int y = center.Y - size / 2;

                path.AddArc(x, y, size / 2, size / 2, 135, 225);
                path.AddArc(x + size / 2, y, size / 2, size / 2, 270, 225);
                path.AddLine(x + size, y + size / 2, x + size / 2, y + size);
                path.AddLine(x + size / 2, y + size, x, y + size / 2);

                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillPath(brush, path);
                }
            }
        }

        private void DrawMemoryIcon(Graphics g, Point center, int size, Color color)
        {
            // Draw a simple brain/photo icon
            using (Pen pen = new Pen(color, 3))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(100, color)))
            {
                // Draw photo frame
                Rectangle frame = new Rectangle(center.X - size / 2, center.Y - size / 2, size, size);
                g.FillRectangle(brush, frame);
                g.DrawRectangle(pen, frame);

                // Draw mountain/memory inside
                Point[] mountain = new Point[]
                {
                    new Point(center.X - size/3, center.Y + size/3),
                    new Point(center.X - size/6, center.Y - size/6),
                    new Point(center.X, center.Y + size/6),
                    new Point(center.X + size/4, center.Y - size/4),
                    new Point(center.X + size/3, center.Y + size/3)
                };

                using (Pen mountainPen = new Pen(color, 2))
                {
                    g.DrawLines(mountainPen, mountain);
                }

                // Draw sun/circle
                g.FillEllipse(new SolidBrush(color), center.X + size / 5, center.Y - size / 3, size / 6, size / 6);
            }
        }

        private void PanelContainer_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (System.Drawing.Drawing2D.GraphicsPath path = GetRoundedRectangle(panel.ClientRectangle, 8))
            {
                panel.Region = new Region(path);
            }
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Top left arc
            path.AddArc(arc, 180, 90);

            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowValidationError("Please enter your username.", txtUsername);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowValidationError("Please enter your password.", txtPassword);
                return;
            }

            // Show loading state
            btnLogin.Enabled = false;
            btnLogin.Text = "Logging in...";
            Application.DoEvents();

            try
            {
                if (DatabaseHelper.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text))
                {
                    MessageBox.Show($"Welcome back, {DatabaseHelper.CurrentUserName}! 🎉",
                        "Login Successful",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    MainDashboard dashboard = new MainDashboard();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.\n\nPlease check your credentials and try again.\n\nIf you don't have an account, click 'Create New Account'.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login →";
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

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            if (isPasswordVisible)
            {
                txtPassword.PasswordChar = '\0';
                btnTogglePassword.Text = "🙈";
            }
            else
            {
                txtPassword.PasswordChar = '●';
                btnTogglePassword.Text = "👁";
            }
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact your care provider to reset your password.\n\nFor security reasons, password resets require caregiver assistance.",
                "Password Recovery",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShowValidationError(string message, Control focusControl)
        {
            MessageBox.Show(message, "Validation Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focusControl.Focus();
        }

        // Hover effects for buttons
        private void btn_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnLogin)
            {
                btn.BackColor = Color.FromArgb(130, 45, 220);
            }
            else if (btn == btnRegister)
            {
                btn.BackColor = Color.FromArgb(250, 245, 255);
            }
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == btnLogin)
            {
                btn.BackColor = Color.FromArgb(147, 51, 234);
            }
            else if (btn == btnRegister)
            {
                btn.BackColor = Color.White;
            }
        }

        // Focus effects for text boxes
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            panelUsernameContainer.BackColor = Color.FromArgb(240, 235, 255);
            using (Graphics g = panelUsernameContainer.CreateGraphics())
            {
                g.DrawRectangle(new Pen(Color.FromArgb(147, 51, 234), 2),
                    0, 0, panelUsernameContainer.Width - 1, panelUsernameContainer.Height - 1);
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            panelUsernameContainer.BackColor = Color.FromArgb(245, 245, 250);
            panelUsernameContainer.Invalidate();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            panelPasswordContainer.BackColor = Color.FromArgb(240, 235, 255);
            using (Graphics g = panelPasswordContainer.CreateGraphics())
            {
                g.DrawRectangle(new Pen(Color.FromArgb(147, 51, 234), 2),
                    0, 0, panelPasswordContainer.Width - 1, panelPasswordContainer.Height - 1);
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            panelPasswordContainer.BackColor = Color.FromArgb(245, 245, 250);
            panelPasswordContainer.Invalidate();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }
    }
}