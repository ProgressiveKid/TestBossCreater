﻿using System;
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
using TestBossCreater.Pages.DialogePage;
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
        private string TypeOfQuestion => comboBox1?.SelectedItem?.ToString();

        public SoundpadClass soundpad = new SoundpadClass();

        private readonly InputMode _inputMode;
        /// <summary>
        /// Проверка на то, существуют ли созданный вопрос с текущим индексом в листе вопросов
        /// </summary>
        private bool IsCurrentQuestionExistsInTest => CurPage < CreatableQuestions.Count &&
                CreatableQuestions[CurPage]?.QuestionText != string.Empty;

        public string _currentUser;
        public int ErrorCount = 0;
        public int CurPage = 0;
        public Test CreatableTest = new Test();
        public List<BaseQuestion> CreatableQuestions = new List<BaseQuestion>();

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
        public CreateTest(string currentUser, InputMode inputMode, Test updatableTest = null)
        {
            _context = new AppDbContext();
            _currentUser = currentUser;
            _inputMode = inputMode;
            CreatableTest.Creater = currentUser;
            InitializeComponent();
            HideAllTabPage();
            comboBox1.Items.AddRange(TypeQuestions.AllTypes);

            if (_inputMode == InputMode.Update &&
                updatableTest != null)
            {
                CreatableTest = updatableTest;
                CreatableQuestions = new List<BaseQuestion>(updatableTest.Questions);
                var currentQuestion = CreatableQuestions[0];
                ShowPageForProperty(currentQuestion);
                this.Text = $"Редактирование теста: {updatableTest.Title}";
            }
        }

        /// <summary>
        /// Прошлая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
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
            ShowPageForProperty(CreatableQuestions[CurPage]);
        }
        /// <summary>
        /// Следующая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {


        }

        public void showPicture(string path)
        {
            PathImage = string.Empty;
            if (string.IsNullOrEmpty(path))
                return;

            string relativePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, path);
            if (File.Exists(relativePath))
            {
                PathImage = path;
                pictureBox1.Image = new Bitmap(relativePath);
            }
        }

        /// <summary>
        /// Заполнить страницу информацией по текущему вопросу
        /// </summary>
        /// <param name="question"></param>
        private void ShowPageForProperty(BaseQuestion question)
        {
            comboBox1.SelectedItem = question.Type;
            showPicture(question.PathImage);
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
                    RangeDeviationRichTextBox.Text = rangeQuestion.BaseDeviation.ToString();
                    RangeFirstRichTextBox.Text = rangeQuestion.MinValue.ToString();
                    RangeSecondRichTextBox.Text = rangeQuestion.MaxValue.ToString();
                    break;
                case TermQuestion termQuestion:
                    questionDescription.Text = termQuestion.QuestionText;
                    TermTextBoxUserAnswer.Text = termQuestion.CorrectTerm;
                    break;
            }
        }

        /// <summary>
        /// Создать вопрос
        /// </summary>
        /// <param name="question"></param>
        private bool CreateQuestion()
        {
            if (!ValidateQuestionInputs(TypeOfQuestion))
            {
                CheckDumpUser();
                return false;
            }
            // Проверка, чтобы проверить добавляем или редактируем существующий вопрос
            if (IsCurrentQuestionExistsInTest)
            {
                // если тип вопроса не поменялся то редактируем вопрос 
                var question = CreatableQuestions[CurPage];

                if (question.Type == comboBox1.SelectedItem)
                {
                    var updatedQuestion = UpdateQuestion(question);
                    CreatableQuestions[CurPage] = updatedQuestion;
                    ClearPageForNewQuestion();
                    return true;
                }
                else
                {   // иначе удаляем вопрос и создаём его за нового
                    CreatableQuestions.Remove(question);
                }
                // Вместо редактирования удаляем старый вопрос и добавляем обновленный новый
            }

            switch (TypeOfQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    var createdMultipleQuestion = new MultipleQuestion()
                    {
                        PathImage = PathImage,
                        QuestionText = questionDescription.Text,
                        OptionA = MultipleARichTextBox.Text,
                        OptionB = MultipleBRichTextBox.Text,
                        OptionC = MultipleCRichTextBox.Text,
                        OptionD = MultipleDRichTextBox.Text,
                        CorrectOption = SelectedMultipleOption
                    };
                    CreatableQuestions.Add(createdMultipleQuestion);
                    break;
                case TypeQuestions.RangeChoise:
                    var createdRangeQuestion = new RangeQuestion()
                    {
                        PathImage = PathImage,
                        QuestionText = questionDescription.Text,
                        BaseDeviation = string.IsNullOrEmpty(RangeDeviationRichTextBox.Text)
                        ? Convert.ToInt32(RangeDeviationRichTextBox.Text)
                        : 10,
                        MinValue = Convert.ToInt32(RangeFirstRichTextBox.Text),
                        MaxValue = Convert.ToInt32(RangeSecondRichTextBox.Text)
                    };
                    CreatableQuestions.Add(createdRangeQuestion);
                    break;
                case TypeQuestions.TermChoise:
                    var createdTermQuestion = new TermQuestion()
                    {
                        PathImage = PathImage,
                        CorrectTerm = TermTextBoxUserAnswer.Text,
                        QuestionText = questionDescription.Text,
                    };
                    CreatableQuestions.Add(createdTermQuestion);
                    break;


            }
            ClearPageForNewQuestion();
            return true;
        }

        /// <summary>
        /// Обновить вопрос
        /// </summary>
        /// <param name="question"></param>
        private BaseQuestion UpdateQuestion(BaseQuestion question)
        {
            //if (!ValidateQuestionInputs(TypeOfQuestion))
            //{
            //    CheckDumpUser();
            //    return question;
            //}
            switch (question)
            {
                case MultipleQuestion multipleQuestion:
                    multipleQuestion.PathImage = PathImage;
                    multipleQuestion.QuestionText = questionDescription.Text;
                    multipleQuestion.OptionA = MultipleARichTextBox.Text;
                    multipleQuestion.OptionB = MultipleBRichTextBox.Text;
                    multipleQuestion.OptionC = MultipleCRichTextBox.Text;
                    multipleQuestion.OptionD = MultipleDRichTextBox.Text;
                    multipleQuestion.CorrectOption = SelectedMultipleOption;
                    return multipleQuestion;
                case RangeQuestion rangeQuestion:
                    rangeQuestion.PathImage = PathImage;
                    rangeQuestion.QuestionText = questionDescription.Text;
                    rangeQuestion.BaseDeviation = Convert.ToInt32(RangeDeviationRichTextBox.Text);
                    rangeQuestion.MinValue = Convert.ToInt32(RangeFirstRichTextBox.Text);
                    rangeQuestion.MaxValue = Convert.ToInt32(RangeSecondRichTextBox.Text);
                    return rangeQuestion;
                case TermQuestion termQuestion:
                    termQuestion.PathImage = PathImage;
                    termQuestion.QuestionText = questionDescription.Text;
                    termQuestion.CorrectTerm = TermTextBoxUserAnswer.Text;
                    return termQuestion;
                default:
                    return question;
            }
        }


        /// <summary>
        /// Закончить создание теста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            // Если не заполнили корректно инфу про тест - не завершаем создание теста
            if (!ShowDialogePageForTestInformation(_inputMode))
                return;

            if (_inputMode == InputMode.Update)
            {
                var updatableTest = _context.Tests
                    .Include(t => t.Questions)
                    .FirstOrDefault(x => x.Id == CreatableTest.Id);

                if (updatableTest == null)
                {
                    MessageBox.Show("Тест для обновления не найден в БД - ты что-то намутил");
                    return;
                }

                // Удаляем вопросы, которых больше нет
                var questionsToRemove = updatableTest.Questions
                    .Where(oldQ => !CreatableQuestions.Any(newQ => newQ.Id == oldQ.Id))
                    .ToList();

                foreach (var q in questionsToRemove)
                {
                    _context.Questions.Remove(q);
                }

                // Обновляем существующие вопросы
                foreach (var newQ in CreatableQuestions)
                {
                    var existingQ = updatableTest.Questions
                        .FirstOrDefault(q => q.Id == newQ.Id);
                    newQ.TestId = updatableTest.Id;
                    if (existingQ != null)
                    {
                        existingQ.QuestionText = newQ.QuestionText;
                        existingQ.UserOption = newQ.UserOption;
                        existingQ.PathImage = newQ.PathImage;
                        switch (existingQ)
                        {
                            case MultipleQuestion multipleQuestion:
                                var newQBultiple = newQ as MultipleQuestion;
                                multipleQuestion.OptionA = newQBultiple.OptionA;
                                multipleQuestion.OptionB = newQBultiple.OptionB;
                                multipleQuestion.OptionC = newQBultiple.OptionC;
                                multipleQuestion.OptionD = newQBultiple.OptionD;
                                multipleQuestion.CorrectOption = newQBultiple.CorrectOption;
                                break;
                            case RangeQuestion rangeQuestion:
                                var newQRange = newQ as RangeQuestion;

                                rangeQuestion.BaseDeviation = newQRange.BaseDeviation;
                                rangeQuestion.MinValue = newQRange.MinValue;
                                rangeQuestion.MaxValue = newQRange.MaxValue;
                                break;
                            case TermQuestion termQuestion:
                                var newQRange1 = newQ as TermQuestion;

                                termQuestion.CorrectTerm = newQRange1.CorrectTerm;
                                break;
                            default:
                                break;
                        }
                       // _context.Questions.Update(existingQ);

                        // обновляем другие поля по типам вопросов (если нужно)
                    }
                    else
                    {
                        // Новый вопрос — добавляем
                        _context.Questions.Add(newQ);
                    }
                }

                // Обновляем сам тест
                _context.Tests.Update(updatableTest);

                // Сохраняем всё сразу
                _context.SaveChanges();
                MessageBox.Show("Тест отредактирован");
                NavigationService.ShowMainMenu(this); // передаётся экземляр текущего класса  CreateTest : Form
                return;
            }
            _context.Tests.Add(CreatableTest);
            _context.SaveChanges();

            int testId = CreatableTest.Id;
            // кажому вопросу теста присвоили его принадлежность к тесту
            CreatableQuestions.ForEach(x => x.TestId = testId);
            _context.AddRange(CreatableQuestions);
            _context.SaveChanges();
            NavigationService.ShowMainMenu(this); // передаётся экземляр текущего класса  CreateTest : Form
        }

        private void CreateTest_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pictureBox4, "Прошлый вопрос");
            toolTip1.SetToolTip(pictureBox3, "Следующий вопрос");
            toolTip1.SetToolTip(pictureBox1, "Добавить изображение для вопроса");
            toolTip1.SetToolTip(pictureBox5, "Добавить изображение для вопроса");

            authorLabel.Text = $"Test Made By:\n {_currentUser}";
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

        private void ClearPageForNewQuestion()
        {
            pictureBox1.Image = null;
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
                    case TextBox textBox:
                        textBox.Clear();
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

        // Регион валидации формы
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

        private void RangeDeviationRichTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить цифры и управляющие символы (Backspace, Delete и т.п.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменить ввод
            }
        }

        /// <summary>
        /// Валидация заполненных пунктов
        /// </summary>
        /// <returns></returns>
        private bool ValidateQuestionInputs(string typeQuestion)
        {
            if (string.IsNullOrWhiteSpace(typeQuestion))
            {
                MessageBox.Show("Выбери тип вопроса");
                return false;
            }
            if (string.IsNullOrWhiteSpace(questionDescription.Text))
            {
                MessageBox.Show("Заполни описание вопроса");
                questionDescription.Focus();
                return false;
            }
            switch (typeQuestion)
            {
                case TypeQuestions.MultipleChoise:
                    if (SelectedMultipleOption == string.Empty)
                    {
                        MessageBox.Show("Amogus выбери правильный radioButton");
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
                        MessageBox.Show("Заполни вариант ответа B!");
                        MultipleBRichTextBox.Focus();
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(MultipleCRichTextBox.Text))
                    {
                        MessageBox.Show("Заполни вариант ответа C!");
                        MultipleCRichTextBox.Focus();
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(MultipleDRichTextBox.Text))
                    {
                        MessageBox.Show("Заполни вариант ответа D!");
                        MultipleDRichTextBox.Focus();
                        return false;
                    }
                    break;
                case TypeQuestions.RangeChoise:

                    if (string.IsNullOrWhiteSpace(RangeFirstRichTextBox.Text))
                    {
                        MessageBox.Show("Заполни первый диапазон!");
                        MultipleARichTextBox.Focus();
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(RangeSecondRichTextBox.Text))
                    {
                        MessageBox.Show("Заполни второй диапазон!");
                        MultipleBRichTextBox.Focus();
                        return false;
                    }

                    break;

            };

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

        private void button5_Click(object sender, EventArgs e)
        {

        }


        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                    "Уверены, что хотите выйти без сохранения?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (result == DialogResult.Yes)
            {
                NavigationService.ShowMainMenu(this); // передаётся экземляр текущего класса CreateTest : Form
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        public bool ShowDialogePageForTestInformation(InputMode inputMode)
        {
            // Создаём диалоговое окно
            DialogePageForCreateTest dialogPage = new DialogePageForCreateTest();

            if (inputMode == InputMode.Update)
            {
                dialogPage = new DialogePageForCreateTest(
                    CreatableTest.Title,
                    CreatableTest.Description,
                    CreatableTest.NeededTrueAnswers);
            }

            // Если окно закрывается с результатом "ОК"
            if (dialogPage.ShowDialog() == DialogResult.OK)
            {
                // Получаем параметры из диалогового окна
                CreatableTest.Title = dialogPage.Title;
                CreatableTest.Description = dialogPage.Description;
                CreatableTest.NeededTrueAnswers = dialogPage.NeededTrueAnswers;
                if (CreatableTest.NeededTrueAnswers > CreatableQuestions.Count)
                {
                    MessageBox.Show("Кол-во правильных для сдачи теста вопроса не может превышать кол-во вопросов в самом тесте");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (CreatableQuestions.Count >= 1)
            {
                var deletedQuestion = CreatableQuestions[CurPage];
                CreatableQuestions.Remove(deletedQuestion);

                //if (CurPage >= 1)
                //    CurPage--;

                if (IsCurrentQuestionExistsInTest)
                {
                    var currentQuestion = CreatableQuestions[CurPage];
                    ShowPageForProperty(currentQuestion);
                }
                else
                {
                    ClearPageForNewQuestion();
                }
            }
        }
        public string PathImage { get; set; }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogePageForAddImageInTest dialogePageForAddImage = new DialogePageForAddImageInTest();
            dialogePageForAddImage.ShowDialog();

            // Если окно закрывается с результатом "ОК"
            if (dialogePageForAddImage.ShowDialog() == DialogResult.OK)
            {
                // Получаем параметры из диалогового окна
                if (dialogePageForAddImage.SelectedImage != null)
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dialogePageForAddImage.ImagePath);

                    if (!string.IsNullOrEmpty(dialogePageForAddImage.ImagePath))
                    {
                        pictureBox1.Image = Image.FromFile(fullPath);
                        PathImage = dialogePageForAddImage.ImagePath;
                    }
                }
                dialogePageForAddImage.Dispose();
            }

        }

        /// <summary>
        /// Следующий вопрос
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!CreateQuestion())
                return;

            CurPage++;
            if (IsCurrentQuestionExistsInTest)
            {
                ShowPageForProperty(CreatableQuestions[CurPage]);
            }
        }

        /// <summary>
        /// Предыдущая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Проверка, чтобы проверить добавляем или редактируем существующий вопрос
            if (IsCurrentQuestionExistsInTest && CurPage >= 1)
            {
                // если тип вопроса не поменялся то редактируем вопрос 
                var question = CreatableQuestions[CurPage];

                if (question.Type == comboBox1.SelectedItem)
                {
                    var updatedQuestion = UpdateQuestion(question);
                    CreatableQuestions[CurPage] = updatedQuestion;
                    ClearPageForNewQuestion();
                }
                // Вместо редактирования удаляем старый вопрос и добавляем обновленный новый
            }
            ShowPreviusQuestion();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            DialogePageForAddImageInTest dialogePageForAddImage = new DialogePageForAddImageInTest();
            dialogePageForAddImage.ShowDialog();

            // Если окно закрывается с результатом "ОК"
            if (dialogePageForAddImage.ShowDialog() == DialogResult.OK)
            {
                // Получаем параметры из диалогового окна
                if (dialogePageForAddImage.SelectedImage != null)
                {
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dialogePageForAddImage.ImagePath);
                    pictureBox1.Image = Image.FromFile(fullPath);
                    PathImage = dialogePageForAddImage.ImagePath;
                }
                dialogePageForAddImage.Dispose();
            }
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            string text = comboBox1.Items[e.Index].ToString();
            e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                toolTip2.Show($"Подсказка для: {text}", comboBox1, e.Bounds.X, e.Bounds.Y + e.Bounds.Height, 2000);
            }

            e.DrawFocusRectangle();
        }
    }
}
