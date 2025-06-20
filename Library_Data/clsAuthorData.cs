using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace Library_Data
{
    public class clsAuthorData
    {
        public static int AddNewAuthor(string FirstName, string LastName)
        {
            int? AuthorID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewAuthor", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        SqlParameter outputParam = new SqlParameter("@AuthorID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        AuthorID = (int)command.Parameters["@AuthorID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return AuthorID ?? -1;
        }

        public static bool IsAuthorExists(string FirstName, string LastName)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_IsAuthorExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return isFound;
        }

        public static bool FindAuthorByBookID(int BookID, ref int AuthorID,
            ref string FirstName, ref string LastName)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAuthorByBookID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                AuthorID = (int)reader["AuthorID"];
                                FirstName = (string)reader["FirstName"];
                                LastName = (string)reader["LastName"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return isFound;
        }

        public static bool Find(string FirstName, string LastName, ref int AuthorID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAuthorByFirstAndLastName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@LastName", LastName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                AuthorID = (int)reader["AuthorID"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return isFound;
        }
    }
}
