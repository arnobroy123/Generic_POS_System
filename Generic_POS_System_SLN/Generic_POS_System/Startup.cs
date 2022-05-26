using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Generic_POS_System.Data;
using Generic_POS_System.Repository;
using Microsoft.AspNetCore.Identity;
using Generic_POS_System.Mdoels;
using Generic_POS_System.Seeder;
using Generic_POS_System.Helper;

namespace Generic_POS_System
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get;  }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PosContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("dbConn")));
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<PosContext>();

            services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                
                });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/login";
                config.AccessDeniedPath = new PathString("/account/accessdenied");

            });
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#endif      
            services.AddScoped<ProductRepository, ProductRepository>();
            services.AddScoped<ShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppUserClaimsPrincipalFactory>();
            services.AddScoped<UserHelper, UserHelper>();
        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
                UserManager<AppUser> userManager, 
                RoleManager<IdentityRole> roleManager)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseStaticFiles();

                app.UseSession();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                SeedData.Seed(userManager, roleManager);

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();

                    /*endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}");*/
                });
            }
    }
}
