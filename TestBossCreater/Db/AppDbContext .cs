using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;

public class AppDbContext : DbContext
{
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<BaseQuestion> Questions => Set<BaseQuestion>();

    public DbSet<MultipleQuestion> MyltiplaeQuestions => Set<MultipleQuestion>();


    public AppDbContext()
    {
        if (Database.CanConnect())
        {
            // для пересоздания бд, чтобы не накатывать миграции
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        else
        {
            Database.EnsureCreated();
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // локальная бд - можно посмотреть прямо в вижле - это Обозреватель объектов SQL
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=testCreaterDB;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка TPH по умолчанию (Discriminator автоматически)
        modelBuilder.Entity<BaseQuestion>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<MultipleQuestion>(TypeQuestions.MultipleChoise);
    }
}
