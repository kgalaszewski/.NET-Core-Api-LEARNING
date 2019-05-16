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
    public class SalespersonsController : ControllerBase
    {
        public MyDbContext _db { get; set; }

        public SalespersonsController(MyDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult CreateSalesperson([FromBody] Salesperson salesperson)
        {
            if (ModelState.IsValid)
            {
                _db.Salespersons.Add(salesperson);
                _db.SaveChanges();
                return Accepted();
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetListOfCustomers()
        {
            var salespersons = _db.Salespersons.ToList();

            if (salespersons != null)
                return Ok(salespersons);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetSalesperson([FromRoute] int id)
        {
            var salesperson = _db.Salespersons.Find(id);

            if (salesperson != null)
                return Ok(salesperson);

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Salesperson salesperson)
        {
            if (ModelState.IsValid)
            {
                UpdateSalesperson(salesperson, id);

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
        private void UpdateSalesperson(Salesperson salesperson, int id)
        {
            var salespersonFromDb = _db.Salespersons.Find(id);
            salespersonFromDb.Address = salesperson.Address;
            _db.SaveChanges();
        }
        #endregion
    }
}