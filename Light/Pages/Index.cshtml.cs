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
        // �������� ����������� � ��� ����� (�������������� ��� ����� ��� �������� ���� ����� ���).
        if (ValidateUser(Username, Password))
        {
            var claims = new[] { new Claim(ClaimTypes.Name, Username) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // ����������, ���� ���������
            };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProperties);

            return RedirectToPage("/LightManagement"); // ������� �� ������� ��������� ����������
        }

        // ������������ ���� ��� ������.
        ModelState.AddModelError(string.Empty, "������������ ���� ��� ������.");
        return Page();
    }

    private bool ValidateUser(string username, string password)
    {
        // �������� ��������� ���������� (���������, ����� ���������� �� ���� �����)
        var optionsBuilder = new DbContextOptionsBuilder<YourDbContext>();
        var connString = _configuration["ConnectionStrings:DefaultConnection"];
        optionsBuilder.UseSqlServer(connString);

        // �������������� ��� �������� ���� ����� (� ����� ������� EF Core).
        using (var dbContext = new YourDbContext())
        {
            // �������� �������� ����������� � ��� ����� �� ��'�� ����������� �� �������.
            var user = dbContext.Users.FirstOrDefault(u => u.Email == username && u.Password == password);

            // ���������� true, ���� ���������� ���� �� �������� ������ �����.
            // � ������ ������� ���������� false.
            return user != null;
        }
    }
}
