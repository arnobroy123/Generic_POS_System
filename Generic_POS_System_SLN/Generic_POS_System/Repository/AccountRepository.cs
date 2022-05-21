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
        //private RoleManager<IdentityUser> _roleManager;

        public AccountRepository(UserManager<AppUser> userManager, SignInManager<AppUser> logInManager) /*RoleManager<IdentityUser> roleManager*/
        {
            _userManager = userManager;
            _logInManager = logInManager;
            //_roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
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
    }
}
