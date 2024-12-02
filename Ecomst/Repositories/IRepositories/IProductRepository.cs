using Ecomst.DTO;
using Ecomst.Entities;

namespace Ecomst.Repositories.IRepositories
{
    public interface IProductRepository
    {
        public bool Add(Product product);
        public bool Update(Product product);
        public Product? FindById(int? id);
        public bool Delete(int id);
        public SearchResult<Product> GetPageData(ProductSearch searchModel, string sortColumn, int start, int length);
    }
}
