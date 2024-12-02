using Ecomst.Entities;
using Ecomst.Services.IServices;
using Ecomst.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ecomst.Helpers;
using System.Text.RegularExpressions;
using Ecomst.DTO;

namespace Ecomst.Services
{
    public class CategoryService : ICategoryService
    {
        private IValidationDictionary? _modelState;
        private ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void SetModelStateDictionary(IValidationDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool ValidateCategory(Category category)
        {
            if (_modelState == null)
                throw new ArgumentNullException(nameof(_modelState));

            if (!String.IsNullOrEmpty(category.Name) && category.Name.ToLower() == "test")
                _modelState.AddError("", "\"Test\" е невалидна стойност!");

            Category? category1 = _repository.FindByName(category.Name);
            if (category1 != null)
            {
                if(category.Id != category1.Id)
                    _modelState.AddError("", $"Категория {category1.Name} вече съществува.");
            }
                
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(category.Name);
            if (match.Success)
                _modelState.AddError("", "Името на категорията не може да съдържа число.");


            return _modelState.IsValid;
        }

        public List<Category> GetCategoryList()
        {
            return _repository.ToList();
        }

        public bool AddCategory(Category category)
        {
            try
            {
                if (!ValidateCategory(category))
                    return false;
                return _repository.Add(category);
            }
            catch
            {
                return false;
            }
        }

        public Category? GetCategoryById(int? id) 
        {
            return _repository.FindById(id);
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                if (!ValidateCategory(category))
                    return false;
                return _repository.Update(category);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(int id) {
            return _repository.Delete(id);
        }

        public SearchResult<Category> Search(CategorySearch searchModel, string sortColumn, int pageNumber, int length)
        {
            int start = (pageNumber - 1) * length;
            SearchResult<Category> result = _repository.GetPageData(searchModel, sortColumn, start, length);

            int recordsFiltered = result.RecordsFiltered;
            int totalPages = (int)Math.Ceiling((double) recordsFiltered / length);
            result.Start = start;
            result.TotalPages = totalPages;
            return result;
        }
    }
}
