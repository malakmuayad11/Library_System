using System;
using System.Data;
using System.Threading.Tasks;
using Library_Data;
using LibrarySystem_Business;

namespace Library_Business
{
    public class clsLoan
    {
        public int LoanID { get; set; }
        public int BookID { get; set; }
        public clsBook BookInfo;
        public int MemberID { get; set; }
        public clsMember MemberInfo;

        public DateTime LoanStartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public float? FineAmount { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public enum enMode { AddNew = 1, Update = 2}
        private enMode _Mode;

        public clsLoan()
        {
            this.LoanID = -1;
            this.BookID = -1;
            this.MemberID = -1;
            this.LoanStartDate = DateTime.MinValue;
            this.DueDate = DateTime.MinValue;
            this.ReturnDate = null;
            this.FineAmount = null;
            this.CreatedByUserID = -1;
            this._Mode = enMode.AddNew;
        }

        private clsLoan(int loanID, int bookID, int memberID, DateTime loanStartDate, DateTime dueDate,
            DateTime? returnDate, float? fineAmount, int createdByUserID)
        {
            LoanID = loanID;
            BookID = bookID;
            BookInfo = clsBook.Find(bookID);
            MemberID = memberID;
            MemberInfo = clsMember.Find(memberID);
            LoanStartDate = loanStartDate;
            DueDate = dueDate;
            ReturnDate = returnDate;
            FineAmount = fineAmount;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUser.Find(createdByUserID);
            this._Mode = enMode.Update;
        }

        public static DataTable GetAllLoans() => clsLoanData.GetAllLoans();

        public async static Task<DataTable> GetAllLoansAsync() => await clsLoanData.GetAllLoansAsync();

        private bool _AddNewLoan()
        {
            this.LoanID = clsLoanData.AddNewLoan(this.BookID, this.MemberID, CreatedByUserID);
            return this.LoanID != -1;
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLoan())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        private float? _CalculateLoanAmount(DateTime DueDate, DateTime ReturnDate)
        {
            TimeSpan t = new TimeSpan();
            if (ReturnDate < DueDate)
                return null;
            t = ReturnDate - DueDate;
            return t.Days * 5; // It is specified by the system that for each late day, pay 5.
        }

        private bool _AddFine()
        {
            if (FineAmount.HasValue)
            {
                clsFine Fine = new clsFine();
                Fine.MemberID = this.MemberID;
                Fine.LoanID = this.LoanID;
                Fine.FineAmount = FineAmount.Value;
                return Fine.Save();
            }
            return true;
        }

        public bool Return(int LoanID)
        {
            clsBook ReturnedBook = clsBook.Find(this.BookID);
            if(ReturnedBook == null)
                return false;
            ReturnedBook.SetAvailabilityStatus(clsBook.enAvailabilityStatus.Available);
            float? FineAmount = _CalculateLoanAmount(this.DueDate, DateTime.Now);
            if (!_AddFine()) return false;
            return clsLoanData.ReturnLoan(LoanID, DateTime.Now, FineAmount);
        }

        public static clsLoan Find(int LoanID)
        {
            int BookID = -1;
            int MemberID = -1;
            DateTime LoanStartDate = DateTime.MinValue;
            DateTime DueDate = DateTime.MinValue;
            DateTime? ReturnDate = null;
            float? FineAmount = null;
            int CreatedByUserID = -1;

            if (clsLoanData.Find(LoanID, ref BookID, ref MemberID, ref LoanStartDate,
                ref DueDate, ref ReturnDate, ref FineAmount, ref CreatedByUserID))
                return new clsLoan(LoanID, BookID, MemberID, LoanStartDate, DueDate,
                    ReturnDate, FineAmount, CreatedByUserID);
            else
                return null;
        }

        public static clsLoan FindByMemberID(int MemberID)
        {
            int BookID = -1;
            int LoanID = -1;
            DateTime LoanStartDate = DateTime.MinValue;
            DateTime DueDate = DateTime.MinValue;
            DateTime? ReturnDate = null;
            float? FineAmount = null;
            int CreatedByUserID = -1;

            if (clsLoanData.FindByMemberID(MemberID,ref LoanID, ref BookID, ref LoanStartDate,
                ref DueDate, ref ReturnDate, ref FineAmount, ref CreatedByUserID))
                return new clsLoan(LoanID, BookID, MemberID, LoanStartDate, DueDate,
                    ReturnDate, FineAmount, CreatedByUserID);
            else
                return null;
        }

        public static bool CanReturnBook(int LoanID) => clsLoanData.CanReturnBook(LoanID);

        public static bool CanExtendLoan(int LoanID) => Find(LoanID).ReturnDate.HasValue ? false : true;

        public static bool ExtendDueDate(int LoanID, DateTime DueDate) =>
            clsLoanData.UpdateDueDate(LoanID, DueDate.AddDays(14));
    }
}
