using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoProductController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public RepoProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        var data = _repo.GetAll();
        //        return Ok(data);
        //    }
        //    catch (Exception ex) { return BadRequest(); }
        //}

        [HttpGet]
        public IActionResult Get(string? name, double? from = null, double? to = null, string? sort = "productNameAsc", int page = 1)
        {
            try
            {
                var data = _repo.Get(name, from, to, sort, page);
                return Ok(data);
            }
            catch (Exception ex) { return BadRequest($"We can't get {name}"); }
        }

        [HttpPost]
        public IActionResult Create(ProductsVM model)
        {
            try
            {
                var data = _repo.Create(model);
                return Ok(data);
            }
            catch (Exception ex) { return BadRequest(); }
        }
    }
}
