using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services;
using Infrastructure.Core.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class BrandService : BaseDataService<ApplicationDbContext>, IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, IBrandRepository brandRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<int?> AddAsync(string name)
        {
            return await ExecuteSafeAsync(async () => await _brandRepository.AddAsync(name));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _brandRepository.DeleteAsync(id));
        }

        public async Task<BrandDto?> UpdateAsync(int id, string property, string value)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _brandRepository.UpdateAsync(id, property, value);
                if (result == null)
                {
                    return null;
                }

                return _mapper.Map<BrandDto>(result);
            });
        }
    }
}
