using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class UserProfileModel : PageModel
{
    private readonly YourDbContext _dbContext;

    public UserProfileModel(YourDbContext dbContext)
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

    public UserProfile Profile { get; set; }

    public void OnGet()
    {
        // Отримання поточних даних профілю при завантаженні сторінки
        // Припустимо, що ви визначили ідентифікатор поточного користувача, наприклад, через авторизацію
        var userId = GetCurrentUserId(); // Реалізуйте цю функцію залежно від вашого способу авторизації

        Profile = _dbContext.UserProfiles.FirstOrDefault(u => u.Id == userId);
    }

    public IActionResult OnPostUpdateProfile()
    {
        // Логіка для оновлення даних профілю користувача в базі даних
        var userId = GetCurrentUserId(); // Реалізуйте цю функцію залежно від вашого способу авторизації

        var existingProfile = _dbContext.UserProfiles.FirstOrDefault(u => u.Id == userId);

        if (existingProfile != null)
        {
            // Якщо профіль існує, оновити його дані
            existingProfile.Name = Name;
            existingProfile.Surname = Surname;
            existingProfile.Email = Email;
            existingProfile.Phone = Phone;
        }
        else
        {
            // Якщо профіль не існує, додати новий
            var newProfile = new UserProfile
            {
                Id = userId,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Phone = Phone
            };

            _dbContext.UserProfiles.Add(newProfile);
        }

        _dbContext.SaveChanges();

        // Повернення на ту ж сторінку
        return RedirectToPage("/UserProfile");
    }

    private int GetCurrentUserId()
    {
        // Реалізуйте цю функцію залежно від вашого способу отримання ідентифікатора поточного користувача
        return 1; // Приклад: завжди повертаємо ідентифікатор 1 для тестування
    }
}
