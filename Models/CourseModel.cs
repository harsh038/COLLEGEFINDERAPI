namespace CollegeFinderAPI.Models
{
    public class CourseModel
    {
        public int? CourseID { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
    }
    public class CourseDropDownModel
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
    }
}
