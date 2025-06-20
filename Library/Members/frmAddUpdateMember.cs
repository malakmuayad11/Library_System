using System;
using LibrarySystem_Business;
using System.Windows.Forms;
using System.Data;
using Library.Properties;
using System.IO;

namespace Library
{
    public partial class frmAddUpdateMember : Form
    {
        public class MemberInfoEventArgs : EventArgs
        {
            public int MemberID { get; }
            public MemberInfoEventArgs(int MemberID)
            {
                this.MemberID = MemberID;
            }
        }
        public event EventHandler<MemberInfoEventArgs> MemberSaved;

        private void RaiseMemberSaved(MemberInfoEventArgs e) =>
            MemberSaved?.Invoke(this, e);

        protected void RaiseMemberSaved(int MemberID) =>
            RaiseMemberSaved(new MemberInfoEventArgs(MemberID));

        private int _MemberID;
        private clsMember _Member;
        public enum enMode { AddNew =  0, Update = 1 }
        private enMode _Mode;
        public frmAddUpdateMember()
        {
            InitializeComponent();
            _MemberID = 0;
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateMember(int MemberID)
        {
            InitializeComponent();
            _MemberID = MemberID;
            _Mode = enMode.Update;
            _Member = clsMember.Find(_MemberID);
        }

        private void _FillMembershipTypes()
        {
            DataTable dt = clsMembershipType.GetAllMembershipTypes();
            foreach (DataRow dr in dt.Rows)
            {
                cbMembershipType.Items.Add(dr[1]);
            }
        }

        private void _LoadAddMode()
        {
            _Member = new clsMember();
            this.Text = "Add New Member";
            lblMode.Text = "Add New Member";
            dtDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtDateOfBirth.Value = dtDateOfBirth.MaxDate;
            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = dtStartDate.Value.AddYears(1);
            llAddImage.Visible = true;
        }

        private void _LoadUpdateMode()
        {
            lblID.Text = _Member.MemberID.ToString();
            txtFirstName.Text = _Member.FirstName;
            txtSecondName.Text = _Member.SecondName;
            txtThirdName.Text = _Member.ThirdName ?? string.Empty;
            txtLastName.Text = _Member.LastName;
            dtDateOfBirth.Value = _Member.DateOfBirth;
            dtStartDate.Value = _Member.StartDate;
            dtEndDate.Value = _Member.ExpiryDate;
            mtxtPhone.Text = _Member.Phone;
            txtEmail.Text = _Member.Email ?? string.Empty;
            txtAddress.Text = _Member.Address;
            pbImage.ImageLocation = _Member.ImagePath ?? string.Empty;
            llAddImage.Visible = string.IsNullOrEmpty(_Member.ImagePath);
            llRemoveImage.Visible = !llAddImage.Visible;
        }

        private void _LoadData()
        {
            _FillMembershipTypes();
            cbMembershipType.SelectedIndex = 0;
            if (_Mode == enMode.AddNew)
            {
                _LoadAddMode();
                return;
            }
            cbMembershipType.SelectedIndex = _Member.MembershipTypeID - 1;
            this.Text = "Update Member";
            lblMode.Text = "Update Member";

            if(_Member == null)
            {
                MessageBox.Show("An error occurred and the screen will be closed", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            _LoadUpdateMode();
        }

        private void btnClose_Click(object sender, EventArgs e) =>
            this.Close();

        private void frmAddUpdateMember_Load(object sender, EventArgs e) =>
            _LoadData();

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

        private void txtEmail_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(txtEmail, "The email is not valid!");
            }
            else
                errorProvider1.SetError(txtEmail, "");
        }

        private void _HandleImagePath()
        {
            if (!string.IsNullOrEmpty(_Member.ImagePath) ||
                pbImage.Image != Resources.Male_512)
            {
                string SourceFile = _Member.ImagePath;
                if(File.Exists(SourceFile))
                    clsUtil.CopyImageToProjectImagesFolder(ref SourceFile);
            }
        }

        private void _SaveData()
        {
            _Member.FirstName = txtFirstName.Text;
            _Member.SecondName = txtSecondName.Text;
            _Member.ThirdName = txtThirdName.Text;
            _Member.LastName = txtLastName.Text;
            _Member.DateOfBirth = dtDateOfBirth.Value;
            _Member.Phone = mtxtPhone.Text;
            _Member.StartDate = dtStartDate.Value;
            _Member.ExpiryDate = dtEndDate.Value;
            _Member.Email = txtEmail.Text;
            _Member.Address = txtAddress.Text;
            _Member.ImagePath = pbImage.ImageLocation ?? string.Empty;
            _Member.MembershipTypeID = cbMembershipType.SelectedIndex + 1;
            _HandleImagePath();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Please make sure that all required fields are not empty", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _SaveData();
            if (_Member.Save())
            {
                RaiseMemberSaved(_Member.MemberID);
                MessageBox.Show("Data is saved successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _Mode = enMode.Update;
                this.Text = "Update Member";
                lblMode.Text = "Update Member";
                lblID.Text = _Member.MemberID.ToString();
            }
            else
                MessageBox.Show("Data is not saved, an error occurred", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            _Member.ImagePath = null;
            llAddImage.Visible = true;
            llRemoveImage.Visible = false;
        }

        private void llAddImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbImage.ImageLocation = openFileDialog1.FileName;
                llRemoveImage.Visible = true;
            }
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e) =>
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        private void cbMembershipType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbMembershipType.Text))
            {
                e.Cancel = false;
                errorProvider1.SetError(cbMembershipType, "This filed is required");
            }
            else
                errorProvider1.SetError(cbMembershipType, "");
        }

        private void mtxtPhone_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(string.IsNullOrEmpty(mtxtPhone.Text) || !mtxtPhone.MaskFull)
            {
                e.Cancel = false;
                errorProvider1.SetError(mtxtPhone, "This filed is required");
            }
            else
                errorProvider1.SetError(mtxtPhone, "");
        }

        private void frmAddUpdateMember_Activated(object sender, EventArgs e) =>
            txtFirstName.Focus();
    }
}
