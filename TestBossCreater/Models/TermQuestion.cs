using TestBossCreater.Models.Consts;

namespace TestBossCreater.Models
{
    public class TermQuestion : BaseQuestion
    {
        public string CorrectTerm { get; set; } = null!;
        public override string Type => TypeQuestions.TermChoise;

        /// <summary>
        /// Если корректный ответ содержит часть правильного ответа, игнорирую регистр
        /// </summary>
        /// <returns></returns>
        public override bool CheckAnswer()
        {
            return CorrectTerm.Contains(UserOption, StringComparison.OrdinalIgnoreCase);
        }
    }
}
