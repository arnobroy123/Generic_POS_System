using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Generic_POS_System.Controllers
{
    

    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {
        private readonly PosContext _context = null;

        private IAccountRepository _accountRepository;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(PosContext context, IAccountRepository accountRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [ViewData]
        public string Title { get; set; }
        
        [Route("admin")]
        public IActionResult Main()
        {
            Title = "Admin";

            //var user = new List<AppUser>();.
            var user = _userManager.Users;
            /*var allUsers = _context.Users.ToList();
            if (allUsers?.Any() == true)
            {
                foreach (var users in allUsers)
                {
                    user.Add(new AppUser()
                    {
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        Dob = users.Dob,
                        Email = users.Email,
                        JoinDate = users.JoinDate,
                        PhoneNumber = users.PhoneNumber,
                        UserName = users.UserName
                        

                    });
                }

            }*/

            return View(user);
        }


        [Route("admin/adduser")]
        public IActionResult AddUser()
        {
            Title = "Add User";

            /*var role = new SignUpUserModel
            {
                UserRole = GetAllRoles()
            };

            return View(role);*/
            return View();
        }


        [Route("admin/adduser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(SignUpUserModel userModel)
        {
            
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.AddUserAsync(userModel);

                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    //userModel.UserRole = GetAllRoles();
                    return View(userModel);
                }

                ModelState.Clear();
                return RedirectToAction("Main");

            }
            return View();
        }


        [Route("admin/edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Title = "Edit";

            if (id == null)
                return BadRequest();


            //var userId = _userManager.Users.Select(x => x.Id).Where(x => x.Contains(id));

            
            var user = await GetUserById(id);

            //var userRole = await _userManager.

            var model = new EditUserModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email
                //UserRole = GetAllRoles();
            };

            return View(model);

        }


        //[Route("admin/edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return BadRequest();
            else
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Dob = model.Dob;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Main");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

        }


          

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await GetUserById(id);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Main");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View("Main");

            }
        }

        /*[HttpGet]
        public async Task<IActionResult> EditUserRole(string id)
        {
            Title = "Edit User Role";

            if (id == null)
                return BadRequest();


            //var userId = _userManager.Users.Select(x => x.Id).Where(x => x.Contains(id));

            ViewBag.userId = id;

            var user = await GetUserById(id);

            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var model = new List<RolesModel>();
            foreach (var r in _roleManager.Roles)
            {
                var userRole = new RolesModel
                {
                    Id = r.Id,
                    Name = r.Name
                };

                
                var userIn = await _userManager.IsInRoleAsync(user, r.Name);
                if (userIn)
                {
                    userRole.IsSelected = true;
                }
                else
                {
                    userRole.IsSelected = false;
                }

                model.Add(userRole);
            }

            return View(model);
        }*/

            



        private async Task<AppUser> GetUserById(string id) =>
            await _userManager.FindByIdAsync(id);
        private SelectList GetAllRoles() => new SelectList(_roleManager.Roles.OrderBy(c => c.Name));
    }
}
