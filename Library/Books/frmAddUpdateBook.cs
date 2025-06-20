using System;
using System.Windows.Forms;
using Library_Business;
using LibrarySystem_Business;
using static System.Net.Mime.MediaTypeNames;

namespace Library.Books
{
    public partial class frmAddUpdateBook : Form
    {
        public class BookInfoEventArgs : EventArgs
        {
            public int BookID { get; }
            public BookInfoEventArgs(int BookID)
            {
                this.BookID = BookID;
            }
        }

        public event EventHandler<BookInfoEventArgs> BookSaved;
        private void RaiseBookSaved(BookInfoEventArgs e) =>
            BookSaved?.Invoke(this, e);

        protected void RaiseBookSaved(int BookID) =>
            RaiseBookSaved(new BookInfoEventArgs(BookID));

        private int _BookID;
        private clsBook _Book;
        private clsAuthor _Author;
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;
        public frmAddUpdateBook()
        {
            InitializeComponent();
            _BookID = 0;
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateBook(int BookID)
        {
            InitializeComponent();
            _BookID = BookID;
            _Mode = enMode.Update;
            _Book = clsBook.Find(_BookID);
            _Author = clsAuthor.FindByBookID(BookID);
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _LoadUpdateData()
        {
            lblID.Text = _Book.BookID.ToString();
            txtTitle.Text = _Book.Title;
            txtGenre.Text = _Book.Genre;
            txtISBN.Text = _Book.ISBN;
            dtPublicationDate.Value = _Book.PublicationDate;
            cbCondition.SelectedIndex = (byte)_Book.Condition - 1;
            cbLanguage.SelectedIndex = (byte)_Book.Language - 1;
            cbStatus.SelectedIndex = (byte)_Book.AvailabilityStatus - 1;
            txtFirstName.Text = _Author.FirstName;
            txtLastName.Text = _Author.LastName;

            txtTitle.Enabled = false;
            txtGenre.Enabled = false;
            txtISBN.Enabled = false;
            dtPublicationDate.Enabled = false;
            cbLanguage.Enabled = false;
            gbAuthor.Enabled = false;
        }

        private void _LoadData()
        {
            dtPublicationDate.MaxDate = DateTime.Now;

            if (_Mode == enMode.AddNew)
            {
                _Book = new clsBook();
                this.Text = "Add New Book";
                lblMode.Text = "Add New Book";
                cbCondition.SelectedIndex = 0;
                cbStatus.SelectedIndex = 0;
                cbLanguage.SelectedIndex = 0;
                return;
            }
            
            this.Text = "Update Book";
            lblMode.Text = "Update Book";
            if (_Book == null || _Author == null)
            {
                MessageBox.Show("An error occurred and the screen will be closed", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            _LoadUpdateData();
        }

        private void frmAddUpdateBook_Load(object sender, EventArgs e) => _LoadData();

        private void _ValidateRequiredFields(TextBox txt, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "This filed is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
            }
        }

        private void txt_Validating(object sender, System.ComponentModel.CancelEventArgs e) =>
            _ValidateRequiredFields((TextBox)sender, e);

        private void _SaveAddMode()
        {
            _Book.Title = txtTitle.Text.Trim();
            _Book.Genre = txtGenre.Text.Trim();
            _Book.ISBN = txtISBN.Text.Trim();
            _Book.Condition = (clsBook.enCondition)cbCondition.SelectedIndex + 1;
            _Book.PublicationDate = dtPublicationDate.Value;
            _Book.AvailabilityStatus = (clsBook.enAvailabilityStatus)cbStatus.SelectedIndex + 1;
            _Book.Language = (clsBook.enLanguage)cbLanguage.SelectedIndex + 1;

            if (_Book.Save(txtFirstName.Text.Trim(), txtLastName.Text.Trim()))
            {
                RaiseBookSaved(_Book.BookID);
                MessageBox.Show("Data is saved successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                this.Text = "Update Book";
                lblMode.Text = "Update Book";
                lblID.Text = _Book.BookID.ToString();
            }
            else
                MessageBox.Show("Data is not saved, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void _SaveUpdateMode()
        {
            if (_Book.SetCondition((clsBook.enCondition)cbCondition.SelectedIndex + 1) &&
                _Book.SetAvailabilityStatus((clsBook.enAvailabilityStatus)cbStatus.SelectedIndex + 1))
                MessageBox.Show("Data is saved successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Data is not saved, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please make sure that all required fields are not empty", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.AddNew)
                _SaveAddMode();
            else
                _SaveUpdateMode();
        }

        private void txtISBN_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_Mode == enMode.Update)
                return;

            _ValidateRequiredFields(txtISBN, e);
            if (clsBook.DoesISBNExist(txtISBN.Text.Trim()))
            {
                e.Cancel = true;
                txtISBN.Focus();
                errorProvider1.SetError(txtISBN, "This ISBN already exists!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtISBN, "");
            }
        }
    }
}
