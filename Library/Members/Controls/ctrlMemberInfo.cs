using System;
using System.IO;
using System.Windows.Forms;
using Library.Global_Classes;
using Library.Properties;
using LibrarySystem_Business;

namespace Library.Members
{
    public partial class ctrlMemberInfo : UserControl
    {
        private clsMember _Member;

        public bool IsFound { get; set; }
        public ctrlMemberInfo() => InitializeComponent();

        public void LoadData(int MemberID)
        {
            _Member = clsMember.Find(MemberID);

            if(_Member == null)
            {
                MessageBox.Show("The member is not found, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsFound = false;
                return;
            }

            IsFound = true;
            lblID.Text = _Member.MemberID.ToString();
            lblFullName.Text = _Member.FullName;
            lblDateOfBirth.Text = _Member.DateOfBirth.ToString(clsFormat.DateFormat);
            lblPhone.Text = _Member.Phone;
            lblEmail.Text = _Member.Email ?? "No Data";
            lblMembershipType.Text = _Member.MembershipTypeInfo.MembershipName;
            lblStartDate.Text = _Member.StartDate.ToString(clsFormat.DateFormat);
            lblExpiryDate.Text = _Member.ExpiryDate.ToString(clsFormat.DateFormat);
            lblIsCancelled.Text = _Member.IsCancelled ? "Yes" : "No";
            txtAddress.Text = _Member.Address;
            if (string.IsNullOrEmpty(_Member.ImagePath))
                pbImage.Image = Resources.Male_512;
            else
            {
                if(File.Exists(_Member.ImagePath))
                    pbImage.ImageLocation = _Member.ImagePath;
            }
        }

    }
}
