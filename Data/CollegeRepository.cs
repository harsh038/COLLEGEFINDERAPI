using CollegeFinderAPI.Models;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class CollegeRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public CollegeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<CollegeModel> SelectAll()
        {
            var colleges = new List<CollegeModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COLLEGES_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    colleges.Add(new CollegeModel()
                    {
                        CollegeID = Convert.ToInt32(reader["COLLEGEID"]),
                        Name = reader["NAME"].ToString(),
                        CityID = Convert.ToInt32(reader["CITYID"]),
                        Type = reader["TYPE"].ToString(),
                        Address = reader["ADDRESS"].ToString(),
                        EstablishmentYear = Convert.ToInt32(reader["ESTABLISHMENT_YEAR"]),
                        Website = reader["WEBSITE"].ToString(),
                        Description = reader["DESCRIPTION"].ToString(),
                        cityModel = new CityModel()
                        {
                            Name = reader["CITYNAME"].ToString()
                        }
                    });
                }
            }
            return colleges;
        }
        #endregion

        #region SelectByID
        public CollegeModel SelectById(int CollegeID)
        {
            CollegeModel college = new CollegeModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGES_SELECTBYID";
                    cmd.Parameters.AddWithValue("@COLLEGEID", CollegeID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        college.CollegeID = Convert.ToInt32(reader["COLLEGEID"]);
                        college.Name = reader["NAME"].ToString();
                        college.CityID = Convert.ToInt32(reader["CITYID"]);
                        college.Type = reader["TYPE"].ToString();
                        college.Address = reader["ADDRESS"].ToString();
                        college.EstablishmentYear = Convert.ToInt32(reader["ESTABLISHMENT_YEAR"]);
                        college.Website = reader["WEBSITE"].ToString();
                        college.Description = reader["DESCRIPTION"].ToString();
                        college.CountryID = Convert.ToInt32(reader["COUNTRYID"]);
                        college.StateID = Convert.ToInt32(reader["STATEID"]);
                        college.cityModel = new CityModel
                        {
                            Name = reader["CityName"].ToString()
                        };
                    }
                }
            }
            return college;
        }
        #endregion

        #region Insert
        public Boolean Insert(CollegeModel college)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGES_INSERT";
                    cmd.Parameters.AddWithValue("@NAME", college.Name);
                    cmd.Parameters.AddWithValue("@CITYID", college.CityID);
                    cmd.Parameters.AddWithValue("@TYPE", college.Type);
                    cmd.Parameters.AddWithValue("@ADDRESS", college.Address);
                    cmd.Parameters.AddWithValue("@ESTABLISHMENT_YEAR", college.EstablishmentYear);
                    cmd.Parameters.AddWithValue("@WEBSITE", college.Website);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", college.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int CollegeID, CollegeModel college)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGES_UPDATE";
                    cmd.Parameters.AddWithValue("@COLLEGEID", CollegeID);
                    cmd.Parameters.AddWithValue("@NAME", college.Name);
                    cmd.Parameters.AddWithValue("@CITYID", college.CityID);
                    cmd.Parameters.AddWithValue("@TYPE", college.Type);
                    cmd.Parameters.AddWithValue("@ADDRESS", college.Address);
                    cmd.Parameters.AddWithValue("@ESTABLISHMENT_YEAR", college.EstablishmentYear);
                    cmd.Parameters.AddWithValue("@WEBSITE", college.Website);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", college.Description);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int CollegeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGES_DELETE";
                    cmd.Parameters.AddWithValue("@COLLEGEID", CollegeID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region DropdownForCity
        public List<CollegeDropDownModel> CollegeDropDown(int CityID)
        {
            var collegeDD = new List<CollegeDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COLLEGES_DROPDOWN";
                    cmd.Parameters.AddWithValue("@CITYID", CityID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        collegeDD.Add(new CollegeDropDownModel()
                        {
                            CollegeID = Convert.ToInt32(reader["COLLEGEID"]),
                            Name = reader["COLLEGENAME"].ToString()
                        });
                    }
                }
            }
            return collegeDD;
        }
        #endregion

        #region OnlyCollegeDropDown
        public List<CollegeDropDownModel> OnlyCollegeDropDown()
        {
            var collegeDD = new List<CollegeDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_ONLY_COLLEGES_DROPDOWN";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    collegeDD.Add(new CollegeDropDownModel()
                    {
                        CollegeID = Convert.ToInt32(reader["COLLEGEID"]),
                        Name = reader["COLLEGENAME"].ToString()
                    });
                }
            }
            return collegeDD;
        }
        #endregion

        #region CollegeIDInCollageCourses
        public Boolean CollegeIDInCollageCourses(int CollegeID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();

                // First query checking CollegeCourses
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_COLLEGEID_IN_COLLEGECOURSES";
                    cmd.Parameters.AddWithValue("@CollegeID", CollegeID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }

            return false;
        }
        #endregion
    }
}
