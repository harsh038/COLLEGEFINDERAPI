using CollegeFinderAPI.Models;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class BranchRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public BranchRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<BranchModel> SelectAll()
        {
            var branches = new List<BranchModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_BRANCHES_SELECTALL"; 
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    branches.Add(new BranchModel()
                    {
                        BranchID = Convert.ToInt32(reader["BRANCHID"]), 
                        CourseID = Convert.ToInt32(reader["COURSEID"]),
                        Content = reader["CONTENT"].ToString(),
                        BranchName = reader["BRANCHNAME"].ToString(),
                        About = reader["ABOUT"].ToString(),
                        CourseModel = new CourseModel()
                        {
                            Name = reader["COURSENAME"].ToString()
                        }
                    });
                }
            }
            return branches;
        }
        #endregion

        #region SelectByID
        public BranchModel SelectById(int BranchID) 
        {
            BranchModel branch = new BranchModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_BRANCHES_SELECTBYID";  
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID); 
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        branch.BranchID = Convert.ToInt32(reader["BRANCHID"]);  
                        branch.CourseID = Convert.ToInt32(reader["COURSEID"]);
                        branch.Content = reader["CONTENT"].ToString();
                        branch.About = reader["ABOUT"].ToString();
                        branch.BranchName = reader["BRANCHNAME"].ToString();
                        branch.CourseModel = new CourseModel
                        {
                            Name = reader["COURSENAME"].ToString() 
                        };

                    }
                }
            }
            return branch;
        }
        #endregion

        #region Insert
        public Boolean Insert(BranchModel branch)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_BRANCHES_INSERT";  
                    cmd.Parameters.AddWithValue("@COURSEID", branch.CourseID);
                    cmd.Parameters.AddWithValue("@CONTENT", branch.Content);
                    cmd.Parameters.AddWithValue("@ABOUT", branch.About);
                    cmd.Parameters.AddWithValue("@BRANCHNAME", branch.BranchName); 
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int BranchID, BranchModel branch)  
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_BRANCHES_UPDATE";  
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);  
                    cmd.Parameters.AddWithValue("@COURSEID", branch.CourseID);
                    cmd.Parameters.AddWithValue("@ABOUT", branch.About);
                    cmd.Parameters.AddWithValue("@CONTENT", branch.Content);
                    cmd.Parameters.AddWithValue("@BRANCHNAME", branch.BranchName);  
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int BranchID)  
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_BRANCHES_DELETE";  
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);  
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region BranchIDInCollegeCourses
        public Boolean BranchIDInCollegeCourses(int BranchID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_BRANCHID_IN_COLLEGECOURSES";
                    cmd.Parameters.AddWithValue("@branchID", BranchID);
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

        #region Dropdown
        public List<BranchDropDownModel> BranchDropDown(int courseID)
        {
            var branchDD = new List<BranchDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_BRANCHES_DROPDOWN";
                cmd.Parameters.AddWithValue("@CourseID", courseID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    branchDD.Add(new BranchDropDownModel()
                    {
                        BranchID = Convert.ToInt32(reader["BRANCHID"]),
                        BranchName = reader["BRANCHNAME"].ToString()
                    });
                }
            }
            return branchDD;
        }
        #endregion
    }
}
