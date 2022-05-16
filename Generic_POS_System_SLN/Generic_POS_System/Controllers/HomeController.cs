using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string Title { get; set; }

        public ViewResult Index()
        {
            Title = "Home";
            return View();
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
