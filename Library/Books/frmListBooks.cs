using System;
using System.Data;
using System.Windows.Forms;
using Library_Business;
using System.Threading.Tasks;

namespace Library.Books
{
    public partial class frmListBooks : Form
    {

        private DataTable _dtAllBooks;
        public frmListBooks() => InitializeComponent();

        private async Task _LoadDataAsync()
        {
            _dtAllBooks = await clsBook.GetAllBooksAsync();
            dgvBooks.DataSource = _dtAllBooks;
            lblRecords.Text = dgvBooks.Rows.Count.ToString();
            cmbFilterBy.SelectedIndex = 5;
            cbAvailabilityStatus.SelectedIndex = 0;
            cbCondition.SelectedIndex = 0;
        }

        private async Task _RefreshAsync()
        {
            await _LoadDataAsync();
            if (dgvBooks.Rows.Count > 0)
            {
                dgvBooks.Columns[0].HeaderText = "Book ID";
                dgvBooks.Columns[0].Width = 100;

                dgvBooks.Columns[1].HeaderText = "Title";
                dgvBooks.Columns[1].Width = 200;

                dgvBooks.Columns[2].HeaderText = "Genre";
                dgvBooks.Columns[2].Width = 200;

                dgvBooks.Columns[3].HeaderText = "Condition";
                dgvBooks.Columns[3].Width = 150;

                dgvBooks.Columns[4].HeaderText = "Availability Status";
                dgvBooks.Columns[4].Width = 150;

                dgvBooks.Columns[5].HeaderText = "Author";
                dgvBooks.Columns[5].Width = 250;
            }
        }

        private async void frmListBooks_Load(object sender, EventArgs e) => await _RefreshAsync();

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void cmbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterBy.Text == "None")
            {
                txtFilter.Visible = false;
                cbCondition.Visible = false;
                cbAvailabilityStatus.Visible = false;
            }
            else if (cmbFilterBy.Text != "Condition" && cmbFilterBy.Text != "Availability Status")
            {
                txtFilter.Visible = true;
                cbCondition.Visible = false;
                cbAvailabilityStatus.Visible = false;
            }
            else if (cmbFilterBy.Text == "Condition")
            {
                txtFilter.Visible = false;
                cbCondition.Visible = true;
                cbAvailabilityStatus.Visible = false;
            }
            else
            {
                txtFilter.Visible = false;
                cbCondition.Visible = false;
                cbAvailabilityStatus.Visible = true;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string RowFilter = cmbFilterBy.Text;
            if (cmbFilterBy.Text == "Author")
                RowFilter = "FullName";

            DataView dv = _dtAllBooks.DefaultView;
            if (txtFilter.Text.Trim() == "" || cmbFilterBy.Text == "None")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"{RowFilter} like '{txtFilter.Text}%'";
            lblRecords.Text = dgvBooks.RowCount.ToString();
        }

        private void cbAvailabilityStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtAllBooks.DefaultView;
            if (cbAvailabilityStatus.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[AvailabilityStatus] = '{cbAvailabilityStatus.Text}'";
            lblRecords.Text = dgvBooks.Rows.Count.ToString();
        }

        private void cbCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = _dtAllBooks.DefaultView;
            if (cbCondition.Text == "All")
                dv.RowFilter = "";
            else
                dv.RowFilter = $"[Condition] = '{cbCondition.Text}'";
            lblRecords.Text = dgvBooks.Rows.Count.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook();
            frm.ShowDialog();
            frmListBooks_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook((int)dgvBooks.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListBooks_Load(null, null);
        }

        private void _Delete()
        {
            if (clsBook.DeleteBook((int)dgvBooks.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("Book is deleted successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                 frmListBooks_Load(null, null); // Refresh
            }
            else
                MessageBox.Show("Book is not deleted, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this book?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                _Delete();
        }

        private void showInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowBookInfo frm = new frmShowBookInfo((int)dgvBooks.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cmsBook_Opening(object sender, System.ComponentModel.CancelEventArgs e) =>
            deleteToolStripMenuItem.Enabled = (string)dgvBooks.CurrentRow.Cells[3].Value
                == "Damaged" ? true : false;
    }
}
