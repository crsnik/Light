using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class UserManagementModel : PageModel
{
    private readonly YourDbContext _dbContext;

    public UserManagementModel(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Surname { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Phone { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IQueryable<User> Users { get; set; }

    public void OnGet()
    {
        // Отримання всіх користувачів при завантаженні сторінки
        Users = _dbContext.Users.AsQueryable();
    }

    public IActionResult OnPostAddUser()
    {
        // Логіка для додавання нового користувача в базу даних
        var newUser = new User
        {
            Name = Name,
            Surname = Surname,
            Email = Email,
            Phone = Phone,
            Password = Password
        };

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        // Повернення на ту ж сторінку
        return RedirectToPage("/UserManagement");
    }
}
