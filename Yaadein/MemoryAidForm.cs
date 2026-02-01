using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Yaadein.Services;

namespace Yaadein
{
    public partial class MemoryAidForm : Form
    {
        private GroqService groqService;
        private List<MemoryCard> memoryCards;

        public MemoryAidForm()
        {
            InitializeComponent();
            groqService = new GroqService();
            memoryCards = new List<MemoryCard>();
        }

        private void MemoryAidForm_Load(object sender, EventArgs e)
        {
            LoadSampleMemories();
            DisplayMemoryCards();
        }

        private void LoadSampleMemories()
        {
            memoryCards.Add(new MemoryCard
            {
                Title = "Home Address",
                Content = "123 Oak Street, Springfield\nPhone: (555) 123-4567",
                Category = "Important Info",
                Icon = "🏠"
            });

            memoryCards.Add(new MemoryCard
            {
                Title = "Emergency Contacts",
                Content = "Daughter Sarah: (555) 123-4567\nSon Robert: (555) 234-5678\nDr. Roberts: (555) 987-6543",
                Category = "Important Info",
                Icon = "🚨"
            });

            memoryCards.Add(new MemoryCard
            {
                Title = "Daily Schedule",
                Content = "8:00 AM - Breakfast & Medication\n12:30 PM - Lunch\n3:00 PM - Afternoon Walk\n6:00 PM - Dinner & Medication",
                Category = "Routine",
                Icon = "📅"
            });

            memoryCards.Add(new MemoryCard
            {
                Title = "Favorite Activities",
                Content = "• Gardening in the backyard\n• Reading mystery novels\n• Watching old movies\n• Calling grandchildren",
                Category = "Personal",
                Icon = "❤️"
            });

            memoryCards.Add(new MemoryCard
            {
                Title = "Safety Reminders",
                Content = "• Turn off stove after cooking\n• Lock doors at night\n• Take keys when leaving\n• Charge phone daily",
                Category = "Safety",
                Icon = "⚠️"
            });
        }

        private void DisplayMemoryCards()
        {
            flowLayoutPanel.Controls.Clear();

            foreach (var card in memoryCards)
            {
                Panel cardPanel = CreateMemoryCardPanel(card);
                flowLayoutPanel.Controls.Add(cardPanel);
            }
        }

        private Panel CreateMemoryCardPanel(MemoryCard card)
        {
            Panel panel = new Panel
            {
                Width = 320,
                Height = 180,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10),
                Cursor = Cursors.Hand
            };

            // Icon Label
            Label iconLabel = new Label
            {
                Text = card.Icon,
                Font = new Font("Segoe UI Emoji", 32, FontStyle.Regular),
                Location = new Point(10, 10),
                Size = new Size(60, 60),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Title Label
            Label titleLabel = new Label
            {
                Text = card.Title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(80, 15),
                Size = new Size(220, 30),
                ForeColor = Color.FromArgb(255, 105, 180)
            };

            // Category Label
            Label categoryLabel = new Label
            {
                Text = card.Category,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                Location = new Point(80, 45),
                Size = new Size(220, 20),
                ForeColor = Color.Gray
            };

            // Content Label
            Label contentLabel = new Label
            {
                Text = card.Content,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Location = new Point(15, 75),
                Size = new Size(285, 95),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(categoryLabel);
            panel.Controls.Add(contentLabel);

            // Hover effects
            panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(255, 240, 245);
            panel.MouseLeave += (s, e) => panel.BackColor = Color.White;

            return panel;
        }

        private void btnAddMemory_Click(object sender, EventArgs e)
        {
            using (AddMemoryDialog dialog = new AddMemoryDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    memoryCards.Add(dialog.NewMemoryCard);
                    DisplayMemoryCards();
                    MessageBox.Show("Memory card added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Memory Card Model
    public class MemoryCard
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }

        public MemoryCard()
        {
            CreatedDate = DateTime.Now;
        }
    }

    // Simple dialog for adding new memory cards
    public class AddMemoryDialog : Form
    {
        private TextBox txtTitle;
        private TextBox txtContent;
        private ComboBox cmbCategory;
        private ComboBox cmbIcon;
        private Button btnSave;
        private Button btnCancel;

        public MemoryCard NewMemoryCard { get; private set; }

        public AddMemoryDialog()
        {
            InitializeDialog();
        }

        private void InitializeDialog()
        {
            this.Text = "Add Memory Card";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(255, 250, 250);

            // Title
            Label lblTitle = new Label
            {
                Text = "Title:",
                Location = new Point(20, 20),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            txtTitle = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(390, 30),
                Font = new Font("Segoe UI", 12)
            };

            // Content
            Label lblContent = new Label
            {
                Text = "Content:",
                Location = new Point(20, 90),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            txtContent = new TextBox
            {
                Location = new Point(20, 120),
                Size = new Size(390, 100),
                Font = new Font("Segoe UI", 11),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Category
            Label lblCategory = new Label
            {
                Text = "Category:",
                Location = new Point(20, 230),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            cmbCategory = new ComboBox
            {
                Location = new Point(20, 260),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCategory.Items.AddRange(new string[] { "Important Info", "Routine", "Personal", "Safety", "Family", "Medical" });
            cmbCategory.SelectedIndex = 0;

            // Icon
            Label lblIcon = new Label
            {
                Text = "Icon:",
                Location = new Point(230, 230),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            cmbIcon = new ComboBox
            {
                Location = new Point(230, 260),
                Size = new Size(180, 30),
                Font = new Font("Segoe UI Emoji", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbIcon.Items.AddRange(new string[] { "🏠", "🚨", "📅", "❤️", "⚠️", "👨‍👩‍👧", "💊", "📝", "🔔", "⭐" });
            cmbIcon.SelectedIndex = 0;

            // Buttons
            btnSave = new Button
            {
                Text = "Save",
                Location = new Point(230, 310),
                Size = new Size(90, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 192, 203),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(330, 310),
                Size = new Size(90, 40),
                Font = new Font("Segoe UI", 11),
                BackColor = Color.LightGray,
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblContent);
            this.Controls.Add(txtContent);
            this.Controls.Add(lblCategory);
            this.Controls.Add(cmbCategory);
            this.Controls.Add(lblIcon);
            this.Controls.Add(cmbIcon);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NewMemoryCard = new MemoryCard
            {
                Title = txtTitle.Text.Trim(),
                Content = txtContent.Text.Trim(),
                Category = cmbCategory.SelectedItem.ToString(),
                Icon = cmbIcon.SelectedItem.ToString()
            };

            this.DialogResult = DialogResult.OK;
        }
    }
}