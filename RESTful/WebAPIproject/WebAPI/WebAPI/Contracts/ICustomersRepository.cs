using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Data;

namespace WebAPI.Controllers
{
    public interface ICustomersRepository
    {
        Task<Customer> Add(Customer customer);
        Task<Customer> Find(int id);
        Task<Customer> Update(Customer customer);
        Task<Customer> Remove(int id);
        IEnumerable<Customer> GetAll();
        Task<bool> Exist(int id);
    }
}