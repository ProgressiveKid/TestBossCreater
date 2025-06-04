using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace TestBossCreater.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; } = string.Empty; // описание вопроса

        [Required]
        public string OptionA { get; set; } = string.Empty;

        [Required]
        public string OptionB { get; set; } = string.Empty;

        [Required]
        public string OptionC { get; set; } = string.Empty;

        [Required]
        public string OptionD { get; set; } = string.Empty;

        [Required]
        public string CorrectOption { get; set; } = string.Empty; // ссылка на OptionX

        [JsonIgnore]
        public string UserOption { get; set; } = string.Empty; // Выбор пользователя при прохождени теста

        // Внешний ключ на Test
        public int TestId { get; set; }

        [ForeignKey("TestId")]
        public Test? Test { get; set; }
    }
}
