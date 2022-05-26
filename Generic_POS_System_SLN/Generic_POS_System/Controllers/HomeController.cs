using Generic_POS_System.Helper;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Controllers
{
    public class HomeController : Controller
    {
        //private UserManager<AppUser> _userManager;
        private readonly UserHelper _userHelper;
        private readonly ProductRepository _productRepository = null;

        public HomeController(UserHelper userHelper, ProductRepository productRepository)
        {
            _userHelper = userHelper;
            _productRepository = productRepository;
        }

        [ViewData]
        public string Title { get; set; }

        /*[Route("home")]*/
        public async Task<IActionResult> Index()
        {
            Title = "Home";
            /*var userId = _userHelper.GetUserId();*/

            ViewBag.UserId = _userHelper.GetUserId();



            //var data = await _productRepository.GetProductByCategory();

            ViewBag.DiscountProduct = await _productRepository.GetProductByDiscount();
            ViewBag.CategoryProduct = await _productRepository.GetProductByCategory();

            


            return View();


            /*var loggedIn = _userHelper.IsLoggedIn();

            if (!loggedIn)
            {
            }*/

            /*return RedirectToAction("GetAllProducts", "Product");*/
        }

        

        public ViewResult LogIn()
        {
            Title = "Log In";
            return View();
        }

        public ViewResult SignUp()
        {
            Title = "Sign Up";
            return View();
        }
    }
}
