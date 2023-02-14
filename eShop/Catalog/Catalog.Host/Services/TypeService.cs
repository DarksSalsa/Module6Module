using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services.Interfaces;
using Infrastructure.Core.Services;

namespace Catalog.Host.Services
{
    public class TypeService : BaseDataService<ApplicationDbContext>, ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        private readonly IMapper _mapper;

        public TypeService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, ITypeRepository typeRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        public async Task<int?> AddAsync(string name)
        {
            return await ExecuteSafeAsync(async () => await _typeRepository.AddAsync(name));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _typeRepository.DeleteAsync(id));
        }

        public async Task<TypeDto?> UpdateAsync(int id, string property, string value)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _typeRepository.UpdateAsync(id, property, value);
                if (result == null)
                {
                    return null;
                }

                return _mapper.Map<TypeDto>(result);
            });
        }
    }
}
