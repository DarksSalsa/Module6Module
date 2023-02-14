using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public class PaginatedItems<T>
    {
        public long Count { get; set; }
        public IEnumerable<T> Content { get; set; } = new List<T>();
    }
}
