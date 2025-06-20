using System;
using System.Data;
using System.Windows.Forms;
using Library.Users;
using Library_Business;
using System.Threading.Tasks;

namespace Library
{
    public partial class frmListUsers : Form
    {
        private DataTable _dtAllUsers;
        public frmListUsers() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _LoadUIData()
        {
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 4;
            cbIsActive.SelectedIndex = 0;

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Username";
                dgvUsers.Columns[1].Width = 150;

                dgvUsers.Columns[2].HeaderText = "Role";
                dgvUsers.Columns[2].Width = 150;

                dgvUsers.Columns[3].HeaderText = "Is Active";
                dgvUsers.Columns[3].Width = 100;
            }
        }

        private async void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = await clsUser.GetAllUsersAsync();
            dgvUsers.DataSource = _dtAllUsers;
            _LoadUIData();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtAllUsers.DefaultView;

            if (cbRole.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[Role] = '{cbRole.Text}'";
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txtFilter.Visible = false;
                cbRole.Visible = false;
                cbIsActive.Visible = false;
            }
            else if (cmbFilterBy.Text != "Role" && cmbFilterBy.Text != "Status")
            {
                txtFilter.Visible = true;
                cbRole.Visible = false;
                cbIsActive.Visible = false;
            }
            else if(cmbFilterBy.Text == "Status")
            {
                txtFilter.Visible = false;
                cbRole.Visible = false;
                cbIsActive.Visible = true;
            }
            else
            {
                txtFilter.Visible = false;
                cbRole.Visible = true;
                cbIsActive.Visible = false;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;

            switch (cmbFilterBy.Text.Trim().ToLower())
            {
                case "user id":
                    RowFilter = "UserID";
                    break;
                case "username":
                    RowFilter = "Username";
                    break;
                case "none":
                    RowFilter = "";
                    break;
            }

            DataView dv = _dtAllUsers.DefaultView;

            if (RowFilter == "UserID")
            {
                if(int.TryParse(txtFilter.Text.Trim(), out int UserID))
                    dv.RowFilter = $"[{RowFilter}] = {UserID}";
            }
            else
                dv.RowFilter = $"[{RowFilter}] like '{txtFilter.Text}%'";

            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = "";

            lblRecords.Text = dgvUsers.RowCount.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "User ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtAllUsers.DefaultView;
            string FilterValue = string.Empty;
            switch(cbIsActive.Text)
            {
                case "Active":
                    FilterValue = "1";
                    break;
                case "Inactive":
                    FilterValue = "0";
                    break;
            }
            if (cbIsActive.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[IsActive] = {FilterValue}";
            lblRecords.Text = dgvUsers.Rows.Count.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUser frm = new frmAddUser();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }
    }
}
