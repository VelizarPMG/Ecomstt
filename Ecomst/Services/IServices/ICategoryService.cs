using Ecomst.DTO;
using Ecomst.Entities;
using Ecomst.Helpers;

namespace Ecomst.Services.IServices
{
    public interface ICategoryService
    {
        public List<Category> GetCategoryList();
        bool AddCategory(Category category);
        Category? GetCategoryById(int? id);
        public bool UpdateCategory(Category category);
        public bool DeleteCategory(int id);
        public void SetModelStateDictionary(IValidationDictionary modelState);
        public SearchResult<Category> Search(CategorySearch searchModel, string sortColumn, int start, int length);
    }
}
