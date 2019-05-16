using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        public ApplicationDbContext _db { get; set; }

        public QuotesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/Quotes
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;

            switch (sort)
            {
                case "asc":
                    quotes = _db.Quotes.OrderBy(x => x.CreatedAt);
                    break;
                case "desc":
                    quotes = _db.Quotes.OrderByDescending(x => x.CreatedAt);
                    break;
                default:
                    quotes = _db.Quotes;
                    break;
            }

            return Ok(quotes);
        }

        [HttpGet("[action]")]
        public IActionResult PagingQuote(int? pgSize, int? pgNumber)
        {
            var quotes = _db.Quotes;

            var pgSizeProceed = pgSize ?? 3;
            var pgNumberProceed = pgNumber ?? 1;

            return Ok(quotes.Skip((pgNumberProceed - 1)* pgSizeProceed).Take(pgSizeProceed));
        }

        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            if (QuoteExists(id))
                return Ok(_db.Quotes.Find(id));

            return NotFound("Such Quote does not exist");
        }

        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            // aby uniknac takiego problemu, ze kazdy moze edytowac kazdy Quote, trzeba dodac cos takiego co rozpozna usera np string IdOfUserThatCreatedQuote
            // tak mozna pobrac token usera     quote.IdOfUserThatCreatedQuote = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var createTime = DateTime.Now;
            quote.CreatedAt = createTime;
            _db.Quotes.Add(quote);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, $"Created {quote.Title} at {createTime} successfully");
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            if (QuoteExists(id))
            {
                var quoteToUpdate = _db.Quotes.Find(id);
                quoteToUpdate.Author = quote.Author;
                quoteToUpdate.Description = quote.Description;
                quoteToUpdate.Title = quote.Title;
                quoteToUpdate.Type = quote.Type;
                _db.SaveChanges();

                return Ok("quote updated successfully");
            }

            return NotFound($"Could not find quote with id : {id}");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (QuoteExists(id))
            {
                _db.Quotes.Remove(_db.Quotes.Find(id));
                _db.SaveChanges();

                return Ok("quote deleted successfully");
            }

            return NotFound($"Quote with id : {id} does not exist");
        }

        private bool QuoteExists(int id)
        {
            var quoteToCheck = _db.Quotes.Find(id);

            if (quoteToCheck == null)
                return false;

            return true;
        }
    }
}
