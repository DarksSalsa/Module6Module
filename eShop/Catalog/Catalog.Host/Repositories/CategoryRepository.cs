using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;

        public CategoryRepository(ApplicationDbContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name, int typeId)
        {
            var item = new Category() { Name = name, TypeId = typeId };
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
                _context.Categories.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var item = await _context.Categories.FindAsync(id);
            return item;
        }

        public async Task<IEnumerable<Category?>> GetAllAsync()
        {
            var content = await _context.Categories.Include(i => i.Type).ToListAsync();
            return content;
        }

        public async Task<Category?> UpdateAsync(int id, string property, string value)
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
                    _context.Categories.Update(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
            }

            return null;
        }
    }
}
