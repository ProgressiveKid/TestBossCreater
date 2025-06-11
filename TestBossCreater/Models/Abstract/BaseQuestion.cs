using System.Text.Json.Serialization;

namespace TestBossCreater.Models
{
    public abstract class BaseQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;

        [JsonIgnore]
        public string UserOption { get; set; } = string.Empty; // Выбор пользователя при прохождени теста

        public int TestId { get; set; }
        public Test Test { get; set; } = null!;

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public abstract string Type { get; }

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
