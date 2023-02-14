using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Responses
{
    public class UpdateClothingResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string Size { get; set; } = null!;
        public CategoryDto Category { get; set; } = null!;
        public BrandDto Brand { get; set; } = null!;
        public decimal Price { get; set; }
        public int AvailableStock { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Season { get; set; } = null!;
    }
}
