using CollegeFinderAPI.Models;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CollegeFinderAPI.Data
{
    public class CityRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public CityRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<CityModel> SelectAll()
        {
            var cities = new List<CityModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_CITY_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine(reader.Read());
                while (reader.Read())
                {
                    cities.Add(new CityModel()
                    {
                        CityID = Convert.ToInt32(reader["CITYID"]),
                        StateID = Convert.ToInt32(reader["STATEID"]),
                        Name = reader["NAME"].ToString(),
                        stateModel = new StateModel()
                        {
                            Name = reader["STATENAME"].ToString()
                        }
                        });
                }
            }
            return cities;
        }
        #endregion

        #region SelectByID
        public CityModel SelectById(int CityID)
        {
            CityModel city = new CityModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_CITY_SELECTBYID";
                    cmd.Parameters.AddWithValue("@CITYID", CityID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        city.CityID = Convert.ToInt32(reader["CITYID"]);
                        city.StateID = Convert.ToInt32(reader["STATEID"]);
                        city.Name = reader["NAME"].ToString();
                        city.CountryId = Convert.ToInt32(reader["COUNTRYID"]);
                    }
                }
            }
     
            return city;
        }
        #endregion

        #region Insert
        public Boolean Insert(CityModel city)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_CITY_INSERT";
                    cmd.Parameters.AddWithValue("@NAME", city.Name);
                    cmd.Parameters.AddWithValue("@STATEID", city.StateID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int CityID, CityModel city)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_CITY_UPDATE";
                    cmd.Parameters.AddWithValue("@CITYID", CityID);
                    cmd.Parameters.AddWithValue("@NAME", city.Name);
                    cmd.Parameters.AddWithValue("@STATEID", city.StateID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int CityID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_CITY_DELETE";
                    cmd.Parameters.AddWithValue("@CITYID", CityID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region DROPDOWN
        public List<CityDropDownModel> CityDropDown(int stateId)
        {
            var cityDD = new List<CityDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_CITY_DROPDOWN";
                    cmd.Parameters.AddWithValue("@STATEID", stateId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cityDD.Add(new CityDropDownModel()
                        {
                            CityID = Convert.ToInt32(reader["CITYID"]),
                            Name = reader["CITYNAME"].ToString(),
                        });
                    }
                }
            }
            return cityDD;
        }
        #endregion

        #region CityIDInColleges
        public Boolean CityIDInColleges(int CityID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_CITYID_IN_COLLEGES";
                    cmd.Parameters.AddWithValue("@CityID", CityID);
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
