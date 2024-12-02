using System.ComponentModel.DataAnnotations;

namespace Ecomst.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        //https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
        public int CategoryId {  get; set; }
        public Category Category { get; set; } = null!;
        public string CodeNumber {  get; set; }
        public string Title {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool InStock {  get; set; }
        public string ThumbnailImagePath {  get; set; }
    }
}
