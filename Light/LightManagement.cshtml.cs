using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Додайте простір імен для Entity Framework Core

public class LightManagementModel : PageModel
{
    private readonly YourDbContext _dbContext; // Потрібно визначити ваш контекст бази даних

    public LightManagementModel(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [BindProperty]
    public bool? LightSwitch { get; set; }

    public string LightSwitchText => LightSwitch.HasValue && LightSwitch.Value ? "Увімкнено" : "Вимкнено";

    public void OnGet()
    { 
         
        var user = _dbContext.Users.FirstOrDefault(); // Припустимо, що у вас є таблиця Users

        if (user != null)
        {
            LightSwitch = user.IsLightOn; 
        }
    }

    public IActionResult OnPostToggleLights()
    { 
        var user = _dbContext.Users.FirstOrDefault();  

        if (user != null)
        {
            user.IsLightOn = !user.IsLightOn;  
            _dbContext.SaveChanges(); // Зберегти зміни в базі даних
        }

        // Повертайте на ту ж сторінку або на іншу, якщо потрібно.
        return RedirectToPage("/LightManagement");
    }
}
