using CollegeFinderAPI.Models;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class CourseRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public CourseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<CourseModel> SelectAll()
        {
            var courses = new List<CourseModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COURSES_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courses.Add(new CourseModel()
                    {
                        CourseID = Convert.ToInt32(reader["COURSEID"]),
                        Name = reader["COURSENAME"].ToString(),
                        Duration = reader["DURATION"].ToString()
                    });
                }
            }
            return courses;
        }
        #endregion

        #region SelectByID
        public CourseModel SelectById(int CourseID)
        {
            CourseModel course = new CourseModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COURSES_SELECTBYID";
                    cmd.Parameters.AddWithValue("@COURSEID", CourseID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        course.CourseID = Convert.ToInt32(reader["COURSEID"]);
                        course.Name = reader["COURSENAME"].ToString();
                        course.Duration = reader["DURATION"].ToString();
                    }
                }
            }
            return course;
        }
        #endregion

        #region Insert
        public Boolean Insert(CourseModel course)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COURSES_INSERT";
                    cmd.Parameters.AddWithValue("@NAME", course.Name);
                    cmd.Parameters.AddWithValue("@DURATION", course.Duration);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int CourseID,CourseModel course)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COURSES_UPDATE";
                    cmd.Parameters.AddWithValue("@COURSEID",CourseID);
                    cmd.Parameters.AddWithValue("@NAME", course.Name);
                    cmd.Parameters.AddWithValue("@DURATION", course.Duration);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int CourseID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COURSES_DELETE";
                    cmd.Parameters.AddWithValue("@COURSEID", CourseID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Dropdown
        public List<CourseDropDownModel> CourseDropDown()
        {
            var courseDD = new List<CourseDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COURSE_DROPDOWN";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    courseDD.Add(new CourseDropDownModel()
                    {
                        CourseID = Convert.ToInt32(reader["COURSEID"]),
                        Name = reader["COURSENAME"].ToString()
                    });
                }
            }
            return courseDD;
        }
        #endregion

        #region CourseIDInCollegeCourses
        public Boolean CourseIDInCollegeCourses(int CourseID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_COURSEID_IN_COLLEGECOURSES";
                    cmd.Parameters.AddWithValue("@CourseID", CourseID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                           return true;
                    }
                }
                
            }
            return false;
        }
        #endregion
    }
}
