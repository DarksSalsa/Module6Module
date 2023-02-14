using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class AddCategoryRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;
        [Required]
        public int TypeId { get; set; }
    }
}
