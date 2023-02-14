using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces
{
    public interface IShowcaseService
    {
        Task<IEnumerable<BrandDto?>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto?>> GetAllTypesAsync();
        Task<IEnumerable<CategoryDto?>> GetAllCategoriesAsync();
        Task<PaginatedItemResponse<ClothingDto>?> GetByPageAsync(int pageIndex, int pageSize, int? brandIdFilter = null, int? categoryIdFilter = null);
        Task<ClothingDto?> GetClothingByIdAsync(int id);
    }
}
