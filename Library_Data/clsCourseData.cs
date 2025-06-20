using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Library_Data
{
    public class clsCourseData
    {
        public static DataTable GetAllCourses()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllCourses", connection))
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

        public async static Task<DataTable> GetAllCoursesAsync()
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllCourses", connection))
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

        public static DataTable GetAllMembersForCourse(int CourseID)
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllMembersForCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseID", CourseID);
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

        public async static Task<DataTable> GetAllMembersForCourseAsync(int CourseID)
        {
            DataTable table = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllMembersForCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseID", CourseID);
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

        public static int AddNewCourse(string CourseName, string TutorFirstName, string TutorLastName,
            float EnrollmentFees, byte MaxParticipants, DateTime StartDate, DateTime EndDate, string Notes)
        {
            int? CourseID = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseName", CourseName);
                        command.Parameters.AddWithValue("@TutorFirstName", TutorFirstName);
                        command.Parameters.AddWithValue("@TutorLastName", TutorLastName);
                        command.Parameters.AddWithValue("@EnrollmentFees", EnrollmentFees);
                        command.Parameters.AddWithValue("@MaxParticipants", MaxParticipants);
                        command.Parameters.AddWithValue("@StartDate", StartDate);
                        command.Parameters.AddWithValue("@EndDate", EndDate);
                        command.Parameters.AddWithValue("@Notes", (object)Notes ?? DBNull.Value);

                        SqlParameter outputParam = new SqlParameter("@NewCourseID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);
                        command.ExecuteNonQuery();
                        CourseID = (int)command.Parameters["@NewCourseID"].Value;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return CourseID ?? -1;
        }

        public static bool EnrollMemberInCourse(int MemberID, int CourseID)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_EnrollMemberInCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@CourseID", CourseID);
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out rowsEffected)) { }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

        public static bool Find(int CourseID, ref string CourseName, ref string TutorFirstName, ref string TutorLastName,
            ref decimal EnrollmentFees, ref byte MaxParticipants, ref DateTime StartDate, ref DateTime EndDate,
            ref string Notes)
        {
            bool isFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetCourseByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseID", CourseID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;
                                CourseName = (string)reader["CourseName"];
                                TutorFirstName = (string)reader["TutorFirstName"];
                                TutorLastName = (string)reader["TutorLastName"];
                                EnrollmentFees = (decimal)reader["EnrollmentFees"];
                                MaxParticipants = (byte)reader["MaxParticipants"];
                                StartDate = (DateTime)reader["StartDate"];
                                EndDate = (DateTime)reader["EndDate"];
                                Notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
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

        public static byte GetNumberOfEnrolledMembers(int CourseID)
        {
            byte NumberOfEnrolledMembers = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("SP_GetNumberOfEnrolledMembers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseID", CourseID);
                        object result = command.ExecuteScalar();
                        if (result != null && byte.TryParse(result.ToString(), out byte insertedResult))
                            NumberOfEnrolledMembers = insertedResult;
                    }
                }
            }
            catch(SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return NumberOfEnrolledMembers;
        }

        public static bool UpdateCourse(int CourseID, string CourseName, string TutorFirstName, 
            string TutorLastName, float EnrollmentFees, byte MaxParticipants, DateTime StartDate,
            DateTime EndDate, string Notes)
        {
            int rowsEffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateCourse", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CourseName", CourseName);
                        command.Parameters.AddWithValue("@TutorFirstName", TutorFirstName);
                        command.Parameters.AddWithValue("@TutorLastName", TutorLastName);
                        command.Parameters.AddWithValue("@EnrollmentFees", EnrollmentFees);
                        command.Parameters.AddWithValue("@MaxParticipants", MaxParticipants);
                        command.Parameters.AddWithValue("@StartDate", StartDate);
                        command.Parameters.AddWithValue("@EndDate", EndDate);
                        command.Parameters.AddWithValue("@Notes", Notes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CourseID", CourseID);

                        rowsEffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex.Message, EventLogEntryType.Error);
            }
            return rowsEffected > 0;
        }

    }
}
