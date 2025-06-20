using System;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Data;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;

namespace Library_Data
{
    public class clsBookData
    {
        public static int AddNewBook(string Title, string Genre, string ISBN, byte Condition,
            DateTime PublicationDate, byte Language)
        {
            int? BookID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@Title", Title);
                        command.Parameters.AddWithValue("@Genre", Genre);
                        command.Parameters.AddWithValue("@ISBN", ISBN);
                        command.Parameters.AddWithValue("@Condition", Condition);
                        command.Parameters.AddWithValue("@PublicationDate", PublicationDate);
                        command.Parameters.AddWithValue("@Language", Language);
                        SqlParameter outputParam = new SqlParameter("@NewBookID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        BookID = (int)command.Parameters["@NewBookID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return BookID ?? -1;
        }

        public static bool UpdateCondition(int BookID, byte Condition)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateCondition", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Condition", Condition);
                        command.Parameters.AddWithValue("@BookID", BookID);

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

        public static bool UpdateAvailabilityStatus(int BookID, byte AvailabilityStatus)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateAvailabilityStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AvailabilityStatus", AvailabilityStatus);
                        command.Parameters.AddWithValue("@BookID", BookID);

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

        public static DataTable GetAllBooks()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllBooks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                table.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex) { clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            return table;
        }

        public static async Task<DataTable> GetAllBooksAsync()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllBooks", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader =  await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                                table.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex) { clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            return table;
        }

        public static bool Find(int BookID, ref string Title, ref string Genre, ref string ISBN,
            ref byte Condition, ref DateTime PublicationDate, ref byte AvailabilityStatus,
            ref byte Language)

        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetBookByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                Title = (string)reader["Title"];
                                Genre = (string)reader["Genre"];
                                ISBN = (string)reader["ISBN"];
                                Condition = (byte)reader["Condition"];
                                PublicationDate = (DateTime)reader["PublicationDate"];
                                AvailabilityStatus = (byte)reader["AvailabilityStatus"];
                                Language = (byte)reader["Language"];
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

        public static bool Find(string Title, ref int BookID, ref string Genre, ref string ISBN,
            ref byte Condition, ref DateTime PublicationDate, ref byte AvailabilityStatus,
            ref byte Language)

        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetBookByTitle", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Title", Title);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                BookID = (int)reader["BookID"];
                                Genre = (string)reader["Genre"];
                                ISBN = (string)reader["ISBN"];
                                Condition = (byte)reader["Condition"];
                                PublicationDate = (DateTime)reader["PublicationDate"];
                                AvailabilityStatus = (byte)reader["AvailabilityStatus"];
                                Language = (byte)reader["Language"];
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

        public static bool DoesISBNExist(string ISBN)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DoesISBNExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ISBN", ISBN);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            isFound = reader.HasRows;
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

        public static bool AddAuthorToBook(int AuthorID, int BookID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddAuthorToBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AuthorID", AuthorID);
                        command.Parameters.AddWithValue("@BookID", BookID);
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

        public static bool DeleteBook(int BookID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);

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

        public static int GetAuthorID(int BookID)
        {
            int? AuthorID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAuthorID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);
                        object result = command.ExecuteScalar();
                        if (int.TryParse(result.ToString(), out int insertedID))
                            AuthorID = insertedID;
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return AuthorID ?? -1;
        }
    }
}
