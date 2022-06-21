using Generic_POS_System.Data;
using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _logInManager;
        private PosContext _context;

        //private RoleManager<IdentityUser> _roleManager;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> logInManager, PosContext context) 
        {
            _userManager = userManager;
            _logInManager = logInManager;
            _context = context;
            //_roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpCustomerModel userModel)
        {
            var user = new AppUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Dob = userModel.Dob,
                JoinDate = DateTime.UtcNow.AddHours(6),
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email,
                UserName = userModel.Email
                
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            return result;
        }

        public async Task<IdentityResult> AddUserAsync(SignUpUserModel userModel2)
        {
            var user2 = new AppUser()
            {
                FirstName = userModel2.FirstName,
                LastName = userModel2.LastName,
                Dob = userModel2.Dob,
                JoinDate = DateTime.UtcNow.AddHours(6),
                PhoneNumber = userModel2.PhoneNumber,
                Email = userModel2.Email,
                UserName = userModel2.Email

            };
            var result = await _userManager.CreateAsync(user2, userModel2.Password);
            //var isInRole = await _userManager.IsInRoleAsync(user2, userModel2.NewRole);

            if (result.Succeeded)
            {
                    //var user = await _userManager.FindByEmailAsync(userModel2.Email);
                    await _userManager.AddToRoleAsync(user2, userModel2.Role);
                /*if (!isInRole)
                {
                }*/
            }

            return result;
        }


        public async Task<SignInResult> PasswordSignInAsync(LoginModel logInModel) 
        {
            var lm = logInModel;
            
            var result = await _logInManager.PasswordSignInAsync(lm.Email, lm.Password, lm.RememberMe, false);
                
            return result;
            
            
             
        }

        public async Task SignOutAsync() 
        {
            await _logInManager.SignOutAsync();
        }

        /*public async Task<List<RolesModel>> GetRolesUserAsync()
        {
            var user = _context.Users.ToList();
            var role = _context.Roles.ToList();
            var userRole = _context.UserRoles.ToList();

            var roleofUser = from u in user
                             join ur in userRole
                             on u.Id equals ur.UserId
                             join r in role
                             on ur.RoleId equals r.Id
                             select new RolesModel
                             {
                                 Id = r.Id,
                                 Name = r.Name
                             };

            return await Task.FromResult(roleofUser.ToList());


        }*/
    }
}
