using CollegeFinderAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CollegeFinderAPI.Data
{
    public class StateRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public StateRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region SelectAll
        public List<StateModel> SelectAll()
        {
            var states = new List<StateModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "API_STATE_SELECTALL";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    states.Add(new StateModel()
                    {
                        StateID = Convert.ToInt32(reader["STATEID"]),
                        CountryID = Convert.ToInt32(reader["COUNTRYID"]),
                        Name = reader["NAME"].ToString(),
                        countryModel = new CountryModel()
                        {
                            Name = reader["COUNTRYNAME"].ToString(),
                        }
                    });
                }
            }
            return states;
        }
        #endregion

        #region SelectByID
        public StateModel SelectById(int StateID)
        {
            StateModel state = new StateModel();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_STATE_SELECTBYID";
                    cmd.Parameters.AddWithValue("@STATEID", StateID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        state.StateID = Convert.ToInt32(reader["STATEID"]);
                        state.CountryID = Convert.ToInt32(reader["COUNTRYID"]);
                        state.Name = reader["NAME"].ToString();
                    }
                }
            }
            return state;
        }
        #endregion

        #region Insert
        public Boolean Insert(StateModel state)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_STATE_INSERT";
                    cmd.Parameters.AddWithValue("@NAME", state.Name);
                    cmd.Parameters.AddWithValue("@COUNTRYID", state.CountryID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Update
        public Boolean Update(int StateID, StateModel state)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_STATE_UPDATE";
                    cmd.Parameters.AddWithValue("@STATEID",StateID);
                    cmd.Parameters.AddWithValue("@NAME", state.Name);
                    cmd.Parameters.AddWithValue("@COUNTRYID", state.CountryID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region Delete
        public Boolean Delete(int StateID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_STATE_DELETE";
                    cmd.Parameters.AddWithValue("@STATEID", StateID);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        #endregion

        #region DropDown
        public List<StateDropDownModel> StateDropDown(int countryID)
        {
            var stateDD = new List<StateDropDownModel>();
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_STATE_DROPDOWN";
                    cmd.Parameters.AddWithValue("@COUNTRYID", countryID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        stateDD.Add(new StateDropDownModel() 
                        {
                            StateID = Convert.ToInt32(reader["STATEID"]),
                            Name = reader["NAME"].ToString(),
                        });
                    }
                }
            }
            return stateDD;
        }
        #endregion

        #region StateIDInCity
        public Boolean StateIDInCity(int stateID)
        {
            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FIND_STATEID_IN_CITY";
                    cmd.Parameters.AddWithValue("@StateID", stateID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.HasRows)
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
