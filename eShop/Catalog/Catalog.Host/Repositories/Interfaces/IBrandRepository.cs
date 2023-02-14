using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<int?> AddAsync(string name);
        Task<Brand?> UpdateAsync(int id, string property, string value);
        Task<Brand?> GetByIdAsync(int id);
        Task<IEnumerable<Brand?>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
