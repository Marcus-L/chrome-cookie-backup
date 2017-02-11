namespace ChromeCookieBackup
{
	partial class RestoreForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.mapGridView = new System.Windows.Forms.DataGridView();
			this.LocalProfile = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BackupProfile = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.CancelButton = new System.Windows.Forms.Button();
			this.RestoreButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mapGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.mapGridView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.CancelButton);
			this.splitContainer1.Panel2.Controls.Add(this.RestoreButton);
			this.splitContainer1.Size = new System.Drawing.Size(494, 265);
			this.splitContainer1.SplitterDistance = 208;
			this.splitContainer1.TabIndex = 0;
			// 
			// mapGridView
			// 
			this.mapGridView.AllowUserToAddRows = false;
			this.mapGridView.AllowUserToDeleteRows = false;
			this.mapGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.mapGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocalProfile,
            this.BackupProfile});
			this.mapGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mapGridView.Location = new System.Drawing.Point(0, 0);
			this.mapGridView.Name = "mapGridView";
			this.mapGridView.Size = new System.Drawing.Size(494, 208);
			this.mapGridView.TabIndex = 0;
			// 
			// LocalProfile
			// 
			this.LocalProfile.DataPropertyName = "LocalProfile";
			this.LocalProfile.Frozen = true;
			this.LocalProfile.HeaderText = "Profile";
			this.LocalProfile.MinimumWidth = 225;
			this.LocalProfile.Name = "LocalProfile";
			this.LocalProfile.ReadOnly = true;
			this.LocalProfile.Width = 225;
			// 
			// BackupProfile
			// 
			this.BackupProfile.DataPropertyName = "BackupProfile";
			this.BackupProfile.HeaderText = "Backup Profile";
			this.BackupProfile.MinimumWidth = 225;
			this.BackupProfile.Name = "BackupProfile";
			this.BackupProfile.Width = 225;
			// 
			// CancelButton
			// 
			this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelButton.Location = new System.Drawing.Point(388, 8);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(85, 35);
			this.CancelButton.TabIndex = 1;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
			// 
			// RestoreButton
			// 
			this.RestoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.RestoreButton.Location = new System.Drawing.Point(291, 8);
			this.RestoreButton.Name = "RestoreButton";
			this.RestoreButton.Size = new System.Drawing.Size(85, 35);
			this.RestoreButton.TabIndex = 0;
			this.RestoreButton.Text = "Restore";
			this.RestoreButton.UseVisualStyleBackColor = true;
			this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
			// 
			// RestoreForm
			// 
			this.AcceptButton = this.RestoreButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(494, 265);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(510, 304);
			this.Name = "RestoreForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Profile Mapping";
			this.Load += new System.EventHandler(this.RestoreForm_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mapGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button RestoreButton;
		private System.Windows.Forms.DataGridView mapGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn LocalProfile;
		private System.Windows.Forms.DataGridViewComboBoxColumn BackupProfile;
	}
}