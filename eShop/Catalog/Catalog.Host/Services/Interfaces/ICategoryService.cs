using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<int?> AddAsync(string name, int typeId);
        Task<bool> DeleteAsync(int id);
        Task<CategoryDto?> UpdateAsync(int id, string property, string value);
    }
}
