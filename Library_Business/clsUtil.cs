using Library_Data;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Library_Business
{
    /// <summary>
    /// This class contains multiple random methods for the Business Layer.
    /// </summary>
    public class clsUtil
    {
        /// <summary>
        /// This method generates a hexadecimal hash string of the input.
        /// </summary>
        /// <param name="input">The string to be hashed.</param>
        /// <returns>The hash signature of the input.</returns>
        public static string ComputeHash(string input)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                StringBuilder sb = new StringBuilder();
                Byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// This method backups the current Library's information to a backup file.
        /// </summary>
        /// <param name="BackupDestination">The backup file that will store the current library's data.</param>
        /// <returns>Whether the data was successfully backedup to the specified destination.</returns>
        public static bool BackupDatabase(string BackupDestination) => 
            clsSettingsData.BackupDatabase(BackupDestination);

        /// <summary>
        /// This method resotre the database from the backed up file.
        /// </summary>
        /// <returns>Whether the data was successfully restored from the backup file.</returns>
        public static bool RestoreDatabase() => clsSettingsData.RestoreDatabase();
    }
}
