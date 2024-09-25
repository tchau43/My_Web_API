using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 5;

        public ProductRepository(MyDbContext context)
        {
            _context = context;
        }

        public IProductModel Create(ProductsVM model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryName == model.productCategory);

            if (category == null)
            {
                throw new Exception($"Category '{model.productCategory}' not found.");
            }

            var products = new Product()
            {
                //productId = model.productId,
                productName = model.productName,
                productUnitPrice = model.productUnitPrice,
                Category = category,
            };

            _context.Add(products);
            _context.SaveChanges();

            return new IProductModel()
            {
                productId = products.productId,
                productName = products.productName,
                productUnitPrice = products.productUnitPrice,
                productCategory = products.Category.CategoryName
            };
        }

        //public List<IProductModel> GetAll()
        //{
        //    var products = _context.Products.Select(c => new IProductModel()
        //    {
        //        productId = c.productId,
        //        productName = c.productName,
        //        productUnitPrice = c.productUnitPrice,
        //        productCategory = c.Category.CategoryName,
        //    });
        //    return products.ToList();
        //}

        public List<IProductModel> Get(string searchName, double? from, double? to, string sort, int page)
        {
            var products = _context.Products.Include(c => c.Category).AsQueryable();

            #region filtering
            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(c => c.productName.Contains(searchName));
            }
            if (from != null)
            {
                products = products.Where(c => c.productUnitPrice >= from);
            }
            else if (to != null)
            {
                products = products.Where(c => c.productUnitPrice <= to);
            }
            #endregion

            #region sort
            products = products.OrderBy(c => c.productName);
            switch (sort)
            {
                case "productNameDesc":
                    products = products.OrderByDescending(c => c.productName); break;
                case "productPriceAsc":
                    products = products.OrderBy(c => c.productUnitPrice); break;
                case "productPriceDesc":
                    products = products.OrderByDescending(c => c.productUnitPrice); break;
            }
            #endregion

            #region using paging
            //products = products.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            //var results = products.Select(c => new IProductModel()
            //{
            //    productId = c.productId,
            //    productName = c.productName,
            //    productUnitPrice = c.productUnitPrice,
            //    productCategory = c.Category.CategoryName,
            //});
            //return results.ToList();

            #endregion

            #region using paginate
            var results = PaginatedList<Product>.Create(products, page, PAGE_SIZE);
            Console.WriteLine(results.TotalPage);
            return results.Select(c => new IProductModel()
            {
                productId = c.productId,
                productName = c.productName,
                productUnitPrice = c.productUnitPrice,
                productCategory = c.Category.CategoryName,
            }).ToList();
            #endregion
        }
    }
}
