using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.VisualBasic.ApplicationServices;
using TestBossCreater.Models;
using User = TestBossCreater.Models.User;

public class AppDbContext : DbContext
{
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<BaseQuestion> Questions => Set<BaseQuestion>();
    public DbSet<RangeQuestion> RangeQuestions => Set<RangeQuestion>();
    public DbSet<TermQuestion> TermQuestions => Set<TermQuestion>();
    public DbSet<MultipleQuestion> QuizQuestions => Set<MultipleQuestion>();
    public DbSet<TestStatistic> TestStatistic => Set<TestStatistic>();
    public DbSet<User> Users => Set<User>();

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
        modelBuilder.Entity<TestStatistic>(ConfigureTestStatistic);


        // Seed Test
        modelBuilder.Entity<Test>().HasData(new Test
        {
            Id = 1,
            Title = "Demo Test",
            Description = "Тест с тремя типами вопросов",
            CreatedAt = DateTime.Now,
            Creater = "Valdemar",
            NeededTrueAnswers = 2,
        });

        // Seed Questions (BaseQuestion is abstract, so seed only derived)
        modelBuilder.Entity<RangeQuestion>().HasData(new RangeQuestion
        {
            Id = 1,
            TestId = 1,
            QuestionText = "Выбери значение от 1 до 10",
            MinValue = 1,
            MaxValue = 10
        });

        modelBuilder.Entity<TermQuestion>().HasData(new TermQuestion
        {
            Id = 2,
            TestId = 1,
            QuestionText = "Что такое инкапсуляция?",
            CorrectTerm = "Сокрытие данных"
        });

        modelBuilder.Entity<MultipleQuestion>().HasData(new MultipleQuestion
        {
            Id = 3,
            TestId = 1,
            QuestionText = "Что из ниже перечисленного — язык программирования?",
            OptionA = "HTML",
            OptionB = "CSS",
            OptionC = "C#",
            OptionD = "Photoshop",
            CorrectOption = "C"
        });
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

    private void ConfigureTestStatistic(EntityTypeBuilder<TestStatistic> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName)
               .IsRequired()
               .HasMaxLength(255); // необязательно, но желательно ограничение

        builder.Property(x => x.TrueAnswer)
               .IsRequired();

        builder.Property(x => x.TestQuestion)
               .IsRequired();

        builder.Property(x => x.Grade)
               .IsRequired();

        builder.Property(x => x.Comment);

        builder.HasOne(x => x.User)
               .WithMany(u => u.TestStatistics)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade); // если пользователь удалён — удаляем статистику

        builder.HasOne(x => x.Test)
                 .WithMany() // если Test не содержит коллекцию статистик
                 .HasForeignKey(x => x.TestId)
                 .OnDelete(DeleteBehavior.Restrict); // или Cascade, если хочешь удалять вместе
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

