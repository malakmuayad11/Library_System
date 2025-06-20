using System;
using System.Windows.Forms;
using System.ComponentModel;
using Library_Business;

namespace Library.Users
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUser _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.LoadData(_UserID);
            _User = clsUser.Find(_UserID);
            if(_User == null)
            {
                MessageBox.Show("The user is not found, and this screen will be closed", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void _ValidateRequiredFields(TextBox txt, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txt.Text.Trim()))
            {
                e.Cancel = true;
                txt.Focus();
                errorProvider1.SetError(txt, "This field is required");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txt, "");
            }
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            _ValidateRequiredFields(txtCurrentPassword, e);

            if (clsUtil.ComputeHash(txtCurrentPassword.Text.Trim()) != _User.Password)
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "The current password is not correct");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCurrentPassword, "");
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            _ValidateRequiredFields(txtNewPassword, e);

            if (clsUtil.ComputeHash(txtNewPassword.Text.Trim()) == _User.Password)
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "The new password matches the current one!");
            }
            else if (clsUser.IsPasswordUsedByUser(_UserID, clsUtil.ComputeHash(txtNewPassword.Text.Trim())))
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "This password was used previously, choose another one!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtNewPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            _ValidateRequiredFields(txtConfirmPassword, e);

            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Confirm password does not match new" +
                    " password");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Please check the red icons near the text boxes", "Missing Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_User.ChangePassword(txtNewPassword.Text.Trim()))
                MessageBox.Show("Password is changed successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("An error occurred during changing the password", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
