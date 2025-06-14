using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class PassTest : Form
    {
        private string CurrentUser;
        private Test PassableTest;
        private List<BaseQuestion> PassableQuestions;
        private int CurPage = 0;
        private int CountAnsweredQuestion = 0;
        private string TypeOfQuestion => comboBox1?.SelectedItem?.ToString();

        public PassTest(string currentUser, Test passableTest)
        {
            CurrentUser = currentUser;
            PassableTest = passableTest;
            PassableQuestions = new List<BaseQuestion>(PassableTest.Questions);
            InitializeComponent();
        }


        private void PassTest_Load(object sender, EventArgs e)
        {

        }

        private void PassTest_Load_1(object sender, EventArgs e)
        {
            authorLabel.Text = $"Test Made By:\n {PassableTest.Creater}";
            comboBox1.Items.AddRange(TypeQuestions.AllTypes);
            CountQuestionsLabel.Text = $"Вопросов в тесте:\n " +
                $"{PassableTest.CountQuestion.ToString()}";
            CountTrueAnswersLabel.Text = $"Необходимо правильных ответов\n " +
                $"для сдачи теста: {PassableTest.NeededTrueAnswers.ToString()}";
            ShowPageForProperty(PassableQuestions[CurPage]);
        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private void ShowPageForProperty(BaseQuestion question)
        {
            comboBox1.SelectedItem = question.Type;
            switch (question)
            {
                case MultipleQuestion multipleQuestion:
                    questionDescription.Text = multipleQuestion.QuestionText;
                    MultipleARichTextBox.Text = multipleQuestion.OptionA;
                    MultipleBRichTextBox.Text = multipleQuestion.OptionB;
                    MultipleCRichTextBox.Text = multipleQuestion.OptionC;
                    MultipleDRichTextBox.Text = multipleQuestion.OptionD;
                    break;
                case RangeQuestion rangeQuestion:
                    questionDescription.Text = rangeQuestion.QuestionText;
                    RangeDeviationRichTextBox.Text = rangeQuestion.BaseDeviation.ToString();
                    RangeAnswerRichTextBox.Text = rangeQuestion.MinValue.ToString();
                    break;
                case TermQuestion termQuestion:
                    TermTextBoxUserAnswer.Text = termQuestion.CorrectTerm;
                    break;
            }
        }

        private void GiveAnswerOnQuestion()
        {


        }
        private void previusQuestionButton_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
