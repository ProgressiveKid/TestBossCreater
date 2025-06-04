using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;

public class AppDbContext : DbContext
{
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();

    public AppDbContext()
    {
        if (Database.CanConnect())
        {
            // для пересоздания бд, чтобы не накатывать миграции
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
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
}
