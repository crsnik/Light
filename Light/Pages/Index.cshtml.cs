using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using YourDbConnect;

public class IndexModel : PageModel
{
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
        optionsBuilder.UseSqlServer("your_connection_string_here");

        // �������������� ��� �������� ���� ����� (� ����� ������� EF Core).
        using (var dbContext = new YourDbContext())
        {
            // �������� �������� ����������� � ��� ����� �� ��'�� ����������� �� �������.
            var user = dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            // ���������� true, ���� ���������� ���� �� �������� ������ �����.
            // � ������ ������� ���������� false.
            return user != null;
        }
    }
}
