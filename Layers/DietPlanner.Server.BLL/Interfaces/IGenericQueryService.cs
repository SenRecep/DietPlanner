using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DietPlanner.DTO.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Interfaces
{
    public interface IGenericQueryService<T>
        where T : class, IEntityBase, new()
    {
        public Task<IEnumerable<D>> GetAllAsync<D>() where D : IDTO;
        public Task<IEnumerable<D>> GetAllWithDeletedAsync<D>() where D : IDTO;
    }
}
