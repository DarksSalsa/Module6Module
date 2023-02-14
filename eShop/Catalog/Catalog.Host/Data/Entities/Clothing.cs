namespace Catalog.Host.Data.Entities
{
    public class Clothing
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public string Image { get; set; } = null!;
        public string Season { get; set; } = null!;
    }
}
