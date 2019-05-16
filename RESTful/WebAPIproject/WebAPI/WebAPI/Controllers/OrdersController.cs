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
    public class OrdersController : ControllerBase
    {
        public MyDbContext _db { get; set; }

        public OrdersController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(order);
                _db.SaveChanges();
                return Accepted();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetListOfOrders()
        {
            var orders = _db.Orders.ToList();

            if (orders != null)
                return Ok(orders);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder([FromRoute] int id)
        {
            var order = _db.Orders.Find(id);

            if (order != null)
                return Ok(order);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                UpdateOrder(order, id);

                return Accepted();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var order = _db.Orders.Find(id);

            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        #region Helper Methods
        private void UpdateOrder(Order order, int id)
        {
            var orderFromDb = _db.Orders.Find(id);
            orderFromDb.OrderItem = order.OrderItem;
            _db.SaveChanges();
        }
        #endregion
    }
}