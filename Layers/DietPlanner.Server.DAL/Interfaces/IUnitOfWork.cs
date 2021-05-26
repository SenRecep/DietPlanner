using System;
using System.Threading.Tasks;

namespace DietPlanner.Server.DAL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public Task<bool> Commit(bool state = true);
    }
}
