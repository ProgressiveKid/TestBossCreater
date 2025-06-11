using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestBossCreater.Models.Consts;

namespace TestBossCreater.Models
{
    public class RangeQuestion : BaseQuestion
    {
        [Key]
        public int Id { get; set; }

        public int minValue { get; set; }

        public int maxValue { get; set; }
        public override string Type => TypeQuestions.RangeChoise;

        public override bool CheckAnswer()
        {
            int intIserAnswer = Convert.ToInt32(UserOption);
            return minValue <= intIserAnswer && maxValue <= intIserAnswer;
        }
    }
}
