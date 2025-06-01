using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class UserRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<UserModel> selectAll() 
        {
            var users = new List<UserModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr)) 
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_USERS_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new UserModel()
                    {
                        UserID = Convert.ToInt32(reader["USERID"]),
                        FirstName = reader["FIRSTNAME"].ToString(),
                        LastName = reader["LASTNAME"].ToString(),
                        Email = reader["EMAIL"].ToString(),
                        PasswordHash = reader["PASSWORDHASH"].ToString(),
                        Role = reader["ROLE"].ToString()
                        
                    });
                }
            }
            return users;
        }
        #endregion

        #region SelectByID
        public UserModel SelectById(int UserId)
        {
            UserModel user = new UserModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_USERS_SELECTBYID";
                    cmd.Parameters.AddWithValue("@USERID", UserId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user.UserID = Convert.ToInt32(reader["USERID"]);
                        user.FirstName = reader["FIRSTNAME"].ToString();
                        user.LastName = reader["LASTNAME"].ToString();
                        user.Email = reader["EMAIL"].ToString();
                        user.PasswordHash = reader["PASSWORDHASH"].ToString();
                        user.Role = reader["ROLE"].ToString();
                    }
                    return user;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int userID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_USERS_DELETE";
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    if (cmd.ExecuteNonQuery() > 0) return true;
                    return false;
                }
            }
        }
        #endregion

        #region Insert
        public Boolean Insert(UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_USERS_INSERT";
                    cmd.Parameters.AddWithValue("@FIRSTNAME", user.FirstName);
                    cmd.Parameters.AddWithValue("@LASTNAME", user.LastName);
                    cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                    cmd.Parameters.AddWithValue("@PASSWORDHASH", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@ROLE", user.Role);

                    if (cmd.ExecuteNonQuery() > 0) return true;
                    else return false;
                }
            }

        }
        #endregion

        #region Update
        public Boolean Update(int UserId,UserModel user)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_USERS_UPDATE";
                    cmd.Parameters.AddWithValue("@USERID", UserId);
                    cmd.Parameters.AddWithValue("@FIRSTNAME", user.FirstName);
                    cmd.Parameters.AddWithValue("@LASTNAME", user.LastName);
                    cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                    cmd.Parameters.AddWithValue("@PASSWORDHASH", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@ROLE", user.Role);
                    if (cmd.ExecuteNonQuery() > 0) return true;
                    else return false;
                }
            }
        }
        #endregion

        #region userIDInReview
        public Boolean userIDInReview(int UserId)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_USERID_IN_REVIEWS";
                    cmd.Parameters.AddWithValue("@USERID", UserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
        #endregion

    }
}
