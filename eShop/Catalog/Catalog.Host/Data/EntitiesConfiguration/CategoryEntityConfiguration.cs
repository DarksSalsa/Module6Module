using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfiguration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable<Category>("Categories");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).UseHiLo("catalog_hilo").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
            builder.HasOne(o => o.Type).WithMany(m => m.Categories).HasForeignKey(k => k.TypeId);
        }
    }
}
