using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Interfaces;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Managers
{
    public class GenericQueryManager<T> : IGenericQueryService<T>
        where T : class, IEntityBase, new()
    {
        private readonly IGenericQueryRepository<T> genericRepository;
        private readonly IMapper mapper;

        public GenericQueryManager(IGenericQueryRepository<T> genericRepository, IMapper mapper)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<D>> GetAllAsync<D>() where D : IDTO
        {
            IEnumerable<T> entities = await genericRepository.GetAllAsync();
            IEnumerable<D> result = mapper.Map<IEnumerable<D>>(entities);
            return result;
        }

        public async Task<IEnumerable<D>> GetAllWithDeletedAsync<D>() where D : IDTO
        {
            IEnumerable<T> entities = await genericRepository.GetAllWithDeletedAsync();
            IEnumerable<D> result = mapper.Map<IEnumerable<D>>(entities);
            return result;
        }
    }
}
