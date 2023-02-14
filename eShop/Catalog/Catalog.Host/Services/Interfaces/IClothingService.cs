using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface IClothingService
    {
        Task<int?> AddAsync(string name, string color, string size, int categoryId, int brandId, decimal price, int availableStock, string image, string season);
        Task<bool> DeleteAsync(int id);
        Task<ClothingDto?> UpdateAsync(int id, string property, string value);
    }
}
