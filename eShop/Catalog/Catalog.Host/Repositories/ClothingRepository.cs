using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClothingRepository> _logger;

        public ClothingRepository(ApplicationDbContext context, ILogger<ClothingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int?> AddAsync(string name, string color, string size, int categoryId, int brandId, decimal price, int availableStock, string image, string season)
        {
            var item = new Clothing()
            {
                Name = name,
                Color = color,
                Size = size,
                CategoryId = categoryId,
                BrandId = brandId,
                Price = price,
                AvailableStock = availableStock,
                Image = image,
                Season = season,
            };
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
                _context.Clothing.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Clothing?> GetByIdAsync(int id)
        {
            var item = await _context.Clothing.Include(i => i.Brand).Include(i => i.Category).Where(w => w.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<PaginatedItems<Clothing>?> GetByPageAsync(int pageIndex, int pageSize, int? brandIdFilter = null, int? categoryIdFilter = null)
        {
            IQueryable<Clothing> query = _context.Clothing;
            if (brandIdFilter.HasValue)
            {
                query = query.Where(w => w.BrandId == brandIdFilter.Value);
            }

            if (categoryIdFilter.HasValue)
            {
                query = query.Where(w => w.CategoryId == categoryIdFilter.Value);
            }

            var count = await query.LongCountAsync();
            var content = await query
                .Include(i => i.Brand)
                .Include(i => i.Category)
                .OrderBy(o => o.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<Clothing>() { Content = content, Count = count };
        }

        public async Task<Clothing?> UpdateAsync(int id, string property, string value)
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
                    _context.Clothing.Update(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
            }

            return null;
        }
    }
}
