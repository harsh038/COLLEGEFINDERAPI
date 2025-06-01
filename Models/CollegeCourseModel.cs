namespace CollegeFinderAPI.Models
{
    public class CollegeCourseModel
    {
        public int? CollegeCourseID { get; set; }
        public int CollegeID { get; set; }
        public int CourseID { get; set; }
        public int BranchID { get; set; }
        public int SeatAvailable { get; set; }
        public string AdmissionCriteria { get; set; }
        public string Fee { get; set; }
        public int? TotalCourses { get; set; }
        public int? TotalSeatAvailable { get; set; }
        public CollegeModel? collegeModel { get; set; }
        public CourseModel? courseModel { get; set; }
        public BranchModel? branchModel { get; set; }

    }
}
