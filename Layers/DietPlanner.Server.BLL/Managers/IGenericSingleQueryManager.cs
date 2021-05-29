
using System;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Interfaces;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Managers
{
    public class IGenericSingleQueryManager<T> : IGenericSingleQueryService<T>
        where T : class, IEntityBase, new()
    {
        private readonly IGenericSingleQueryRepository<T> genericRepository;
        private readonly IMapper mapper;

        public IGenericSingleQueryManager(IGenericSingleQueryRepository<T> genericRepository, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }

        public async Task<D> GetByIdAsync<D>(Guid id) where D : IDTO
        {
            T entity = await genericRepository.GetByIdAsync(id);
            D result = mapper.Map<D>(entity);
            return result;
        }
    }
}
