namespace Library.Courses
{
    partial class frmEnrolledMembers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnrolledMembers));
            this.ctrlCourseInfo1 = new Library.Courses.ctrlCourseInfo();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvEnrolledMembers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblParticipantsCount = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnrolledMembers)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlCourseInfo1
            // 
            this.ctrlCourseInfo1.Location = new System.Drawing.Point(0, 66);
            this.ctrlCourseInfo1.Name = "ctrlCourseInfo1";
            this.ctrlCourseInfo1.Size = new System.Drawing.Size(769, 335);
            this.ctrlCourseInfo1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(208, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 54);
            this.label2.TabIndex = 43;
            this.label2.Text = "Enrolled Members";
            // 
            // dgvEnrolledMembers
            // 
            this.dgvEnrolledMembers.AllowUserToAddRows = false;
            this.dgvEnrolledMembers.AllowUserToDeleteRows = false;
            this.dgvEnrolledMembers.AllowUserToOrderColumns = true;
            this.dgvEnrolledMembers.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvEnrolledMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnrolledMembers.Location = new System.Drawing.Point(26, 407);
            this.dgvEnrolledMembers.Name = "dgvEnrolledMembers";
            this.dgvEnrolledMembers.ReadOnly = true;
            this.dgvEnrolledMembers.RowHeadersWidth = 62;
            this.dgvEnrolledMembers.RowTemplate.Height = 28;
            this.dgvEnrolledMembers.Size = new System.Drawing.Size(733, 150);
            this.dgvEnrolledMembers.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(21, 571);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 28);
            this.label1.TabIndex = 45;
            this.label1.Text = "Participants Count:";
            // 
            // lblParticipantsCount
            // 
            this.lblParticipantsCount.AutoSize = true;
            this.lblParticipantsCount.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParticipantsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblParticipantsCount.Location = new System.Drawing.Point(212, 571);
            this.lblParticipantsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParticipantsCount.Name = "lblParticipantsCount";
            this.lblParticipantsCount.Size = new System.Drawing.Size(23, 28);
            this.lblParticipantsCount.TabIndex = 46;
            this.lblParticipantsCount.Text = "0";
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Image = global::Library.Properties.Resources.AddAppointment_32;
            this.btnAdd.Location = new System.Drawing.Point(684, 571);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 42);
            this.btnAdd.TabIndex = 47;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmEnrolledMembers
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(783, 618);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblParticipantsCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEnrolledMembers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ctrlCourseInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEnrolledMembers";
            this.Text = "Enrolled Members";
            this.Load += new System.EventHandler(this.frmEnrolledMembers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnrolledMembers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlCourseInfo ctrlCourseInfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvEnrolledMembers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblParticipantsCount;
        private System.Windows.Forms.Button btnAdd;
    }
}