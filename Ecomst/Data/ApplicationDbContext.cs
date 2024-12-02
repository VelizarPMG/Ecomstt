using Ecomst.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomst.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; } //Category is the entity and Categories is the table in the database
        public DbSet<Product> Products { get; set; }
    }
}
