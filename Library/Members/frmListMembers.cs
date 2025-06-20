using System;
using System.Data;
using System.Windows.Forms;
using Library.Members;
using LibrarySystem_Business;
using System.Threading.Tasks;

namespace Library
{
    public partial class frmListMembers : Form
    {
        private DataTable _dtAllMembers;
        public frmListMembers() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private async Task _LoadDataAsync()
        {
            _dtAllMembers = await clsMember.GetAllMembersAsync();
            dgvMembers.DataSource = _dtAllMembers;
            lblRecords.Text = dgvMembers.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 5;
            cbIsCancelled.SelectedIndex = 0;
            cbMembershipType.SelectedIndex = 0;
        }

        private void _LoadUIData()
        {
            if (dgvMembers.Rows.Count > 0)
            {
                dgvMembers.Columns[0].HeaderText = "Member ID";
                dgvMembers.Columns[0].Width = 110;

                dgvMembers.Columns[1].HeaderText = "Full Name";
                dgvMembers.Columns[1].Width = 300;

                dgvMembers.Columns[2].HeaderText = "Date of Birth";
                dgvMembers.Columns[2].Width = 150;

                dgvMembers.Columns[3].HeaderText = "Phone";
                dgvMembers.Columns[3].Width = 150;

                dgvMembers.Columns[4].HeaderText = "Email";
                dgvMembers.Columns[4].Width = 200;

                dgvMembers.Columns[5].HeaderText = "Membership Type";
                dgvMembers.Columns[5].Width = 150;

                dgvMembers.Columns[6].HeaderText = "Start Date";
                dgvMembers.Columns[6].Width = 150;

                dgvMembers.Columns[7].HeaderText = "Expiry Date";
                dgvMembers.Columns[7].Width = 150;

                dgvMembers.Columns[8].HeaderText = "Is Cancelled?";
                dgvMembers.Columns[8].Width = 150;
            }
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();
            _LoadUIData();
        }

        private async void frmListMembers_Load(object sender, EventArgs e) => await _RefreshAsync();

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFilterBy.Text == "None")
            {
                txtFilter.Visible = false;
                cbIsCancelled.Visible = false;
                cbMembershipType.Visible = false;
            }
            else if(cmbFilterBy.Text != "Is Cancelled" && cmbFilterBy.Text != "Membership Type")
            {
                txtFilter.Visible = true;
                cbIsCancelled.Visible = false;
                cbMembershipType.Visible = false;
            }
            else if (cmbFilterBy.Text == "Is Cancelled")
            {
                txtFilter.Visible = false;
                cbIsCancelled.Visible = true;
                cbMembershipType.Visible = false;
            }
            else
            {
                txtFilter.Visible = false;
                cbIsCancelled.Visible = false;
                cbMembershipType.Visible = true;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;

            switch (cmbFilterBy.Text.Trim().ToLower())
            {
                case "member id":
                    RowFilter = "MemberID";
                    break;
                case "full name":
                    RowFilter = "FullName";
                    break;
                case "phone":
                    RowFilter = "Phone";
                    break;
                case "none":
                    RowFilter = "";
                    break;
            }

            DataView dv = _dtAllMembers.DefaultView;
            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = "";

            if (RowFilter == "MemberID")
            {
                if(int.TryParse(txtFilter.Text, out int result))
                    dv.RowFilter = $"{RowFilter} = {result}";
            }
            else
                dv.RowFilter = $"{RowFilter} like '{txtFilter.Text}%'";
            lblRecords.Text = dgvMembers.RowCount.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cmbFilterBy.Text == "Member ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbIsCancelled_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RowFilter = "Status"; // The query retrieves Is Cancelled as Status.
            string RowValue = string.Empty;

            switch(cbIsCancelled.Text)
            {
                case "All":
                    RowValue = "";
                    break;
                case "Active":
                    RowValue = "Active";
                    break;
                case "Cancelled":
                    RowValue = "Cancelled";
                    break;
            }
            DataView dv = _dtAllMembers.DefaultView;
            if (cbIsCancelled.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[{RowFilter}] = '{RowValue}'";
            lblRecords.Text = dgvMembers.Rows.Count.ToString();
        }

        private void cbMembershipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RowFilter = "MembershipName";
            string RowValue = string.Empty;

            switch (cbMembershipType.Text)
            {
                case "All":
                    RowValue = "";
                    break;
                case "Regular":
                    RowValue = clsMembershipType.enMembershipTypeID.Regular.ToString();
                    break;
                case "Student":
                    RowValue = clsMembershipType.enMembershipTypeID.Student.ToString();
                    break;
                case "Premium":
                    RowValue = clsMembershipType.enMembershipTypeID.Premium.ToString();
                    break;
            }
           
            DataView dv = _dtAllMembers.DefaultView;
            if (cbMembershipType.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[{RowFilter}] = '{RowValue}'";
            lblRecords.Text = dgvMembers.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frm = new frmAddUpdateMember();
            frm.ShowDialog();
            frmListMembers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frm = new frmAddUpdateMember((int)dgvMembers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListMembers_Load(null, null);
        }

        private async Task _Renew()
        {
            if (clsMember.RenewMembership((int)dgvMembers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Membership is renewed successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Membership is not renewed, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            await _RefreshAsync();
        }

        private async void renewMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew this membership?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            await _Renew();
        }

        private async Task _Cancel()
        {
            if (clsMember.CancelMembership((int)dgvMembers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Membership is cancelled successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Membership is not cancelled, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            await _RefreshAsync();
        }

        private async void cancelMembershipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this membership?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            await _Cancel();
        }

        private void cmsMember_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!clsMember.CanRenewMembership((DateTime)dgvMembers.CurrentRow.Cells[7].Value))
                renewMembershipToolStripMenuItem.Enabled = false;

            if ((string)dgvMembers.CurrentRow.Cells[8].Value == "Cancelled")
            {
                cancelMembershipToolStripMenuItem.Enabled = false;
                renewMembershipToolStripMenuItem.Enabled = false;
            }
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMemberInfo frm = new frmMemberInfo((int)dgvMembers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
