using System;
using System.Windows.Forms;

namespace Library.Books
{
    public partial class frmShowBookInfo : Form
    {
        private int _BookID;
        public frmShowBookInfo(int bookID)
        {
            InitializeComponent();
            _BookID = bookID;
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();

        private void frmShowBookInfo_Load(object sender, EventArgs e) => ctrlBookInfo1.LoadBook(_BookID);
    }
}
