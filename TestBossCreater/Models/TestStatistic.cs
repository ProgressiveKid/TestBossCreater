using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBossCreater.Models
{
    public class TestStatistic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty; // имя того, кто проходил тест

        public int TrueAnswer { get; set; }                  // количество правильных ответов

        public int TestQuestion { get; set; }                // количество вопросов в тесте

        public bool Grade { get; set; }                      // оценка: прошёл или нет (true/false)

        public string? Comment { get; set; }                 // комментарий, например "молодец"

        public int UserId { get; set; }                      // FK на пользователя

        [ForeignKey(nameof(UserId))]
        public virtual User? User { get; set; }              // навигационное свойство

        public int TestId { get; set; }

        [ForeignKey(nameof(TestId))]
        public virtual Test? Test { get; set; }
    }
}
