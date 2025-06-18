using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;
using TestBossCreater.Service.Navigation;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class PassTest : Form
    {
        private readonly AppDbContext _context;

        private string CurrentUser;
        private Test PassableTest;
        private List<BaseQuestion> Questions;
        private int CurPage = 0;
        private int CountAnsweredQuestion = 0;
        private string TypeOfQuestion => comboBox1?.SelectedItem?.ToString();

        public string SelectedMultipleOption
        {
            get
            { // получаем значение выбранного radioButton
                if (radioButtonA.Checked) return "A";
                if (radioButtonB.Checked) return "B";
                if (radioButtonC.Checked) return "C";
                if (radioButtonD.Checked) return "D";
                return "";
            }
            set
            { // присваиваем значение
                switch (value)
                {
                    case "A": radioButtonA.Checked = true; break;
                    case "B": radioButtonB.Checked = true; break;
                    case "C": radioButtonC.Checked = true; break;
                    case "D": radioButtonD.Checked = true; break;
                    default:
                        radioButtonA.Checked = false;
                        radioButtonB.Checked = false;
                        radioButtonC.Checked = false;
                        radioButtonD.Checked = false;
                        break;
                }
            }
        }

        public PassTest(string currentUser, Test passableTest)
        {
            _context = new AppDbContext();
            CurrentUser = currentUser;
            PassableTest = passableTest;
            Questions = new List<BaseQuestion>(PassableTest.Questions);
            InitializeComponent();
        }


        private void PassTest_Load(object sender, EventArgs e)
        {

        }

        private void PassTest_Load_1(object sender, EventArgs e)
        {
            this.Text = $"Прохождение теста: {PassableTest.Title}";
            authorLabel.Text = $"Test Made By:\n {PassableTest.Creater}";
            comboBox1.Items.AddRange(TypeQuestions.AllTypes);
            CountQuestionsLabel.Text = $"Вопросов в тесте:\n " +
                $"{PassableTest.CountQuestion.ToString()}";
            CountTrueAnswersLabel.Text = $"Необходимо правильных ответов\n " +
                $"для сдачи теста: {PassableTest.NeededTrueAnswers.ToString()}";
            LeftQuestionLabel.Text = $"Осталось ответить на {Questions.Count - CountAnsweredQuestion} вопросов";
            ShowPageForProperty(Questions[CurPage]);
        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private void ShowPageForProperty(BaseQuestion question)
        {
            comboBox1.SelectedItem = question.Type;
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, question.PathImage);
            if (File.Exists(fullPath))
            {
                pictureBox1.Image = Image.FromFile(fullPath);
            }
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
                    //RangeAnswerRichTextBox.Text = rangeQuestion.MinValue.ToString();
                    break;
                case TermQuestion termQuestion:
                    questionDescription.Text = termQuestion.QuestionText;
                    //TermTextBoxUserAnswer.Text = termQuestion.CorrectTerm;
                    break;
            }
        }


        private void previusQuestionButton_Click(object sender, EventArgs e)
        {

        }


        private void nexQuestionButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Показать предыдущий вопрос
        /// </summary>
        public void ShowPreviusQuestion()
        {
            if (CurPage == 0)
            {
                return;
            }
            CurPage--;
            ShowPageForProperty(Questions[CurPage]);
        }

        /// <summary>
        /// Показать следующий вопрос + дать ответ на вопрос
        /// </summary>
        public void ShowNextQuestionsAndGiveUserAnswer()
        {
            string curTypeQuestion = comboBox1.SelectedItem.ToString();
            if (!CheckUserAnswer(curTypeQuestion))
                return;
            var question = Questions[CurPage];
            // Если отвечаем впервые на этот вопрос
            if (string.IsNullOrEmpty(question.UserOption))
            {
                CountAnsweredQuestion++;
                LeftQuestionLabel.Text = $"Осталось ответить на {Questions.Count - CountAnsweredQuestion} вопросов";
            }
            switch (curTypeQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    // тут получать корректный выбор пользователя                 
                    GiveAnswerOnQuestion(question, SelectedMultipleOption);
                    break;
                case TypeQuestions.RangeChoise:
                    GiveAnswerOnQuestion(question, RangeAnswerRichTextBox.Text);
                    break;
                case TypeQuestions.TermChoise:
                    GiveAnswerOnQuestion(question, TermTextBoxUserAnswer.Text);
                    break;
            }
            CurPage++;
            if (CurPage < Questions.Count)
            {
                ShowPageForProperty(Questions[CurPage]);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            HideAllTabPage();
            ShowNeededTabPage(selectedItem);
        }

        // Регион очистки
        private void HideAllTabPage()
        {
            tabControl1.TabPages.Remove(tabPage1);
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.TabPages.Remove(tabPage3);
        }

        private void ShowNeededTabPage(string typeQuestion)
        {
            switch (typeQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    tabControl1.TabPages.Add(tabPage1);
                    break;
                case TypeQuestions.RangeChoise:
                    tabControl1.TabPages.Add(tabPage2);
                    break;
                case TypeQuestions.TermChoise:
                    tabControl1.TabPages.Add(tabPage3);
                    break;
            }
        }


        private bool CreateAndSaveStatistic(Test passedTest, string curUserName, string comment)
        {
            int countTrueAnswers = Questions.Count(x => x.CheckAnswer());
            bool grade = countTrueAnswers >= PassableTest.NeededTrueAnswers;
            User curUser = _context.Users.FirstOrDefault(x => x.UserName == curUserName);
            if (curUser == null)
            {
                MessageBox.Show($"Пользователь с ником {curUserName} не найден");
                return false;
            }
            TestStatistic testStatistic = new TestStatistic()
            {
                TestId = passedTest.Id,
                UserId = curUser.Id,
                UserName = curUserName,
                TrueAnswer = countTrueAnswers,
                TestQuestion = passedTest.Questions.Count,
                Grade = grade,
                Comment = !string.IsNullOrWhiteSpace(comment) // если есть комментарии - выводим его
                        ? comment // иначе если grade = true то выводим первое сообщение, иначе второе
                        : grade ? "Прошёл тест полностью" : "Я не ходил в школу"
            };
            _context.TestStatistic.Add(testStatistic);
            _context.SaveChanges();
            return grade;
        }
        private void EndTest()
        {
            bool grade = CreateAndSaveStatistic(PassableTest, CurrentUser, string.Empty);
            if (grade)
            {
                MessageBox.Show("Прошёл тест");
            }
            else
            {
                MessageBox.Show("Ты чё творишь ма бой? Ты не прошёл");
            }

            NavigationService.ShowMainMenu(this);
            // TODO сделать статистику пользователя
            // Статистику теста
            // диалоговое окно какое-то 
            // выход в главное меню
        }

        private bool CheckUserAnswer(string typeQuestion)
        {
            switch (typeQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    if (!radioButtonA.Checked &&
                        !radioButtonB.Checked &&
                        !radioButtonC.Checked &&
                        !radioButtonD.Checked)
                    {
                        MessageBox.Show("Выбери radiobuTton");
                        return false;
                    }
                    break;
                case TypeQuestions.RangeChoise:
                    if (string.IsNullOrEmpty(RangeAnswerRichTextBox.Text))
                    {
                        MessageBox.Show("Дай ответ на вопрос");
                        return false;
                    }
                    break;
                case TypeQuestions.TermChoise:
                    if (string.IsNullOrEmpty(TermTextBoxUserAnswer.Text))
                    {
                        MessageBox.Show("Дай ответ на вопрос");
                        return false;
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// Присвоить вопросу значение ответа - у каждого типа вопроса валидация ответа встроена в его класс
        /// </summary>
        /// <param name="question">Вопрос которому присваиваеим значение</param>
        /// <param name="userAnswer">Ответ пользователя</param>
        private void GiveAnswerOnQuestion(BaseQuestion question, string userAnswer) => question.UserOption = userAnswer;

        private void endTestsButton_Click(object sender, EventArgs e)
        {
            EndTest();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // Запись что чел ливнул с теста
            NavigationService.ShowMainMenu(this);
        }

        private void RangeAnswerRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить цифры и управляющие символы (Backspace, Delete и т.п.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменить ввод
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ShowPreviusQuestion();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ShowNextQuestionsAndGiveUserAnswer();
            // При ответе на все вопросы при нажатии на кнопку "Следующий вопрос" - тест завершиться
            if (CurPage == Questions.Count)
            {
                EndTest();
                return;
            }
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
    }
}
