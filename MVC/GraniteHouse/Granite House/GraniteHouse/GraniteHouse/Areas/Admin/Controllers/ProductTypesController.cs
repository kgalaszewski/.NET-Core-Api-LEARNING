using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraniteHouse.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes type)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(type);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(type);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var type = await _db.ProductTypes.FindAsync(id);
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes type)
        {
            if (ModelState.IsValid)
            {
                _db.Update(type);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(type);
        }

        public IActionResult Details(int id)
        {
            var type = _db.ProductTypes.Find(id);
            return View(type);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var type = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(type);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}