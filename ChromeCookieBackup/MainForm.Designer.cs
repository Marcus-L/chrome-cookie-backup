namespace ChromeCookieBackup
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.RestoreButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			this.folderBrowse = new System.Windows.Forms.Button();
			this.UserDataTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// restoreButton
			// 
			this.RestoreButton.Location = new System.Drawing.Point(111, 67);
			this.RestoreButton.Name = "restoreButton";
			this.RestoreButton.Size = new System.Drawing.Size(88, 26);
			this.RestoreButton.TabIndex = 19;
			this.RestoreButton.Text = "Restore";
			this.RestoreButton.UseVisualStyleBackColor = true;
			this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
			// 
			// saveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(17, 67);
			this.SaveButton.Name = "saveButton";
			this.SaveButton.Size = new System.Drawing.Size(88, 26);
			this.SaveButton.TabIndex = 16;
			this.SaveButton.Text = "Backup";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.BackupButton_Click);
			// 
			// folderBrowse
			// 
			this.folderBrowse.Location = new System.Drawing.Point(408, 37);
			this.folderBrowse.Name = "folderBrowse";
			this.folderBrowse.Size = new System.Drawing.Size(24, 20);
			this.folderBrowse.TabIndex = 15;
			this.folderBrowse.Text = "...";
			this.folderBrowse.UseVisualStyleBackColor = true;
			this.folderBrowse.Click += new System.EventHandler(this.FolderBrowse_Click);
			// 
			// userDataTextBox
			// 
			this.UserDataTextBox.Location = new System.Drawing.Point(17, 37);
			this.UserDataTextBox.Name = "userDataTextBox";
			this.UserDataTextBox.Size = new System.Drawing.Size(385, 20);
			this.UserDataTextBox.TabIndex = 14;
			this.UserDataTextBox.TextChanged += new System.EventHandler(this.UserDataTextBox_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(113, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "Chrome User Data Dir:";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.statusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 112);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.statusStrip1.Size = new System.Drawing.Size(444, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 20;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(13, 17);
			this.toolStripStatusLabel1.Text = "  ";
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.statusLabel.Size = new System.Drawing.Size(64, 17);
			this.statusLabel.Text = "Status: Idle";
			// 
			// openFileDialog
			// 
			this.openFileDialog.FileName = "*.db";
			this.openFileDialog.InitialDirectory = "Desktop";
			this.openFileDialog.Title = "Choose backup file...";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(444, 134);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.RestoreButton);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.folderBrowse);
			this.Controls.Add(this.UserDataTextBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Chrome Cookie Backup";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.Button RestoreButton;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button folderBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		public System.Windows.Forms.TextBox UserDataTextBox;
	}
}

