using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library_Business;

namespace Library.Payments
{
    public partial class frmListPayments : Form
    {
        private DataTable _dtAllFines;
        public frmListPayments() => InitializeComponent();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _LoadUIData()
        {
            lblRecords.Text = dgvPayments?.Rows?.Count.ToString();
            cmbFilterBy.SelectedIndex = 5;
            cbIsPaid.SelectedIndex = 0;

            if (dgvPayments.Rows.Count > 0)
            {
                dgvPayments.Columns[0].HeaderText = "Payment ID";
                dgvPayments.Columns[0].Width = 110;

                dgvPayments.Columns[1].HeaderText = "Member ID";
                dgvPayments.Columns[1].Width = 110;

                dgvPayments.Columns[2].HeaderText = "Loan ID";
                dgvPayments.Columns[2].Width = 150;

                dgvPayments.Columns[3].HeaderText = "Payment Amount";
                dgvPayments.Columns[3].Width = 150;

                dgvPayments.Columns[4].HeaderText = "Is Paid";
                dgvPayments.Columns[4].Width = 150;
            }
        }

        private async Task _LoadDataAsync()
        {
            _dtAllFines = await clsFine.GetAllFinesAsync();
            dgvPayments.DataSource = _dtAllFines;
            _LoadUIData();
        }

        private async void frmListPayments_Load(object sender, EventArgs e) => await _LoadDataAsync();

        private async void payFineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsFine.PayFines((int)dgvPayments.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Fines are paid successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _LoadDataAsync();
            }
            else
                MessageBox.Show("An error occurred during payment.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cmsPayment_Opening(object sender, CancelEventArgs e) =>
                payFinesToolStripMenuItem.Enabled = !(bool)dgvPayments.CurrentRow.Cells[4].Value;

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e) =>
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;

            switch(cmbFilterBy.Text)
            {
                case "Payment ID":
                    RowFilter = "FineID";
                    break;
                case "Loan ID":
                    RowFilter = "LoanID";
                    break;
                case "Member ID":
                    RowFilter = "MemberID";
                    break;
                case "Fine Amount":
                    RowFilter = "FineAmount";
                    break;
            }
            DataView dv = _dtAllFines.DefaultView;
            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = "";
            else
            {
                if (int.TryParse(txtFilter.Text, out int result))
                    dv.RowFilter = $"[{RowFilter}] = {result}";
            }

            lblRecords.Text = dv.Count.ToString();
        }

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFilterBy.Text == "None")
            {
                txtFilter.Visible = false;
                cbIsPaid.Visible = false;
            }
            else if(cmbFilterBy.Text != "Is Paid")
            {
                txtFilter.Visible = true;
                cbIsPaid.Visible = false;
            }
            else
            {
                txtFilter.Visible = false ;
                cbIsPaid.Visible = true;
            }
        }

        private void cbIsPaid_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte RowValue = 0;

            switch(cbIsPaid.Text)
            {
                case "Paid":
                    RowValue = 1;
                    break;
                case "Not Paid":
                    RowValue = 0;
                    break;
            }
            DataView dv = _dtAllFines.DefaultView;

            if (cmbFilterBy.Text == "None" || cbIsPaid.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[IsPaid] = {RowValue}";
        }
    }
}
