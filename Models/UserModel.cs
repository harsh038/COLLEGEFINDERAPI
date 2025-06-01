namespace CollegeFinderAPI.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        private string _role = "User"; 
        public string? Role
        {
            get => _role;
            set => _role = string.IsNullOrEmpty(value) ? "User" : value;
        }

    }
}
