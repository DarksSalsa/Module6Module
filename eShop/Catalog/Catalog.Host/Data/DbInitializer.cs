using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Brands.Any())
            {
                await context.Brands.AddRangeAsync(GetPreconfiguredCatalogBrands());

                await context.SaveChangesAsync();
            }

            if (!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredCatalogTypes());

                await context.SaveChangesAsync();
            }

            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(GetPreconfiguredCategories());

                await context.SaveChangesAsync();
            }

            if (!context.Clothing.Any())
            {
                await context.Clothing.AddRangeAsync(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Brand> GetPreconfiguredCatalogBrands()
        {
            return new List<Brand>()
            {
                new Brand() { Id = 1, Name = "Test1" },
                new Brand() { Id = 2, Name = "Test2" },
                new Brand() { Id = 3, Name = "Test3" },
                new Brand() { Id = 4, Name = "Test4" },
                new Brand() { Id = 5, Name = "Test5" }
            };
        }

        private static IEnumerable<TypeOfClothing> GetPreconfiguredCatalogTypes()
        {
            return new List<TypeOfClothing>()
            {
                new TypeOfClothing() { Id = 1, Name = "Test1" },
                new TypeOfClothing() { Id = 2, Name = "Test2" },
                new TypeOfClothing() { Id = 3, Name = "Test3" },
                new TypeOfClothing() { Id = 4, Name = "Test4" },
                new TypeOfClothing() { Id = 5, Name = "Test5" }
            };
        }

        private static IEnumerable<Clothing> GetPreconfiguredItems()
        {
            return new List<Clothing>()
            {
                new Clothing() { Name = "Name1", Color = "Color1", Size = "Size1", CategoryId = 1, BrandId = 1, AvailableStock = 100, Price = 10M, Season = "Summer", Image = "1.jpg" },
                new Clothing() { Name = "Name2", Color = "Color2", Size = "Size2", CategoryId = 1, BrandId = 1, AvailableStock = 100, Price = 20M, Season = "Summer", Image = "1.jpg" },
                new Clothing() { Name = "Name3", Color = "Color3", Size = "Size3", CategoryId = 2, BrandId = 1, AvailableStock = 100, Price = 30M, Season = "Summer", Image = "1.jpg" },
                new Clothing() { Name = "Name4", Color = "Color4", Size = "Size4", CategoryId = 2, BrandId = 1, AvailableStock = 100, Price = 40M, Season = "Summer", Image = "1.jpg" },
                new Clothing() { Name = "Name5", Color = "Color5", Size = "Size5", CategoryId = 3, BrandId = 2, AvailableStock = 100, Price = 50M, Season = "Autumn", Image = "2.jpg" },
                new Clothing() { Name = "Name6", Color = "Color6", Size = "Size6", CategoryId = 3, BrandId = 2, AvailableStock = 100, Price = 60M, Season = "Autumn", Image = "2.jpg" },
                new Clothing() { Name = "Name7", Color = "Color7", Size = "Size7", CategoryId = 4, BrandId = 2, AvailableStock = 100, Price = 70M, Season = "Autumn", Image = "2.jpg" },
                new Clothing() { Name = "Name8", Color = "Color8", Size = "Size8", CategoryId = 4, BrandId = 2, AvailableStock = 100, Price = 80M, Season = "Autumn", Image = "2.jpg" },
                new Clothing() { Name = "Name9", Color = "Color9", Size = "Size9", CategoryId = 5, BrandId = 3, AvailableStock = 100, Price = 90M, Season = "Winter", Image = "3.jpg" },
                new Clothing() { Name = "Name10", Color = "Color10", Size = "Size10", CategoryId = 5, BrandId = 3, AvailableStock = 100, Price = 100M, Season = "Winter", Image = "3.jpg" },
                new Clothing() { Name = "Name11", Color = "Color11", Size = "Size11", CategoryId = 6, BrandId = 3, AvailableStock = 100, Price = 110M, Season = "Winter", Image = "3.jpg" },
                new Clothing() { Name = "Name12", Color = "Color12", Size = "Size12", CategoryId = 6, BrandId = 3, AvailableStock = 100, Price = 120M, Season = "Winter", Image = "3.jpg" },
                new Clothing() { Name = "Name13", Color = "Color13", Size = "Size13", CategoryId = 7, BrandId = 4, AvailableStock = 100, Price = 130M, Season = "Spring", Image = "4.jpg" },
                new Clothing() { Name = "Name14", Color = "Color14", Size = "Size14", CategoryId = 7, BrandId = 4, AvailableStock = 100, Price = 140M, Season = "Spring", Image = "4.jpg" },
                new Clothing() { Name = "Name15", Color = "Color15", Size = "Size15", CategoryId = 8, BrandId = 4, AvailableStock = 100, Price = 150M, Season = "Spring", Image = "4.jpg" },
                new Clothing() { Name = "Name16", Color = "Color16", Size = "Size16", CategoryId = 8, BrandId = 4, AvailableStock = 100, Price = 160M, Season = "Spring", Image = "4.jpg" },
                new Clothing() { Name = "Name17", Color = "Color17", Size = "Size17", CategoryId = 9, BrandId = 5, AvailableStock = 100, Price = 170M, Season = "Summer/Spring", Image = "5.jpg" },
                new Clothing() { Name = "Name18", Color = "Color18", Size = "Size18", CategoryId = 9, BrandId = 5, AvailableStock = 100, Price = 180M, Season = "Winter/Autumn", Image = "5.jpg" },
                new Clothing() { Name = "Name19", Color = "Color19", Size = "Size19", CategoryId = 10, BrandId = 5, AvailableStock = 100, Price = 190M, Season = "Winter/Spring", Image = "5.jpg" },
                new Clothing() { Name = "Name20", Color = "Color20", Size = "Size20", CategoryId = 10, BrandId = 5, AvailableStock = 100, Price = 200M, Season = "Summer/Autumn", Image = "5.jpg" }
            };
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new Category() { Id = 1, Name = "Test1", TypeId = 1 },
                new Category() { Id = 2, Name = "Test2", TypeId = 1 },
                new Category() { Id = 3, Name = "Test3", TypeId = 2 },
                new Category() { Id = 4, Name = "Test4", TypeId = 2 },
                new Category() { Id = 5, Name = "Test5", TypeId = 3 },
                new Category() { Id = 6, Name = "Test6", TypeId = 3 },
                new Category() { Id = 7, Name = "Test7", TypeId = 4 },
                new Category() { Id = 8, Name = "Test8", TypeId = 4 },
                new Category() { Id = 9, Name = "Test9", TypeId = 5 },
                new Category() { Id = 10, Name = "Test10", TypeId = 5 }
            };
        }
    }
}
