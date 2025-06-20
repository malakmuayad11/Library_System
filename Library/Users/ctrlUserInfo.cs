using System;
using System.Windows.Forms;
using Library_Business;

namespace Library.Users
{
    public partial class ctrlUserInfo : UserControl
    {
        private int _UserID;
        private clsUser _User;
        public ctrlUserInfo() =>
            InitializeComponent();

        public void LoadData(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(_UserID);

            if(_User == null)
            {
                MessageBox.Show("The user is not found, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            lblUsername.Text = _User.Username;
            lblRole.Text = _User.Role.ToString();
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }
    }
}
