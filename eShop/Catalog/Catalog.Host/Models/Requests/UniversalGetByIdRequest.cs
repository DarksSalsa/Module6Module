using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class UniversalGetByIdRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
