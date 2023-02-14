using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Microsoft.EntityFrameworkCore;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BrandRepository> _logger;

        public BrandRepository(ApplicationDbContext context, ILogger<BrandRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name)
        {
            var item = new Brand() { Name = name };
            var result = await _context.AddAsync(item);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item == null)
            {
                return false;
            }
            else
            {
                _context.Brands.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
            var item = await _context.Brands.FindAsync(id);
            return item;
        }

        public async Task<IEnumerable<Brand?>> GetAllAsync()
        {
            var content = await _context.Brands.ToListAsync();
            return content;
        }

        public async Task<Brand?> UpdateAsync(int id, string property, string value)
        {
            var result = await GetByIdAsync(id);

            if (result != null)
            {
                var changingValue = result.GetType().GetProperty(property);
                if (changingValue != null)
                {
                    var propertyType = changingValue.PropertyType;
                    var res = Convert.ChangeType(value, propertyType);
                    changingValue.SetValue(result, res, null);
                    _context.Brands.Update(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
            }

            return null;
        }
    }
}
