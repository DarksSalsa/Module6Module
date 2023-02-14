using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntitiesConfiguration
{
    public class TypeOfClothingEntityConfiguration : IEntityTypeConfiguration<TypeOfClothing>
    {
        public void Configure(EntityTypeBuilder<TypeOfClothing> builder)
        {
            builder.ToTable<TypeOfClothing>("Types");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).UseHiLo("catalog_hilo").IsRequired();
            builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        }
    }
}
