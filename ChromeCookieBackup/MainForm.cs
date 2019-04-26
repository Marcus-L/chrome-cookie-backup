using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChromeCookieBackup
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void FolderBrowse_Click(object sender, EventArgs e)
        {
            if (FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                UserDataTextBox.Text = FolderBrowser.SelectedPath;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // try to guess the profile folder
            string defaultDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data";
            if (Directory.Exists(defaultDir))
            {
                UserDataTextBox.Text = defaultDir;
            }

            saveFileDialog.FileName = $"cc-backup-{ DateTime.Now.ToString("yyyyMMdd") }.db";
        }

        public IEnumerable<ProfileInfo> GetProfiles()
        {
            var retVal = new DirectoryInfo(UserDataTextBox.Text)
                .GetDirectories()
                .Where(d => d.Name == "Default" || d.Name.StartsWith("Profile"))
                .Select(d => new ProfileInfo { Directory = d, Name = GetProfileName(d) })
                .Where(p => p.Name != null)
                .ToList();

            foreach (var group in retVal.GroupBy(pi => pi.Name).Where(g => g.Count() > 1))
            {
                foreach (var profile in group)
                {
                    profile.Name += $" [{profile.Directory.Name}]";
                }
            }

            return retVal;
        }

        private string GetProfileName(DirectoryInfo profile_dir)
        {
            try
            {
                using (var sr = File.OpenText(profile_dir.FullName + "\\Preferences"))
                using (var jr = new JsonTextReader(sr))
                {
                    var json = JObject.ReadFrom(jr);
                    return json["account_info"] == null ?
                        json["profile"]["name"].ToString() :
                        $"{json["account_info"][0]["given_name"]} ({json["account_info"][0]["email"]})";
                }
            }
            catch
            {
                return null;
            }
        }

        private void BackupButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DisableForm();

                Task.Factory.StartNew(() =>
                {
                    string message = "";
                    try
                    {
                        // create output file
                        SQLiteConnection.CreateFile(saveFileDialog.FileName);

                        // populate output
                        using (var target = new SQLiteConnection($"Data Source={saveFileDialog.FileName}"))
                        {
                            target.Open();
                            using (var cmd = new SQLiteCommand("CREATE TABLE cookies (profile TEXT NOT NULL, creation_utc INTEGER NOT NULL UNIQUE PRIMARY KEY,host_key TEXT NOT NULL,name TEXT NOT NULL,value TEXT NOT NULL,path TEXT NOT NULL,expires_utc INTEGER NOT NULL,is_secure INTEGER NOT NULL,is_httponly INTEGER NOT NULL,last_access_utc INTEGER NOT NULL, has_expires INTEGER NOT NULL DEFAULT 1, is_persistent INTEGER NOT NULL DEFAULT 1,priority INTEGER NOT NULL DEFAULT 1,encrypted_value BLOB DEFAULT '',firstpartyonly INTEGER NOT NULL DEFAULT 0)", target))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            using (var tran = target.BeginTransaction())
                            {
                                foreach (var profile in GetProfiles())
                                {
                                    statusLabel.Text = $"Status: Backing up '{profile.Name}'...";
                                    int backed_up = BackupProfile(target, profile);
                                    message += $"\n{profile.Name}: {backed_up} cookies backed up.";
                                }
                                tran.Commit();
                            }
                        }

                        // clean up, release files
                        SQLiteConnection.ClearAllPools();

                        MessageBox.Show("Backup Complete:" + message, "All Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }).ContinueWith(t => EnableForm());
            }
        }

        private int BackupProfile(SQLiteConnection target, ProfileInfo profile)
        {
            int backed_up = 0;
            try
            {
                string db = profile.Directory.FullName + "\\Cookies";
                using (var source = new SQLiteConnection($"Data Source={db};Read Only=True;"))
                using (var cmd = new SQLiteCommand("SELECT * FROM cookies", source))
                {
                    source.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            using (var insertCmd = new SQLiteCommand("INSERT INTO cookies (profile, creation_utc, host_key, name, value, path, expires_utc, is_secure, is_httponly, last_access_utc, has_expires, is_persistent, priority, encrypted_value, firstpartyonly) VALUES (@profile, @creation_utc, @host_key, @name, @value, @path, @expires_utc, @is_secure, @is_httponly, @last_access_utc, @has_expires, @is_persistent, @priority, '', @firstpartyonly)", target))
                            {
                                object decrypted_value = DecryptValue((byte[])reader["encrypted_value"]);
                                if (decrypted_value != null)
                                {
                                    insertCmd.Parameters.Add(new SQLiteParameter("@profile", profile.Name));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@creation_utc", reader["creation_utc"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@host_key", reader["host_key"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@name", reader["name"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@value", decrypted_value));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@path", reader["path"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@expires_utc", reader["expires_utc"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@is_secure", reader["is_secure"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@is_httponly", reader["is_httponly"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@last_access_utc", reader["last_access_utc"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@has_expires", reader["has_expires"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@is_persistent", reader["is_persistent"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@priority", reader["priority"]));
                                    insertCmd.Parameters.Add(new SQLiteParameter("@firstpartyonly", reader["firstpartyonly"]));
                                    insertCmd.ExecuteNonQuery();

                                    backed_up++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error backing up '{profile.Name}' profile: {ex.Message}");
            }
            return backed_up;
        }

        private object DecryptValue(byte[] encrypted_value)
        {
            try
            {
                var unencrypted_value = ProtectedData.Unprotect(encrypted_value, null, DataProtectionScope.CurrentUser);
                return Encoding.ASCII.GetString(unencrypted_value);
            }
            catch
            {
                // if the cookie was not encrypted with the current profile
                return null;
            }
        }

        private void UserDataTextBox_TextChanged(object sender, EventArgs e)
        {
            // check directory
            bool validDir = Directory.Exists(UserDataTextBox.Text) && GetProfiles().Count() > 0;

            SaveButton.Enabled = validDir;
            RestoreButton.Enabled = validDir;
            statusLabel.Text = validDir ? "Status: Idle" : "Status: Invalid User Data Dir";
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var restoreForm = new RestoreForm(this) { BackupFile = openFileDialog.FileName };
                if (restoreForm.ShowDialog() == DialogResult.OK)
                {
                    DisableForm();

                    Task.Factory.StartNew(() =>
                    {
                        string message = "";
                        try
                        {
                            using (var source = new SQLiteConnection($"Data Source={openFileDialog.FileName};Read Only=True;"))
                            {
                                source.Open();
                                foreach (DataRow map in restoreForm.Map.Rows)
                                {
                                    string profile_name = map["LocalProfile"].ToString();
                                    string localprofile_dir = map["LocalProfileDir"].ToString();
                                    string backupprofile_name = map["BackupProfile"].ToString();
                                    if (map["BackupProfile"] != DBNull.Value)
                                    {
                                        statusLabel.Text = $"Status: Restoring '{profile_name}'...";
                                        int restored = RestoreProfile(source, localprofile_dir, backupprofile_name);
                                        message += $"\n{profile_name}: {restored} cookies restored.";
                                    }
                                }
                            }

                            // clean up, release files
                            SQLiteConnection.ClearAllPools();

                            MessageBox.Show("Restore Complete:" + message, "All Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }).ContinueWith(t => EnableForm());
                }
            }
        }

        private int RestoreProfile(SQLiteConnection source, string profile_dir, string profile_name)
        {
            int restored = 0;
            try
            {
                string db = profile_dir + "\\Cookies";
                using (var target = new SQLiteConnection($"Data Source={db};"))
                {
                    using (var cmd = new SQLiteCommand("SELECT * FROM cookies WHERE profile = @profile", source))
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@profile", profile_name));
                        target.Open();


                        using (var tran = target.BeginTransaction())
                        {

                            var delCmd = new SQLiteCommand("DELETE FROM COOKIES", target);
                            delCmd.ExecuteNonQuery();

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    using (var upsertCmd = new SQLiteCommand(
                                        "UPDATE cookies SET encrypted_value=@encrypted_value WHERE creation_utc=@creation_utc AND host_key=@host_key AND name=@name AND path=@path AND expires_utc=@expires_utc;" +
                                        "INSERT INTO cookies (creation_utc, host_key, name, value, path, expires_utc, is_secure, is_httponly, last_access_utc, has_expires, is_persistent, priority, encrypted_value, firstpartyonly) SELECT @creation_utc, @host_key, @name, '', @path, @expires_utc, @is_secure, @is_httponly, @last_access_utc, @has_expires, @is_persistent, @priority, @encrypted_value, @firstpartyonly WHERE (SELECT Changes()=0);",
                                        target))
                                    {
                                        var unenc_bytes = Encoding.ASCII.GetBytes(reader["value"].ToString());
                                        var enc_bytes = ProtectedData.Protect(unenc_bytes, null,
                                            DataProtectionScope.CurrentUser);

                                        upsertCmd.Parameters.Add(new SQLiteParameter("@creation_utc",
                                            reader["creation_utc"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@host_key", reader["host_key"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@name", reader["name"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@path", reader["path"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@expires_utc",
                                            reader["expires_utc"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@is_secure", reader["is_secure"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@is_httponly",
                                            reader["is_httponly"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@last_access_utc",
                                            reader["last_access_utc"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@has_expires",
                                            reader["has_expires"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@is_persistent",
                                            reader["is_persistent"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@priority", reader["priority"]));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@encrypted_value", enc_bytes));
                                        upsertCmd.Parameters.Add(new SQLiteParameter("@firstpartyonly",
                                            reader["firstpartyonly"]));
                                        upsertCmd.ExecuteNonQuery();

                                        restored++;
                                    }
                                }

                                tran.Commit();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error restoring from '" + profile_name + "' profile: " + ex.Message);
            }
            return restored;
        }

        private void EnableForm()
        {
            this.Invoke((MethodInvoker)delegate
            {
                SaveButton.Enabled = true;
                RestoreButton.Enabled = true;
                UserDataTextBox.Enabled = true;
                folderBrowse.Enabled = true;
                statusLabel.Text = "Status: Idle";
            });
        }

        private void DisableForm()
        {
            SaveButton.Enabled = false;
            RestoreButton.Enabled = false;
            UserDataTextBox.Enabled = false;
            folderBrowse.Enabled = false;
        }

    }
}

