using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Responses
{
    public class UpdateCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TypeDto Type { get; set; } = null!;
    }
}
