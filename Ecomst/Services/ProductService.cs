using Ecomst.Entities;
using Ecomst.Services.IServices;
using Ecomst.Repositories.IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ecomst.Helpers;
using System.Text.RegularExpressions;
using Ecomst.DTO;

namespace Ecomst.Services
{
    public class ProductService : IProductService
    {
        private IValidationDictionary? _modelState;
        private IProductRepository _repository;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductService(
            IProductRepository repository, 
            IWebHostEnvironment webHostEnvironment
        )
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void SetModelStateDictionary(IValidationDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool ValidateProductOnCreate(Product product, IFormFile? file)
        {
            if (_modelState == null)
                throw new ArgumentNullException(nameof(_modelState));

            if (file == null)
                _modelState.AddError("", "Добавете снимка на продукта!");
            if (file != null)
            {
                string ext = System.IO.Path.GetExtension(file.FileName);
                if (ext != ".jpg")
                    _modelState.AddError("", "Невалиден файл! Допускат се файлове с разширение .jpg!");
            }
            return _modelState.IsValid;
        }

        public bool ValidateProductOnUpdate(Product product, IFormFile? file)
        {
            if (_modelState == null)
                throw new ArgumentNullException(nameof(_modelState));

            if (file != null)
            {
                string ext = System.IO.Path.GetExtension(file.FileName);
                if (ext != ".jpg")
                    _modelState.AddError("", "Невалиден файл! Допускат се файлове с разширение .jpg!");
            }
            return _modelState.IsValid;
        }

        public Product? GetProductById(int? id)
        {
            return _repository.FindById(id);
        }

        public bool AddProduct(Product product, IFormFile? file) 
        {
            string fileName = "";
            string filePath = "";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string productIamgeDirectory = StaticData.GetProductImageDir();
            string fullPath = Path.Combine(wwwRootPath, productIamgeDirectory);

            try
            {
                if (!ValidateProductOnCreate(product, file))
                    return false;

                fileName = Utils.SaveFormFile(file, fullPath);
                filePath = productIamgeDirectory + Path.DirectorySeparatorChar + fileName;
                product.ThumbnailImagePath = filePath;

                if (!_repository.Add(product))
                {
                    Utils.DeleteFile(Path.Combine(wwwRootPath, filePath));
                    return false;
                }
                return true;
            }
            catch
            {
                if (!String.IsNullOrEmpty(fullPath))
                    Utils.DeleteFile(Path.Combine(wwwRootPath, filePath));
                return false;
            }
        }

        public bool UpdateProduct(Product product, IFormFile? file)
        {
            string fileName = "";
            string filePath = "";
            string oldPath = product.ThumbnailImagePath;
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string productIamgeDirectory = StaticData.GetProductImageDir();
            string fullPath = Path.Combine(wwwRootPath, productIamgeDirectory);

            try
            {
                if (!ValidateProductOnUpdate(product, file))
                    return false;

                if (file != null)
                {
                    fileName = Utils.SaveFormFile(file, fullPath);
                    filePath = productIamgeDirectory + Path.DirectorySeparatorChar + fileName;
                    product.ThumbnailImagePath = filePath;
                }

                bool isSaved = _repository.Update(product);
                if (file != null && isSaved)
                {
                    Utils.DeleteFile(Path.Combine(wwwRootPath, oldPath));
                }
                else if (file != null && !isSaved)
                {
                    Utils.DeleteFile(Path.Combine(wwwRootPath, filePath));
                }
                return isSaved;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                Product? product = _repository.FindById(id);
                if (product == null)
                    return false;
                
                bool isDeleted = _repository.Delete(id);
                if (isDeleted)
                {
                    string filePath = product.ThumbnailImagePath;
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    Utils.DeleteFile(Path.Combine(wwwRootPath, filePath));
                }
                return isDeleted;
            }
            catch
            {
                return false;
            }
        }

        public SearchResult<Product> Search(ProductSearch searchModel, string sortColumn, int pageNumber, int length)
        {
            int start = (pageNumber - 1) * length;
            SearchResult<Product> result = _repository.GetPageData(searchModel, sortColumn, start, length);

            int recordsFiltered = result.RecordsFiltered;
            int totalPages = (int)Math.Ceiling((double) recordsFiltered / length);
            result.Start = start;
            result.TotalPages = totalPages;
            return result;
        }
    }
}
