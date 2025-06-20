using System;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Library.Books;
using Library.Courses;
using Library.Global_Classes;
using Library.Loans;
using Library.Payments;
using Library.Users;
using Library_Business;
using LibrarySystem_Presentation;

namespace Library
{
    public partial class frmMainScreen : Form
    {
        private Form _LoginForm;

        private bool _DoesCurrentUserHavePermission(clsUser.enPermissions PermissionToCheck)
        {
            if(!clsUser.CheckIfUserHasPermission((int)clsGlobal.CurrentUser.Permissions, PermissionToCheck))
            {
                MessageBox.Show("Sorry, you don't have access to this section, please contact your admin", 
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public frmMainScreen(Form LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
        }

        private void manageUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageUsers))
            {
                frmListUsers frm = new frmListUsers();
                frm.ShowDialog();
            }
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            this.Close();
        }

        private void paymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManagePayments))
            {
                frmListPayments frm = new frmListPayments();
                frm.ShowDialog();
            }
        }

        private void manageMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageMembers))
            {
                frmListMembers frm = new frmListMembers();
                frm.ShowDialog();
            }
        }

        private void manageMembershipTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageMembers))
            {
                frmListMembershipTypes frm = new frmListMembershipTypes();
                frm.ShowDialog();
            }
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageMembers))
            {   
                frmAddUpdateMember frm = new frmAddUpdateMember();
                frm.ShowDialog();
            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageUsers))
            {
                frmAddUser frm = new frmAddUser();
                frm.ShowDialog();
            }
        }

        private void manageBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageBooks))
            {
                frmListBooks frm = new frmListBooks();
                frm.ShowDialog();
            }
        }

        private void addNewBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageBooks))
            {
                frmAddUpdateBook frm = new frmAddUpdateBook();
                frm.ShowDialog();
            }
        }

        private void manageLoansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageBooks))
            {
                frmListLoans frm = new frmListLoans();
                frm.ShowDialog();
            }
        }

        private void addNewLoanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManagePayments))
            {
                frmAddLoan frm = new frmAddLoan();
                frm.ShowDialog();
            }
        }

        private void _Backup()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (Library_Business.clsUtil.BackupDatabase(saveFileDialog1.FileName))
                    MessageBox.Show("Successfully backed up the data.", "Backup Completed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("An error occurred while backing up the data.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(clsGlobal.CurrentUser.Role != clsUser.enRole.Admin)
            {
                MessageBox.Show("Sorry, only admins are allowed to backup data.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            if (MessageBox.Show("Do you want to backup the current data to a backup file?", "Confirm Backup",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                _Backup();
        }

        private void _Restore()
        {
            if (Library_Business.clsUtil.RestoreDatabase())
                MessageBox.Show("Database was restored successfully.", "Success.",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("An error occurred during database restoring.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser.Role != clsUser.enRole.Admin)
            {
                MessageBox.Show("Sorry, only admins are allowed to restore data.", "Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to restore data from the backup file?\nThis" +
                " operation will overwrite the current database.", "Confirm Restore",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                _Restore();
        }

        private void manageCoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageCourses))
            {
                frmListCourses frm = new frmListCourses();
                frm.ShowDialog();
            }
        }

        private void launchCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_DoesCurrentUserHavePermission(clsUser.enPermissions.eManageCourses))
            {
                frmAddUpdateCourse frm = new frmAddUpdateCourse();
                frm.ShowDialog();
            }
        }
    }
}
