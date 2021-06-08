using System.Threading.Tasks;

using DietPlanner.Server.BLL.Models;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IMessageService
    {
        public Task SendWelcomeAsync(WelcomeRequest request);
    }
}
