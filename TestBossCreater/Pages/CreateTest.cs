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
using TestBossCreater.Service.Navigation;
using TestBossCreater.Service.Soundpad;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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
        public Test CreatableTest = new Test() { Name = "На дебила"};
        public List<Question> CreatableQuestions = new List<Question>();
        public CreateTest()
        {
            InitializeComponent();
            _context = new AppDbContext();
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
            if (!CreateQuestion())
                return;
            CurPage++;
        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private void ShowPageForProperty(Question question)
        {

        }

        /// <summary>
        /// Заполнить страницу информацией по вопросу
        /// </summary>
        /// <param name="question"></param>
        private bool CreateQuestion()
        {
            if (!ValidateQuestionInputs())
            {
                CheckDumpUser();
                return false;
            }

            var createdQuestion = new Question()
            {
                QuestionText = questionDescription.Text,
                OptionA = richTextBox1.Text,
                OptionB = richTextBox2.Text,
                OptionC = richTextBox3.Text,
                OptionD = richTextBox4.Text,
                CorrectOption = SelectedOption
            };
            CreatableQuestions.Add(createdQuestion);
            return true;
        }


        string GetCorrectOptionText(Question q)
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
            _context.Questions.AddRange(CreatableQuestions);
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

            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Заполни вариант ответа А!");
                richTextBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                MessageBox.Show("Заполни вариант ответа Б! ЖОПА");
                richTextBox2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(richTextBox3.Text))
            {
                MessageBox.Show("Заполни вариант ответа В!");
                richTextBox3.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(richTextBox4.Text))
            {
                MessageBox.Show("Заполни вариант ответа Г!");
                richTextBox4.Focus();
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
    }
}
