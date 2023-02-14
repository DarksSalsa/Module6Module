using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ITypeService
    {
        Task<int?> AddAsync(string name);
        Task<bool> DeleteAsync(int id);
        Task<TypeDto?> UpdateAsync(int id, string property, string value);
    }
}
