using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Yaadein.Models;

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
            LoadSampleData();
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            // Set initial time and date
            UpdateClock();

            // Set random motivational quote
            Random rand = new Random();
            lblMotivation.Text = motivationalQuotes[rand.Next(motivationalQuotes.Length)];

            // Load upcoming reminders
            LoadUpcomingReminders();

            // Start timers
            timerClock.Start();
            timerReminder.Start();
        }

        private void LoadSampleData()
        {
            // Add some sample reminders for demonstration
            reminders.Add(new Reminder
            {
                Id = 1,
                Title = "Take Morning Medication",
                Description = "Take blood pressure medicine with breakfast",
                ReminderTime = DateTime.Today.AddHours(8).AddMinutes(30),
                IsRecurring = true,
                Recurrence = RecurrenceType.Daily,
                Category = "Medication",
                Priority = "1",
                IsActive = true,
                IsCompleted = false  // FIXED: Added missing property initialization
            });

            reminders.Add(new Reminder
            {
                Id = 2,
                Title = "Lunch Time",
                Description = "Have a healthy lunch",
                ReminderTime = DateTime.Today.AddHours(12).AddMinutes(30),
                IsRecurring = true,
                Recurrence = RecurrenceType.Daily,
                Category = "Meal",
                Priority = "2",
                IsActive = true,
                IsCompleted = false  // FIXED: Added missing property initialization
            });

            reminders.Add(new Reminder
            {
                Id = 3,
                Title = "Afternoon Walk",
                Description = "30-minute walk in the park",
                ReminderTime = DateTime.Today.AddHours(15).AddMinutes(0),
                IsRecurring = true,
                Recurrence = RecurrenceType.Daily,
                Category = "Exercise",
                Priority = "2",
                IsActive = true,
                IsCompleted = false  // FIXED: Added missing property initialization
            });

            reminders.Add(new Reminder
            {
                Id = 4,
                Title = "Evening Medication",
                Description = "Take evening medicines before dinner",
                ReminderTime = DateTime.Today.AddHours(18).AddMinutes(0),
                IsRecurring = true,
                Recurrence = RecurrenceType.Daily,
                Category = "Medication",
                Priority = "1",
                IsActive = true,
                IsCompleted = false  // FIXED: Added missing property initialization
            });

            reminders.Add(new Reminder
            {
                Id = 5,
                Title = "Call Daughter",
                Description = "Video call with Sarah",
                ReminderTime = DateTime.Today.AddHours(19).AddMinutes(0),
                IsRecurring = false,
                Category = "Social",
                Priority = "2",
                IsActive = true,
                IsCompleted = false  // FIXED: Added missing property initialization
            });
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
                    int priorityNum = 2;
                    int.TryParse(reminder.Priority, out priorityNum);

                    string priority = priorityNum == 1 ? "🔴" : priorityNum == 2 ? "🟡" : "🟢";
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
            }
        }

        private void ShowReminderNotification(Reminder reminder)
        {
            string message = $"⏰ {reminder.Title}\n\n{reminder.Description}";
            string title = "Reminder Alert";

            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Navigation Button Events
        private void btnReminders_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            btnReminders.BackColor = Color.FromArgb(255, 192, 203);
            btnReminders.ForeColor = Color.White;
            btnReminders.Font = new Font("Segoe UI", 14, FontStyle.Bold);

            RemindersForm remindersForm = new RemindersForm();
            remindersForm.ShowDialog();

            // Refresh after returning
            LoadUpcomingReminders();  // FIXED: Added reload of upcoming reminders after dialog closes
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

            // Refresh after returning
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

            // Refresh after returning
            btnRoutines.BackColor = Color.Transparent;
            btnRoutines.ForeColor = Color.FromArgb(100, 100, 100);
            btnRoutines.Font = new Font("Segoe UI", 14, FontStyle.Regular);
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
            // Reset all navigation buttons to default state
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
        }
    }
}