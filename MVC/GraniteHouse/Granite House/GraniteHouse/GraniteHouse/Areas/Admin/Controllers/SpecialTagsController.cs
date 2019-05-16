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
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag Tag)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(Tag);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Tag);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tag = await _db.SpecialTags.FindAsync(id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag Tag)
        {
            if (ModelState.IsValid)
            {
                _db.Update(Tag);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Tag);
        }

        public IActionResult Details(int id)
        {
            var tag = _db.SpecialTags.Find(id);
            return View(tag);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tag = await _db.SpecialTags.FindAsync(id);
            _db.SpecialTags.Remove(tag);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}