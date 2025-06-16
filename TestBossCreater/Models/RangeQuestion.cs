using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestBossCreater.Models.Consts;
using TestBossCreater.Pages;

namespace TestBossCreater.Models
{

    public class RangeQuestion : BaseQuestion
    {
        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        /// <summary>
        /// Отклонение от диапазона правильного ответа
        /// </summary>
        public int BaseDeviation { get; set; } = 10;
        public override string Type => TypeQuestions.RangeChoise;

        public override bool CheckAnswer()
        {
            int intIserAnswer = Convert.ToInt32(UserOption);
            return MinValue - BaseDeviation <= intIserAnswer &&
                MaxValue + BaseDeviation >= intIserAnswer;
        }
    }
}
