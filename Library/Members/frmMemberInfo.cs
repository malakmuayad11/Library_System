using System;
using System.Windows.Forms;

namespace Library.Members
{
    public partial class frmMemberInfo : Form
    {
        private int _MemberID;
        public frmMemberInfo(int memberID)
        {
            InitializeComponent();
            _MemberID = memberID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmMemberInfo_Load(object sender, EventArgs e) => ctrlMemberInfo1.LoadData(_MemberID);
    }
}
