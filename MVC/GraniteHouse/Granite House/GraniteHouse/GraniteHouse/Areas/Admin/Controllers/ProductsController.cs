using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Models.ViewModel;
using GraniteHouse.Utility;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly HostingEnvironment _hostingEnvironment; // needed to save an image

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }

        public ProductsController(ApplicationDbContext db, HostingEnvironment hostingEnvironment)
        {
            _db = db;
            ProductsVM = ConfigureVM();
            _hostingEnvironment = hostingEnvironment;
        }

        private ProductsViewModel ConfigureVM()
        {
            return new ProductsViewModel()
            {
                ProductTypes = _db.ProductTypes.ToList(),
                SpecialTags = _db.SpecialTags.ToList(),
                Products = new Models.Products()
            };
        }

        public async Task <IActionResult> Index()
        {
            var products = _db.Products
                .Include(x => x.ProductType)
                .Include(x => x.SpecialTag);
            return View(await products.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(ProductsVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost() // productsVM jest binded
        {
            // save product, save image, get saved product and change its image name
            if (!ModelState.IsValid)
                return View(ProductsVM);

            _db.Products.Add(ProductsVM.Products);
            await _db.SaveChangesAsync();

            // image being saved
            string webRootPath = _hostingEnvironment.WebRootPath; // getting path to our  wwwroot folder in our app 
            var files = HttpContext.Request.Form.Files;

            var productFromDb = _db.Products.Find(ProductsVM.Products.Id);

            if (files.Count != 0)
            {
                // image has been uploaded
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                productFromDb.Image = $@"\{SD.ImageFolder}\{ProductsVM.Products.Id}{extension}";
            }
            else
            {
                // if user did not upload any file
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultProductImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".jpg");
                productFromDb.Image = @"\" + SD.ImageFolder + @"\" + ProductsVM.Products.Id + ".jpg";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            ProductsVM.Products = await _db.Products.Include(x => x.SpecialTag).Include(x => x.ProductType).SingleOrDefaultAsync(m => m.Id == id);

            return View(ProductsVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editt(int id) // ProductsVM jest zbindowany do tego tez jesli chcemy, osobno mozemy id
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var productFromDb = await _db.Products.Where(x => x.Id == id).SingleOrDefaultAsync();

                if (files.Count != 0)
                {
                    // if file was uploaded by user in edit view
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(productFromDb.Image);

                    if (System.IO.File.Exists(Path.Combine(uploads, ProductsVM.Products.Id + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, ProductsVM.Products.Id + extension_old));
                    }

                    using (var filestream = new FileStream(Path.Combine(uploads, ProductsVM.Products.Id + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(filestream);
                    }
                    ProductsVM.Products.Image = $@"\{SD.ImageFolder}\{ProductsVM.Products.Id}{extension_new}";
                }

                if (ProductsVM.Products.Image != null)
                {
                    productFromDb.Image = ProductsVM.Products.Image;
                }

                productFromDb.Name = ProductsVM.Products.Name;
                productFromDb.Price = ProductsVM.Products.Price;
                productFromDb.ProductTypeId = ProductsVM.Products.ProductTypeId;
                productFromDb.ShadeColor = ProductsVM.Products.ShadeColor;
                productFromDb.SpecialTagId = ProductsVM.Products.SpecialTagId;
                productFromDb.Available = ProductsVM.Products.Available;

                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(ProductsVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productFromDb = await _db.Products.FindAsync(id);
            _db.Remove(productFromDb);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            ProductsVM.Products = await _db.Products.Include(x => x.SpecialTag).Include(x => x.ProductType).SingleOrDefaultAsync(m => m.Id == id);

            return View(ProductsVM.Products);
        }
    }
}
