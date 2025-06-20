using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library.Global_Classes;
using Library_Business;

namespace Library
{
    public partial class frmLogin : Form
    {
        public frmLogin() => InitializeComponent();

        private async Task<bool> _CheckUsernameAndPasswordAsync()
        {
            clsGlobal.CurrentUser = await clsUser.Find(txtUsername.Text.Trim(), txtPassword.Text.Trim());

            if (clsGlobal.CurrentUser == null)
            {
                MessageBox.Show("Wrong username/password!", "Wrong Credentials",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!clsGlobal.CurrentUser.IsActive)
            {
                MessageBox.Show("Your account is deactivated, please contact your admin",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private async Task _LoadUI()
        {
            lblLoading.Visible = true;
            await Task.Delay(200);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await _LoadUI();
            if (await _CheckUsernameAndPasswordAsync())
            {
                frmMainScreen frm = new frmMainScreen(this);
                frm.ShowDialog();
            }
            lblLoading.Visible = false;
        }

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRememberMe.Checked)
            {
                if (!string.IsNullOrEmpty(txtUsername.Text.Trim())
                    && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    clsUtil.RememberUsernameAndPassword(txtUsername.Text.Trim(),
                        txtPassword.Text.Trim());
                }
            }
            else
            {
                clsUtil.WriteInRegistry(clsUtil.KeyPath, "Username", string.Empty);
                clsUtil.WriteInRegistry(clsUtil.KeyPath, "Password", string.Empty);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = string.Empty, Password = string.Empty;

            clsUtil.GetUsernameAndPassword(ref Username, ref Password);
            if (Username != null && Password != null)
            {
                txtUsername.Text = Username;
                txtPassword.Text = clsUtil.Decrypt_AES(Password, clsUtil.Key);
                chkRememberMe.Checked = false;
                chkRememberMe_CheckedChanged(null, null);
            }
        }
    }
}
