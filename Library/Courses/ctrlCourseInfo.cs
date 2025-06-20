using Library_Business;
using System;
using System.Windows.Forms;
using Library.Global_Classes;

namespace Library.Courses
{
    public partial class ctrlCourseInfo : UserControl
    {
        private clsCourse _Course;
        public ctrlCourseInfo() => InitializeComponent();

        public void LoadData(int CourseID)
        {
            _Course = clsCourse.Find(CourseID);
            if (_Course == null)
            {
                MessageBox.Show("The course is not found, an error occurred.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llEditCourseInfo.Enabled = false;
                return;
            }
            lblID.Text = _Course.CourseID.ToString();
            lblCourseName.Text = _Course.CourseName;
            lblTutorName.Text = _Course.TutorName;
            lblStatus.Text = _Course.Status;
            lblStartDate.Text = _Course.StartDate.ToString(clsFormat.DateFormat);
            lblEndDate.Text = _Course.EndDate.ToString(clsFormat.DateFormat);
            lblEnrollmentFees.Text = _Course.EnrollmentFees.ToString();
            lblNotes.Text = _Course.Notes ?? string.Empty;
            lblMaxParticipants.Text = _Course.MaxParticipants.ToString();
            lblEnrolledParticipants.Text = _Course.NumberOfEnrolledMembers.ToString();
        }

        private void llEditCourseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdateCourse(_Course.CourseID);
            frm.ShowDialog();
        }
    }
}
