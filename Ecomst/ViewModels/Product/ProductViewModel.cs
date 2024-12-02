using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecomst.ViewModels.Product
{
    public class ProductViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Полето \"Категория\" е задължително!")]
        [DisplayName("Категория")]
        public int CategoryId {  get; set; }
        [Required(ErrorMessage = "Полето \"Номер на продукта\" е задължително!")]
        [DisplayName("Номер на продукта")]
        public string CodeNumber { get; set; }
        [Required(ErrorMessage = "Полето \"Заглавие\" е задължително!")]
        [DisplayName("Заглавие")]
        public string Title {  get; set; }
        [Required(ErrorMessage = "Полето \"Описание\" е задължително!")]
        [DisplayName("Описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Полето \"Цена\" е задължително!")]
        [DisplayName("Цена")]
        public decimal? Price {  get; set; }
        [Required(ErrorMessage = "Полето \"В наличност\" е задължително!")]
        [DisplayName("В наличност")]
        public bool InStock {  get; set; }
        [DisplayName("Снимка на продукта")]
        [ValidateNever]
        public string ThumbnailImagePath { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public void PopulateProduct(Ecomst.Entities.Product product)
        {
            product.Title = Title;
            product.CategoryId = CategoryId;
            product.CodeNumber = CodeNumber;
            product.Description = Description;
            product.Price = (decimal) Price;
            product.InStock = InStock;
        }

        public void PopulateFromProduct(Ecomst.Entities.Product product)
        {
            if (product == null)
                return;

            Id = product.Id;
            Title = product.Title;
            CategoryId = product.CategoryId;
            CodeNumber = product.CodeNumber;
            Description = product.Description;
            Price = product.Price;
            InStock = product.InStock;
            ThumbnailImagePath = product.ThumbnailImagePath;
        }
    }
}
