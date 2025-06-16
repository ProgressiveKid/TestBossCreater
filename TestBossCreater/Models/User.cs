using System.ComponentModel.DataAnnotations;

namespace TestBossCreater.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        // список всех прохождений тестов
        public virtual ICollection<TestStatistic> TestStatistics { get; set; } = new List<TestStatistic>();
    }
}
