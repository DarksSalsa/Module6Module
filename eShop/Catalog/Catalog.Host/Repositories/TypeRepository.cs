using System.Reflection;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class TypeRepository : ITypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TypeRepository> _logger;

        public TypeRepository(ApplicationDbContext context, ILogger<TypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name)
        {
            var item = new TypeOfClothing() { Name = name };
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
                _context.Types.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<TypeOfClothing?> GetByIdAsync(int id)
        {
            var item = await _context.Types.FindAsync(id);
            return item;
        }

        public async Task<IEnumerable<TypeOfClothing?>> GetAllAsync()
        {
            var content = await _context.Types.ToListAsync();
            return content;
        }

        public async Task<TypeOfClothing?> UpdateAsync(int id, string property, string value)
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
                    _context.Types.Update(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
            }

            return null;
        }
    }
}
