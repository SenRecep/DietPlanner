namespace DietPlanner.ClientShared.StringInfo
{
    public struct UrlInfo
    {
        public const string Index = "/";
        public const string Admin = "/admin";
        public const string Dietician = "/dietician";
        public const string Report = "/report";

        public const string Login = "/auth/login";
        public const string Register = "/auth/register";
        public const string Logout = "/auth/logout";
        public const string AccessDenied = "/auth/accessdenied";
        public const string Unauthorized = "/auth/unauthorized";

        public const string InternalServerError = "/500";
        public const string NotFound = "/404";
    }
}
