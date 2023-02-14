namespace Catalog.Host.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TypeId { get; set; }
        public TypeOfClothing Type { get; set; } = null!;
        public IEnumerable<Clothing> Clothings { get; set; } = new List<Clothing>();
    }
}
