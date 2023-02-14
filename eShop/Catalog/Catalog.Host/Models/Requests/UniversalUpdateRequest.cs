using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class UniversalUpdateRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]([a-z]*)")]
        public string Property { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[A-Z]([a-z]*)")]
        public string Value { get; set; } = null!;
    }
}
