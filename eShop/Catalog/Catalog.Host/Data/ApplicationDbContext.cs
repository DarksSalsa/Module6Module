using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Catalog.Host.Data.EntitiesConfiguration;

namespace Catalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clothing> Clothing { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<TypeOfClothing> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BrandEntityConfiguration());
            builder.ApplyConfiguration(new TypeOfClothingEntityConfiguration());
            builder.ApplyConfiguration(new CategoryEntityConfiguration());
            builder.ApplyConfiguration(new ClothingEntityConfiguration());
        }
    }
}
