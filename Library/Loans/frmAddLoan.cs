using System;
using System.Windows.Forms;
using Library.Global_Classes;
using Library_Business;
using LibrarySystem_Business;

namespace Library.Loans
{
    public partial class frmAddLoan : Form
    {
        private clsLoan _Loan;
        private clsBook _SelectedBook;
        public frmAddLoan()
        {
            InitializeComponent();
            _Loan = new clsLoan();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void btnNext_Click(object sender, EventArgs e) => tabControl1.SelectTab(1);

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!clsMember.CanBorrowBook(_Loan.MemberID))
            {
                MessageBox.Show("This member is not allowed to borrow.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Loan.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if(_Loan.Save())
            {
                MessageBox.Show("The loan is created successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                ctrlBookInfoWithFilter1.DisableFilter();
                ctrlMemberInfoWithFilter1.DisableFilter();
            }
            else
                MessageBox.Show("The loan is not created, an error occurred", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ctrlBookInfoWithFilter1_OnBookSelected(object sender, Books.ctrlBookInfoWithFilter
            .BookInfoEventArgs e)
        {
            if(e.Book != null && e.Book.AvailabilityStatus != clsBook.
                enAvailabilityStatus.Available)
                MessageBox.Show("This book isn't available, and borrowing it is denied", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            if  (e.Book == null ||
                e.Book.AvailabilityStatus != clsBook.
                enAvailabilityStatus.Available)
            {
                btnNext.Enabled = false;
                btnSave.Enabled = false;
                ctrlBookInfoWithFilter1.DisableFilter();
                ctrlMemberInfoWithFilter1.DisableFilter();
            }
            else
            {
                _SelectedBook = e.Book;
                _Loan.BookID = e.Book.BookID;
            }
        }

        private void ctrlMemberInfoWithFilter1_MemberFound(object sender, Members.
            ctrlMemberInfoWithFilter.MemberInfoEventArgs e)
        { 
            btnSave.Enabled = e.IsFound;
            if(e.IsFound) 
                _Loan.MemberID = e.MemberID;
        }

        private void frmAddLoan_Activated(object sender, EventArgs e)
        {
            ctrlBookInfoWithFilter1.FilterFocus();
            ctrlMemberInfoWithFilter1.FilterFocus();
        }
    }
}
