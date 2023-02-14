using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Responses
{
    public class PaginatedItemResponse<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> Content { get; set; } = null!;
    }
}
