using CollegeFinderAPI.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CollegeFinderAPI.Data
{
    public class FilterCollegesRepository
    {
        public readonly IConfiguration _configuration;
        string connectionStr;

        #region Configuration
        public FilterCollegesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionStr = this._configuration.GetConnectionString("MyConnectionString");
        }
        #endregion

        #region FilterColleges
        public List<FilterCollegesModel> FilterColleges(FilterCollegesInputModel filter)
        {
            var filteredColleges = new List<FilterCollegesModel>();

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "API_FILTERED_COLLEGES";

                    cmd.Parameters.AddWithValue("@CountryID", filter.CountryID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@StateID", filter.StateID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CityID", filter.CityID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CourseID", filter.CourseID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@BranchID", filter.BranchID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@MinFee", filter.MinFee ?? 0);
                    cmd.Parameters.AddWithValue("@MaxFee", filter.MaxFee ?? 1000000);

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Log all column names returned by the query
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Debug.WriteLine($"COLUMN {i}: {reader.GetName(i)}");
                    }

                    while (reader.Read())
                    {
                        var model = new FilterCollegesModel()
                        {
                            CollegeCourseID = Convert.ToInt32(reader["CollegeCourseID"]),
                            CollegeID = Convert.ToInt32(reader["CollegeID"]),
                            CollegeName = reader["CollegeName"].ToString(),
                            CityID = reader["CityID"] == DBNull.Value ? null : Convert.ToInt32(reader["CityID"]),
                            CourseID = reader["CourseID"] == DBNull.Value ? null : Convert.ToInt32(reader["CourseID"]),
                            BranchID = reader["BranchID"] == DBNull.Value ? null : Convert.ToInt32(reader["BranchID"]),
                            SeatsAvailable = reader["SeatsAvailable"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SeatsAvailable"]),
                            AdmissionCriteria = reader["AdmissionCriteria"].ToString(),
                            Fee = Convert.ToDecimal(reader["Fee"]),
                            CollegeType = reader["CollegeType"].ToString(),
                            Website = reader["Website"].ToString(),
                            Description = reader["Description"].ToString(),
                            EstablishmentYear = reader["EstablishmentYear"] == DBNull.Value ? null : Convert.ToInt32(reader["EstablishmentYear"]),
                            Address = reader["Address"].ToString(),

                            // Only assign StateID_Result if the column exists in the result set
                            StateID_Result = reader.HasColumn("StateID") ? (reader["StateID"] == DBNull.Value ? null : Convert.ToInt32(reader["StateID"])) : null,
                            CountryID_Result = reader.HasColumn("CountryID") ? (reader["CountryID"] == DBNull.Value ? null : Convert.ToInt32(reader["CountryID"])) : null
                        };

                        filteredColleges.Add(model);
                    }
                }
            }

            return filteredColleges;
        }
        #endregion
    }

    // Extension method to check for column existence
    public static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
