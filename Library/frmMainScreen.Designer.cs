namespace Library
{
    partial class frmMainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainScreen));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.membersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageMemebershipTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMemberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentUserInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageLoansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewLoanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.courseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageCoursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchCourseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.membersToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.booksToolStripMenuItem,
            this.loansToolStripMenuItem,
            this.courseToolStripMenuItem,
            this.paymentsToolStripMenuItem,
            this.databaseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // membersToolStripMenuItem
            // 
            this.membersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageMembersToolStripMenuItem,
            this.manageMemebershipTypesToolStripMenuItem,
            this.addNewMemberToolStripMenuItem});
            this.membersToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_People;
            this.membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            this.membersToolStripMenuItem.Size = new System.Drawing.Size(127, 29);
            this.membersToolStripMenuItem.Text = "Members";
            // 
            // manageMembersToolStripMenuItem
            // 
            this.manageMembersToolStripMenuItem.Image = global::Library.Properties.Resources.Person_32;
            this.manageMembersToolStripMenuItem.Name = "manageMembersToolStripMenuItem";
            this.manageMembersToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.manageMembersToolStripMenuItem.Text = "Manage Members";
            this.manageMembersToolStripMenuItem.Click += new System.EventHandler(this.manageMembersToolStripMenuItem_Click);
            // 
            // manageMemebershipTypesToolStripMenuItem
            // 
            this.manageMemebershipTypesToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_Applications_32;
            this.manageMemebershipTypesToolStripMenuItem.Name = "manageMemebershipTypesToolStripMenuItem";
            this.manageMemebershipTypesToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.manageMemebershipTypesToolStripMenuItem.Text = "Manage Membership Types";
            this.manageMemebershipTypesToolStripMenuItem.Click += new System.EventHandler(this.manageMembershipTypesToolStripMenuItem_Click);
            // 
            // addNewMemberToolStripMenuItem
            // 
            this.addNewMemberToolStripMenuItem.Image = global::Library.Properties.Resources.Add_Person_40;
            this.addNewMemberToolStripMenuItem.Name = "addNewMemberToolStripMenuItem";
            this.addNewMemberToolStripMenuItem.Size = new System.Drawing.Size(333, 34);
            this.addNewMemberToolStripMenuItem.Text = "Add New Member";
            this.addNewMemberToolStripMenuItem.Click += new System.EventHandler(this.addNewMemberToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageUsersToolStripMenuItem,
            this.currentUserInfoToolStripMenuItem,
            this.addNewUserToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.usersToolStripMenuItem.Image = global::Library.Properties.Resources.User_32__2;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(95, 29);
            this.usersToolStripMenuItem.Text = "Users";
            // 
            // manageUsersToolStripMenuItem
            // 
            this.manageUsersToolStripMenuItem.Image = global::Library.Properties.Resources.Users_2_64;
            this.manageUsersToolStripMenuItem.Name = "manageUsersToolStripMenuItem";
            this.manageUsersToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.manageUsersToolStripMenuItem.Text = "Manage Users";
            this.manageUsersToolStripMenuItem.Click += new System.EventHandler(this.manageUsersToolStripMenuItem_Click);
            // 
            // currentUserInfoToolStripMenuItem
            // 
            this.currentUserInfoToolStripMenuItem.Image = global::Library.Properties.Resources.User_32__2;
            this.currentUserInfoToolStripMenuItem.Name = "currentUserInfoToolStripMenuItem";
            this.currentUserInfoToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.currentUserInfoToolStripMenuItem.Text = "Current User Info";
            this.currentUserInfoToolStripMenuItem.Click += new System.EventHandler(this.currentUserInfoToolStripMenuItem_Click);
            // 
            // addNewUserToolStripMenuItem
            // 
            this.addNewUserToolStripMenuItem.Image = global::Library.Properties.Resources.Add_Person_40;
            this.addNewUserToolStripMenuItem.Name = "addNewUserToolStripMenuItem";
            this.addNewUserToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.addNewUserToolStripMenuItem.Text = "Add New User";
            this.addNewUserToolStripMenuItem.Click += new System.EventHandler(this.addNewUserToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::Library.Properties.Resources.Password_32;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Image = global::Library.Properties.Resources.Sign_Out_32;
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(254, 34);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // booksToolStripMenuItem
            // 
            this.booksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageBooksToolStripMenuItem,
            this.addNewBookToolStripMenuItem});
            this.booksToolStripMenuItem.Image = global::Library.Properties.Resources.book;
            this.booksToolStripMenuItem.Name = "booksToolStripMenuItem";
            this.booksToolStripMenuItem.Size = new System.Drawing.Size(101, 29);
            this.booksToolStripMenuItem.Text = "Books";
            // 
            // manageBooksToolStripMenuItem
            // 
            this.manageBooksToolStripMenuItem.Image = global::Library.Properties.Resources.book;
            this.manageBooksToolStripMenuItem.Name = "manageBooksToolStripMenuItem";
            this.manageBooksToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.manageBooksToolStripMenuItem.Text = "Manage Books";
            this.manageBooksToolStripMenuItem.Click += new System.EventHandler(this.manageBooksToolStripMenuItem_Click);
            // 
            // addNewBookToolStripMenuItem
            // 
            this.addNewBookToolStripMenuItem.Image = global::Library.Properties.Resources.add_book;
            this.addNewBookToolStripMenuItem.Name = "addNewBookToolStripMenuItem";
            this.addNewBookToolStripMenuItem.Size = new System.Drawing.Size(234, 34);
            this.addNewBookToolStripMenuItem.Text = "Add New Book";
            this.addNewBookToolStripMenuItem.Click += new System.EventHandler(this.addNewBookToolStripMenuItem_Click);
            // 
            // loansToolStripMenuItem
            // 
            this.loansToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageLoansToolStripMenuItem,
            this.addNewLoanToolStripMenuItem});
            this.loansToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_Applications_32;
            this.loansToolStripMenuItem.Name = "loansToolStripMenuItem";
            this.loansToolStripMenuItem.Size = new System.Drawing.Size(98, 29);
            this.loansToolStripMenuItem.Text = "Loans";
            // 
            // manageLoansToolStripMenuItem
            // 
            this.manageLoansToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_Applications_32;
            this.manageLoansToolStripMenuItem.Name = "manageLoansToolStripMenuItem";
            this.manageLoansToolStripMenuItem.Size = new System.Drawing.Size(231, 34);
            this.manageLoansToolStripMenuItem.Text = "Manage Loans";
            this.manageLoansToolStripMenuItem.Click += new System.EventHandler(this.manageLoansToolStripMenuItem_Click);
            // 
            // addNewLoanToolStripMenuItem
            // 
            this.addNewLoanToolStripMenuItem.Image = global::Library.Properties.Resources.AddAppointment_32;
            this.addNewLoanToolStripMenuItem.Name = "addNewLoanToolStripMenuItem";
            this.addNewLoanToolStripMenuItem.Size = new System.Drawing.Size(231, 34);
            this.addNewLoanToolStripMenuItem.Text = "Add New Loan";
            this.addNewLoanToolStripMenuItem.Click += new System.EventHandler(this.addNewLoanToolStripMenuItem_Click);
            // 
            // courseToolStripMenuItem
            // 
            this.courseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageCoursesToolStripMenuItem,
            this.launchCourseToolStripMenuItem});
            this.courseToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_Applications_32;
            this.courseToolStripMenuItem.Name = "courseToolStripMenuItem";
            this.courseToolStripMenuItem.Size = new System.Drawing.Size(115, 29);
            this.courseToolStripMenuItem.Text = "Courses";
            // 
            // manageCoursesToolStripMenuItem
            // 
            this.manageCoursesToolStripMenuItem.Image = global::Library.Properties.Resources.Manage_Applications_32;
            this.manageCoursesToolStripMenuItem.Name = "manageCoursesToolStripMenuItem";
            this.manageCoursesToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.manageCoursesToolStripMenuItem.Text = "Manage Courses";
            this.manageCoursesToolStripMenuItem.Click += new System.EventHandler(this.manageCoursesToolStripMenuItem_Click);
            // 
            // launchCourseToolStripMenuItem
            // 
            this.launchCourseToolStripMenuItem.Image = global::Library.Properties.Resources.AddAppointment_32;
            this.launchCourseToolStripMenuItem.Name = "launchCourseToolStripMenuItem";
            this.launchCourseToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.launchCourseToolStripMenuItem.Text = "Launch Course";
            this.launchCourseToolStripMenuItem.Click += new System.EventHandler(this.launchCourseToolStripMenuItem_Click);
            // 
            // paymentsToolStripMenuItem
            // 
            this.paymentsToolStripMenuItem.Image = global::Library.Properties.Resources.money_32;
            this.paymentsToolStripMenuItem.Name = "paymentsToolStripMenuItem";
            this.paymentsToolStripMenuItem.Size = new System.Drawing.Size(128, 29);
            this.paymentsToolStripMenuItem.Text = "Payments";
            this.paymentsToolStripMenuItem.Click += new System.EventHandler(this.paymentsToolStripMenuItem_Click);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.databaseToolStripMenuItem.Image = global::Library.Properties.Resources.database;
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(126, 29);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Image = global::Library.Properties.Resources.backup;
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Image = global::Library.Properties.Resources.restore;
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(173, 34);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "Backup Database";
            this.saveFileDialog1.Filter = "All Backup Files (*.bak) |*.*";
            this.saveFileDialog1.InitialDirectory = "c:/";
            this.saveFileDialog1.Title = "Backup Database";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Library.Properties.Resources.background1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1200, 659);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // frmMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMainScreen";
            this.Text = "Library System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem membersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageUsersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem currentUserInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem booksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageMemebershipTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMemberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageBooksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageLoansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewLoanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem courseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageCoursesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchCourseToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}