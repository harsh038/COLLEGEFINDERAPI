using CollegeFinderAPI.Models;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace CollegeFinderAPI.Data
{
    public class CollegeCourseRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public CollegeCourseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<CollegeCourseModel> SelectAll()
        {
            var collegeCourses = new List<CollegeCourseModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COLLEGECOURSES_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    collegeCourses.Add(new CollegeCourseModel()
                    {
                        CollegeCourseID = Convert.ToInt32(reader["COLLEGECOURSEID"]),
                        CollegeID = Convert.ToInt32(reader["COLLEGEID"]),
                        CourseID = Convert.ToInt32(reader["COURSEID"]),
                        BranchID = Convert.ToInt32(reader["BRANCHID"]),
                        SeatAvailable = Convert.ToInt32(reader["SEATAVAILABLE"]),
                        AdmissionCriteria = reader["ADMISSIONCRITERIA"].ToString(),
                        Fee = reader["FEE"].ToString(),
                        TotalCourses = Convert.ToInt32(reader["TOTALCOURSES"]),
                        TotalSeatAvailable = Convert.ToInt32(reader["TOTALSEATSAVAILABLE"]),
                        collegeModel = new CollegeModel()
                        {
                            Name = reader["COLLEGENAME"].ToString()
                        },
                        courseModel = new CourseModel()
                        {
                            Name = reader["COURSENAME"].ToString()
                        },
                        branchModel = new BranchModel()
                        {
                            BranchName = reader["BRANCHNAME"].ToString()
                        }
                    });
                }
            }
            return collegeCourses;
        }
        #endregion

        #region SelectByID
        public CollegeCourseModel SelectById(int CollegeCourseID)
        {
            CollegeCourseModel collegeCourse = new CollegeCourseModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGECOURSES_SELECTBYID";
                    cmd.Parameters.AddWithValue("@COLLEGECOURSEID", CollegeCourseID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        collegeCourse.CollegeCourseID = Convert.ToInt32(reader["COLLEGECOURSEID"]);
                        collegeCourse.CollegeID = Convert.ToInt32(reader["COLLEGEID"]);
                        collegeCourse.CourseID = Convert.ToInt32(reader["COURSEID"]);
                        collegeCourse.BranchID = Convert.ToInt32(reader["BRANCHID"]);
                        collegeCourse.SeatAvailable = Convert.ToInt32(reader["SEATAVAILABLE"]);
                        collegeCourse.AdmissionCriteria = reader["ADMISSIONCRITERIA"].ToString();
                        collegeCourse.Fee = reader["FEE"].ToString();
                        collegeCourse.courseModel = new CourseModel
                        {
                            Name = reader["COURSENAME"].ToString()
                        };
                        collegeCourse.collegeModel = new CollegeModel
                        {
                            Name = reader["COLLEGENAME"].ToString()
                        };
                        collegeCourse.branchModel = new BranchModel
                        {
                            BranchName = reader["BRANCHNAME"].ToString()
                        };
                    }
                }
            }
            return collegeCourse;
        }
        #endregion

        #region SelectByCollegeID
        public List<CollegeCourseModel> SelectByCollegeID(int collegeID)
        {
            var collegeCourses = new List<CollegeCourseModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGECOURSES_SELECTBYCOLLEGEID";
                    cmd.Parameters.AddWithValue("@CollegeID", collegeID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        collegeCourses.Add(new CollegeCourseModel()
                        {
                            CollegeCourseID = Convert.ToInt32(reader["COLLEGECOURSEID"]),
                            CollegeID = Convert.ToInt32(reader["COLLEGEID"]),
                            CourseID = Convert.ToInt32(reader["COURSEID"]),
                            BranchID = Convert.ToInt32(reader["BRANCHID"]),
                            SeatAvailable = Convert.ToInt32(reader["SEATAVAILABLE"]),
                            AdmissionCriteria = reader["ADMISSIONCRITERIA"].ToString(),
                            Fee = reader["FEE"].ToString(),
                            collegeModel = new CollegeModel()
                            {
                                Name = reader["COLLEGENAME"].ToString()
                            },
                            courseModel = new CourseModel()
                            {
                                Name = reader["COURSENAME"].ToString()
                            },
                            branchModel = new BranchModel()
                            {
                                BranchName = reader["BRANCHNAME"].ToString()
                            }
                        });
                    }
                }
            }
            return collegeCourses;
        }
        #endregion


        #region Insert
        public Boolean Insert(CollegeCourseModel collegeCourse)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGECOURSES_INSERT";
                    cmd.Parameters.AddWithValue("@COLLEGEID", collegeCourse.CollegeID);
                    cmd.Parameters.AddWithValue("@COURSEID", collegeCourse.CourseID);
                    cmd.Parameters.AddWithValue("@BRANCHID", collegeCourse.BranchID);
                    cmd.Parameters.AddWithValue("@SEATAVAILABLE", collegeCourse.SeatAvailable);
                    cmd.Parameters.AddWithValue("@ADMISSIONCRITERIA", collegeCourse.AdmissionCriteria);
                    cmd.Parameters.AddWithValue("@FEE", collegeCourse.Fee);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int collegeCourseID,CollegeCourseModel collegeCourse)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGECOURSES_UPDATE";
                    cmd.Parameters.AddWithValue("@COLLEGECOURSEID", collegeCourseID);
                    cmd.Parameters.AddWithValue("@COLLEGEID", collegeCourse.CollegeID);
                    cmd.Parameters.AddWithValue("@COURSEID", collegeCourse.CourseID);
                    cmd.Parameters.AddWithValue("@BRANCHID", collegeCourse.BranchID);
                    cmd.Parameters.AddWithValue("@SEATAVAILABLE", collegeCourse.SeatAvailable);
                    cmd.Parameters.AddWithValue("@ADMISSIONCRITERIA", collegeCourse.AdmissionCriteria);
                    cmd.Parameters.AddWithValue("@FEE", collegeCourse.Fee);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int CollegeCourseID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGECOURSES_DELETE";
                    cmd.Parameters.AddWithValue("@COLLEGECOURSEID", CollegeCourseID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        
    }
}
