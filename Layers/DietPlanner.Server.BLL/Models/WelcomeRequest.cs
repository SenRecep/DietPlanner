namespace DietPlanner.Server.BLL.Models
{
    public class WelcomeRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ToEmail { get; set; }
    }
}
