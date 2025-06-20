using Library_Data;
using LibrarySystem_Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Library_Business
{
    public class clsCourse
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string TutorFirstName { get; set; }
        public string TutorLastName { get; set; }
        public float EnrollmentFees { get; set; }
        public byte MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }

        private enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;

        public string TutorName
        {
            get => this.TutorFirstName + " " + this.TutorLastName;
        }

        public string Status
        {
            get
            {
                if (this.EndDate <= DateTime.Now)
                    return "Expired";
                if (this.StartDate > DateTime.Now)
                    return "Comming Soon";
                return "Active";
            }
        }

        public byte NumberOfEnrolledMembers
        {
            get => clsCourseData.GetNumberOfEnrolledMembers(this.CourseID);
        }

        public clsCourse()
        {
            this.CourseID = -1;
            this.CourseName = string.Empty;
            this.TutorFirstName = string.Empty;
            this.TutorLastName = string.Empty;
            this.EnrollmentFees = 0;
            this.MaxParticipants = 0;
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MinValue;
            this.Notes = null;
            this._Mode = enMode.AddNew;
        }

        private clsCourse(int courseID, string courseName, string tutorFirstName, string tutorLastName,
            float enrollmentFees, byte maxParticipants, DateTime startDate, DateTime endDate, string notes)
        {
            CourseID = courseID;
            CourseName = courseName;
            TutorFirstName = tutorFirstName;
            TutorLastName = tutorLastName;
            EnrollmentFees = enrollmentFees;
            MaxParticipants = maxParticipants;
            StartDate = startDate;
            EndDate = endDate;
            Notes = notes;
            _Mode = enMode.Update;
        }

        private bool _AddNewCourse()
        {
            this.CourseID = clsCourseData.AddNewCourse(this.CourseName, this.TutorFirstName,
                this.TutorLastName, this.EnrollmentFees, this.MaxParticipants, this.StartDate,
                this.EndDate, this.Notes);
            return (this.CourseID != -1);
        }

        private bool _UpdateCourse()
        {
            return clsCourseData.UpdateCourse(this.CourseID, this.CourseName, this.TutorFirstName,
                this.TutorLastName, this.EnrollmentFees, this.MaxParticipants, this.StartDate,
                this.EndDate, this.Notes);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.AddNew:
                    if(_AddNewCourse())
                    {
                        this._Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateCourse();
            }
            return false;
        }

        public static DataTable GetAllCourses() => clsCourseData.GetAllCourses();

        public async static Task<DataTable> GetAllCoursesAsync() => 
            await clsCourseData.GetAllCoursesAsync();

        public static bool EnrollMemberInCourse(int MemberID, int CourseID) =>
            clsCourseData.EnrollMemberInCourse(MemberID, CourseID);

        public static clsCourse Find(int CourseID)
        {
            string CourseName = string.Empty;
            string TutorFirstName = string.Empty;
            string TutorLastName = string.Empty;
            decimal EnrollmentFees = 0;
            byte MaxParticipants = 0;
            DateTime StartDate = DateTime.MinValue;
            DateTime EndDate = DateTime.MinValue;
            string Notes = null;

            if (clsCourseData.Find(CourseID, ref CourseName, ref TutorFirstName, ref TutorLastName, ref EnrollmentFees,
                ref MaxParticipants, ref StartDate, ref EndDate, ref Notes))
                return new clsCourse(CourseID, CourseName, TutorFirstName, TutorLastName,
                    Convert.ToSingle(EnrollmentFees), MaxParticipants, StartDate, EndDate, Notes);
            else
                return null;
        }

        public static DataTable GetAllMembersForCourse(int CourseID) => 
            clsCourseData.GetAllMembersForCourse(CourseID);

        public async static Task<DataTable> GetAllMembersForCourseAsync(int CourseID)
            => await clsCourseData.GetAllMembersForCourseAsync(CourseID);
    }

}
