using Library_Business;
using System;
using System.Windows.Forms;

namespace Library.Users
{
    public partial class frmAddUser : Form
    {
        private clsUser _User;
        public frmAddUser()
        {
            InitializeComponent();
            _User = new clsUser();
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e) => cbRole.SelectedIndex = 1;

        private int _StorePermissions()
        {
            int Permissions = 0;
            if (chkFullAccess.Checked)
                return -1;
            if (chkManageMembers.Checked)
                Permissions += (int)clsUser.enPermissions.eManageMembers;
            if (chkManageBooks.Checked)
                Permissions += (int)clsUser.enPermissions.eManageBooks;
            if (chkManageCourses.Checked)
                Permissions += (int)clsUser.enPermissions.eManageCourses;
            if (chkManageUsers.Checked)
                Permissions += (int)clsUser.enPermissions.eManageUsers;
            if (chkManagePayments.Checked)
                Permissions += (int)clsUser.enPermissions.eManagePayments;
            return Permissions;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void _Save()
        {
            _User.Username = txtUsername.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.Role = (clsUser.enRole)cbRole.SelectedIndex + 1;
            _User.IsActive = chkIsActive.Checked;
            _User.Permissions = (clsUser.enPermissions)_StorePermissions();

            if (_User.Save())
            {
                MessageBox.Show("The user is added successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblID.Text = _User.UserID.ToString();
                txtPassword.Enabled = false;
                txtUsername.Enabled = false;
                txtConfirmPassword.Enabled = false;
                cbRole.Enabled = false;
                chkIsActive.Enabled = false;
                gbPermissions.Enabled = false;
                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("An error occurred during user saving", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("There are missing fields, please check red circles",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Save();
        }

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

        private void txtConfirmPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ValidateRequiredFields(txtConfirmPassword, e);

            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password does not match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, string.Empty);
            }
        }

        private void txtUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _ValidateRequiredFields(txtUsername, e);
            if (clsUser.DoesUsernameExist(txtUsername.Text.Trim()))
            {
                e.Cancel = true;
                txtUsername.Focus();
                errorProvider1.SetError(txtUsername, "This username already exists");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError (txtUsername, string.Empty);
            }
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e) =>
            chkFullAccess.Checked = cbRole.SelectedIndex == 0;

        private void chkFullAccess_CheckedChanged(object sender, EventArgs e)
        {
            chkManageMembers.Checked = chkFullAccess.Checked;
            chkManageBooks.Checked = chkFullAccess.Checked;
            chkManageCourses.Checked = chkFullAccess.Checked;
            chkManageUsers.Checked = chkFullAccess.Checked;
            chkManagePayments.Checked = chkFullAccess.Checked;
        }
    }
}
