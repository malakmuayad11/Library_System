using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Library_Data
{
    public class clsLoanData
    {
        public static DataTable GetAllLoans()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllLoans", connection))
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
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return table;
        }

        public async static Task<DataTable> GetAllLoansAsync()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllLoans", connection))
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
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return table;
        }

        public static int AddNewLoan(int BookID, int MemberID, int CreatedByUserID)
        {
            int? LoanID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewLoan", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BookID", BookID);
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                        SqlParameter outputParam = new SqlParameter("@NewLoanID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        LoanID = (int)command.Parameters["@NewLoanID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return LoanID ?? -1;
        }

        public static bool ReturnLoan(int LoanID, DateTime ReturnDate, float? FineAmount)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_ReturnLoan", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReturnDate", ReturnDate);
                        command.Parameters.AddWithValue("@FineAmount", FineAmount ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@LoanID", LoanID);

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

        public static bool Find(int LoanID, ref int BookID, ref int MemberID, ref DateTime LoanStartDate,
           ref DateTime DueDate, ref DateTime? ReturnDate, ref float? FineAmount,
           ref int CreatedByUserID)

        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetLoanByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LoanID", LoanID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                BookID = (int)reader["BookID"];
                                MemberID = (int)reader["MemberID"];
                                LoanStartDate = (DateTime)reader["LoanStartDate"];
                                DueDate = (DateTime)reader["DueDate"];
                                ReturnDate = reader["ReturnDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ReturnDate"];
                                FineAmount = reader["FineAmount"] == DBNull.Value ? (float?)null : Convert.ToSingle(reader["FineAmount"]);

                                CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool FindByMemberID(int MemberID, ref int LoanID, ref int BookID, ref DateTime LoanStartDate,
           ref DateTime DueDate, ref DateTime? ReturnDate, ref float? FineAmount,
           ref int CreatedByUserID)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetLoanByMemberID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                BookID = (int)reader["BookID"];
                                LoanID = (int)reader["LoanID"];
                                LoanStartDate = (DateTime)reader["LoanStartDate"];
                                DueDate = (DateTime)reader["DueDate"];
                                ReturnDate = reader["ReturnDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["ReturnDate"];
                                FineAmount = reader["FineAmount"] == DBNull.Value ? (float?)null : Convert.ToSingle(reader["FineAmount"]);
                                CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool CanReturnBook(int LoanID)
        {
            int isFound = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_CanReturnBook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@LoanID", LoanID);
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

        public static bool UpdateDueDate(int LoanID, DateTime DueDate)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateDueDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DueDate", DueDate);
                        command.Parameters.AddWithValue("@LoanID", LoanID);

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
    }
}
