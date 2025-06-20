using System;
using System.Data;
using LibrarySystem_Data;
using Library_Business;
using System.Threading.Tasks;

namespace LibrarySystem_Business
{
    public class clsMember
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public int MembershipTypeID { get; set; }
        public clsMembershipType MembershipTypeInfo;
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsCancelled { get; set; }

        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        public string FullName
        {
            get
                => this.FirstName + " " + this.SecondName + " "
                    + this.ThirdName ?? string.Empty + " " + this.LastName;
        }

        public clsMember()
        {
            this.MemberID = -1;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = null;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.MinValue;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = null;
            this.ImagePath = null;
            this.MembershipTypeID = -1;
            this.StartDate = DateTime.MinValue;
            this.ExpiryDate = DateTime.MinValue;
            this.IsCancelled = false;
            this._Mode = enMode.AddNew;
        }

        private clsMember(int memberID, string firstName, string secondName, string thirdName,
            string lastName, DateTime dateOfBirth, string address, string phone, string email,
            string imagePath, int membershipTypeID,
            DateTime startDate, DateTime expiryDate, bool isCancelled)
        {
            MemberID = memberID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Address = address;
            Phone = phone;
            Email = email;
            ImagePath = imagePath;
            MembershipTypeID = membershipTypeID;
            MembershipTypeInfo = clsMembershipType.Find(MembershipTypeID);
            StartDate = startDate;
            ExpiryDate = expiryDate;
            IsCancelled = isCancelled;
            _Mode = enMode.Update;
        }

        private bool _AddNewMember()
        {
            this.MemberID = clsMemberData.AddNewMember(this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Address, this.Phone, this.Email, this.ImagePath,
                this.MembershipTypeID);
            return MemberID != -1;
        }

        private bool _UpdateMember()
        {
            return clsMemberData.UpdateMember(this.MemberID, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Address, this.Phone, this.Email, this.ImagePath,
                this.MembershipTypeID, this.IsCancelled);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateMember();
            }
            return false;
        }

        public static DataTable GetAllMembers() => clsMemberData.GetAllMembers();

        public async static Task<DataTable> GetAllMembersAsync() =>
            await clsMemberData.GetAllMembersAsync();
        public static clsMember Find(int MemberID)
        {
            string FirstName = string.Empty;
            string SecondName = string.Empty;
            string ThirdName = null;
            string LastName = string.Empty;
            DateTime DateOfBirth = DateTime.MinValue;
            string Address = string.Empty;
            string Phone = string.Empty;
            string Email = null;
            string ImagePath = null;
            int MembershipTypeID = -1;
            DateTime StartDate = DateTime.MinValue;
            DateTime ExpiryDate = DateTime.MinValue;
            bool IsCancelled = false;

            if (clsMemberData.Find(MemberID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, 
                ref Address, ref Phone, ref Email, ref ImagePath, ref MembershipTypeID,
                ref StartDate, ref ExpiryDate, ref IsCancelled))
                return new clsMember(MemberID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Address, Phone, 
                       Email, ImagePath, MembershipTypeID, StartDate, ExpiryDate, IsCancelled);
            else
                return null;
        }

        public static bool CancelMembership(int MemberID) => clsMemberData.UpdateCancel(MemberID, true);

        public static bool CanRenewMembership(DateTime ExpiryDate) => DateTime.Now >= ExpiryDate;

        public static bool RenewMembership(int MemberID) => clsMemberData.RenewMembership(MemberID);

        public static bool CanCancelMembership(bool IsCancelled) => !IsCancelled;

        public static bool CanBorrowBook(int MemberID)
        {
            clsMember member = clsMember.Find(MemberID);
            if(member == null) return false;
            if (clsMemberData.GetNumberOfBorrowedBook(MemberID) > member.MembershipTypeInfo.
                NumberOfAllowedBooksToBorrow)
                return false;
            if (clsFine.GetMemberUnpaidFees(member.MemberID) > 70)
                return false;
            return true;
        }
    }
}
