using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestBossCreater.Models;

public class AppDbContext : DbContext
{
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<BaseQuestion> Questions => Set<BaseQuestion>();
    public DbSet<RangeQuestion> RangeQuestions => Set<RangeQuestion>();
    public DbSet<TermQuestion> TermQuestions => Set<TermQuestion>();
    public DbSet<MultipleQuestion> QuizQuestions => Set<MultipleQuestion>();

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>(ConfigureTest);
        modelBuilder.Entity<BaseQuestion>(ConfigureQuestion);
        modelBuilder.Entity<RangeQuestion>(ConfigureRangeQuestion);
        modelBuilder.Entity<TermQuestion>(ConfigureTermQuestion);
        modelBuilder.Entity<MultipleQuestion>(ConfigureQuizQuestion);
    }

    private void ConfigureTest(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(255);
        builder.Property(x => x.Description);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");

        builder.HasMany(x => x.Questions)
           .WithOne() // если у вопроса нет навигационного свойства к Test
           .HasForeignKey("TestId") // укажи имя FK, если он в таблице вопроса
           .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureQuestion(EntityTypeBuilder<BaseQuestion> builder)
    {
        builder.ToTable("Questions");
        builder.HasKey(q => q.Id);
        builder.Property(q => q.QuestionText).IsRequired();
        builder.HasOne(q => q.Test)
               .WithMany(t => t.Questions)
               .HasForeignKey(q => q.TestId)
               .OnDelete(DeleteBehavior.Cascade);
    }

    private void ConfigureRangeQuestion(EntityTypeBuilder<RangeQuestion> builder)
    {
        builder.ToTable("RangeQuestions");
        builder.Property(r => r.MinValue).IsRequired();
        builder.Property(r => r.MaxValue).IsRequired();
    }

    private void ConfigureTermQuestion(EntityTypeBuilder<TermQuestion> builder)
    {
        builder.ToTable("TermQuestions");
        builder.Property(t => t.CorrectTerm).IsRequired().HasMaxLength(255);
    }

    private void ConfigureQuizQuestion(EntityTypeBuilder<MultipleQuestion> builder)
    {
        builder.ToTable("QuizQuestions");
        builder.Property(q => q.CorrectOption).IsRequired();
    }
}

