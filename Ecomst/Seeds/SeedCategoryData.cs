using Ecomst.Data;
using Ecomst.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomst.Seeds
{
    public class SeedCategoryData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any categories.
                if (context.Categories.Any())
                {
                    return;   // DB has been seeded
                }
                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Обективи",
                        DisplayOrder = 1,
                    },
                    new Category
                    {
                        Name = "Фотоапарати",
                        DisplayOrder = 2,
                    },
                    new Category
                    {
                        Name = "Дронове",
                        DisplayOrder = 3,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
