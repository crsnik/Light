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
        // ��������� ��� ����������� ��� ����������� �������
        Settings = _dbContext.SystemSettings.AsQueryable();
    }

    public IActionResult OnPostUpdateSettings()
    {
        // ����� ��� ��������� ��� ��������� ������ ������������ � ���� �����
        var existingSetting = _dbContext.SystemSettings.FirstOrDefault(s => s.SettingName == SettingName);

        if (existingSetting != null)
        {
            // ���� ������������ ����, ������� ���� ��������
            existingSetting.SettingValue = SettingValue;
        }
        else
        {
            // ���� ������������ �� ����, ������ ����
            var newSetting = new SystemSettings
            {
                SettingName = SettingName,
                SettingValue = SettingValue
            };

            _dbContext.SystemSettings.Add(newSetting);
        }

        _dbContext.SaveChanges();

        // ���������� �� �� � �������
        return RedirectToPage("/SystemSettings");
    }
}
