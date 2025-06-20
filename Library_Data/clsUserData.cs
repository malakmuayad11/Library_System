using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Library_Data
{
    public class clsUserData
    {
        public static bool Find(int UserID, ref string Username, ref string Password,
            ref byte Role, ref bool IsActive, ref int Permissions)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                Username = (string)reader["Username"];
                                Password = (string)reader["Password"];
                                Role = (byte)reader["Role"];
                                IsActive = (bool)reader["IsActive"];
                                Permissions = (int)reader["Permissions"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message,EventLogEntryType.Error);
            }
            return isFound;
        }

        public static bool Find(string Username, string Password, ref int UserID,
            ref byte Role, ref bool IsActive, ref int Permissions)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                  

                    using (SqlCommand command = new SqlCommand("SP_GetUserByUsernameAndPassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                UserID = (int)reader["UserID"];
                                Role = (byte)reader["Role"];
                                IsActive = (bool)reader["IsActive"];
                                Permissions = (int)reader["Permissions"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return isFound;
        }

        public static bool UpdatePassword(int UserID, string Password)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_UpdatePassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@UserID", UserID);

                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return dt;
        }

        public async static Task<DataTable> GetAllUsersAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return dt;
        }

         public static int AddNewUser(string Username, string Password, byte Role, bool IsActive, 
            int Permissions)
        {
            int? UserID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    { 
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Role", Role);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        command.Parameters.AddWithValue("@Permissions", Permissions);

                        SqlParameter outputParam = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        UserID = (int)command.Parameters["@UserID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return UserID ?? -1;
         }

        public static bool DoesUsernameExist(string Username)
        {
            int isFound = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DoesUsernameExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);

                        isFound = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return isFound == 1;
        }

        public static bool IsPasswordUsedByUser(int UserID, string Password)
        {
            int isFound = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_IsPasswordUsedByUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@Password", Password);

                        isFound = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return isFound == 1;
        }
    }
}
