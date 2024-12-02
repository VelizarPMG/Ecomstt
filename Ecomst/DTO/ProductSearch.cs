using Ecomst.Entities;

namespace Ecomst.DTO
{
    public class ProductSearch
    {
        public int? CategoryId { get; set; }
        public string? CodeNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? InStock { get; set; }
    }
}
