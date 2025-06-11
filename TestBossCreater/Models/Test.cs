using System.ComponentModel.DataAnnotations;

namespace TestBossCreater.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        // Навигация: один тест → много вопросов
        public List<MultipleQuestion> Questions { get; set; } = new();
    }
}
