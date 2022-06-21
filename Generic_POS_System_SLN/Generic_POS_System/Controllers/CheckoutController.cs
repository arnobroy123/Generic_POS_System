using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Generic_POS_System.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private ShoppingCartRepository _shopRepo;
        private PosContext _context;

        public CheckoutController(ShoppingCartRepository shopRepo, PosContext context)
        {
            _shopRepo = shopRepo;
            _context = context;
        }
        [ViewData]
        public string Title { get; set; }

        [HttpGet]
        public IActionResult Payment(string id)
        {
            if (id == null)
            {
                return RedirectToAction("GetAllProducts", "Product");
            }
            Title = "Payment";
            return View();

        }


        [HttpPost]
        public IActionResult Payment(Orders order)
        {
            
            order.userId = User.Identity.Name;
            order.genDate = DateTime.UtcNow.AddHours(6);

            _context.Orders.Add(order);
            _context.SaveChanges();

            _shopRepo.CreateOrder(order);


            return RedirectToAction("Complete", new { id = order.orderId });
        }

        [Route("/complete/{id}")]
        public IActionResult Complete(int id)
        {
            Title = "Complete";
            if (id != 0)
            {
                var userId = _shopRepo.GetCart();
                bool isValid = _context.Orders.Any(o => o.orderId == id && o.userId == userId);

                if (isValid)
                {
                    var checkoutReciept = (from oD in _context.OrderDetails.Where(od => od.orderId == id)
                                       
                                           select new OrderDetailsModel()
                                           {
                                               Quantity = oD.Quantity,
                                               unitPrice = oD.UnitPrice,
                                               prodName = oD.Products.productName

                                           }).ToList();
                    var Total = _context.Orders.Where(o => o.orderId == id && o.userId == userId).Select(o => o.OrderTotal).Single();
                    ViewBag.chekoutReciept = checkoutReciept;
                    ViewBag.Total = Total;
                    ViewBag.OrderId = id;
                    return View();
                }
            }

            return RedirectToAction("AccessDenied", "Account");
        }
    }
}
