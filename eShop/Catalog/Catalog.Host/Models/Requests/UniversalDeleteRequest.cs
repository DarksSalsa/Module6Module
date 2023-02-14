using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class UniversalDeleteRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
