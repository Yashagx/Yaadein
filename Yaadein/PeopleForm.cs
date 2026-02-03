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
    public partial class PeopleForm : Form
    {
        private List<Person> people;
        private Person currentPerson;
        private bool isEditMode = false;
        private GroqService groqService;

        public PeopleForm()
        {
            InitializeComponent();
            people = new List<Person>();
            groqService = new GroqService();
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            LoadUserPeople();
            LoadPeopleList();
            ClearForm();
        }

        private void LoadUserPeople()
        {
            try
            {
                people = DatabaseHelper.GetUserPeople(DatabaseHelper.CurrentUserId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading people: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPeopleList()
        {
            lstPeople.Items.Clear();

            var sortedPeople = people.OrderBy(p => p.Name).ToList();

            foreach (var person in sortedPeople)
            {
                string favorite = person.IsFavorite ? "⭐" : "👤";
                string emergency = person.EmergencyContact == "Yes" ? "🚨" : "";
                string displayText = $"{favorite} {person.Name} - {person.Relationship} {emergency}";
                lstPeople.Items.Add(displayText);
            }

            if (lstPeople.Items.Count == 0)
            {
                lstPeople.Items.Add("No people added yet. Click 'Add New' to add someone!");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            ClearForm();
            EnableForm();
            lblFormTitle.Text = "Add New Person";
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a person to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isEditMode = true;
            EnableForm();
            lblFormTitle.Text = "Edit Person Details";
            txtName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a person to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to remove {currentPerson.Name} from your list?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.DeletePerson(currentPerson.Id, DatabaseHelper.CurrentUserId);
                    people.Remove(currentPerson);
                    LoadPeopleList();
                    ClearForm();

                    MessageBox.Show("Person removed successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting person: {ex.Message}", "Error",
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
                    UpdatePersonFromForm(currentPerson);
                    DatabaseHelper.SavePerson(currentPerson, DatabaseHelper.CurrentUserId);

                    MessageBox.Show($"{currentPerson.Name} updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Person newPerson = new Person
                    {
                        Id = 0
                    };
                    UpdatePersonFromForm(newPerson);
                    DatabaseHelper.SavePerson(newPerson, DatabaseHelper.CurrentUserId);
                    LoadUserPeople();

                    MessageBox.Show($"{newPerson.Name} added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadPeopleList();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving person: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePersonFromForm(Person person)
        {
            person.Name = txtName.Text.Trim();
            person.Relationship = txtRelationship.Text.Trim();
            person.PhoneNumber = txtPhone.Text.Trim();
            person.Email = txtEmail.Text.Trim();
            person.Address = txtAddress.Text.Trim();
            person.Notes = txtNotes.Text.Trim();
            person.FavoriteMemory = txtFavoriteMemory.Text.Trim();
            person.ImportantDetails = txtImportantDetails.Text.Trim();
            person.Birthday = dtpBirthday.Checked ? (DateTime?)dtpBirthday.Value : null;
            person.IsFavorite = false;
            person.EmergencyContact = chkEmergencyContact.Checked ? "Yes" : "No";
        }

        private async void btnAIRecall_Click(object sender, EventArgs e)
        {
            if (currentPerson == null)
            {
                MessageBox.Show("Please select a person first to use AI Memory Recall.",
                    "No Person Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnAIRecall.Enabled = false;
            btnAIRecall.Text = "🤖 Thinking...";

            try
            {
                string details = $"{currentPerson.ImportantDetails} {currentPerson.FavoriteMemory} {currentPerson.Notes}";
                string aiResponse = await groqService.RecallPersonInfoAsync(
                    currentPerson.Name,
                    currentPerson.Relationship,
                    details
                );

                MessageBox.Show(
                    aiResponse,
                    $"💭 Remembering {currentPerson.Name}",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    $"Unable to connect to AI service right now.\n\n{currentPerson.Name} is your {currentPerson.Relationship}.",
                    "Memory Recall",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            finally
            {
                btnAIRecall.Enabled = true;
                btnAIRecall.Text = "🤖 AI Memory Recall";
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

        private void lstPeople_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPeople.SelectedIndex < 0 || lstPeople.SelectedIndex >= people.Count)
                return;

            var sortedPeople = people.OrderBy(p => p.Name).ToList();

            if (lstPeople.SelectedIndex >= sortedPeople.Count)
                return;

            currentPerson = sortedPeople[lstPeople.SelectedIndex];
            DisplayPersonDetails(currentPerson);
        }

        private void DisplayPersonDetails(Person person)
        {
            txtName.Text = person.Name;
            txtRelationship.Text = person.Relationship;
            txtPhone.Text = person.PhoneNumber;
            txtEmail.Text = person.Email;
            txtAddress.Text = person.Address;
            txtNotes.Text = person.Notes;
            txtFavoriteMemory.Text = person.FavoriteMemory;
            txtImportantDetails.Text = person.ImportantDetails;

            if (person.Birthday.HasValue)
            {
                dtpBirthday.Value = person.Birthday.Value;
                dtpBirthday.Checked = true;
            }
            else
            {
                dtpBirthday.Checked = false;
            }

            chkEmergencyContact.Checked = person.EmergencyContact == "Yes";

            DisableForm();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for this person.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRelationship.Text))
            {
                MessageBox.Show("Please enter the relationship (e.g., Daughter, Friend, Doctor).",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRelationship.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtRelationship.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtNotes.Clear();
            txtFavoriteMemory.Clear();
            txtImportantDetails.Clear();
            dtpBirthday.Value = DateTime.Today;
            dtpBirthday.Checked = false;
            chkEmergencyContact.Checked = false;

            DisableForm();
            lblFormTitle.Text = "Person Details";
            currentPerson = null;
            isEditMode = false;
        }

        private void EnableForm()
        {
            txtName.Enabled = true;
            txtRelationship.Enabled = true;
            txtPhone.Enabled = true;
            txtEmail.Enabled = true;
            txtAddress.Enabled = true;
            txtNotes.Enabled = true;
            txtFavoriteMemory.Enabled = true;
            txtImportantDetails.Enabled = true;
            dtpBirthday.Enabled = true;
            chkEmergencyContact.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnAIRecall.Enabled = false;

            txtName.BackColor = Color.White;
            txtRelationship.BackColor = Color.White;
            txtPhone.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtAddress.BackColor = Color.White;
            txtNotes.BackColor = Color.White;
            txtFavoriteMemory.BackColor = Color.White;
            txtImportantDetails.BackColor = Color.White;
        }

        private void DisableForm()
        {
            txtName.Enabled = false;
            txtRelationship.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtAddress.Enabled = false;
            txtNotes.Enabled = false;
            txtFavoriteMemory.Enabled = false;
            txtImportantDetails.Enabled = false;
            dtpBirthday.Enabled = false;
            chkEmergencyContact.Enabled = false;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            btnAIRecall.Enabled = currentPerson != null;

            txtName.BackColor = Color.FromArgb(240, 240, 240);
            txtRelationship.BackColor = Color.FromArgb(240, 240, 240);
            txtPhone.BackColor = Color.FromArgb(240, 240, 240);
            txtEmail.BackColor = Color.FromArgb(240, 240, 240);
            txtAddress.BackColor = Color.FromArgb(240, 240, 240);
            txtNotes.BackColor = Color.FromArgb(240, 240, 240);
            txtFavoriteMemory.BackColor = Color.FromArgb(240, 240, 240);
            txtImportantDetails.BackColor = Color.FromArgb(240, 240, 240);
        }
    }
}