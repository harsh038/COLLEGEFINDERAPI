using CollegeFinderAPI.Models;
using System.Text.Json.Serialization;

namespace CollegeFinderAPI.Models
{
    public class CollegeModel
    {
        public int? CollegeID { get; set; }
        public string Name { get; set; }
        public int CityID { get; set; }
        public string Type { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int EstablishmentYear { get; set; }
        public string Address { get; set; }  
        public CityModel? cityModel { get; set; }
    }

    public class CollegeDropDownModel
    {
        public int CollegeID { get; set; }
        public string Name { get; set; }
    }
}
