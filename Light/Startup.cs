using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Light
{
    public class Startup
    {
        public Startup(IConfiguration configuration) // Add IConfiguration as a parameter
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; } // Property to hold the configuration

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<YourDbContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRazorPages();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Index"; // Шлях для входу
                    options.AccessDeniedPath = "/AccessDenied"; // Шлях для відмови в доступі
                });
             
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            app.UseAuthentication(); // Додає middleware для обробки аутентифікації 
        }
    }
}
