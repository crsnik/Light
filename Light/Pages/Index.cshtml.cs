using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Light;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly YourDbContext _context;

    public 
        IndexModel(YourDbContext context, IConfiguration configuration)
    {
        _context = context; _configuration = configuration;
    }

    public async Task OnGetAsync()
    {
        var users = await _context.Users.ToListAsync();
    }

    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Перевірка користувача в базі даних (використовуйте ваш сервіс або контекст бази даних тут).
        if (ValidateUser(Username, Password))
        {
            var claims = new[] { new Claim(ClaimTypes.Name, Username) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Налаштуйте, якщо необхідно
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            return RedirectToPage("/LightManagement"); // Перехід на сторінку управління освітленням
        }

        // Неправильний логін або пароль.
        ModelState.AddModelError(string.Empty, "Неправильний логін або пароль.");
        return Page();
    }

    private bool ValidateUser(string username, string password)
    {
        // Визначте параметри підключення (наприклад, рядок підключення до бази даних)
        var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
        var connString = _configuration["ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseSqlServer(connString);

        // Використовуйте ваш контекст бази даних (в цьому прикладі EF Core).
        using (var dbContext = new YourDbContext())
        {
            // Перевірка наявності користувача в базі даних за ім'ям користувача та паролем.
            var user = dbContext.Users.FirstOrDefault(u => u.Email == username && u.Password == password);

            // Повертайте true, якщо користувач існує та введений пароль вірний.
            // В іншому випадку повертайте false.
            return user != null;
        }
    }
}
