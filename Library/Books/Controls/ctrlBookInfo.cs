using System;
using System.Windows.Forms;
using Library.Global_Classes;
using Library_Business;

namespace Library.Books
{
    public partial class ctrlBookInfo : UserControl
    {
        private clsBook _Book;
        private clsAuthor _Author;
        public ctrlBookInfo() => InitializeComponent();

        public clsBook SelectedBook
        {
            get => _Book; 
        }

        private void _LoadData()
        {
            lblID.Text = _Book.BookID.ToString();
            lblTitle.Text = _Book.Title;
            lblGenre.Text = _Book.Genre;
            lblISBN.Text = _Book.ISBN;
            lblCondition.Text = _Book.Condition.ToString();
            lblPublicationDate.Text = _Book.PublicationDate.ToString(clsFormat.DateFormat);
            lblStatus.Text = _Book.AvailabilityStatus.ToString();
            lblLanguage.Text = _Book.Language.ToString();
            lblFirstName.Text = _Author.FirstName;
            lblLastName.Text = _Author.LastName;
        }

        public void LoadBook(int BookID)
        {
            _Book = clsBook.Find(BookID);
            _Author = clsAuthor.FindByBookID(BookID);

            if (_Book == null || _Author == null)
            {
                MessageBox.Show("The book is not found, an error occurred", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadData();
        }
    }
}
