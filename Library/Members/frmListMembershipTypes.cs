using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using LibrarySystem_Business;

namespace LibrarySystem_Presentation
{
    public partial class frmListMembershipTypes : Form
    {
        private DataTable _dtMembershipTypes;
        public frmListMembershipTypes() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _LoadUIData()
        {
            dgvMembershipTypes.DataSource = _dtMembershipTypes;
            if (dgvMembershipTypes.Rows.Count > 0)
            {
                dgvMembershipTypes.Columns[0].HeaderText = "Membership Type ID";
                dgvMembershipTypes.Columns[0].Width = 110;

                dgvMembershipTypes.Columns[1].HeaderText = "Membership Type ";
                dgvMembershipTypes.Columns[1].Width = 110;

                dgvMembershipTypes.Columns[2].HeaderText = "Number of Allowed Books to Borrow";
                dgvMembershipTypes.Columns[2].Width = 150;
            }
            lblRecords.Text = dgvMembershipTypes?.Rows?.Count.ToString();
        }

        private async void frmListMembershipTypes_Load(object sender, EventArgs e)
        {
            _dtMembershipTypes = await clsMembershipType.GetAllMembershipTypesAsync();
            _LoadUIData();
        }
    }
}
