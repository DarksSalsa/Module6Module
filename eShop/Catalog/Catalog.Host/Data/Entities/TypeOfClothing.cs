namespace Catalog.Host.Data.Entities
{
    public class TypeOfClothing
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
