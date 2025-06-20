using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using Library_Data;
using System.Threading.Tasks;

namespace LibrarySystem_Data
{
    public class clsMemberData
    {
        public static int AddNewMember(string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, string Address, string Phone, string Email,
            string ImagePath, int MembershipTypeID)
        {
            int? MemberID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_AddNewMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        command.Parameters.AddWithValue("@ThirdName", ThirdName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeID);

                        SqlParameter outputParam = new SqlParameter("@MemberID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        MemberID = (int)command.Parameters["@MemberID"].Value;
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return MemberID ?? -1;
        }

        public static bool UpdateMember(int MemberID, string FirstName, string SecondName, string ThirdName,
            string LastName, DateTime DateOfBirth, string Address, string Phone, string Email,
            string ImagePath, int MembershipTypeID, bool IsCancelled)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_UpdateMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        command.Parameters.AddWithValue("@ThirdName", ThirdName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Address", Address);
                        command.Parameters.AddWithValue("@Phone", Phone);
                        command.Parameters.AddWithValue("@Email", Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeID);
                        command.Parameters.AddWithValue("@IsCancelled", IsCancelled);
                        command.Parameters.AddWithValue("@MemberID", MemberID);

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

        public static bool UpdateCancel(int MemberID, bool IsCancelled)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateCancel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IsCancelled", IsCancelled);
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex) { clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            return rowsEffected > 0;
        }

        public static DataTable GetAllMembers()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembers", connection))
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

        public async static Task<DataTable> GetAllMembersAsync()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
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

        public static bool Find(int MemberID, ref string FirstName, ref string SecondName, ref string ThirdName,
           ref string LastName, ref DateTime DateOfBirth, ref string Address, ref string Phone, ref string Email,
            ref string ImagePath, ref int MembershipTypeID, ref DateTime StartDate, ref DateTime ExpiryDate,
            ref bool IsCancelled)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetMemberByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = reader["ThirdName"] == DBNull.Value ? null : (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Address = (string)reader["Address"];
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] == DBNull.Value ? null : (string)reader["Email"];
                                ImagePath = reader["ImagePath"] == DBNull.Value ? null : (string)reader["ImagePath"];
                                MembershipTypeID = (int)reader["MembershipTypeID"];
                                StartDate = (DateTime)reader["StartDate"];
                                ExpiryDate = (DateTime)reader["ExpiryDate"];
                                IsCancelled = (bool)reader["IsCancelled"];
                            }
                        }
                    }
                }
            }
            catch (SqlException ex) { clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error); }
            return isFound;
        }

        public static bool RenewMembership(int MemberID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_RenewMembership", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

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

        public static int GetNumberOfBorrowedBook(int MemberID)
        {
            int? NumberOfBorrowedBooks = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetNumberOfBorrowedBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        NumberOfBorrowedBooks = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return NumberOfBorrowedBooks ?? -1;
        }
    }
}
