using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public static List<ProductModel> products = new List<ProductModel>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpPost("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.productId == Guid.Parse(id));
                if (product == null) { return NotFound(); }
                return Ok(product);
            }
            catch (Exception ex) { return BadRequest(); }
        }

        [HttpPost]
        public IActionResult Create(ProductsVM productsVM)
        {

            var product = new ProductModel
            {
                productId = Guid.NewGuid(),
                productName = productsVM.productName,
                productUnitPrice = productsVM.productUnitPrice,
            }
            ;
            products.Add(product);
            return Ok(new
            {
                Success = true,
                Data = product
            });
        }

        [HttpPut]
        public IActionResult Edit(string id, ProductsVM productEdit)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.productId == Guid.Parse(id));
                if (product == null) { return NotFound(); }

                //if (id != productEdit.productId.ToString()) return BadRequest();

                product.productName = productEdit.productName;
                product.productUnitPrice = productEdit.productUnitPrice;

                return Ok(product);
            }
            catch (Exception ex) { return BadRequest(); }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.productId == Guid.Parse(id));
                if (product == null) { return NotFound(); }

                products.Remove(product);

                return Ok();
            }
            catch (Exception ex) { return BadRequest(); }
        }
    }
}
