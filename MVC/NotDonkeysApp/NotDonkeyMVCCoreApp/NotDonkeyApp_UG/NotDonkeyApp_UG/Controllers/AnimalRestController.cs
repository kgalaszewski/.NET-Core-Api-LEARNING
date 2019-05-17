using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using Microsoft.AspNetCore.Mvc;
using NotDonkeyApp_UG.Data;
using NotDonkeyApp_UG.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NotDonkeyApp_UG.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class RestAnimalController : Controller
    {
        public ApplicationDbContext _db { get; set; }

        public RestAnimalController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public List<AnimalNotDonkey> Get()
        {
            return _db.NotDonkeys.ToList();
        }

        [HttpPost]
        public void Post(AnimalNotDonkey animal)
        {
            _db.NotDonkeys.Add(animal);
            _db.SaveChanges();
        }

        [HttpPut("{newNumber}")]
        public void Put(int newNumber)
        {
            var currentAnimal = _db.NotDonkeys.Find(StaticDetails.CurrentUserId);
            currentAnimal.OwnerPhoneNumber = newNumber;
            _db.SaveChanges();
        }

        [HttpDelete]
        public void Delete()
        {
            var animalToRemove = _db.NotDonkeys.Find(StaticDetails.CurrentUserId);
            StaticDetails.CurrentUserId = -1;
            _db.NotDonkeys.Remove(animalToRemove);
            _db.SaveChanges();
            StaticDetails.IsUserLogged = false;
        }
    }
}
