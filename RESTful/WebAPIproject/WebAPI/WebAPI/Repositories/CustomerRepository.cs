using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Models;
using WebAPI.Models.Data;

namespace WebAPI.Repositories
{
    public class CustomerRepository : ICustomersRepository
    {
        private MyDbContext _db { get; set; }

        public CustomerRepository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Exist(int id)
        {
            return await _db.Customers.AnyAsync(customer => customer.Id == id);
        }

        public async Task<Customer> Find(int id)
        {
            return await _db.Customers.FindAsync(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _db.Customers.ToList();
        }

        public async Task<Customer> Remove(int id)
        {
            var customerToRemove = await _db.Customers.FindAsync(id);
            _db.Customers.Remove(customerToRemove);
            return customerToRemove;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _db.Customers.Update(customer);
            await _db.SaveChangesAsync();
            return customer;
        }
    }
}
