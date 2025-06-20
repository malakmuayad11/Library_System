namespace Library
{
    partial class frmListMembers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListMembers));
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.cmsMember = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewMembershipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelMembershipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.cmbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbIsCancelled = new System.Windows.Forms.ComboBox();
            this.cbMembershipType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            this.cmsMember.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Image = global::Library.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1266, 978);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 35);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvMembers
            // 
            this.dgvMembers.AllowUserToAddRows = false;
            this.dgvMembers.AllowUserToDeleteRows = false;
            this.dgvMembers.AllowUserToOrderColumns = true;
            this.dgvMembers.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.ContextMenuStrip = this.cmsMember;
            this.dgvMembers.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvMembers.Location = new System.Drawing.Point(44, 320);
            this.dgvMembers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.ReadOnly = true;
            this.dgvMembers.RowHeadersWidth = 62;
            this.dgvMembers.Size = new System.Drawing.Size(1362, 639);
            this.dgvMembers.TabIndex = 11;
            // 
            // cmsMember
            // 
            this.cmsMember.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsMember.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.showInfoToolStripMenuItem,
            this.renewMembershipToolStripMenuItem,
            this.cancelMembershipToolStripMenuItem});
            this.cmsMember.Name = "contextMenuStrip1";
            this.cmsMember.Size = new System.Drawing.Size(241, 132);
            this.cmsMember.Opening += new System.ComponentModel.CancelEventHandler(this.cmsMember_Opening);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // showInfoToolStripMenuItem
            // 
            this.showInfoToolStripMenuItem.Name = "showInfoToolStripMenuItem";
            this.showInfoToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.showInfoToolStripMenuItem.Text = "Show Info";
            this.showInfoToolStripMenuItem.Click += new System.EventHandler(this.showInfoToolStripMenuItem_Click);
            // 
            // renewMembershipToolStripMenuItem
            // 
            this.renewMembershipToolStripMenuItem.Name = "renewMembershipToolStripMenuItem";
            this.renewMembershipToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.renewMembershipToolStripMenuItem.Text = "Renew Membership";
            this.renewMembershipToolStripMenuItem.Click += new System.EventHandler(this.renewMembershipToolStripMenuItem_Click);
            // 
            // cancelMembershipToolStripMenuItem
            // 
            this.cancelMembershipToolStripMenuItem.Name = "cancelMembershipToolStripMenuItem";
            this.cancelMembershipToolStripMenuItem.Size = new System.Drawing.Size(240, 32);
            this.cancelMembershipToolStripMenuItem.Text = "Cancel Membership";
            this.cancelMembershipToolStripMenuItem.Click += new System.EventHandler(this.cancelMembershipToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(628, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 45);
            this.label1.TabIndex = 10;
            this.label1.Text = "Members";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(38, 1043);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 28);
            this.label3.TabIndex = 13;
            this.label3.Text = "#Records:";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.Black;
            this.lblRecords.Location = new System.Drawing.Point(152, 1043);
            this.lblRecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(30, 28);
            this.lblRecords.TabIndex = 14;
            this.lblRecords.Text = "??";
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.Items.AddRange(new object[] {
            "Member ID",
            "Full Name",
            "Phone",
            "Membership Type",
            "Is Cancelled",
            "None"});
            this.cmbFilterBy.Location = new System.Drawing.Point(158, 278);
            this.cmbFilterBy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.Size = new System.Drawing.Size(180, 28);
            this.cmbFilterBy.TabIndex = 15;
            this.cmbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cmbFilterBy_SelectedIndexChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(386, 278);
            this.txtFilter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(304, 26);
            this.txtFilter.TabIndex = 16;
            this.txtFilter.Visible = false;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // cbIsCancelled
            // 
            this.cbIsCancelled.FormattingEnabled = true;
            this.cbIsCancelled.Items.AddRange(new object[] {
            "All",
            "Active",
            "Cancelled"});
            this.cbIsCancelled.Location = new System.Drawing.Point(386, 277);
            this.cbIsCancelled.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbIsCancelled.Name = "cbIsCancelled";
            this.cbIsCancelled.Size = new System.Drawing.Size(180, 28);
            this.cbIsCancelled.TabIndex = 17;
            this.cbIsCancelled.SelectedIndexChanged += new System.EventHandler(this.cbIsCancelled_SelectedIndexChanged);
            // 
            // cbMembershipType
            // 
            this.cbMembershipType.FormattingEnabled = true;
            this.cbMembershipType.Items.AddRange(new object[] {
            "All",
            "Regular",
            "Student",
            "Premium"});
            this.cbMembershipType.Location = new System.Drawing.Point(386, 277);
            this.cbMembershipType.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbMembershipType.Name = "cbMembershipType";
            this.cbMembershipType.Size = new System.Drawing.Size(180, 28);
            this.cbMembershipType.TabIndex = 18;
            this.cbMembershipType.Visible = false;
            this.cbMembershipType.SelectedIndexChanged += new System.EventHandler(this.cbMembershipType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Yu Gothic UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(38, 277);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 28);
            this.label2.TabIndex = 19;
            this.label2.Text = "Filter By:";
            // 
            // btnAdd
            // 
            this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Image = global::Library.Properties.Resources.Add_Person_40;
            this.btnAdd.Location = new System.Drawing.Point(1266, 256);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 53);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Library.Properties.Resources.Manage_People;
            this.pictureBox1.Location = new System.Drawing.Point(484, 72);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(431, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // frmListMembers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1434, 1029);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMembershipType);
            this.Controls.Add(this.cbIsCancelled);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.cmbFilterBy);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvMembers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmListMembers";
            this.Text = "List Members";
            this.Load += new System.EventHandler(this.frmListMembers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            this.cmsMember.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.ComboBox cmbFilterBy;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.ComboBox cbIsCancelled;
        private System.Windows.Forms.ComboBox cbMembershipType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsMember;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ToolStripMenuItem showInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewMembershipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelMembershipToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}