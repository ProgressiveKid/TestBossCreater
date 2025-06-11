using TestBossCreater.Models.Consts;

namespace TestBossCreater.Models
{
    public class TermQuestion : BaseQuestion
    {
        public string CorrectTerm { get; set; } = null!;
        public override string Type => TypeQuestions.SingleChoise;

        public override bool CheckAnswer()
        {
            return CorrectTerm == UserOption;
        }
    }
}
