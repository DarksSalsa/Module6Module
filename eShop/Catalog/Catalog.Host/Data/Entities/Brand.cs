namespace Catalog.Host.Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<Clothing> Clothings { get; set; } = new List<Clothing>();
    }
}
