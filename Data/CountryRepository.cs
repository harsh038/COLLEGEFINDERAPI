using CollegeFinderAPI.Models;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class CountryRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public CountryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<CountryModel> SelectAll()
        {
            var countries = new List<CountryModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COUNTRY_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new CountryModel()
                    {
                        CountryID = Convert.ToInt32(reader["COUNTRYID"]),
                        Name = reader["NAME"].ToString()
                    });
                }
            }
            return countries;
        }
        #endregion

        #region SelectByID
        public CountryModel SelectById(int CountryID)
        {
            CountryModel country = new CountryModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COUNTRY_SELECTBYID";
                    cmd.Parameters.AddWithValue("@COUNTRYID", CountryID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        country.CountryID = Convert.ToInt32(reader["COUNTRYID"]);
                        country.Name = reader["NAME"].ToString();
                    }
                }
            }
            return country;
        }
        #endregion

        #region Insert
        public Boolean Insert(CountryModel country)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COUNTRY_INSERT";
                    cmd.Parameters.AddWithValue("@NAME", country.Name);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int countryID,CountryModel country)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COUNTRY_UPDATE";
                    cmd.Parameters.AddWithValue("@COUNTRYID", countryID);
                    cmd.Parameters.AddWithValue("@NAME", country.Name);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int CountryID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_COUNTRY_DELETE";
                    cmd.Parameters.AddWithValue("@COUNTRYID", CountryID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Dropdown
        public List<CountryModel> CountryDropDown()
        {
            var countries = new List<CountryModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_COUNTRY_DROPDOWN";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new CountryModel()
                    {
                        CountryID = Convert.ToInt32(reader["COUNTRYID"]),
                        Name = reader["NAME"].ToString()
                    });
                }
            }
            return countries;
        }
        #endregion

        #region countryIDInState
        public Boolean countryIDInState(int countryID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_COUNTRYID_IN_STATE";
                    cmd.Parameters.AddWithValue("@CountryID", countryID);
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
