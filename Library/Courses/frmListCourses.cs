using Library_Business;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.Courses
{
    public partial class frmListCourses : Form
    {
        private DataTable _dtCourses;
        public frmListCourses() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _LoadUIData()
        {
            dgvCourses.DataSource = _dtCourses;
            lblRecords.Text = dgvCourses.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 4;

            if (dgvCourses.Rows.Count > 0)
            {
                dgvCourses.Columns[0].HeaderText = "Course ID";
                dgvCourses.Columns[0].Width = 110;

                dgvCourses.Columns[1].HeaderText = "Course Name";
                dgvCourses.Columns[1].Width = 300;

                dgvCourses.Columns[2].HeaderText = "Tutor Name";
                dgvCourses.Columns[2].Width = 150;

                dgvCourses.Columns[3].HeaderText = "Enrollment Fees";
                dgvCourses.Columns[3].Width = 150;

                dgvCourses.Columns[4].HeaderText = "Max Participants";
                dgvCourses.Columns[4].Width = 200;

                dgvCourses.Columns[5].HeaderText = "Start Date";
                dgvCourses.Columns[5].Width = 150;

                dgvCourses.Columns[6].HeaderText = "End Date";
                dgvCourses.Columns[6].Width = 150;
            }
        }

        private async Task _LoadDataAsync()
        {
            _dtCourses = await clsCourse.GetAllCoursesAsync();
            _LoadUIData();
        }

        private async void frmListCourses_Load(object sender, EventArgs e) => await _LoadDataAsync();

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateCourse frm = new frmAddUpdateCourse();
            frm.ShowDialog();
            await _LoadDataAsync(); //Refresh
        }

        private void enrollAMemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEnrollMemberInCourse frm = new frmEnrollMemberInCourse((int)dgvCourses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void showCourseDetailsToolStripMenuItem_Click(object sender, EventArgs e) =>
            new frmCourseDetails((int)dgvCourses.CurrentRow.Cells[0].Value).ShowDialog();

        private async void launchNewCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAddUpdateCourse().ShowDialog();
            await _LoadDataAsync();
        }

        private void listParticipantsToolStripMenuItem_Click(object sender, EventArgs e) =>
            new frmEnrolledMembers((int)dgvCourses.CurrentRow.Cells[0].Value).ShowDialog();

        private async void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAddUpdateCourse((int)dgvCourses.CurrentRow.Cells[0].Value).ShowDialog();
            await _LoadDataAsync();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cmbFilterBy.Text != "None");
            dtpFrom.Enabled = (cmbFilterBy.Text != "None");
            dtpTo.Enabled = (cmbFilterBy.Text != "None");
        }

        private void _FilterData()
        {
            string RowFilter = cmbFilterBy.Text;
            switch (cmbFilterBy.Text)
            {
                case "Course ID":
                    RowFilter = "CourseID";
                    break;
                case "Course Name":
                    RowFilter = "CourseName";
                    break;
                case "Tutor Name":
                    RowFilter = "TutorName";
                    break;
                case "Enrollment Fees":
                    RowFilter = "EnrollmentFees";
                    break;
            }

            DataView dv = _dtCourses.DefaultView;
            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = $"StartDate >= '{dtpFrom.Value}' and StartDate <= '{dtpTo.Value}'";

            if (cmbFilterBy.Text != "Course ID" && cmbFilterBy.Text != "Enrollment Fees")
                dv.RowFilter = $"{RowFilter} like '{txtFilter.Text}%'" +
                    $" and StartDate >= '{dtpFrom.Value}' and StartDate <= '{dtpTo.Value}'";
            else
            {
                int.TryParse(txtFilter.Text.Trim(), out int RowValue);
                dv.RowFilter = $"{RowFilter} = {RowValue} and StartDate >= '{dtpFrom.Value}'" +
                    $" and StartDate <= '{dtpTo.Value}'";
            }
            lblRecords.Text = dgvCourses.RowCount.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e) => _FilterData();

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "Course ID" || cmbFilterBy.Text == "Enrollment Fees")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dtp_ValueChanged(object sender, EventArgs e) => _FilterData();
    }
}
