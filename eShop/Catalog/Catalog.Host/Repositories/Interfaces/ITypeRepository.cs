using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ITypeRepository
    {
        Task<int?> AddAsync(string name);
        Task<TypeOfClothing?> UpdateAsync(int id, string property, string value);
        Task<TypeOfClothing?> GetByIdAsync(int id);
        Task<IEnumerable<TypeOfClothing?>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}
