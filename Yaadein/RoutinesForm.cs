using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Yaadein.Models;
using Yaadein.Services;
using Yaadein.Data;

namespace Yaadein
{
    public partial class RoutinesForm : Form
    {
        private List<Routine> routines;
        private Routine currentRoutine;
        private bool isEditMode = false;
        private GroqService groqService;

        public RoutinesForm()
        {
            InitializeComponent();
            routines = new List<Routine>();
            groqService = new GroqService();
        }

        private void RoutinesForm_Load(object sender, EventArgs e)
        {
            InitializeForm();
            LoadUserRoutines();
            LoadRoutinesList();
            ClearForm();
        }

        private void InitializeForm()
        {
            cmbCategory.Items.AddRange(RoutineCategories.GetAll());
            cmbCategory.SelectedIndex = 0;

            dtpStartTime.Value = DateTime.Today.AddHours(8);
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;

            btnSave.Enabled = false;
        }

        private void LoadUserRoutines()
        {
            try
            {
                routines = DatabaseHelper.GetUserRoutines(DatabaseHelper.CurrentUserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading routines: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoutinesList()
        {
            lstRoutines.Items.Clear();

            var activeRoutines = routines
                .Where(r => r.IsActive)
                .OrderBy(r => r.StartTime)
                .ToList();

            foreach (var routine in activeRoutines)
            {
                string icon = GetCategoryIcon(routine.Category);
                string displayText = $"{icon} {routine.StartTime:hh\\:mm tt} - {routine.Name} ({routine.Steps.Count} steps)";
                lstRoutines.Items.Add(displayText);
            }

            if (lstRoutines.Items.Count == 0)
            {
                lstRoutines.Items.Add("No routines yet. Click 'Add New' to create one!");
            }
        }

        private string GetCategoryIcon(string category)
        {
            switch (category)
            {
                case "Morning Routine": return "🌅";
                case "Afternoon Routine": return "☀️";
                case "Evening Routine": return "🌆";
                case "Night Routine": return "🌙";
                case "Medication Routine": return "💊";
                case "Exercise Routine": return "🏃";
                case "Meal Routine": return "🍽️";
                case "Personal Care": return "🧼";
                case "Social Activity": return "👥";
                default: return "📋";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            ClearForm();
            EnableForm();
            lblFormTitle.Text = "Add New Routine";
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a routine to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isEditMode = true;
            EnableForm();
            lblFormTitle.Text = "Edit Routine";
            txtName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a routine to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this routine?\n\n{currentRoutine.Name}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.DeleteRoutine(currentRoutine.Id, DatabaseHelper.CurrentUserId);
                    routines.Remove(currentRoutine);
                    LoadRoutinesList();
                    ClearForm();

                    MessageBox.Show("Routine deleted successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting routine: {ex.Message}", "Error",
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
                    UpdateRoutineFromForm(currentRoutine);
                    DatabaseHelper.SaveRoutine(currentRoutine, DatabaseHelper.CurrentUserId);
                    MessageBox.Show("Routine updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Routine newRoutine = new Routine
                    {
                        Id = 0,
                        Steps = new List<RoutineStep>()
                    };
                    UpdateRoutineFromForm(newRoutine);
                    DatabaseHelper.SaveRoutine(newRoutine, DatabaseHelper.CurrentUserId);
                    LoadUserRoutines();
                    MessageBox.Show("Routine added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadRoutinesList();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving routine: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRoutineFromForm(Routine routine)
        {
            routine.Name = txtName.Text.Trim();
            routine.Description = txtDescription.Text.Trim();
            routine.StartTime = dtpStartTime.Value.TimeOfDay;
            routine.Category = cmbCategory.SelectedItem.ToString();
            routine.IsActive = chkActive.Checked;

            routine.Steps.Clear();
            for (int i = 0; i < lstSteps.Items.Count; i++)
            {
                ListViewItem item = lstSteps.Items[i];
                routine.Steps.Add(new RoutineStep
                {
                    StepNumber = i + 1,
                    Instruction = item.SubItems[1].Text,
                    DurationMinutes = int.Parse(item.SubItems[2].Text.Replace(" min", ""))
                });
            }
        }

        private void btnAddStep_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStepInstruction.Text))
            {
                MessageBox.Show("Please enter step instruction.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stepNumber = lstSteps.Items.Count + 1;
            ListViewItem item = new ListViewItem(stepNumber.ToString());
            item.SubItems.Add(txtStepInstruction.Text.Trim());
            item.SubItems.Add($"{numStepDuration.Value} min");

            lstSteps.Items.Add(item);

            txtStepInstruction.Clear();
            numStepDuration.Value = 5;
            txtStepInstruction.Focus();
        }

        private void btnRemoveStep_Click(object sender, EventArgs e)
        {
            if (lstSteps.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a step to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lstSteps.Items.Remove(lstSteps.SelectedItems[0]);

            for (int i = 0; i < lstSteps.Items.Count; i++)
            {
                lstSteps.Items[i].SubItems[0].Text = (i + 1).ToString();
            }
        }

        private async void btnAISuggest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbCategory.Text))
            {
                MessageBox.Show("Please select a category first.", "Category Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnAISuggest.Enabled = false;
            btnAISuggest.Text = "🤖 Thinking...";

            try
            {
                string routineType = cmbCategory.SelectedItem.ToString();
                string preferences = txtDescription.Text;

                string aiResponse = await groqService.SuggestRoutineAsync(routineType, preferences);

                MessageBox.Show(
                    aiResponse,
                    $"💡 AI Suggestions for {routineType}",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Unable to get AI suggestions right now. Please create your routine manually.",
                    "AI Service",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            finally
            {
                btnAISuggest.Enabled = true;
                btnAISuggest.Text = "🤖 AI Suggest Steps";
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

        private void lstRoutines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRoutines.SelectedIndex < 0)
                return;

            var activeRoutines = routines
                .Where(r => r.IsActive)
                .OrderBy(r => r.StartTime)
                .ToList();

            if (lstRoutines.SelectedIndex >= activeRoutines.Count)
                return;

            currentRoutine = activeRoutines[lstRoutines.SelectedIndex];
            DisplayRoutineDetails(currentRoutine);
        }

        private void DisplayRoutineDetails(Routine routine)
        {
            txtName.Text = routine.Name;
            txtDescription.Text = routine.Description;
            dtpStartTime.Value = DateTime.Today.Add(routine.StartTime);
            cmbCategory.SelectedItem = routine.Category;
            chkActive.Checked = routine.IsActive;

            lstSteps.Items.Clear();
            foreach (var step in routine.Steps)
            {
                ListViewItem item = new ListViewItem(step.StepNumber.ToString());
                item.SubItems.Add(step.Instruction);
                item.SubItems.Add($"{step.DurationMinutes} min");
                lstSteps.Items.Add(item);
            }

            DisableForm();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for the routine.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (lstSteps.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one step to the routine.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtDescription.Clear();
            dtpStartTime.Value = DateTime.Today.AddHours(8);
            cmbCategory.SelectedIndex = 0;
            chkActive.Checked = true;
            lstSteps.Items.Clear();
            txtStepInstruction.Clear();
            numStepDuration.Value = 5;

            DisableForm();
            lblFormTitle.Text = "Routine Details";
            currentRoutine = null;
            isEditMode = false;
        }

        private void EnableForm()
        {
            txtName.Enabled = true;
            txtDescription.Enabled = true;
            dtpStartTime.Enabled = true;
            cmbCategory.Enabled = true;
            chkActive.Enabled = true;
            txtStepInstruction.Enabled = true;
            numStepDuration.Enabled = true;
            btnAddStep.Enabled = true;
            btnRemoveStep.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnAISuggest.Enabled = true;

            txtName.BackColor = Color.White;
            txtDescription.BackColor = Color.White;
            txtStepInstruction.BackColor = Color.White;
        }

        private void DisableForm()
        {
            txtName.Enabled = false;
            txtDescription.Enabled = false;
            dtpStartTime.Enabled = false;
            cmbCategory.Enabled = false;
            chkActive.Enabled = false;
            txtStepInstruction.Enabled = false;
            numStepDuration.Enabled = false;
            btnAddStep.Enabled = false;
            btnRemoveStep.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAISuggest.Enabled = false;

            txtName.BackColor = Color.FromArgb(240, 240, 240);
            txtDescription.BackColor = Color.FromArgb(240, 240, 240);
            txtStepInstruction.BackColor = Color.FromArgb(240, 240, 240);
        }
    }
}