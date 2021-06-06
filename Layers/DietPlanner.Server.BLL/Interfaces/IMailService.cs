using System.Threading.Tasks;

using DietPlanner.Server.BLL.Models;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IMailService
    {
        Task SendWelcomeEmailAsync(WelcomeRequest request);
    }
}
