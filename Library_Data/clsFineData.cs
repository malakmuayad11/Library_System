using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;

namespace Library_Data
{
    public class clsFineData
    {
        public static int AddNewFine(int MemberID, int LoanID, float FineAmount, bool IsPaid)
        {
            int? FineID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewFine", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@LoanID", LoanID);
                        command.Parameters.AddWithValue("@FineAmount", FineAmount);
                        SqlParameter outputParam = new SqlParameter("@NewFineID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        FineID = (int)command.Parameters["@NewFineID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return FineID ?? -1;
        }

        public static DataTable GetAllFines()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllFines", connection))
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

        public async static Task<DataTable> GetAllFinesAsync()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllFines", connection))
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

        public static bool UpdatePaymentStatus(int FineID, bool IsPaid)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdatePaymentStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FineID", FineID);
                        command.Parameters.AddWithValue("@IsPaid", IsPaid);

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

        public static bool UpdateFineAmount(int FineID, float FineAmount)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateFineAmount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FineID", FineID);
                        command.Parameters.AddWithValue("@FineAmount", FineAmount);

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

        public static float? GetMemberUnpaidFines(int MemberID)
        {
            float? UnpaidFees = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetMemberUnpaidFines", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        object result = command.ExecuteScalar();
                        if (float.TryParse(result.ToString(), out float insertedResult))
                            UnpaidFees = insertedResult;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return UnpaidFees ?? -1;
        }
    }
}
