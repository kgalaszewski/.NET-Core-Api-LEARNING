using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraniteHouse.Data;
using GraniteHouse.Extensions;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraniteHouse.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext db)
        {
            _db = db;

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Products>()
            };
        }

        public async Task <IActionResult> Index()
        {
            List<int> lstShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstShoppingCart != null ? lstShoppingCart.Any() : false)
            {
                foreach (var cartItem in lstShoppingCart)
                {
                    Products prod = await _db.Products.Include(x => x.SpecialTag).Include(x => x.ProductType).Where(x => x.Id == cartItem).FirstOrDefaultAsync();
                    ShoppingCartVM.Products.Add(prod);
                }
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost() // vm jest binded
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate
                .AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour)
                .AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);

            Appointments appointments = ShoppingCartVM.Appointments;
            _db.Appointments.Add(appointments);
            _db.SaveChanges();

            int appointmentId = appointments.Id;

            foreach (var productId in lstCartItems)
            {
                ProductsSelectedForAppointment productsSelectedForAppointment = new ProductsSelectedForAppointment()
                {
                    AppointmentId = appointmentId,
                    ProductId = productId
                };
                _db.ProductsSelectedForAppointment.Add(productsSelectedForAppointment);
            }
            _db.SaveChanges();
            lstCartItems = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction("AppointmentConfirmation", "ShoppingCart", new { id = appointmentId});
        }

        public IActionResult Remove(int Id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");

            if (lstCartItems.Any() && lstCartItems.Contains(Id))
                lstCartItems.Remove(Id);

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AppointmentConfirmation(int id)
        {
            ShoppingCartVM.Appointments = _db.Appointments.Where(x => x.Id == id).FirstOrDefault();
            List<ProductsSelectedForAppointment> objProdList = _db.ProductsSelectedForAppointment.Where(x => x.AppointmentId == id).ToList();

            foreach (var productAppointmentObj in objProdList)
            {
                ShoppingCartVM.Products.Add(_db.Products
                    .Include(x => x.ProductType)
                    .Include(x => x.SpecialTag)
                    .Where(x => x.Id == productAppointmentObj.ProductId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }
    }
}