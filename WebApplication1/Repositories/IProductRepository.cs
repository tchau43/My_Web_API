using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IProductRepository
    {
        //List<IProductModel> GetAll();
        List<IProductModel> Get(string searchName, double? from, double? to, string sort, int page);
        //List<IProductModel> GetByPrice(string searchName);
        IProductModel Create(ProductsVM model);
    }
}
