using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class PaginatedClothingRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int PageIndex { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
        public int? BrandIdFilter { get; set; } = null;
        public int? CategoryIdFilter { get; set; } = null;
    }
}
