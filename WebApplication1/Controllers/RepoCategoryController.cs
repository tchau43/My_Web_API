using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepoCategoryController : ControllerBase
    {
        private readonly ICategoryRepository _cateRepo;

        public RepoCategoryController(ICategoryRepository cateRepo)
        {
            _cateRepo = cateRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_cateRepo.GetAll());
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError); }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var data = _cateRepo.GetById(id);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError); }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryVM model)
        {
            try
            {
                var data = _cateRepo.Update(id, model);
                if (data != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex) { return StatusCode(StatusCodes.Status500InternalServerError); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _cateRepo.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            var data = _cateRepo.Add(model);
            if (data != null)
            {
                return Ok(data);
            }
            return BadRequest();
        }
    }
}
