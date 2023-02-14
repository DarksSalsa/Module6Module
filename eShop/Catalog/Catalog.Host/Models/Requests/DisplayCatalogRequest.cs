using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class DisplayCatalogRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int PageIndex { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
