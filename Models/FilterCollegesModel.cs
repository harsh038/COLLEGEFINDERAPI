namespace CollegeFinderAPI.Models
{
    public class FilterCollegesModel
    {
        // Input Parameters for Filtering
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public int? CourseID { get; set; }
        public int? BranchID { get; set; }
        public decimal? MinFee { get; set; }
        public decimal? MaxFee { get; set; }

        // Output Properties from Stored Procedure
        public int CollegeCourseID { get; set; }
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public int? SeatsAvailable { get; set; }
        public string AdmissionCriteria { get; set; }
        public decimal Fee { get; set; }
        public string CollegeType { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int? EstablishmentYear { get; set; }
        public string Address { get; set; }

        // Newly Added Properties
        public int? StateID_Result { get; set; }
        public int? CountryID_Result { get; set; }
    }
}
