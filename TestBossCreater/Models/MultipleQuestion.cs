using System.ComponentModel.DataAnnotations;
using TestBossCreater.Models.Consts;

namespace TestBossCreater.Models
{
    public class MultipleQuestion : BaseQuestion
    {
        public string CorrectOption { get; set; }

        [Required]
        public string OptionA { get; set; } = string.Empty;

        [Required]
        public string OptionB { get; set; } = string.Empty;

        [Required]
        public string OptionC { get; set; } = string.Empty;

        [Required]
        public string OptionD { get; set; } = string.Empty;

        public override string Type => TypeQuestions.MultipleChoise;

        public override bool CheckAnswer()
        {
            return CorrectOption == UserOption;
        }
    }
}
