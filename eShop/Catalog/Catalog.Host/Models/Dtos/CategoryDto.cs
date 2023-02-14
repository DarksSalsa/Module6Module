namespace Catalog.Host.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TypeDto Type { get; set; } = null!;
    }
}
