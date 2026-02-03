using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Yaadein.Models;
using Yaadein.Data;

namespace Yaadein
{
    public partial class MainDashboard : Form
    {
        private List<Reminder> reminders;
        private string[] motivationalQuotes = new string[]
        {
            "\"Every moment is a fresh beginning.\nYou are doing wonderfully today!\"",
            "\"Take it one step at a time.\nYou've got this!\"",
            "\"Your strength inspires everyone around you.\nKeep shining!\"",
            "\"Today is full of possibilities.\nEmbrace each moment with joy!\"",
            "\"You are loved and cherished.\nNever forget how special you are!\"",
            "\"Small steps lead to great achievements.\nBe proud of yourself!\"",
            "\"You bring joy to those around you.\nYour presence matters!\"",
            "\"Every day is a gift.\nMake the most of this beautiful moment!\""
        };

        public MainDashboard()
        {
            InitializeComponent();
            reminders = new List<Reminder>();
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            UpdateClock();

            Random rand = new Random();
            lblMotivation.Text = motivationalQuotes[rand.Next(motivationalQuotes.Length)];

            LoadUserReminders();
            LoadUpcomingReminders();

            timerClock.Start();
            timerReminder.Start();
        }

        private void LoadUserReminders()
        {
            try
            {
                reminders = DatabaseHelper.GetUserReminders(DatabaseHelper.CurrentUserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reminders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUpcomingReminders()
        {
            lstUpcoming.Items.Clear();

            var upcoming = reminders
                .Where(r => r.IsActive && !r.IsCompleted)
                .Where(r => r.ReminderTime.Date == DateTime.Today)
                .OrderBy(r => r.ReminderTime)
                .Take(5)
                .ToList();

            if (upcoming.Count == 0)
            {
                lstUpcoming.Items.Add("✅ No upcoming reminders for today!");
            }
            else
            {
                foreach (var reminder in upcoming)
                {
                    string priority = reminder.Priority == 1 ? "🔴" : reminder.Priority == 2 ? "🟡" : "🟢";
                    string time = reminder.ReminderTime.ToString("hh:mm tt");
                    string displayText = $"{priority} {time} - {reminder.Title}";

                    lstUpcoming.Items.Add(displayText);
                }
            }
        }

        private void UpdateClock()
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm tt");
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void timerReminder_Tick(object sender, EventArgs e)
        {
            CheckReminders();
        }

        private void CheckReminders()
        {
            var now = DateTime.Now;
            var dueReminders = reminders
                .Where(r => r.IsActive && !r.IsCompleted)
                .Where(r => Math.Abs((r.ReminderTime - now).TotalMinutes) < 1)
                .ToList();

            foreach (var reminder in dueReminders)
            {
                ShowReminderNotification(reminder);
                reminder.IsCompleted = true;

                try
                {
                    DatabaseHelper.SaveReminder(reminder, DatabaseHelper.CurrentUserId);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error updating reminder: {ex.Message}");
                }
            }
        }

        private void ShowReminderNotification(Reminder reminder)
        {
            string message = $"⏰ {reminder.Title}\n\n{reminder.Description}";
            string title = "Reminder Alert";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReminders_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnReminders.BackColor = Color.FromArgb(255, 192, 203);
            btnReminders.ForeColor = Color.White;
            btnReminders.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            RemindersForm remindersForm = new RemindersForm();
            remindersForm.ShowDialog();

            LoadUserReminders();
            LoadUpcomingReminders();

            btnReminders.BackColor = Color.Transparent;
            btnReminders.ForeColor = Color.FromArgb(100, 100, 100);
            btnReminders.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnDashboard.BackColor = Color.FromArgb(255, 192, 203);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnPeople.BackColor = Color.FromArgb(255, 192, 203);
            btnPeople.ForeColor = Color.White;
            btnPeople.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            PeopleForm peopleForm = new PeopleForm();
            peopleForm.ShowDialog();

            btnPeople.BackColor = Color.Transparent;
            btnPeople.ForeColor = Color.FromArgb(100, 100, 100);
            btnPeople.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnDashboard.BackColor = Color.FromArgb(255, 192, 203);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void btnRoutines_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnRoutines.BackColor = Color.FromArgb(255, 192, 203);
            btnRoutines.ForeColor = Color.White;
            btnRoutines.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            RoutinesForm routinesForm = new RoutinesForm();
            routinesForm.ShowDialog();

            btnRoutines.BackColor = Color.Transparent;
            btnRoutines.ForeColor = Color.FromArgb(100, 100, 100);
            btnRoutines.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnDashboard.BackColor = Color.FromArgb(255, 192, 203);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void btnCompanion_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnCompanion.BackColor = Color.FromArgb(255, 192, 203);
            btnCompanion.ForeColor = Color.White;
            btnCompanion.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            CompanionChatForm companionChatForm = new CompanionChatForm();
            companionChatForm.ShowDialog();

            btnCompanion.BackColor = Color.Transparent;
            btnCompanion.ForeColor = Color.FromArgb(100, 100, 100);
            btnCompanion.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            btnDashboard.BackColor = Color.FromArgb(255, 192, 203);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Font = new Font("Segoe UI", 14, FontStyle.Bold);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit Yaadein?",
                "Exit Application",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void ResetButtonColors()
        {
            btnDashboard.BackColor = Color.Transparent;
            btnDashboard.ForeColor = Color.FromArgb(100, 100, 100);
            btnDashboard.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnReminders.BackColor = Color.Transparent;
            btnReminders.ForeColor = Color.FromArgb(100, 100, 100);
            btnReminders.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnPeople.BackColor = Color.Transparent;
            btnPeople.ForeColor = Color.FromArgb(100, 100, 100);
            btnPeople.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnRoutines.BackColor = Color.Transparent;
            btnRoutines.ForeColor = Color.FromArgb(100, 100, 100);
            btnRoutines.Font = new Font("Segoe UI", 14, FontStyle.Regular);

            btnCompanion.BackColor = Color.Transparent;
            btnCompanion.ForeColor = Color.FromArgb(100, 100, 100);
            btnCompanion.Font = new Font("Segoe UI", 14, FontStyle.Regular);
        }
    }
}