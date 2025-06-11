using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TestBossCreater.Models
{
    public abstract class BaseQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty; // описание вопроса

        [JsonIgnore]
        public string UserOption { get; set; } = string.Empty; // Выбор пользователя при прохождени теста

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public abstract string Type { get; }

        // Внешний ключ на Test
        public int TestId { get; set; }

        [ForeignKey("TestId")]
        public Test? Test { get; set; }

        /// <summary>
        /// Проверка типа вопроса в зависимости от его типа
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckAnswer();

        public void setUserOption(string userAnswer)
        { 
            UserOption = userAnswer;
        }
    }
}
