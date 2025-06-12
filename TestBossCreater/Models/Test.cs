using System.ComponentModel.DataAnnotations;

namespace TestBossCreater.Models
{
    //public class Test
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [Required]
    //    public string Name { get; set; } = "";

    //    [Required]
    //    public string Description { get; set; } = "";

    //    // Навигация: один тест → много вопросов
    //    public List<MultipleQuestion> Questions { get; set; } = new();
    //}

    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;   
        public string Creater { get; set; } = string.Empty;
        public ICollection<BaseQuestion> Questions { get; set; } = new List<BaseQuestion>();
    }
}
