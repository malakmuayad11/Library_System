using Library_Business;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Library.Courses
{
    public partial class frmEnrollMemberInCourse : Form
    {
        private int _CourseID;
        private int _MemberID;
        private clsCourse _Course;
        public frmEnrollMemberInCourse(int CourseID)
        {
            InitializeComponent();
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - 100,
                Screen.PrimaryScreen.WorkingArea.Height - 100);

            _CourseID = CourseID;
            _Course = clsCourse.Find(_CourseID);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmEnrollMemberInCourse_Load(object sender, EventArgs e) =>
            ctrlCourseInfo1.LoadData(_CourseID);

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_Course.Status != "Comming Soon")
            {
                MessageBox.Show("Sorry, applying for this course is not available", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsCourse.EnrollMemberInCourse(_MemberID, _CourseID))
            {
                btnSave.Enabled = false;
                ctrlCourseInfo1.Enabled = false;
                ctrlMemberInfoWithFilter1.Enabled = false;
                MessageBox.Show("Member is enrolled in the course successfully", "Enrolled in Course!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("An error occurred during course enrollment or the course is full.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlMemberInfoWithFilter1_MemberFound(object sender, Members.ctrlMemberInfoWithFilter.
            MemberInfoEventArgs e)
        {
            if(e.IsFound)
                _MemberID = e.MemberID;
        }

        private void frmEnrollMemberInCourse_Activated(object sender, EventArgs e) =>
            ctrlMemberInfoWithFilter1.FilterFocus();
    }
}
