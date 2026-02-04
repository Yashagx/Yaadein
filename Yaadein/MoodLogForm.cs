using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Yaadein.Data;

namespace Yaadein
{
    public partial class MoodLogForm : Form
    {
        private Panel panelTop;
        private Label lblTitle;
        private Button btnClose;

        private Panel panelEntry;
        private Label lblFormTitle;
        private Label lblDate;
        private DateTimePicker dtpMoodDate;
        private Label lblMood;
        private Button btnHappy;
        private Button btnNeutral;
        private Button btnSad;
        private Label lblNotes;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnClear;

        private Panel panelHistory;
        private Label lblHistory;
        private DataGridView dgvMoodHistory;

        private string selectedMood = "";
        private const string WATERMARK_TEXT = "How are you feeling today? (Optional)";
        private bool _watermarkVisible = false;

        public MoodLogForm()
        {
            InitializeComponents();
            InitializeMoodDatabase();
            LoadMoodHistory();
            ShowWatermark();
        }

        private void InitializeComponents()
        {
            this.Text = "Yaadein – Mood Log";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Top Panel
            panelTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(255, 192, 203)
            };

            lblTitle = new Label
            {
                Text = "📊  Mood Log",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(20, 0, 0, 0),
                AutoSize = false,
                Width = 450
            };

            btnClose = new Button
            {
                Text = "✕",
                Dock = DockStyle.Right,
                Width = 60,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(255, 105, 180),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += btnClose_Click;

            panelTop.Controls.Add(lblTitle);
            panelTop.Controls.Add(btnClose);

            // Entry Panel
            panelEntry = new Panel
            {
                Location = new Point(20, 90),
                Size = new Size(420, 540),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblFormTitle = new Label
            {
                Text = "How are you feeling?",
                Location = new Point(20, 20),
                Size = new Size(380, 35),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 105, 180)
            };

            lblDate = new Label
            {
                Text = "Date:",
                Location = new Point(20, 70),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            dtpMoodDate = new DateTimePicker
            {
                Location = new Point(20, 100),
                Size = new Size(380, 30),
                Font = new Font("Segoe UI", 12F),
                Format = DateTimePickerFormat.Long,
                Value = DateTime.Today
            };

            lblMood = new Label
            {
                Text = "Your Mood:",
                Location = new Point(20, 150),
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            // Mood Buttons
            btnHappy = new Button
            {
                Text = "😊 Happy",
                Location = new Point(20, 185),
                Size = new Size(380, 60),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = Color.FromArgb(144, 238, 144),
                ForeColor = Color.FromArgb(40, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = "Happy"
            };
            btnHappy.FlatAppearance.BorderSize = 2;
            btnHappy.FlatAppearance.BorderColor = Color.FromArgb(144, 238, 144);
            btnHappy.Click += MoodButton_Click;

            btnNeutral = new Button
            {
                Text = "😐 Neutral",
                Location = new Point(20, 255),
                Size = new Size(380, 60),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 250, 205),
                ForeColor = Color.FromArgb(40, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = "Neutral"
            };
            btnNeutral.FlatAppearance.BorderSize = 2;
            btnNeutral.FlatAppearance.BorderColor = Color.FromArgb(255, 250, 205);
            btnNeutral.Click += MoodButton_Click;

            btnSad = new Button
            {
                Text = "😢 Sad",
                Location = new Point(20, 325),
                Size = new Size(380, 60),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                BackColor = Color.FromArgb(173, 216, 230),
                ForeColor = Color.FromArgb(40, 40, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = "Sad"
            };
            btnSad.FlatAppearance.BorderSize = 2;
            btnSad.FlatAppearance.BorderColor = Color.FromArgb(173, 216, 230);
            btnSad.Click += MoodButton_Click;

            lblNotes = new Label
            {
                Text = "Notes (Optional):",
                Location = new Point(20, 400),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            txtNotes = new TextBox
            {
                Location = new Point(20, 430),
                Size = new Size(380, 60),
                Font = new Font("Segoe UI", 11F),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ForeColor = Color.FromArgb(60, 60, 60)
            };
            txtNotes.GotFocus += txtNotes_GotFocus;
            txtNotes.LostFocus += txtNotes_LostFocus;

            btnSave = new Button
            {
                Text = "💾 Save Mood",
                Location = new Point(20, 500),
                Size = new Size(185, 45),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 182, 193),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;

            btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(215, 500),
                Size = new Size(185, 45),
                Font = new Font("Segoe UI", 12F),
                BackColor = Color.LightGray,
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += btnClear_Click;

            panelEntry.Controls.Add(lblFormTitle);
            panelEntry.Controls.Add(lblDate);
            panelEntry.Controls.Add(dtpMoodDate);
            panelEntry.Controls.Add(lblMood);
            panelEntry.Controls.Add(btnHappy);
            panelEntry.Controls.Add(btnNeutral);
            panelEntry.Controls.Add(btnSad);
            panelEntry.Controls.Add(lblNotes);
            panelEntry.Controls.Add(txtNotes);
            panelEntry.Controls.Add(btnSave);
            panelEntry.Controls.Add(btnClear);

            // History Panel
            panelHistory = new Panel
            {
                Location = new Point(460, 90),
                Size = new Size(420, 540),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblHistory = new Label
            {
                Text = "📅 Mood History",
                Location = new Point(20, 20),
                Size = new Size(380, 35),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 105, 180)
            };

            dgvMoodHistory = new DataGridView
            {
                Location = new Point(20, 65),
                Size = new Size(380, 455),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 10F),
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };
            dgvMoodHistory.EnableHeadersVisualStyles = false;
            dgvMoodHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 203);
            dgvMoodHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMoodHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvMoodHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMoodHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 240, 245);
            dgvMoodHistory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(60, 60, 60);
            dgvMoodHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);

            panelHistory.Controls.Add(lblHistory);
            panelHistory.Controls.Add(dgvMoodHistory);

            // Add all to form
            this.Controls.Add(panelTop);
            this.Controls.Add(panelEntry);
            this.Controls.Add(panelHistory);
        }

        private void InitializeMoodDatabase()
        {
            try
            {
                using (SQLiteConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS MoodLog (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            UserId INTEGER NOT NULL,
                            Date TEXT NOT NULL,
                            Mood TEXT NOT NULL,
                            Notes TEXT,
                            CreatedAt TEXT NOT NULL,
                            FOREIGN KEY (UserId) REFERENCES Users(Id)
                        )";

                    using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing mood database: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MoodButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            // Reset all buttons
            ResetMoodButtons();

            // Highlight selected button
            selectedMood = clickedButton.Tag.ToString();
            clickedButton.FlatAppearance.BorderSize = 4;

            switch (selectedMood)
            {
                case "Happy":
                    clickedButton.FlatAppearance.BorderColor = Color.FromArgb(34, 139, 34);
                    break;
                case "Neutral":
                    clickedButton.FlatAppearance.BorderColor = Color.FromArgb(255, 165, 0);
                    break;
                case "Sad":
                    clickedButton.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 180);
                    break;
            }

            btnSave.Enabled = true;
        }

        private void ResetMoodButtons()
        {
            btnHappy.FlatAppearance.BorderSize = 2;
            btnHappy.FlatAppearance.BorderColor = Color.FromArgb(144, 238, 144);

            btnNeutral.FlatAppearance.BorderSize = 2;
            btnNeutral.FlatAppearance.BorderColor = Color.FromArgb(255, 250, 205);

            btnSad.FlatAppearance.BorderSize = 2;
            btnSad.FlatAppearance.BorderColor = Color.FromArgb(173, 216, 230);
        }

        private void ShowWatermark()
        {
            if (string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                txtNotes.Text = WATERMARK_TEXT;
                txtNotes.ForeColor = Color.FromArgb(160, 160, 160);
                _watermarkVisible = true;
            }
        }

        private void HideWatermark()
        {
            if (_watermarkVisible)
            {
                txtNotes.Text = "";
                txtNotes.ForeColor = Color.FromArgb(60, 60, 60);
                _watermarkVisible = false;
            }
        }

        private void txtNotes_GotFocus(object sender, EventArgs e)
        {
            HideWatermark();
        }

        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNotes.Text))
                ShowWatermark();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMood))
            {
                MessageBox.Show("Please select a mood before saving.",
                    "Mood Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string notes = _watermarkVisible ? "" : txtNotes.Text.Trim();
                SaveMoodEntry(dtpMoodDate.Value.Date, selectedMood, notes);

                MessageBox.Show("Your mood has been recorded! 💕",
                    "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMoodHistory();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving mood: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveMoodEntry(DateTime date, string mood, string notes)
        {
            using (SQLiteConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string insertQuery = @"
                    INSERT INTO MoodLog (UserId, Date, Mood, Notes, CreatedAt)
                    VALUES (@UserId, @Date, @Mood, @Notes, @CreatedAt)";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", DatabaseHelper.CurrentUserId);
                    cmd.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@Mood", mood);
                    cmd.Parameters.AddWithValue("@Notes", notes);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadMoodHistory()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("Mood", typeof(string));
                dt.Columns.Add("Notes", typeof(string));

                using (SQLiteConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string selectQuery = @"
                        SELECT Date, Mood, Notes 
                        FROM MoodLog 
                        WHERE UserId = @UserId
                        ORDER BY Date DESC
                        LIMIT 50";

                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", DatabaseHelper.CurrentUserId);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime date = DateTime.Parse(reader["Date"].ToString());
                                string mood = reader["Mood"].ToString();
                                string notes = reader["Notes"]?.ToString() ?? "";

                                string moodEmoji = mood == "Happy" ? "😊" :
                                                 mood == "Neutral" ? "😐" : "😢";

                                dt.Rows.Add(
                                    date.ToString("MMM dd, yyyy"),
                                    $"{moodEmoji} {mood}",
                                    string.IsNullOrEmpty(notes) ? "—" : notes
                                );
                            }
                        }
                    }
                }

                dgvMoodHistory.DataSource = dt;

                if (dgvMoodHistory.Columns.Count > 0)
                {
                    dgvMoodHistory.Columns[0].Width = 100;
                    dgvMoodHistory.Columns[1].Width = 90;
                    dgvMoodHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                // Apply mood-based row colors
                foreach (DataGridViewRow row in dgvMoodHistory.Rows)
                {
                    string moodCell = row.Cells[1].Value?.ToString() ?? "";
                    if (moodCell.Contains("😊"))
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 255, 240);
                    else if (moodCell.Contains("😢"))
                        row.DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading mood history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            dtpMoodDate.Value = DateTime.Today;
            selectedMood = "";
            ResetMoodButtons();
            txtNotes.Clear();
            ShowWatermark();
            btnSave.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}