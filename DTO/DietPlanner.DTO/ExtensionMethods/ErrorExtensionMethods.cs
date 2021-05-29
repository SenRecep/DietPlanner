
using DietPlanner.DTO.Response;

namespace DietPlanner.DTO.ExtensionMethods
{
    public static class ErrorExtensionMethods
    {
        public static string GetErrors(this Error error, string separator = "\n") => string.Join(separator, error.Errors);
    }
}
