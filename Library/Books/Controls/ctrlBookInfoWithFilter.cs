using System;
using System.Windows.Forms;
using Library_Business;

namespace Library.Books
{
    public partial class ctrlBookInfoWithFilter : UserControl
    {
        public class BookInfoEventArgs : EventArgs
        {
            public clsBook Book { get; }
            public BookInfoEventArgs(clsBook book)
            {
                Book = book;
            }
        }
        public event EventHandler<BookInfoEventArgs> OnBookSelected;
        private void RaiseBookSelected(BookInfoEventArgs e) =>
            OnBookSelected?.Invoke(this, e);
        protected void RaiseBookSelected(clsBook Book) =>
            RaiseBookSelected(new BookInfoEventArgs(Book));
        public ctrlBookInfoWithFilter() => InitializeComponent();

        public void FilterFocus() => txtBookID.Focus();

        public void DisableFilter() => gbFilter.Enabled = false;

        private void btnFind_Click(object sender, EventArgs e)
        {
            ctrlBookInfo1.LoadBook(Convert.ToInt32(txtBookID.Text));
            RaiseBookSelected(ctrlBookInfo1.SelectedBook);
        }

        private void txtBookID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)13)
                btnFind.PerformClick();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateBook frm = new frmAddUpdateBook();
            frm.BookSaved += Frm_BookSaved;
            frm.ShowDialog();
        }

        private void Frm_BookSaved(object sender, frmAddUpdateBook.BookInfoEventArgs e)
        {
            txtBookID.Text = e.BookID.ToString();
            ctrlBookInfo1.LoadBook(e.BookID);
        }
    }
}
