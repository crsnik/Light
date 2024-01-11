using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // ������� ������ ���� ��� Entity Framework Core

public class LightManagementModel : PageModel
{
    private readonly YourDbContext _dbContext; // ������� ��������� ��� �������� ���� �����

    public LightManagementModel(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [BindProperty]
    public bool? LightSwitch { get; set; }

    public string LightSwitchText => LightSwitch.HasValue && LightSwitch.Value ? "��������" : "��������";

    public void OnGet()
    { 
         
        var user = _dbContext.Users.FirstOrDefault(); // ����������, �� � ��� � ������� Users

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
            _dbContext.SaveChanges(); // �������� ���� � ��� �����
        }

        // ���������� �� �� � ������� ��� �� ����, ���� �������.
        return RedirectToPage("/LightManagement");
    }
}
