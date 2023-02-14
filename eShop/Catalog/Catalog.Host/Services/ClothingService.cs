using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services.Interfaces;
using Infrastructure.Core.Services;

namespace Catalog.Host.Services
{
    public class ClothingService : BaseDataService<ApplicationDbContext>, IClothingService
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly IMapper _mapper;

        public ClothingService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, IClothingRepository clothingRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _clothingRepository = clothingRepository;
            _mapper = mapper;
        }

        public async Task<int?> AddAsync(string name, string color, string size, int categoryId, int brandId, decimal price, int availableStock, string image, string season)
        {
            return await ExecuteSafeAsync(async () => await _clothingRepository.AddAsync(name, color, size, categoryId, brandId, price, availableStock, image, season));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _clothingRepository.DeleteAsync(id));
        }

        public async Task<ClothingDto?> UpdateAsync(int id, string property, string value)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _clothingRepository.UpdateAsync(id, property, value);
                if (result == null)
                {
                    return null;
                }

                return _mapper.Map<ClothingDto>(result);
            });
        }
    }
}
