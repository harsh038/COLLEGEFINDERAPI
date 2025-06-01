namespace CollegeFinderAPI.Models
{
    public class BranchModel
    {
        public int? BranchID { get; set; }
        public int CourseID { get; set; }
        public string BranchName { get; set; }
        public string Content { get; set; }
        public string About { get; set; }
        public CourseModel? CourseModel { get; set; }
    }
    public class BranchDropDownModel
    {
        public int BranchID { get; set; }
        public string BranchName { get; set; }
    }
}