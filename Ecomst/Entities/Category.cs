using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace Ecomst.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        //https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
