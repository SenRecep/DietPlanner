using System;
using System.Threading.Tasks;

using DietPlanner.DTO.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IGenericSingleQueryService<T>
        where T : class, IEntityBase, new()
    {
        public Task<D> GetByIdAsync<D>(Guid id) where D : IDTO;
    }
}
