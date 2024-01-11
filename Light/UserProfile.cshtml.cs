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
        // ��������� �������� ����� ������� ��� ����������� �������
        // ����������, �� �� ��������� ������������� ��������� �����������, ���������, ����� �����������
        var userId = GetCurrentUserId(); // ��������� �� ������� ������� �� ������ ������� �����������

        Profile = _dbContext.UserProfiles.FirstOrDefault(u => u.Id == userId);
    }

    public IActionResult OnPostUpdateProfile()
    {
        // ����� ��� ��������� ����� ������� ����������� � ��� �����
        var userId = GetCurrentUserId(); // ��������� �� ������� ������� �� ������ ������� �����������

        var existingProfile = _dbContext.UserProfiles.FirstOrDefault(u => u.Id == userId);

        if (existingProfile != null)
        {
            // ���� ������� ����, ������� ���� ���
            existingProfile.Name = Name;
            existingProfile.Surname = Surname;
            existingProfile.Email = Email;
            existingProfile.Phone = Phone;
        }
        else
        {
            // ���� ������� �� ����, ������ �����
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

        // ���������� �� �� � �������
        return RedirectToPage("/UserProfile");
    }

    private int GetCurrentUserId()
    {
        // ��������� �� ������� ������� �� ������ ������� ��������� �������������� ��������� �����������
        return 1; // �������: ������ ��������� ������������� 1 ��� ����������
    }
}
