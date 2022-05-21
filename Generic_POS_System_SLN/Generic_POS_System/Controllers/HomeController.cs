using Generic_POS_System.Helper;
using Generic_POS_System.Mdoels;
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

        public HomeController(UserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [ViewData]
        public string Title { get; set; }

        /*[Route("home")]*/
        public IActionResult Index()
        {
            Title = "Home";

            //return View();
            var userId = _userHelper.GetUserId();

            Console.WriteLine($"User id: {userId}");

            var loggedIn = _userHelper.IsLoggedIn();

            if (!loggedIn)
            {
                return View();
            }

            return RedirectToAction("GetAllProducts", "Product");
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
