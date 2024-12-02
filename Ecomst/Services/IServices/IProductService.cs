using Ecomst.DTO;
using Ecomst.Entities;
using Ecomst.Helpers;

namespace Ecomst.Services.IServices
{
    public interface IProductService
    {
        public SearchResult<Product> Search(ProductSearch searchModel, string sortColumn, int start, int length);
        public void SetModelStateDictionary(IValidationDictionary modelState);
        public bool AddProduct(Product product, IFormFile? file);
        public bool UpdateProduct(Product product, IFormFile? file);
        public Product? GetProductById(int? id);
        public bool DeleteProduct(int id);
    }
}
