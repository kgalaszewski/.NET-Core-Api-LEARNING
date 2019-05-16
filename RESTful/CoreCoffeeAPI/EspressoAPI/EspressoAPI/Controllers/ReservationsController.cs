using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspressoAPI.Data;
using EspressoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EspressoAPI.Controllers
{
    [Route("espressoAPI/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        public ApplicationDbContext _db { get; set; }

        public ReservationsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult> AddReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _db.Reservations.Add(reservation);
                await _db.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(400);
        }

        [HttpPut]
        public async Task<ActionResult> EditReservationTime(int reservationId, string newTime)
        {
            if (ModelState.IsValid)
            {
                var currentReservation = _db.Reservations.Find(reservationId);
                currentReservation.Time = newTime;
                await _db.SaveChangesAsync();
                return Ok();
            }

            return StatusCode(400);
        }
    }
}