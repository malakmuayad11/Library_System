using System;
using System.Windows.Forms;

namespace Library.Members
{
    public partial class ctrlMemberInfoWithFilter : UserControl
    {

        public class MemberInfoEventArgs : EventArgs
        {
            public int MemberID { get; }
            public bool IsFound { get; }

            public MemberInfoEventArgs(int MemberID, bool IsFound)
            {
                this.MemberID = MemberID;
                this.IsFound = IsFound;
            }        
        }
        public bool IsFound
        {
            get => ctrlMemberInfo1.IsFound;
        }

        public ctrlMemberInfoWithFilter() => InitializeComponent();

        public void DisableFilter() => gbFilter.Enabled = false;

        public void FilterFocus() => txtMemberID.Focus();

        public event EventHandler<MemberInfoEventArgs> MemberFound;

        private void RaiseMemberFound(MemberInfoEventArgs e) =>
            MemberFound?.Invoke(this, e);

        protected void RaiseMemberFound(int MemberID, bool IsFound) => 
            RaiseMemberFound(new MemberInfoEventArgs(MemberID, IsFound));
        private void txtMemberID_KeyPress(object sender, KeyPressEventArgs e) 
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)13)
                btnFind.PerformClick();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ctrlMemberInfo1.LoadData(Convert.ToInt32(txtMemberID.Text));
            RaiseMemberFound(Convert.ToInt32(txtMemberID.Text), ctrlMemberInfo1.IsFound);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frm = new frmAddUpdateMember();
            frm.MemberSaved += Frm_MemberSaved;
            frm.ShowDialog();
        }

        private void Frm_MemberSaved(object sender, frmAddUpdateMember.MemberInfoEventArgs e)
        {
            txtMemberID.Text = e.MemberID.ToString();
            ctrlMemberInfo1.LoadData(e.MemberID);
        }
    }
}
