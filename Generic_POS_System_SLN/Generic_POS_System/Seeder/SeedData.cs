using Generic_POS_System.Mdoels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Generic_POS_System.Seeder
{
    public static class SeedData
    {
        public static void Seed(UserManager<AppUser> userManager,
                RoleManager<IdentityRole> roleManager) 
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            
        }


        private static void SeedUsers(UserManager<AppUser> userManager) 
        {
            if (userManager.FindByNameAsync("arnob3").Result == null)
            {
                var user = new AppUser()
                {
                    UserName = "arnob3@admin",
                    Email = "arnob3@admin.com",
                    FirstName = "Admin",
                    LastName = "admin",
                    JoinDate = DateTime.UtcNow.AddHours(6)
                };

                var result = userManager.CreateAsync(user, "123456").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }

            }
            
            /*if (userManager.FindByNameAsync("fokrul").Result == null)
            {
                var user = new AppUser()
                {
                    UserName = "fokrul",
                    Email = "fokrul@salesman"
                };

                var result = userManager.CreateAsync(user, "123456").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Salesman").Wait();
                }

            }*/
            
        
        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager) 
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };
                var result = roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Salesman").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Salesman"
                };
                var result = roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Customer"
                };
                var result = roleManager.CreateAsync(role);
            }
        }

    }
}
