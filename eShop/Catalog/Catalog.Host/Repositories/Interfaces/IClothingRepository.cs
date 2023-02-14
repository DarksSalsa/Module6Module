using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IClothingRepository
    {
        Task<int?> AddAsync(string name, string color, string size, int categoryId, int brandId, decimal price, int availableStock, string image, string season);
        Task<Clothing?> UpdateAsync(int id, string property, string value);
        Task<Clothing?> GetByIdAsync(int id);
        Task<PaginatedItems<Clothing>?> GetByPageAsync(int pageIndex, int pageSize, int? brandIdFilter = null, int? categoryIdFilter = null);
        Task<bool> DeleteAsync(int id);
    }
}
