namespace Library.Loans
{
    partial class frmAddLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddLoan));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpBook = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlBookInfoWithFilter1 = new Library.Books.ctrlBookInfoWithFilter();
            this.tpMember = new System.Windows.Forms.TabPage();
            this.ctrlMemberInfoWithFilter1 = new Library.Members.ctrlMemberInfoWithFilter();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpBook.SuspendLayout();
            this.tpMember.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpBook);
            this.tabControl1.Controls.Add(this.tpMember);
            this.tabControl1.Location = new System.Drawing.Point(4, -2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1196, 675);
            this.tabControl1.TabIndex = 0;
            // 
            // tpBook
            // 
            this.tpBook.Controls.Add(this.btnNext);
            this.tpBook.Controls.Add(this.ctrlBookInfoWithFilter1);
            this.tpBook.Location = new System.Drawing.Point(4, 29);
            this.tpBook.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpBook.Name = "tpBook";
            this.tpBook.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpBook.Size = new System.Drawing.Size(1188, 642);
            this.tpBook.TabIndex = 1;
            this.tpBook.Text = "Select Book";
            this.tpBook.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Image = global::Library.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(972, 577);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(140, 35);
            this.btnNext.TabIndex = 40;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlBookInfoWithFilter1
            // 
            this.ctrlBookInfoWithFilter1.Location = new System.Drawing.Point(108, 0);
            this.ctrlBookInfoWithFilter1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctrlBookInfoWithFilter1.Name = "ctrlBookInfoWithFilter1";
            this.ctrlBookInfoWithFilter1.Size = new System.Drawing.Size(966, 612);
            this.ctrlBookInfoWithFilter1.TabIndex = 0;
            this.ctrlBookInfoWithFilter1.OnBookSelected += new System.EventHandler<Library.Books.ctrlBookInfoWithFilter.BookInfoEventArgs>(this.ctrlBookInfoWithFilter1_OnBookSelected);
            // 
            // tpMember
            // 
            this.tpMember.Controls.Add(this.ctrlMemberInfoWithFilter1);
            this.tpMember.Location = new System.Drawing.Point(4, 29);
            this.tpMember.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpMember.Name = "tpMember";
            this.tpMember.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tpMember.Size = new System.Drawing.Size(1188, 642);
            this.tpMember.TabIndex = 0;
            this.tpMember.Text = "Select Member";
            this.tpMember.UseVisualStyleBackColor = true;
            // 
            // ctrlMemberInfoWithFilter1
            // 
            this.ctrlMemberInfoWithFilter1.Location = new System.Drawing.Point(120, 9);
            this.ctrlMemberInfoWithFilter1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ctrlMemberInfoWithFilter1.Name = "ctrlMemberInfoWithFilter1";
            this.ctrlMemberInfoWithFilter1.Size = new System.Drawing.Size(876, 632);
            this.ctrlMemberInfoWithFilter1.TabIndex = 0;
            this.ctrlMemberInfoWithFilter1.MemberFound += new System.EventHandler<Library.Members.ctrlMemberInfoWithFilter.MemberInfoEventArgs>(this.ctrlMemberInfoWithFilter1_MemberFound);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Library.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(909, 683);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 36;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Image = global::Library.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1054, 683);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddLoan
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1200, 725);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmAddLoan";
            this.Text = "Add Loan";
            this.Activated += new System.EventHandler(this.frmAddLoan_Activated);
            this.tabControl1.ResumeLayout(false);
            this.tpBook.ResumeLayout(false);
            this.tpMember.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpMember;
        private System.Windows.Forms.TabPage tpBook;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private Members.ctrlMemberInfoWithFilter ctrlMemberInfoWithFilter1;
        private Books.ctrlBookInfoWithFilter ctrlBookInfoWithFilter1;
        private System.Windows.Forms.Button btnNext;
    }
}