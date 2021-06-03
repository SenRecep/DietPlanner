
using Microsoft.AspNetCore.Components;

namespace DietPlanner.ClientShared.ExtensionMethods
{
    public static class NavigationManagerExtensionMethods
    {
        public static string GetUrl(this NavigationManager navigationManager) => $"/{navigationManager.ToBaseRelativePath(navigationManager.Uri)}";
    }
}
