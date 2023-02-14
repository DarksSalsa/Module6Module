using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services;
using Infrastructure.Core.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class ShowcaseService : BaseDataService<ApplicationDbContext>, IShowcaseService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IClothingRepository _clothingRepository;
        private readonly IMapper _mapper;

        public ShowcaseService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, IBrandRepository brandRepository, ITypeRepository typeRepository, ICategoryRepository categoryRepository, IClothingRepository clothingRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _categoryRepository = categoryRepository;
            _clothingRepository = clothingRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BrandDto?>> GetAllBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _brandRepository.GetAllAsync();
                return result.Select(s => _mapper.Map<BrandDto>(s));
            });
        }

        public async Task<IEnumerable<CategoryDto?>> GetAllCategoriesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _categoryRepository.GetAllAsync();
                return result.Select(s => _mapper.Map<CategoryDto>(s));
            });
        }

        public async Task<IEnumerable<TypeDto?>> GetAllTypesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _typeRepository.GetAllAsync();
                return result.Select(s => _mapper.Map<TypeDto>(s));
            });
        }

        public async Task<PaginatedItemResponse<ClothingDto>?> GetByPageAsync(int pageIndex, int pageSize, int? brandIdFilter = null, int? categoryIdFilter = null)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _clothingRepository.GetByPageAsync(pageIndex, pageSize, brandIdFilter, categoryIdFilter);
                if (result == null)
                {
                    return null;
                }

                return new PaginatedItemResponse<ClothingDto>()
                {
                    Count = result.Count,
                    Content = result.Content.Select(s => _mapper.Map<ClothingDto>(s)),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<ClothingDto?> GetClothingByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _clothingRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return null;
                }

                return _mapper.Map<ClothingDto>(result);
            });
        }
    }
}
