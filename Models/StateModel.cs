namespace CollegeFinderAPI.Models
{
    public class StateModel
    {
        public int? StateID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }
        public CountryModel? countryModel { get; set; }

    }
    public class StateDropDownModel
    {
        public int StateID { get; set; }
        public string Name { get; set; }
    }
}
