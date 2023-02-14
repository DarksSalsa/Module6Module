using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class AddClothingRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public string Size { get; set; } = null!;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int AvailableStock { get; set; }
        [Required]
        [RegularExpression(@"^.*.(jpg | JPG | png | PNG | bmp | BMP)")]
        public string Image { get; set; } = null!;
        [Required]
        public string Season { get; set; } = null!;
    }
}
