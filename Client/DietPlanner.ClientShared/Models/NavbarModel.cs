namespace DietPlanner.ClientShared.Models
{
    public class NavbarModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
    public enum NavbarType
    {
        None,
        Admin,
        Dietician,
        Patient
    }
}
