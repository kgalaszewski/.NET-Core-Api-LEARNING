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
    public class OrderItemsController : ControllerBase
    {
        public MyDbContext _db { get; set; }

        public OrderItemsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _db.OrderItems.Add(orderItem);
                _db.SaveChanges();
                return Accepted();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetListOfOrderItems()
        {
            var orderItems = _db.OrderItems.ToList();

            if (orderItems != null)
                return Ok(orderItems);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderItem([FromRoute] int id)
        {
            var orderitem = _db.OrderItems.Find(id);

            if (orderitem != null)
                return Ok(orderitem);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                UpdateOrderitem(orderItem, id);

                return Accepted();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var salesperson = _db.Salespersons.Find(id);

            if (salesperson != null)
            {
                _db.Salespersons.Remove(salesperson);
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        #region Helper Methods
        private void UpdateOrderitem(OrderItem orderItem, int id)
        {
            var orderitemFromDb = _db.OrderItems.Find(id);
            orderitemFromDb.ProductId = orderItem.ProductId;
            _db.SaveChanges();
        }
        #endregion
    }
}