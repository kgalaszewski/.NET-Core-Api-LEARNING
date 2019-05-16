
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
    public class CustomersController : ControllerBase
    {
        public ICustomersRepository _repository { get; set; }

        public CustomersController(ICustomersRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Produces(typeof(Customer))]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(customer);
                return Accepted(ModelState.ValidationState);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetListOfCustomers()
        {
            var customers = _repository.GetAll();

            if (customers != null)
                return Ok(customers);

            return NotFound(ModelState.ValidationState);
        }

        [HttpGet("{id}")]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var customer = _repository.Find(id);

            if (customer != null)
                return Ok(customer);

            return NotFound(ModelState.ValidationState);
        }

        [HttpPut("{id}")]
        [Produces(typeof(Customer))]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerToUpdate = await _repository.Find(id);
                customerToUpdate.Address = customer.Address;
                var updated = await _repository.Update(customerToUpdate);

                return Accepted(updated);
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var customer = _repository.Remove(id);
                return Ok(customer);
            }
            return NotFound(ModelState.ValidationState);
        }
    }
}