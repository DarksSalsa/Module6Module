using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<int?> AddAsync(string name, int typeId);
        Task<Category?> UpdateAsync(int id, string property, string value);
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category?>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
