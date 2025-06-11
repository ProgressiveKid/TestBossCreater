using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;
using TestBossCreater.Service.Navigation;
using TestBossCreater.Service.Soundpad;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using GroupBox = System.Windows.Forms.GroupBox;
using RadioButton = System.Windows.Forms.RadioButton;

namespace TestBossCreater.Pages
{
    public partial class CreateTest : Form
    {
        private readonly AppDbContext _context;

        public SoundpadClass soundpad = new SoundpadClass();

        public int ErrorCount = 0;
        public int CurPage = 0;
        public string SelectedOption
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
        public Test CreatableTest = new Test() { Title = "На дебила" };
        public List<BaseQuestion> CreatableQuestions = new List<BaseQuestion>();
        public CreateTest(string currentUser)
        {
            InitializeComponent();
            _context = new AppDbContext();
            HideAllTabPage();
        }

        /// <summary>
        /// Прошлая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (CurPage == 0)
            {
                return;
            }
            CurPage--;
            ShowPageForProperty(CreatableQuestions[CurPage]);
        }

        /// <summary>
        /// Следующая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            // Проверка, чтобы не добавить редактируемый вопрос
            if (CurPage < CreatableQuestions.Count &&
                CreatableQuestions[CurPage]?.QuestionText != string.Empty)
            {
                Console.WriteLine();
            }
            if (!CreateQuestion())
                return;
            CurPage++;
            ClearPageForNewQuestion();
        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private void ShowPageForProperty(BaseQuestion question)
        {
            switch (question)
            {
                case MultipleQuestion multipleQuestion:
                    questionDescription.Text = multipleQuestion.QuestionText;
                    MultipleARichTextBox.Text = multipleQuestion.OptionA;
                    MultipleBRichTextBox.Text = multipleQuestion.OptionB;
                    MultipleCRichTextBox.Text = multipleQuestion.OptionC;
                    MultipleDRichTextBox.Text = multipleQuestion.OptionD;
                    switch (multipleQuestion.CorrectOption)
                    {
                        case "A": radioButtonA.Checked = true; break;
                        case "B": radioButtonB.Checked = true; break;
                        case "C": radioButtonC.Checked = true; break;
                        case "D": radioButtonD.Checked = true; break;
                    }
                    break;
                case RangeQuestion rangeQuestion:
                    questionDescription.Text = rangeQuestion.QuestionText;
                    RangeFirstRichTextBox.Text = rangeQuestion.MinValue.ToString();
                    RangeSecondRichTextBox.Text = rangeQuestion.MaxValue.ToString();
                    break;
            }
        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private bool CreateQuestion()
        {
            //if (!ValidateQuestionInputs())
            //{
            //    CheckDumpUser();
            //    return false;
            //}
            switch (TypeOfQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    var createdMultipleQuestion = new MultipleQuestion()
                    {
                        QuestionText = questionDescription.Text,
                        OptionA = MultipleARichTextBox.Text,
                        OptionB = MultipleBRichTextBox.Text,
                        OptionC = MultipleCRichTextBox.Text,
                        OptionD = MultipleDRichTextBox.Text,
                        CorrectOption = SelectedOption
                    };
                    CreatableQuestions.Add(createdMultipleQuestion);
                    break;
                case TypeQuestions.RangeChoise:
                    var createdRangeQuestion = new RangeQuestion()
                    {
                        QuestionText = questionDescription.Text,
                        MinValue = Convert.ToInt32(RangeFirstRichTextBox.Text),
                        MaxValue = Convert.ToInt32(RangeSecondRichTextBox.Text)
                    };
                    CreatableQuestions.Add(createdRangeQuestion);
                    break;


            }

            return true;
        }


        string GetCorrectOptionText(MultipleQuestion q)
        {
            return q.CorrectOption switch
            {
                "A" => q.OptionA,
                "B" => q.OptionB,
                "C" => q.OptionC,
                "D" => q.OptionD,
                _ => ""
            };
        }

        /// <summary>
        /// Закончить создание теста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (!CreateQuestion())
                return;

            _context.Tests.Add(CreatableTest);
            _context.SaveChanges();

            int testId = CreatableTest.Id;
            // кажому вопросу теста присвоили его принадлежность к тесту
            CreatableQuestions.ForEach(x => x.TestId = testId);
            _context.AddRange(CreatableQuestions);
            _context.SaveChanges();
            Navigation.ShowMainMenu(this); // передаётся экземляр текущего класса  CreateTest : Form
        }

        /// <summary>
        /// Валидация заполненных пунктов
        /// </summary>
        /// <returns></returns>
        private bool ValidateQuestionInputs()
        {
            if (SelectedOption == string.Empty)
            {
                MessageBox.Show("Amogus выбери правильный radioButton");
                return false;
            }

            if (string.IsNullOrWhiteSpace(questionDescription.Text))
            {
                MessageBox.Show("Заполни описание вопроса");
                questionDescription.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(MultipleARichTextBox.Text))
            {
                MessageBox.Show("Заполни вариант ответа А!");
                MultipleARichTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(MultipleBRichTextBox.Text))
            {
                MessageBox.Show("Заполни вариант ответа Б! ЖОПА");
                MultipleBRichTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(MultipleDRichTextBox.Text))
            {
                MessageBox.Show("Заполни вариант ответа В!");
                MultipleDRichTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(MultipleCRichTextBox.Text))
            {
                MessageBox.Show("Заполни вариант ответа Г!");
                MultipleCRichTextBox.Focus();
                return false;
            }

            return true;
        }

        public void CheckDumpUser()
        {
            ErrorCount++;
            if (ErrorCount > 4)
            {
                soundpad.PlayMp3(Path.Combine(Application.StartupPath, "Resources", "Sound", "errorSound.mp3"));
            }
        }

        private void CreateTest_Load(object sender, EventArgs e)
        {
            var allTests = _context.Tests
                .Include(t => t.Questions).FirstOrDefault();

            if (allTests != null)
            {
                var allQuestion = _context.Questions.Where(x => x.TestId == allTests.Id);
            }
            comboBox1.Items.AddRange(TypeQuestions.AllTypes);
        }
        private string TypeOfQuestion => comboBox1?.SelectedItem.ToString();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            HideAllTabPage();
            ShowNeededTabPage(selectedItem);
        }

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
                case TypeQuestions.SingleChoise:
                    tabControl1.TabPages.Add(tabPage3);
                    break;
            }
        }

        private void ClearPageForNewQuestion()
        {
            questionDescription.Clear();
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                ClearControls(tabPage.Controls);
            }
        }

        private void ClearControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case RadioButton radioButton:
                        radioButton.Checked = false;
                        break;

                    case RichTextBox richTextBox:
                        richTextBox.Clear();
                        break;

                    case GroupBox or Panel or FlowLayoutPanel or TableLayoutPanel:
                        // Рекурсивно очищаем вложенные контейнеры
                        ClearControls(control.Controls);
                        break;
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void RangeFirstRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить цифры и управляющие символы (Backspace, Delete и т.п.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменить ввод
            }
        }

        private void RangeSecondRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить цифры и управляющие символы (Backspace, Delete и т.п.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменить ввод
            }
        }
    }
}
