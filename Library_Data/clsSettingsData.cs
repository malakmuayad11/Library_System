using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Library_Data
{
    public static class clsSettingsData
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["MyDb"].ConnectionString;
        public static string BackupFilePath = GetBackupFile();
        /// <summary>
        /// This method gets the backup file from the database, it will be used in
        /// other methods. 
        /// </summary>
        /// <returns>The stored backup file in the settings.</returns>
        public static string GetBackupFile()
        {
            string BackupFile = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetBackupFile", connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != null)
                            BackupFile = result.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return BackupFile ?? string.Empty;
        }

        /// <summary>
        /// This method updates the backup file to the specified one by the admin.
        /// /// </summary>
        /// <param name="NewBackupFile">Specifies the new destination of the backup file.</param>
        /// <returns>Whether updating the backup file succeed or not.</returns>
        public static bool UpdateBackupFile(string NewBackupFile)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateBackupFile", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BackupFile", NewBackupFile);
                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            { 
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

        /// <summary>
        /// This method backups the current Library's information to a backup file.
        /// </summary>
        /// <param name="BackupDestination">The backup file that will store the current library's data.</param>
        /// <returns>Whether the data was successfully backed up to the specified destination.</returns>
        public static bool BackupDatabase(string BackupDestination)
        {
            if (BackupDestination != BackupFilePath)
                UpdateBackupFile(BackupFilePath);
            int rowsEffected = 0;
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_BackupDatabase", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BackupDestination", BackupDestination);
                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return rowsEffected == -1;
        }

        /// <summary>
        /// This method resotres the database from the backed up file.
        /// </summary>
        /// <returns>Whether the data was successfully restored from the backup file.</returns>
        public static bool RestoreDatabase()
        {
            int rowsEffected = 0;
            try
            {

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = $@"
                    use master
                    RESTORE DATABASE Library_Database
                    FROM DISK = '{BackupFilePath}'
                    WITH REPLACE";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return rowsEffected == -1;
        }
    }
}
