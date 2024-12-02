using Ecomst.Data;
using Ecomst.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomst.Seeds
{
    public class SeedProductData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any categories.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                var category = context.Categories.First(s=>s.Name == "Обективи");
                context.Products.AddRange(
                    new Product
                    {
                        CodeNumber = "000001",
                        Title = "Обектив Zuiko 12-100mm MFT",
                        Description = "Oписание.",
                        Price = 2500.00m,
                        InStock = true,
                        ThumbnailImagePath = "",
                        Category = category,
                    },
                    new Product
                    {
                        CodeNumber = "000002",
                        Title = "Обектив Zuiko 45mm MFT",
                        Description = "Oписание.",
                        Price = 500.00m,
                        InStock = true,
                        ThumbnailImagePath = "",
                        Category = category,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
