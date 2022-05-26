﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generic_POS_System.Helper;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Generic_POS_System.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private IAccountRepository _accountRepository;
        private readonly UserHelper _userHelper;
        private readonly IHttpContextAccessor _httpContext;

        [ViewData]
        public string Title { get; set; }

        public AccountController(IAccountRepository accountRepository, UserManager<AppUser> userManager, UserHelper userHelper, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _accountRepository = accountRepository;
            _userHelper = userHelper;
            _httpContext = httpContext;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            Title = "Sign Up";
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);

                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }

                    return View(userModel);
                }

                ModelState.Clear();
                return RedirectToAction("login");

            }
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            Title = "Log In";
            var userId = _userHelper.GetUserId();

            var loggedIn = _userHelper.IsLoggedIn();

            if (!loggedIn)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
            
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            _httpContext.HttpContext.Session.Clear();



            if (returnUrl.Contains("IndexWithCartId"))
            {
                returnUrl = null;
            }

            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(loginModel);

                var user = await _userManager.FindByNameAsync(loginModel.Email);

                var roleAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                var roleSalesman = await _userManager.IsInRoleAsync(user, "Salesman");

                /*var uRole = _accountRepository.GetRolesUserAsync().Result;

                var mainRole = uRole.Select(x => x.Name);*/
                


                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && !roleAdmin && !roleSalesman)
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else if(roleAdmin)
                    {
                        return RedirectToAction("Main", "Admin");
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View(loginModel);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            _httpContext.HttpContext.Session.Clear();
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult AccessDenied()
        {

            /*return View();*/
            return Forbid();
        }
    }
}
