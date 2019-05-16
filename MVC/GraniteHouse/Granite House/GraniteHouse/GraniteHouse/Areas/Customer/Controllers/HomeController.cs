using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraniteHouse.Models;
using GraniteHouse.Data;
using Microsoft.EntityFrameworkCore;
using GraniteHouse.Extensions;

namespace GraniteHouse.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var allProductsFromDb = await _db.Products.Include(x => x.SpecialTag).Include(x => x.ProductType).ToListAsync();
            return View(allProductsFromDb);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(x => x.SpecialTag).Include(x => x.ProductType).SingleOrDefaultAsync(x => x.Id == id);
            return View(product);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart == null)
                lstShoppingCart = new List<int>();

            lstShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index", "Home", new { area = "Customer"});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart.Any() && lstShoppingCart.Contains(id))
                lstShoppingCart.Remove(id);

            HttpContext.Session.Set("ssShoppingCart", lstShoppingCart);

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
