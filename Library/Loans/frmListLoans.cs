using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library_Business;

namespace Library.Loans
{
    public partial class frmListLoans : Form
    {
        private DataTable _dtAllLoans;
        public frmListLoans() => InitializeComponent();

        private void _LoadUIData()
        {
            dgvLoans.DataSource = _dtAllLoans;
            cmbFilterBy.SelectedIndex = 4;

            if (dgvLoans.Rows.Count > 0)
            {
                dgvLoans.Columns[0].HeaderText = "Loan ID";
                dgvLoans.Columns[0].Width = 100;

                dgvLoans.Columns[1].HeaderText = "Book Title";
                dgvLoans.Columns[1].Width = 250;

                dgvLoans.Columns[2].HeaderText = "Member Name";
                dgvLoans.Columns[2].Width = 250;

                dgvLoans.Columns[3].HeaderText = "Loan Start Date";
                dgvLoans.Columns[3].Width = 100;

                dgvLoans.Columns[4].HeaderText = "Loan Due Date";
                dgvLoans.Columns[4].Width = 100;

                dgvLoans.Columns[5].HeaderText = "Loan Return Date";
                dgvLoans.Columns[5].Width = 100;

                dgvLoans.Columns[6].HeaderText = "Fine Amount";
                dgvLoans.Columns[6].Width = 80;

                dgvLoans.Columns[7].HeaderText = "Created By User";
                dgvLoans.Columns[7].Width = 180;
            }
            lblRecords.Text = dgvLoans.Rows.Count.ToString();
        }

        private async Task _LoadDataAsync()
        {
            _dtAllLoans = await clsLoan.GetAllLoansAsync();
            _LoadUIData();
        }

        private async void frmListLoans_Load(object sender, EventArgs e) => await _LoadDataAsync();

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e) =>
            txtFilter.Visible = cmbFilterBy.Text != "None";

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = string.Empty;

            switch(cmbFilterBy.Text)
            {
                case "Loan ID":
                    RowFilter = "LoanID";
                    break;
                case "Book Title":
                    RowFilter = "Title";
                    break;
                case "Member Name":
                    RowFilter = "FullName";
                    break;
                case "Username":
                    RowFilter = "Username";
                    break;
            }
            DataView dv= _dtAllLoans.DefaultView;

            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = "";

            if (RowFilter != "LoanID")
                dv.RowFilter = $"[{RowFilter}] like '{txtFilter.Text}%'";
            else
            {
                if(int.TryParse(txtFilter.Text, out int loanID))
                    dv.RowFilter = $"[{RowFilter}] = {loanID}";
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmbFilterBy.Text == "Loan ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddLoan frm = new frmAddLoan();
            frm.ShowDialog();
            await _LoadDataAsync(); // Refresh
        }

        private async void _Return()
        {
            clsLoan Loan = clsLoan.Find((int)dgvLoans.CurrentRow.Cells[0].Value);
            if (Loan.Return(Loan.LoanID))
            {
                MessageBox.Show("Book is returned successfully, please check payments if applicable.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _LoadDataAsync();
            }
            else
                MessageBox.Show("Failed to return Loan, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void returnBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to return book?", "Confirm",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                _Return();
        }

        private void cmsLoan_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            returnBookToolStripMenuItem.Enabled = clsLoan.CanReturnBook
            ((int)dgvLoans.CurrentRow.Cells[0].Value);

            extendDueDateToolStripMenuItem.Enabled = clsLoan.CanExtendLoan
                ((int)dgvLoans.CurrentRow.Cells[0].Value);
        }

        private async void extendDueDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsLoan.ExtendDueDate((int)dgvLoans.CurrentRow.Cells[0].Value,
                (DateTime)dgvLoans.CurrentRow.Cells[4].Value))
            {
                MessageBox.Show("Loan is extended successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _LoadDataAsync(); //Refresh
            }
            else
                MessageBox.Show("An error occurred during loan extending.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
