using System.Threading.Tasks;

using AutoMapper;

using DietPlanner.DTO.Interfaces;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.DAL.Interfaces;
using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.BLL.Managers
{
    public class GenericCommandManager<T> : IGenericCommandService<T>
        where T : class, IEntityBase, new()
    {
        private readonly IGenericCommandRepository<T> genericRepository;
        private readonly IGenericSingleQueryRepository<T> genericSingleQueryRepository;
        private readonly IMapper mapper;
        private readonly ICustomMapper customMapper;

        public GenericCommandManager(IGenericCommandRepository<T> genericRepository, IGenericSingleQueryRepository<T> genericSingleQueryRepository, IMapper mapper, ICustomMapper customMapper)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.customMapper = customMapper;
            this.genericSingleQueryRepository = genericSingleQueryRepository;
        }

        public async Task<T> AddAsync<D>(D dto) where D : IDTO
        {
            T entity = mapper.Map<T>(dto);
            await genericRepository.AddAsync(entity);
            return entity;
        }
        public async Task RemoveAsync<D>(D dto, bool hardDelete = false) where D : IDTO
        {
            T dummyEntity = mapper.Map<T>(dto);
            T orjinal = await genericSingleQueryRepository.GetByIdAsync(dummyEntity.Id);
            orjinal = customMapper.Map(dto, orjinal);
            await genericRepository.RemoveAsync(orjinal, hardDelete);
        }

        public async Task UpdateAsync<D>(D dto) where D : IDTO
        {
            T dummyEntity = mapper.Map<T>(dto);
            T orjinal = await genericSingleQueryRepository.GetByIdAsync(dummyEntity.Id);
            orjinal = customMapper.Map(dto, orjinal);
            await genericRepository.UpdateAsync(orjinal);
        }

        public async Task<bool> Commit(bool state = true) => await genericRepository.Commit(state);
    }
}
