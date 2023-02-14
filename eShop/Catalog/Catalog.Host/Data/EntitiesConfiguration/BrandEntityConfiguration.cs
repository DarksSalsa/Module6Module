using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfiguration
{
    public class BrandEntityConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable<Brand>("Brands");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).UseHiLo("catalog_hilo").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}
