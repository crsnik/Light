using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class StatisticModel : PageModel
{
    private readonly YourDbContext _dbContext;

    public StatisticModel(YourDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [BindProperty]
    public string AdminState { get; set; }

    [BindProperty]
    public DateTime DateTime { get; set; }

    [BindProperty]
    public int BuildingId { get; set; }

    public IQueryable<Statistic> Statistics { get; set; }

    public void OnGet()
    {
        // ��������� ��� ������ ���������� ��� ����������� �������
        Statistics = _dbContext.Statistics.AsQueryable();
    }

    public IActionResult OnPostSubmitStatistics()
    {
        // ����� ��� ���������� ������ ������ ���������� � ��� �����
        var newStatistic = new Statistic
        {
            AdminState = AdminState,
            DateTime = DateTime,
            BuildingId = BuildingId
        };

        _dbContext.Statistics.Add(newStatistic);
        _dbContext.SaveChanges();

        // ���������� �� �� � �������
        return RedirectToPage("/Statistic");
    }
}
