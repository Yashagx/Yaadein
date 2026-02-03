using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Yaadein.Models;
using Yaadein.Data;

namespace Yaadein
{
    public partial class RemindersForm : Form
    {
        private List<Reminder> reminders;
        private Reminder currentReminder;
        private bool isEditMode = false;

        public RemindersForm()
        {
            InitializeComponent();
            reminders = new List<Reminder>();
        }

        private void RemindersForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
            LoadUserReminders();
            LoadRemindersList();
            ClearForm();
        }

        private void InitializeForm()
        {
            cmbCategory.Items.AddRange(ReminderCategories.GetAll());
            cmbCategory.SelectedIndex = 0;

            cmbRecurrence.Items.AddRange(new string[] { "None", "Daily", "Weekly", "Monthly" });
            cmbRecurrence.SelectedIndex = 0;

            numPriority.Minimum = 1;
            numPriority.Maximum = 3;
            numPriority.Value = 2;

            dtpDate.Format = DateTimePickerFormat.Long;
            dtpDate.Value = DateTime.Today;

            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ShowUpDown = true;
            dtpTime.Value = DateTime.Today.AddHours(9);

            chkRecurring.Checked = false;
            cmbRecurrence.Enabled = false;
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

        private void LoadRemindersList()
        {
            lstReminders.Items.Clear();

            var activeReminders = reminders
                .Where(r => r.IsActive && !r.IsCompleted)
                .OrderBy(r => r.ReminderTime)
                .ToList();

            foreach (var reminder in activeReminders)
            {
                string priority = reminder.Priority == 1 ? "🔴" : reminder.Priority == 2 ? "🟡" : "🟢";
                string time = reminder.ReminderTime.ToString("MM/dd hh:mm tt");
                string displayText = $"{priority} {time} - {reminder.Title}";
                lstReminders.Items.Add(displayText);
            }

            if (lstReminders.Items.Count == 0)
            {
                lstReminders.Items.Add("No reminders yet. Click 'Add New' to create one!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            ClearForm();
            EnableForm();
            lblFormTitle.Text = "Add New Reminder";
            txtTitle.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstReminders.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a reminder to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isEditMode = true;
            EnableForm();
            lblFormTitle.Text = "Edit Reminder";
            txtTitle.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstReminders.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a reminder to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this reminder?\n\n{currentReminder.Title}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.DeleteReminder(currentReminder.Id, DatabaseHelper.CurrentUserId);
                    reminders.Remove(currentReminder);
                    LoadRemindersList();
                    ClearForm();

                    MessageBox.Show("Reminder deleted successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting reminder: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                if (isEditMode)
                {
                    currentReminder.Title = txtTitle.Text.Trim();
                    currentReminder.Description = txtDescription.Text.Trim();
                    currentReminder.ReminderTime = dtpDate.Value.Date.Add(dtpTime.Value.TimeOfDay);
                    currentReminder.IsRecurring = chkRecurring.Checked;
                    currentReminder.Recurrence = GetRecurrenceType();
                    currentReminder.Category = cmbCategory.SelectedItem.ToString();
                    currentReminder.Priority = (int)numPriority.Value;

                    DatabaseHelper.SaveReminder(currentReminder, DatabaseHelper.CurrentUserId);

                    MessageBox.Show("Reminder updated successfully!", "Updated",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var newReminder = new Reminder
                    {
                        Id = 0,
                        Title = txtTitle.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        ReminderTime = dtpDate.Value.Date.Add(dtpTime.Value.TimeOfDay),
                        IsRecurring = chkRecurring.Checked,
                        Recurrence = GetRecurrenceType(),
                        Category = cmbCategory.SelectedItem.ToString(),
                        Priority = (int)numPriority.Value,
                        IsActive = true,
                        IsCompleted = false
                    };

                    DatabaseHelper.SaveReminder(newReminder, DatabaseHelper.CurrentUserId);
                    LoadUserReminders();

                    MessageBox.Show("Reminder added successfully!", "Added",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadRemindersList();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving reminder: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstReminders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstReminders.SelectedIndex < 0)
                return;

            var activeReminders = reminders
                .Where(r => r.IsActive && !r.IsCompleted)
                .OrderBy(r => r.ReminderTime)
                .ToList();

            if (lstReminders.SelectedIndex >= activeReminders.Count)
                return;

            currentReminder = activeReminders[lstReminders.SelectedIndex];
            DisplayReminderDetails(currentReminder);
        }

        private void DisplayReminderDetails(Reminder reminder)
        {
            txtTitle.Text = reminder.Title;
            txtDescription.Text = reminder.Description;
            dtpDate.Value = reminder.ReminderTime.Date;
            dtpTime.Value = reminder.ReminderTime;
            chkRecurring.Checked = reminder.IsRecurring;
            cmbCategory.SelectedItem = reminder.Category;
            numPriority.Value = reminder.Priority;

            if (reminder.IsRecurring)
            {
                cmbRecurrence.Enabled = true;
                switch (reminder.Recurrence)
                {
                    case RecurrenceType.Daily:
                        cmbRecurrence.SelectedIndex = 1;
                        break;
                    case RecurrenceType.Weekly:
                        cmbRecurrence.SelectedIndex = 2;
                        break;
                    case RecurrenceType.Monthly:
                        cmbRecurrence.SelectedIndex = 3;
                        break;
                    default:
                        cmbRecurrence.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                cmbRecurrence.SelectedIndex = 0;
                cmbRecurrence.Enabled = false;
            }

            DisableForm();
        }

        private void chkRecurring_CheckedChanged(object sender, EventArgs e)
        {
            cmbRecurrence.Enabled = chkRecurring.Checked;
            if (!chkRecurring.Checked)
            {
                cmbRecurrence.SelectedIndex = 0;
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Please enter a title for the reminder.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (dtpDate.Value.Date.Add(dtpTime.Value.TimeOfDay) < DateTime.Now && !isEditMode)
            {
                MessageBox.Show("Reminder time cannot be in the past.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private RecurrenceType GetRecurrenceType()
        {
            if (!chkRecurring.Checked || cmbRecurrence.SelectedIndex == 0)
                return RecurrenceType.None;

            switch (cmbRecurrence.SelectedIndex)
            {
                case 1: return RecurrenceType.Daily;
                case 2: return RecurrenceType.Weekly;
                case 3: return RecurrenceType.Monthly;
                default: return RecurrenceType.None;
            }
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            dtpDate.Value = DateTime.Today;
            dtpTime.Value = DateTime.Today.AddHours(9);
            chkRecurring.Checked = false;
            cmbRecurrence.SelectedIndex = 0;
            cmbRecurrence.Enabled = false;
            cmbCategory.SelectedIndex = 0;
            numPriority.Value = 2;

            DisableForm();
            lblFormTitle.Text = "Reminder Details";
            currentReminder = null;
            isEditMode = false;
        }

        private void EnableForm()
        {
            txtTitle.Enabled = true;
            txtDescription.Enabled = true;
            dtpDate.Enabled = true;
            dtpTime.Enabled = true;
            chkRecurring.Enabled = true;
            cmbRecurrence.Enabled = chkRecurring.Checked;
            cmbCategory.Enabled = true;
            numPriority.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            txtTitle.BackColor = Color.White;
            txtDescription.BackColor = Color.White;
        }

        private void DisableForm()
        {
            txtTitle.Enabled = false;
            txtDescription.Enabled = false;
            dtpDate.Enabled = false;
            dtpTime.Enabled = false;
            chkRecurring.Enabled = false;
            cmbRecurrence.Enabled = false;
            cmbCategory.Enabled = false;
            numPriority.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            txtTitle.BackColor = Color.FromArgb(240, 240, 240);
            txtDescription.BackColor = Color.FromArgb(240, 240, 240);
        }
    }
}