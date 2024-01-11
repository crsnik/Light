using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

public class SystemSettingsModel : PageModel
{
    private readonly YourDbContext _dbContext;

    public SystemSettingsModel(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [BindProperty]
    public string SettingName { get; set; }

    [BindProperty]
    public string SettingValue { get; set; }

    public IQueryable<SystemSettings> Settings { get; set; }

    public void OnGet()
    {
        // Отримання всіх налаштувань при завантаженні сторінки
        Settings = _dbContext.SystemSettings.AsQueryable();
    }

    public IActionResult OnPostUpdateSettings()
    {
        // Логіка для оновлення або додавання нового налаштування в базу даних
        var existingSetting = _dbContext.SystemSettings.FirstOrDefault(s => s.SettingName == SettingName);

        if (existingSetting != null)
        {
            // Якщо налаштування існує, оновити його значення
            existingSetting.SettingValue = SettingValue;
        }
        else
        {
            // Якщо налаштування не існує, додати нове
            var newSetting = new SystemSettings
            {
                SettingName = SettingName,
                SettingValue = SettingValue
            };

            _dbContext.SystemSettings.Add(newSetting);
        }

        _dbContext.SaveChanges();

        // Повернення на ту ж сторінку
        return RedirectToPage("/SystemSettings");
    }
}
