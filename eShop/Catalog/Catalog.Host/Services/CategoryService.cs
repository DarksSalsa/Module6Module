using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Core.Services.Interfaces;
using Infrastructure.Core.Services;

namespace Catalog.Host.Services
{
    public class CategoryService : BaseDataService<ApplicationDbContext>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IDbContextWrapper<ApplicationDbContext> dbContextWrapper, ILogger<BaseDataService<ApplicationDbContext>> logger, ICategoryRepository categoryRepository, IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<int?> AddAsync(string name, int typeId)
        {
            return await ExecuteSafeAsync(async () => await _categoryRepository.AddAsync(name, typeId));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _categoryRepository.DeleteAsync(id));
        }

        public async Task<CategoryDto?> UpdateAsync(int id, string property, string value)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _categoryRepository.UpdateAsync(id, property, value);
                if (result == null)
                {
                    return null;
                }

                return _mapper.Map<CategoryDto>(result);
            });
        }
    }
}
