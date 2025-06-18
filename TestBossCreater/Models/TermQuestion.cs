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
            // Если у нас половина символов совпадает, тогда возвращаем true
            if (string.IsNullOrWhiteSpace(CorrectTerm) || string.IsNullOrWhiteSpace(UserOption))
                return false;

            CorrectTerm = CorrectTerm.Trim().ToLower();
            UserOption = UserOption.Trim().ToLower();

            int minLength = Math.Min(CorrectTerm.Length, UserOption.Length);
            int matchCount = 0;

            for (int i = 0; i < minLength; i++)
            {
                if (CorrectTerm[i] == UserOption[i])
                {
                    matchCount++;
                }
            }

            double similarity = (double)matchCount / CorrectTerm.Length;

            return similarity >= 0.5;
        }
    }
}
