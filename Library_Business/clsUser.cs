using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Library_Data;

namespace Library_Business
{
    public class clsUser
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public enum enRole { Admin = 1, Staff = 2 }
        public enRole Role { get; set; }
        public bool IsActive { get; set; }
        public enum enPermissions { eAll = -1, eManageMembers = 1, eManageBooks = 2, eManageCourses = 4,
        eManageUsers = 8, eManagePayments = 16 }
        
        public enPermissions Permissions { get; set; }
        public enum enMode { AddNew = 1, Update = 2}

        private enMode _Mode;

        public clsUser()
        {
            this.UserID = -1;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Role = enRole.Staff;
            this.IsActive = false;
            this._Mode = enMode.AddNew;
            this.Permissions = enPermissions.eAll;
        }

        private clsUser(int userID, string username, string password, enRole Role,
            bool IsActive, enPermissions Permission)
        {
            UserID = userID;
            Username = username;
            Password = password;
            this.Role = Role;
            this.IsActive = IsActive;
            this._Mode = enMode.Update;
            this.Permissions = Permission;
        }

        public static clsUser Find(int UserID)
        {
            string Username = string.Empty;
            string Password = string.Empty;
            enRole Role = enRole.Staff;
            byte role = (byte)Role;
            bool IsActive = false;
            enPermissions Permissions = enPermissions.eAll;
            int permissions = (int)Permissions;

            if (clsUserData.Find(UserID, ref Username, ref Password, ref role, ref IsActive, ref permissions))
                return new clsUser(UserID, Username, Password, (enRole)role, IsActive, (enPermissions)permissions);
            else
                return null;
        }

        public async static Task<clsUser> Find(string Username, string Password)
        {
            int UserID = -1;
            enRole Role = enRole.Staff;
            byte role = (byte)Role;
            bool IsActive = false;
            enPermissions Permissions = enPermissions.eAll;
            int permissions = (int)Permissions;

            if (clsUserData.Find(Username, clsUtil.ComputeHash(Password), ref UserID,
                ref role, ref IsActive, ref permissions))
                return new clsUser(UserID, Username, Password, (enRole)role, IsActive,
                    (enPermissions)permissions);
            else
                return null;
        }
      

        public bool ChangePassword(string NewPassword) => 
            clsUserData.UpdatePassword(this.UserID, clsUtil.ComputeHash(NewPassword));

        public static DataTable GetAllUsers() => clsUserData.GetAllUsers();

        public async static Task<DataTable> GetAllUsersAsync() =>
            await clsUserData.GetAllUsersAsync();

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.Username, clsUtil.ComputeHash(this.Password),
                (byte)this.Role, this.IsActive, (int)this.Permissions);
            return (this.UserID != -1);
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    if (this._AddNewUser())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
            }
            return false;
        }

        public static bool DoesUsernameExist(string Username) =>
            clsUserData.DoesUsernameExist(Username);

        /// <summary>
        /// This method checks whether a specific user has a specific permission.
        /// </summary>
        /// <param name="UserPermissions">The user's stored permissions.</param>
        /// <param name="PermissionToCheck">The permission intented to be checked if the user has.</param>
        /// <returns>Whether the user has the specific permission or not.</returns>
        public static bool CheckIfUserHasPermission(int UserPermissions, enPermissions PermissionToCheck)
            => (UserPermissions & (int)PermissionToCheck) != 0;
        /// <summary>
        /// This method checks wheter a user used a specified password previously.
        /// </summary>
        /// <param name="UserID">The user's id to check for.</param>
        /// <param name="Password">The password to check if it was used previously.</param>
        /// <returns>Wheter a user used a specified password previously.</returns>
        public static bool IsPasswordUsedByUser(int UserID, string Password) =>
            clsUserData.IsPasswordUsedByUser(UserID, Password);
    }
}
