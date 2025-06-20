using Library.Global_Classes;
using Library_Business;
using System;
using System.Windows.Forms;

namespace Library.Courses
{
    public partial class frmAddUpdateCourse : Form
    {
        private clsCourse _Course;
        private enum enMode { AddNew = 1, Update  = 2 }
        private enMode _Mode;
        public frmAddUpdateCourse()
        {
            InitializeComponent();
            _Course = new clsCourse();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateCourse(int CourseID)
        {
            InitializeComponent();
            _Course = clsCourse.Find(CourseID);
            _Mode = enMode.Update;
        }

        private void _LoadUpdateData()
        {
            this.Text = "Edit Course";
            lblMode.Text = "Edit Course";
            lblID.Text = _Course.CourseID.ToString();
            txtCourseName.Text = _Course.CourseName;
            nudEnrollmentFees.Value = Convert.ToDecimal(_Course.EnrollmentFees);
            nudMaxParticipants.Value = Convert.ToDecimal(_Course.MaxParticipants);
            txtNotes.Text = _Course.Notes ?? string.Empty;
            dtpStartDate.Value = _Course.StartDate;
            dtpEndDate.Value = _Course.EndDate;
            txtFirstName.Text = _Course.TutorFirstName;
            txtLastName.Text = _Course.TutorLastName;
        }

        private void _LoadData()
        {
            if (_Mode == enMode.AddNew)
            {
                dtpStartDate.Value = DateTime.Now;
                dtpEndDate.Value = DateTime.Now;
                return;
            }
            if (_Course == null)
            {
                MessageBox.Show("An error occurred, and this screen will be closed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            _LoadUpdateData();
        }

        private void frmAddCourse_Load(object sender, EventArgs e) => _LoadData();

        private void _ValidateRequiredFields(TextBox txt, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "This filed is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
            }
        }

        private void txt_Validating(object sender, System.ComponentModel.CancelEventArgs e) =>
            _ValidateRequiredFields((TextBox)sender, e);

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _SaveCourseInfo()
        {
            _Course.CourseName = txtCourseName.Text.Trim();
            _Course.EnrollmentFees = Convert.ToSingle(nudEnrollmentFees.Value);
            _Course.MaxParticipants = Convert.ToByte(nudMaxParticipants.Value);
            _Course.StartDate = dtpStartDate.Value;
            _Course.EndDate = dtpEndDate.Value;
            _Course.TutorFirstName = txtFirstName.Text.Trim();
            _Course.TutorLastName = txtLastName.Text.Trim();
            _Course.Notes = txtNotes.Text.Trim();
        }

        private void _DisableFields()
        {
            txtCourseName.Enabled = false;
            nudEnrollmentFees.Enabled = false;
            nudMaxParticipants.Enabled = false;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            gbTutor.Enabled = false;
            txtNotes.Enabled = false;
            btnSave.Enabled = false;
        }

        private void _AddNewModeSave()
        {
            if (_Course.Save())
            {
                MessageBox.Show("Course is launched successfully!", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblID.Text = _Course.CourseID.ToString();
                _DisableFields();
            }
            else
                MessageBox.Show("An error occurred during course launchig", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _UpdateModeSave()
        {
            if(_Course.Save()) 
                 MessageBox.Show("Course is updated successfully!", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("An error occurred during course updating", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("There are missing files, please check red circles.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _SaveCourseInfo();
            if (_Mode == enMode.AddNew)
                _AddNewModeSave();
            else
                _UpdateModeSave();
        }

        private void frmAddCourse_Activated(object sender, EventArgs e) => txtCourseName.Focus();
    }
}
