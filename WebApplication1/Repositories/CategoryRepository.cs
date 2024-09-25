using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _context;

        public CategoryRepository(MyDbContext context)
        {
            _context = context;
        }
        public CategoryVM Add(CategoryModel model)
        {
            var category = new Category()
            {
                CategoryName = model.CategoryName
            };
            _context.Add(category);
            _context.SaveChanges();
            return new CategoryVM()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };
        }

        public void Delete(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                _context.Remove(category);
                _context.SaveChanges();
            }
        }

        public List<CategoryVM> GetAll()
        {
            var categories = _context.Categories.Select(c => new CategoryVM
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            });
            return categories.ToList();
        }

        public CategoryVM GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return null;
            }
            return new CategoryVM { CategoryId = category.CategoryId, CategoryName = category.CategoryName };
        }

        public CategoryVM Update(int id, CategoryVM model)
        {
            var category = _context.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                _context.SaveChanges();
                return new CategoryVM() { CategoryName = category.CategoryName };
            }
            else return null;
        }
    }
}
