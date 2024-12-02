using Ecomst.Entities;
using Microsoft.AspNetCore.Mvc;
using Ecomst.Services.IServices;
using Ecomst.ViewModels.Category;
using Ecomst.Helpers;
using System.Web;
using Ecomst.DTO;

namespace Ecomst.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index(CategoryTableViewModel viewModel)
        {
            CategorySearch searchModel = new CategorySearch();
            searchModel.Name = viewModel.Name;
            searchModel.DisplayOrder = viewModel.DisplayOrder;
            SearchResult<Category> result = _categoryService.Search(searchModel, viewModel.SortOrder, viewModel.PageNumber, viewModel.Length);
            viewModel.PopulateFromSearchResult(result);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            CategoryViewModel viewModel = new CategoryViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            _categoryService.SetModelStateDictionary(new ModelStateWrapper(ModelState));
           
            Category category = new Category();
            viewModel.PopulateCategory(category);
            if (_categoryService.AddCategory(category))
            {
                TempData["success"] = $"Категория {category.Name} е създадена успешно!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Категорията не може да бъде създадена!";
            }
            
            return View(viewModel);
        }

        public IActionResult Update(int id)
        {
            Category? category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                TempData["error"] = "Не е намерена категория с идентификатор " + id + "!";
                return RedirectToAction("Index");
            }
            CategoryViewModel viewModel = new CategoryViewModel();
            viewModel.PopulateFromCategory(category);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CategoryViewModel viewModel)
        {
            _categoryService.SetModelStateDictionary(new ModelStateWrapper(ModelState));
           
            Category? category = _categoryService.GetCategoryById(viewModel.Id);
            if (category == null)
            {
                TempData["error"] = "Не е намерена категория с идентификатор " + viewModel.Id + "!"; ;
                return RedirectToAction("Index");
            }
            viewModel.PopulateCategory(category);
            if (_categoryService.UpdateCategory(category))
            {
                TempData["success"] = $"Категория {category.Name} е редактирана успешно!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Неуспешно редактиране на категория!";
            }
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (_categoryService.DeleteCategory(id))
            {
                TempData["success"] = "Категорията е изтрита успешно!";
            }
            else
            {
                TempData["error"] = "Категорията не може да бъде изтрита!";
            }
            return RedirectToAction("Index");
        }
    }
}
