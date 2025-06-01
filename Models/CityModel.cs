namespace CollegeFinderAPI.Models
{
    public class CityModel
    {
        public int? CityID { get; set; }
        public int StateID { get; set; }
        public string Name { get; set; }
        public StateModel? stateModel { get; set; }
        public int CountryId { get; set; }
    }
    public class CityDropDownModel
    {
        public int CityID { get; set; }
        public string Name { get; set; }
     }
}
