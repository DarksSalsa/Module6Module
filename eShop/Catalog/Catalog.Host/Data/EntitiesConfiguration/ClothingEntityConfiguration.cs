using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfiguration
{
    public class ClothingEntityConfiguration : IEntityTypeConfiguration<Clothing>
    {
        public void Configure(EntityTypeBuilder<Clothing> builder)
        {
            builder.ToTable<Clothing>("Clothing");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).UseHiLo("catalog_hilo").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.AvailableStock).IsRequired();
            builder.Property(p => p.Color).IsRequired();
            builder.Property(p => p.Image).IsRequired();
            builder.HasOne(o => o.Brand).WithMany(m => m.Clothings).HasForeignKey(k => k.BrandId);
            builder.HasOne(o => o.Category).WithMany(m => m.Clothings).HasForeignKey(k => k.CategoryId);
        }
    }
}
