using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var listCategory = _context.Categories.ToList();
            return Ok(listCategory);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateNew(CategoryModel model)
        {
            try
            {
                var category = new Category
                {
                    CategoryName = model.CategoryName
                };
                _context.Add(category);
                _context.SaveChanges();
                //return Ok(category);
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditCategoryById(int id, CategoryModel model)
        {
            var category = _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            category.CategoryName = model.CategoryName;
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBDeyId(int id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Remove(category);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
