# Chrome Cookie Backup

A Windows Tool for backing up and restoring Chrome's encrypted cookies

## Background:
A [change made to Chromium on 12/16/2013](https://src.chromium.org/viewvc/chrome?view=revision&revision=241004) introduced [DPAPI](https://msdn.microsoft.com/en-us/library/ms995355.aspx) to encrypt protected data including cookies and saved passwords on Windows. DPAPI generates unique encryption keys per user and domain or standalone Windows installation. If you move protected files (e.g. Cookies) to a different user account or a different domain or Windows installation, the protected data will become unusable.

This tool creates a decrypted SQLite backup file of the `Cookies` file in the Chrome [User Data](https://www.chromium.org/user-experience/user-data-directory) Profile folder, using the credentials of the logged on user's profile. The tool can restore from this backup file back into a Chrome User Profile `Cookies` using a different user account or domain or Windows installation, by re-encrypting the data using the other user's credentials.

## Usage

1. Install from chocolatey:
   ```powershell
   choco install ChromeCookieBackup
   ```
2. Run the tool using the Windows user account of the source user/computer and save a backup file
3. Move the `User Data` folder in its entirety to a different user account or Windows installation, or simply create a new profile on the target computer.
4. Run the tool on the target computer using the target user account, and restore the backup to the desired Chrome profile(s). You may map Chrome profiles from the backup file to profiles on the target computer.

![Screenshot](/ChromeCookieBackup/screenshot.png)

## Dev Notes:
* Chromium implementation of Encrypted Cookies: https://codereview.chromium.org/24734007
* DPAPI .NET Wrapper System.Security.Cryptography.ProtectedData docs: https://msdn.microsoft.com/en-us/library/system.security.cryptography.protecteddata(v=vs.110).aspx

### Built With
* Visual Studio 2017
* .NET Framework 4.5.2
* [System.Data.Sqlite](https://system.data.sqlite.org/) - ADO.NET provider for SQLite (Chrome stores cookies in a SQLite database)
* [Json.NET](http://www.newtonsoft.com/json) - For reading Chrome profile Preferences Json