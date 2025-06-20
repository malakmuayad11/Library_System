using System;
using System.Data;
using Library_Data;
using LibrarySystem_Business;
using System.Threading.Tasks;

namespace Library_Business
{
    public class clsFine
    {
        public int FineID { get; set; }
        public int MemberID {  get; set; }
        public clsMember MemberInfo;
        public int LoanID { get; set; }
        public clsLoan LoanInfo;
        public float FineAmount { get; set; }
        public bool IsPaid { get; set; }

        public enum enMode { AddNew = 1, Update = 2}
        private enMode _Mode { get; set; }

        public clsFine()
        {
            this.FineID = -1;
            this.MemberID = -1;
            this.LoanID = -1;
            this.FineAmount = -1;
            this.IsPaid = false;
            this._Mode = enMode.AddNew;
        }

        private bool _AddNewFine()
        {
            this.FineID = clsFineData.AddNewFine(this.MemberID, this.LoanID,
                this.FineAmount, this.IsPaid);
            return (this.FineID != -1);
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (_AddNewFine())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static DataTable GetAllFines() => clsFineData.GetAllFines();
        public async static Task<DataTable> GetAllFinesAsync()
            => await clsFineData.GetAllFinesAsync();

        public static bool UpdatePaymentStatus(int FineID, bool IsPaid) =>
            clsFineData.UpdatePaymentStatus(FineID, IsPaid);

        public static bool UpdateFineAmount(int FineID, float Amount) =>
            clsFineData.UpdateFineAmount(FineID, Amount);

        public static bool PayFines(int FineID) => UpdatePaymentStatus(FineID, true);

        public static float? GetMemberUnpaidFees(int MemberID) => 
            clsFineData.GetMemberUnpaidFines(MemberID);
    }
}
