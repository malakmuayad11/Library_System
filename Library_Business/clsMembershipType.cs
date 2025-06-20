using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Library_Data;
using LibrarySystem_Data;

namespace LibrarySystem_Business
{
    public class clsMembershipType
    {
        public enum enMembershipTypeID { Regular = 1, Student = 2, Premium = 3 }

        public int MembershipTypeID { get; set; }
        public string MembershipName { get; set; }
        public byte NumberOfAllowedBooksToBorrow { get; set; }

        public enum enMode { AddNew = 0, Update = 1}
        private enMode _Mode;
        
        public clsMembershipType()
        {
            this.MembershipTypeID = -1;
            this.MembershipName = string.Empty;
            this.NumberOfAllowedBooksToBorrow = 0;
            this._Mode = enMode.AddNew;
        }

        private clsMembershipType(enMembershipTypeID membershipTypeID, string membershipName, byte numberOfAllowedBooksToBorrow)
        {
            MembershipTypeID = (int)membershipTypeID;
            MembershipName = membershipName;
            NumberOfAllowedBooksToBorrow = numberOfAllowedBooksToBorrow;
            _Mode = enMode.Update;
        }

        public static DataTable GetAllMembershipTypes() =>
            clsMembershipTypeData.GetAllMembershipTypes();

        public async static Task<DataTable> GetAllMembershipTypesAsync() =>
           await clsMembershipTypeData.GetAllMembershipTypesAsync();

        public static clsMembershipType Find(int MembershipTypeID)
        {
            string MembershipName = string.Empty;
            byte NumberOfAllowedBooksToBorrow = 0;

            if (clsMembershipTypeData.Find(MembershipTypeID, ref MembershipName, ref NumberOfAllowedBooksToBorrow))
                return new clsMembershipType((enMembershipTypeID)MembershipTypeID, MembershipName, NumberOfAllowedBooksToBorrow);
            else
                return null;
        }
    }
}
