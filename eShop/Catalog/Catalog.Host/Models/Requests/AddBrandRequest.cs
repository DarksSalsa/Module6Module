using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class AddBrandRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
    }
}
