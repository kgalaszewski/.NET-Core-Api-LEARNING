using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Models.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public MyDbContext _db { get; set; }

        public ProductsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] Product product)
        {
            _db.Products.Add(new Product() { ProductName = "", Price = 0, Size = 0, Status = "", Variety = "", OrderItem = _db.OrderItems.ToList() });
            _db.Products.Add(new Product() { ProductName = "", Price = 0, Size = 0, Status = "", Variety = "", OrderItem = _db.OrderItems.ToList() });
            _db.Products.Add(new Product() { ProductName = "", Price = 0, Size = 0, Status = "", Variety = "", OrderItem = _db.OrderItems.ToList() });
            _db.Products.Add(new Product() { ProductName = "", Price = 0, Size = 0, Status = "", Variety = "", OrderItem = _db.OrderItems.ToList() });
            _db.Products.Add(new Product() { ProductName = "", Price = 0, Size = 0, Status = "", Variety = "", OrderItem = _db.OrderItems.ToList() });

            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return Accepted();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetListOfProducts()
        {
            var products = _db.Products.ToList();

            if (products != null)
                return Ok(products);

            return NotFound();
        }

        [HttpGet("{id}")]
        [Produces(typeof(Product))]
        public IActionResult GetProduct([FromRoute] string id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
                return Ok(product);

            return NotFound("nie ladzia ");
        }

        [HttpPut("{id}/{test}")]
        public IActionResult Update([FromRoute] int id,[FromRoute] int test, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                UpdateProduct(product, id);
                return Accepted();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        #region Helper Methods
        private void UpdateProduct(Product product, int id)
        {
            var productFromDb = _db.Products.Find(id);
            productFromDb.ProductName = product.ProductName;
            _db.SaveChanges();
        }
        #endregion
    }
}