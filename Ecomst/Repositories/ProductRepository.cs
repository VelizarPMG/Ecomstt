using Ecomst.Data;
using Ecomst.DTO;
using Ecomst.Entities;
using Ecomst.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Ecomst.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product? FindById(int? id)
        {
            if (id == null || id == 0)
                return null;

            Product? product = _context.Products.Find(id);
            return product;
        }

        public bool Add(Product product)
        {
            try
            {
                _context.Products.Add(product);
                int stateNumber = _context.SaveChanges();
                return stateNumber > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
                int stateNumber = _context.SaveChanges();
                return stateNumber > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Product? product = FindById(id);
            if (product == null)
                return false;

            try
            {
                _context.Products.Remove(product);
                int stateNumber = _context.SaveChanges();
                return stateNumber > 0;
            }
            catch
            {
                return false;
            }
        }

        public SearchResult<Product> GetPageData(ProductSearch searchModel, string sortColumn, int start, int length)
        {
            IQueryable<Product> query = _context.Set<Product>();
            query.Include("Category");
            int recordsTotal = query.Count();

            query = Search(searchModel, query);
            int recordsFiltered = query.Count();

            query = OrderBy(sortColumn, query);
            query = WithPagination(start, length, query);

            SearchResult<Product> result = new SearchResult<Product>();

            result.RecordsTotal = recordsTotal;
            result.RecordsFiltered = recordsFiltered;
            result.Data = query.ToList();
            return result;
        }

        private IQueryable<Product> OrderBy(string value, IQueryable<Product> query)
        {
            switch (value)
            {
                case "-price":
                    return query.OrderByDescending(s => s.Price);
                case "price":
                    return query.OrderBy(s => s.Price);
                case "-codeNumber":
                    return query.OrderByDescending(s => s.CodeNumber);
                case "title":
                    return query.OrderBy(s => s.Title);
                case "-title":
                    return query.OrderByDescending(s => s.Title);
                default:
                    return query.OrderBy(s => s.CodeNumber);
            }
        }

        private IQueryable<Product> WithPagination(int start, int length, IQueryable<Product> query)
        {
            return query.Skip(start).Take(length);
        }

        private IQueryable<Product> Search(ProductSearch searchModel, IQueryable<Product> query)
        {
            if (!String.IsNullOrEmpty(searchModel.CodeNumber))
                query = query.Where(s => s.CodeNumber!.ToUpper().Contains(searchModel.CodeNumber.ToUpper()));
            if (!String.IsNullOrEmpty(searchModel.Title))
                query = query.Where(s => s.Title!.ToUpper().Contains(searchModel.Title.ToUpper()));
            if (!String.IsNullOrEmpty(searchModel.Description))
                query = query.Where(s => s.Description!.ToUpper().Contains(searchModel.Description.ToUpper()));
            if (searchModel.InStock != null)
            {
                bool flag = searchModel.InStock == 0 ? false : true;
                query = query.Where(s => s.InStock == flag);
            }
            if (searchModel.CategoryId != null)
                query = query.Where(s => s.CategoryId == searchModel.CategoryId);
            return query;
        }
    }
}
