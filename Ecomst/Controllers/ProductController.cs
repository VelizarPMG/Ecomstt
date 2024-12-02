using Ecomst.DTO;
using Ecomst.Entities;
using Ecomst.Helpers;
using Ecomst.Services.IServices;
using Ecomst.ViewModels.Category;
using Ecomst.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecomst.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(ProductTableViewModel viewModel)
        {
            ProductSearch searchModel = new ProductSearch();
            searchModel.CategoryId = viewModel.CategoryId;
            searchModel.CodeNumber = viewModel.CodeNumber;
            searchModel.Title = viewModel.Title;
            searchModel.Description = viewModel.Description;
            searchModel.InStock = viewModel.InStock;
            SearchResult<Product> result = _productService.Search(searchModel, viewModel.SortOrder, viewModel.PageNumber, viewModel.Length);
            viewModel.PopulateFromSearchResult(result);
            List<Category> categoryList = _categoryService.GetCategoryList();
            viewModel.CategoryList = Utils.ListToSelectListItem(categoryList, "Name", "Id");
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel();
            List<Category> categoryList = _categoryService.GetCategoryList();
            viewModel.CategoryList = Utils.ListToSelectListItem(categoryList, "Name", "Id");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel viewModel, IFormFile? file)
        {
            _productService.SetModelStateDictionary(new ModelStateWrapper(ModelState));
            List<Category> categoryList = _categoryService.GetCategoryList();
            viewModel.CategoryList = Utils.ListToSelectListItem(categoryList, "Name", "Id");

            Product product = new Product();
            viewModel.PopulateProduct(product);
            if (_productService.AddProduct(product, file))
            {
                TempData["success"] = $"Продукт {product.Title} е създадена успешно!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Продуктът не може да бъде създадена!";
            }
            return View(viewModel);
        }

        public IActionResult Update(int id)
        {
            List<Category> categoryList = _categoryService.GetCategoryList();
            Product? product = _productService.GetProductById(id);
            if (product == null)
            {
                TempData["error"] = "Продукт с id " + id + " не беше намерен!";
                return RedirectToAction("Index");
            }

            ProductViewModel viewModel = new ProductViewModel();
            viewModel.CategoryList = Utils.ListToSelectListItem(categoryList, "Name", "Id");
            viewModel.PopulateFromProduct(product);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductViewModel viewModel, IFormFile? file)
        {
            _productService.SetModelStateDictionary(new ModelStateWrapper(ModelState));

            Product? product = _productService.GetProductById(viewModel.Id);
            if (product == null)
            {
                TempData["error"] = "Продукт с id " + viewModel.Id + " не беше намерен!";
                return RedirectToAction("Index");
            }
            viewModel.PopulateProduct(product);
            if (_productService.UpdateProduct(product, file))
            {
                TempData["success"] = $"Продукт {product.Title} е актуализиран успешно!";
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid)
            {
                TempData["error"] = "Продуктът не може да бъде актуализиран!";
            }

            List<Category> categoryList = _categoryService.GetCategoryList();
            viewModel.CategoryList = Utils.ListToSelectListItem(categoryList, "Name", "Id");
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (_productService.DeleteProduct(id))
            {
                TempData["success"] = "Продуктът е изтрит успешно!";
            }
            else
            {
                TempData["error"] = "Продуктът не може да бъде изтрит!";
            }
            return RedirectToAction("Index");
        }
    }
}
