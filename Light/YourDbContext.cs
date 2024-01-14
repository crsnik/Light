using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class YourDbContext : DbContext
{
    public YourDbContext()
    {
    }

    // Конструктор, що приймає параметри, які передаються базовому класу
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

    // Набір даних для сутності User
    public DbSet<User> Users { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<SystemSettings> SystemSettings { get; set; }

    // Додайте інші набори даних для інших сутностей, якщо потрібно

    // Перевизначення методу OnModelCreating для вказання конфігурації моделей
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ваша конфігурація моделей, наприклад, визначення первинних ключів, зв'язків тощо.
        // Наприклад, якщо у вас є сутність User:
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Statistic>().HasKey(u => u.Id);
        modelBuilder.Entity<UserProfile>().HasKey(u => u.Id);
        modelBuilder.Entity<SystemSettings>().HasKey(u => u.Id);

        // Додайте інші конфігурації моделей за потребою
    }
}
