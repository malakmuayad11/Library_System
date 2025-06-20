using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Library_Data
{
    public class clsMembershipTypeData
    {
        public static DataTable GetAllMembershipTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembershipTypes", connection))
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
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return dt;
        }

        public async static Task<DataTable> GetAllMembershipTypesAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembershipTypes", connection))
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
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return dt;
        }

        public static bool Find(int MembershipTypeID, ref string MembershipName, ref byte NumberOfAllowedBooksToBorrow)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using(SqlCommand command = new SqlCommand("SP_GetMembershipTypeByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MembershipTypeID", MembershipTypeID);

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if(reader.Read())
                            {
                                isFound = true;
                                MembershipName = (string)reader["MembershipName"];
                                NumberOfAllowedBooksToBorrow = (byte)reader["NumberOfAllowedBooksToBorrow"];
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
