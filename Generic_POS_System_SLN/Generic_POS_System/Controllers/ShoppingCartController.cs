using Generic_POS_System.Data;
using Generic_POS_System.Repository;
using Generic_POS_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Generic_POS_System.Mdoels;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Generic_POS_System.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartRepository _shopRepo;
        private readonly PosContext _context;
        private readonly IHttpContextAccessor _httpContext;


        public ShoppingCartController(PosContext context, ShoppingCartRepository shoppingCartRepository, IHttpContextAccessor httpContext)
        {
            _shopRepo = shoppingCartRepository;
            _context = context;
            _httpContext = httpContext;
        }
        // GET: /ShoppingCart/

        [Route("ShoppingCart/IndexWithCartId/{cartId}")]
        public ActionResult Index(string cartId)
        {
            
            if (_httpContext.HttpContext.User.Identity.Name != cartId)
            {
                
                return LocalRedirect("/ShoppingCart/Index");
            }

            if (!String.IsNullOrEmpty(cartId))
            {
                _httpContext.HttpContext.Session.SetString("CartId", cartId);
            }



            var cart = _shopRepo.GetCart();

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _shopRepo.GetCartItems(),
                CartTotal = _shopRepo.GetTotal()
            };
            // Return the view
            /*ViewBag.productName = from c in viewModel.CartItems
                              where c.productId == c.Products.productId
                              select new ShoppingCartViewModel
                              {
                                  ProductName = c.Products.productName
                              };*/


            return View(viewModel);
        }

        public ActionResult Index()
        {
            var cart = _shopRepo.GetCart();


            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _shopRepo.GetCartItems(),
                CartTotal = _shopRepo.GetTotal()
            };
            // Return the view
            /*ViewBag.productName = from c in viewModel.CartItems
                              where c.productId == c.Products.productId
                              select new ShoppingCartViewModel
                              {
                                  ProductName = c.Products.productName
                              };*/


            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id, int quantity)
        {
            
            var addedProduct = _context.Product.Single(product => product.productId == id);
            

            
            var cartId = _shopRepo.GetCart();
            

            _shopRepo.AddToCart(addedProduct, quantity);

            // Go back to the main store page for more shopping
            return Json(_shopRepo.GetCart());
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpGet]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = _shopRepo.GetCart();

            

            var cartItem = _context.Cart.Single(item => item.RecordId == id);
            var productName = _context.Product.Where(p => p.productId == cartItem.productId).Select(p => p.productName).Single();
            

            // Remove from cart
            int itemCount = _shopRepo.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = (productName) +
                    " has been removed from your shopping cart.",
                CartTotal = _shopRepo.GetTotal(),
                CartCount = _shopRepo.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            //var jResult = JsonConvert.SerializeObject(results);

            TempData["msg"] = productName + " has been removed";
            return RedirectToAction("Index");
        }
        //
        // GET: /ShoppingCart/CartSummary
        //[ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = _shopRepo.GetCart();

            ViewData["CartCount"] = _shopRepo.GetCount();
            return PartialView("CartSummary");
        }
    }
}
