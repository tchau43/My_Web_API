using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(int id);
        CategoryVM Add(CategoryModel category);
        CategoryVM Update(int id, CategoryVM category);
        void Delete(int id);
    }
}
