using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Question> Questions { get; set; } = new();
    }
}
