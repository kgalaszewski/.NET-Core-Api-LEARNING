using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnimalApi.Controllers
{
    [Route("api/[controller]")]
    public class AnimalController : Controller
    {
        public ApplicationDbContext _db { get; set; }

        public RestAnimalController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: superAnimalApi/[controller]
        [HttpGet]
        public List<AnimalNotDonkey> Get()
        {
            return _db.NotDonkeys.ToList();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(AnimalNotDonkey animal)
        {
            _db.NotDonkeys.Add(animal);
            _db.SaveChanges();
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, int newNumber)
        {
            var currentAnimal = _db.NotDonkeys.Find(id);
            currentAnimal.OwnerPhoneNumber = newNumber;
            _db.SaveChanges();
        }

        // DELETE superAnimalApi/[controller]/id
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var animalToRemove = _db.NotDonkeys.Find(id);
            _db.NotDonkeys.Remove(animalToRemove);
            _db.SaveChanges();
        }
    }
}
