using Microsoft.AspNetCore.Authentication.Cookies;

namespace Light
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        { 

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Index"; // Шлях для входу
                    options.AccessDeniedPath = "/AccessDenied"; // Шлях для відмови в доступі
                });
             
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        { 
            app.UseAuthentication(); // Додає middleware для обробки аутентифікації 
        }
    }
}
