using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeCookieBackup
{
	public partial class RestoreForm : Form
	{
		public DataTable Map = new DataTable();
		public string BackupFile { get; set; }

		public RestoreForm(MainForm main)
		{
			InitializeComponent();

			Main = main;
			Map.Columns.Add("LocalProfile", typeof(string));
			Map.Columns.Add("LocalProfileDir", typeof(string));
			Map.Columns.Add("BackupProfile", typeof(string));
			mapGridView.AutoGenerateColumns = false;
			mapGridView.DataSource = Map;
		}

		public MainForm Main { get; private set; }

		private void RestoreButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void RestoreForm_Load(object sender, EventArgs e)
		{
			try
			{
				var dd_column = (DataGridViewComboBoxColumn)mapGridView.Columns["BackupProfile"];
				var backup_profiles = GetBackupProfiles();
				dd_column.DataSource = backup_profiles;

				foreach (var profile in Main.GetProfiles())
				{
					var backup_value = backup_profiles.Contains(profile.Name) ? (object)profile.Name : DBNull.Value;
					Map.Rows.Add(profile.Name, profile.Directory.FullName, backup_value);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error loading backup file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

				DialogResult = DialogResult.Abort;
				Close();
			}
		}

		private List<object> GetBackupProfiles()
		{
			var retVal = new List<object>();
			retVal.Add(DBNull.Value);
			using (var source = new SQLiteConnection($"Data Source={BackupFile};"))
			using (var cmd = new SQLiteCommand("SELECT DISTINCT profile FROM cookies", source))
			{
				source.Open();
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						retVal.Add(reader["profile"].ToString());
					}
				}
			}
			return retVal;
		}
	}
}
