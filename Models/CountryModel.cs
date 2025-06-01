namespace CollegeFinderAPI.Models
{
    public class CountryModel
    {
        public int? CountryID { get; set; }
        public string Name { get; set; }
    }
    public class CountryDropDownModel
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
    }
}
