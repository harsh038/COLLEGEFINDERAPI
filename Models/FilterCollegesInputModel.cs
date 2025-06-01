namespace CollegeFinderAPI.Models
{
    public class FilterCollegesInputModel
    {
        private int? _countryID;
        private int? _stateID;
        private int? _cityID;
        private int? _courseID;
        private int? _branchID;

        public int? CountryID
        {
            get => _countryID == 0 ? null : _countryID;
            set => _countryID = value;
        }

        public int? StateID
        {
            get => _stateID == 0 ? null : _stateID;
            set => _stateID = value;
        }

        public int? CityID
        {
            get => _cityID == 0 ? null : _cityID;
            set => _cityID = value;
        }

        public int? CourseID
        {
            get => _courseID == 0 ? null : _courseID;
            set => _courseID = value;
        }

        public int? BranchID
        {
            get => _branchID == 0 ? null : _branchID;
            set => _branchID = value;
        }

        public decimal? MinFee { get; set; } = 0;
        public decimal? MaxFee { get; set; } = 1000000;
    }
}
