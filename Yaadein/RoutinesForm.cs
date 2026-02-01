using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Yaadein.Models;
using Yaadein.Services;

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
            LoadSampleRoutines();
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

        private void LoadSampleRoutines()
        {
            var morningRoutine = new Routine
            {
                Id = 1,
                Name = "Morning Routine",
                Description = "Daily morning activities",
                StartTime = new TimeSpan(7, 0, 0),
                Category = RoutineCategories.Morning,
                IsActive = true
            };
            morningRoutine.Steps.Add(new RoutineStep { StepNumber = 1, Instruction = "Wake up and stretch for 2 minutes", DurationMinutes = 2 });
            morningRoutine.Steps.Add(new RoutineStep { StepNumber = 2, Instruction = "Brush teeth and wash face", DurationMinutes = 10 });
            morningRoutine.Steps.Add(new RoutineStep { StepNumber = 3, Instruction = "Get dressed for the day", DurationMinutes = 10 });
            morningRoutine.Steps.Add(new RoutineStep { StepNumber = 4, Instruction = "Eat a healthy breakfast", DurationMinutes = 20 });
            morningRoutine.Steps.Add(new RoutineStep { StepNumber = 5, Instruction = "Take morning medication", DurationMinutes = 5 });
            routines.Add(morningRoutine);

            var medicationRoutine = new Routine
            {
                Id = 2,
                Name = "Medication Schedule",
                Description = "Daily medication routine",
                StartTime = new TimeSpan(8, 30, 0),
                Category = RoutineCategories.Medication,
                IsActive = true
            };
            medicationRoutine.Steps.Add(new RoutineStep { StepNumber = 1, Instruction = "Take blood pressure medication with water", DurationMinutes = 2 });
            medicationRoutine.Steps.Add(new RoutineStep { StepNumber = 2, Instruction = "Take vitamin supplements", DurationMinutes = 2 });
            medicationRoutine.Steps.Add(new RoutineStep { StepNumber = 3, Instruction = "Record medication in log book", DurationMinutes = 3 });
            routines.Add(medicationRoutine);

            var exerciseRoutine = new Routine
            {
                Id = 3,
                Name = "Light Exercise",
                Description = "Daily physical activity",
                StartTime = new TimeSpan(15, 0, 0),
                Category = RoutineCategories.Exercise,
                IsActive = true
            };
            exerciseRoutine.Steps.Add(new RoutineStep { StepNumber = 1, Instruction = "Put on comfortable walking shoes", DurationMinutes = 3 });
            exerciseRoutine.Steps.Add(new RoutineStep { StepNumber = 2, Instruction = "Walk around the neighborhood", DurationMinutes = 20 });
            exerciseRoutine.Steps.Add(new RoutineStep { StepNumber = 3, Instruction = "Do light stretching exercises", DurationMinutes = 10 });
            exerciseRoutine.Steps.Add(new RoutineStep { StepNumber = 4, Instruction = "Drink water and rest", DurationMinutes = 5 });
            routines.Add(exerciseRoutine);

            var eveningRoutine = new Routine
            {
                Id = 4,
                Name = "Evening Wind Down",
                Description = "Prepare for bedtime",
                StartTime = new TimeSpan(20, 0, 0),
                Category = RoutineCategories.Evening,
                IsActive = true
            };
            eveningRoutine.Steps.Add(new RoutineStep { StepNumber = 1, Instruction = "Have a light dinner", DurationMinutes = 30 });
            eveningRoutine.Steps.Add(new RoutineStep { StepNumber = 2, Instruction = "Take evening medication", DurationMinutes = 5 });
            eveningRoutine.Steps.Add(new RoutineStep { StepNumber = 3, Instruction = "Brush teeth and wash up", DurationMinutes = 15 });
            eveningRoutine.Steps.Add(new RoutineStep { StepNumber = 4, Instruction = "Read or listen to calming music", DurationMinutes = 20 });
            eveningRoutine.Steps.Add(new RoutineStep { StepNumber = 5, Instruction = "Go to bed", DurationMinutes = 0 });
            routines.Add(eveningRoutine);
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
                case RoutineCategories.Morning: return "🌅";
                case RoutineCategories.Afternoon: return "☀️";
                case RoutineCategories.Evening: return "🌆";
                case RoutineCategories.Night: return "🌙";
                case RoutineCategories.Medication: return "💊";
                case RoutineCategories.Exercise: return "🏃";
                case RoutineCategories.Meal: return "🍽️";
                case RoutineCategories.Personal: return "🧼";
                case RoutineCategories.Social: return "👥";
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
                routines.Remove(currentRoutine);
                LoadRoutinesList();
                ClearForm();
                MessageBox.Show("Routine deleted successfully!", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            if (isEditMode)
            {
                UpdateRoutineFromForm(currentRoutine);
                MessageBox.Show("Routine updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Routine newRoutine = new Routine
                {
                    Id = routines.Count > 0 ? routines.Max(r => r.Id) + 1 : 1
                };
                UpdateRoutineFromForm(newRoutine);
                routines.Add(newRoutine);
                MessageBox.Show("Routine added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            LoadRoutinesList();
            ClearForm();
        }

        private void UpdateRoutineFromForm(Routine routine)
        {
            routine.Name = txtName.Text.Trim();
            routine.Description = txtDescription.Text.Trim();
            routine.StartTime = dtpStartTime.Value.TimeOfDay;
            routine.Category = cmbCategory.SelectedItem.ToString();
            routine.IsActive = chkActive.Checked;

            routine.Steps.Clear();
            foreach (ListViewItem item in lstSteps.Items)
            {
                routine.Steps.Add(new RoutineStep
                {
                    StepNumber = int.Parse(item.SubItems[0].Text),
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
                    $"💡 AI Suggestions for {routineType} Routine",
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