using Library_Business;
using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Library.Courses
{
    public partial class frmEnrolledMembers : Form
    {
        private int _CourseID;
        private DataTable _dtAllMembers;
        public frmEnrolledMembers(int CourseID)
        {
            InitializeComponent();
            _CourseID = CourseID;
        }

        private void _LoadUIData()
        {
            lblParticipantsCount.Text = dgvEnrolledMembers.Rows.Count.ToString();

            if (dgvEnrolledMembers.Rows.Count > 0)
            {
                dgvEnrolledMembers.Columns[0].HeaderText = "Course ID";
                dgvEnrolledMembers.Columns[0].Width = 110;

                dgvEnrolledMembers.Columns[1].HeaderText = "Member ID";
                dgvEnrolledMembers.Columns[1].Width = 110;

                dgvEnrolledMembers.Columns[2].HeaderText = "Member Name";
                dgvEnrolledMembers.Columns[2].Width = 150;

                dgvEnrolledMembers.Columns[3].HeaderText = "Phone";
                dgvEnrolledMembers.Columns[3].Width = 150;

                dgvEnrolledMembers.Columns[4].HeaderText = "Enrollment Date";
                dgvEnrolledMembers.Columns[4].Width = 150;
            }
        }

        private async Task _LoadDataAsync()
        {
            _dtAllMembers = await Task.Run(() => clsCourse.GetAllMembersForCourseAsync(_CourseID));
            ctrlCourseInfo1.LoadData(_CourseID);
            dgvEnrolledMembers.DataSource = _dtAllMembers;
            _LoadUIData();
        }

        private async void frmEnrolledMembers_Load(object sender, EventArgs e) => await _LoadDataAsync();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new frmEnrollMemberInCourse(_CourseID).ShowDialog();
            frmEnrolledMembers_Load(null, null); //Refresh
        }
    }
}
