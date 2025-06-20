namespace Library.Courses
{
    partial class frmEnrollMemberInCourse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnrollMemberInCourse));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlCourseInfo1 = new Library.Courses.ctrlCourseInfo();
            this.ctrlMemberInfoWithFilter1 = new Library.Members.ctrlMemberInfoWithFilter();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = true;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(807, 912);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(136, 41);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(949, 912);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(136, 41);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlCourseInfo1
            // 
            this.ctrlCourseInfo1.AutoSize = true;
            this.ctrlCourseInfo1.Location = new System.Drawing.Point(142, 657);
            this.ctrlCourseInfo1.Name = "ctrlCourseInfo1";
            this.ctrlCourseInfo1.Size = new System.Drawing.Size(769, 332);
            this.ctrlCourseInfo1.TabIndex = 1;
            // 
            // ctrlMemberInfoWithFilter1
            // 
            this.ctrlMemberInfoWithFilter1.AutoSize = true;
            this.ctrlMemberInfoWithFilter1.Location = new System.Drawing.Point(120, 1);
            this.ctrlMemberInfoWithFilter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ctrlMemberInfoWithFilter1.Name = "ctrlMemberInfoWithFilter1";
            this.ctrlMemberInfoWithFilter1.Size = new System.Drawing.Size(883, 673);
            this.ctrlMemberInfoWithFilter1.TabIndex = 0;
            this.ctrlMemberInfoWithFilter1.MemberFound += new System.EventHandler<Library.Members.ctrlMemberInfoWithFilter.MemberInfoEventArgs>(this.ctrlMemberInfoWithFilter1_MemberFound);
            // 
            // frmEnrollMemberInCourse
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1097, 965);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ctrlCourseInfo1);
            this.Controls.Add(this.ctrlMemberInfoWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnrollMemberInCourse";
            this.Text = "frmEnrollMemberInCourse";
            this.Activated += new System.EventHandler(this.frmEnrollMemberInCourse_Activated);
            this.Load += new System.EventHandler(this.frmEnrollMemberInCourse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Members.ctrlMemberInfoWithFilter ctrlMemberInfoWithFilter1;
        private ctrlCourseInfo ctrlCourseInfo1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}