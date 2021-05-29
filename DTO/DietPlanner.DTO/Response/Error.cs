using System.Collections.Generic;

namespace DietPlanner.DTO.Response
{
    public record Error(IEnumerable<string> Errors, bool IsShow, string Path)
    {
        public static Error SendError(string path = "", bool isShow = true, params string[] errors) => new(errors, isShow, path);
    }
}
