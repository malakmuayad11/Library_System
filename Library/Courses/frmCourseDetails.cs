using System;
using System.Windows.Forms;

namespace Library.Courses
{
    public partial class frmCourseDetails : Form
    {
        private int _CourseID;
        public frmCourseDetails(int CourseID)
        {
            InitializeComponent();
            _CourseID = CourseID;
        }

        private void btnClose_Click(object sender, EventArgs e) =>
            this.Close();

        private void frmCourseDetails_Load(object sender, EventArgs e) =>
            ctrlCourseInfo1.LoadData(_CourseID);
    }
}
