using Ecomst.DTO;
using Ecomst.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Ecomst.ViewModels.Product
{
    public class ProductTableViewModel : BaseTableViewModel
    {
        public List<Ecomst.Entities.Product> Data { get; set; }
        public void PopulateFromSearchResult(SearchResult<Ecomst.Entities.Product> searchResult)
        {
            TotalPages = searchResult.TotalPages;
            RecordsTotal = searchResult.RecordsTotal;
            RecordsFiltered = searchResult.RecordsFiltered;
            Start = searchResult.Start;
            Data = searchResult.Data;
        }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> InStockList { get; set; } = new []{
            new SelectListItem{Value="0", Text="Не"},
            new SelectListItem{Value="1", Text="Да"},
        };

        //Search properties
        [DisplayName("Категория")]
        public int? CategoryId { get; set; }
        [DisplayName("Номер на продукта")]
        public string? CodeNumber { get; set; }
        [DisplayName("Заглавие")]
        public string? Title { get; set; }
        [DisplayName("Описание")]
        public string? Description { get; set; }
        [DisplayName("В наличност")]
        public int? InStock { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
    }
}
