using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;
using TestBossCreater.Pages;
using TestBossCreater.Service.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TestBossCreater
{
    public partial class MainMenu : Form
    {
        private readonly AppDbContext _context;

        public string CurrentUser
        {
            get => Properties.Settings.Default.UserName;
            set
            {
                Properties.Settings.Default.UserName = value;
                Properties.Settings.Default.Save();
            }
        }
        public MainMenu()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadTests();
        }

        /// <summary>
        /// Отобразить существующие тесты
        /// </summary>
        private void LoadTests()
        {
            var allTests = _context.Tests
                .Include(t => t.Questions).ToList();
            if (allTests.Count == 0)
            {
                pictureBoxDataGrid.BringToFront();
                pictureBoxDataGrid.Visible = true;
            }
            else
                dataGridView1.DataSource = allTests
                    .Select(x => new
                    {
                        x.Id,
                        x.Title,
                        x.Description,
                        x.Creater,
                        x.NeededTrueAnswers,
                        CountQuestion = x.Questions.Count
                    })
                    .ToList();

            // Настройки отображения

            dataGridView1.Columns[nameof(Test.Id)].HeaderText = "Id";
            dataGridView1.Columns[nameof(Test.Title)].HeaderText = "Название теста";
            dataGridView1.Columns[nameof(Test.Description)].HeaderText = "Описание";
            dataGridView1.Columns[nameof(Test.Creater)].HeaderText = "Автор";
            dataGridView1.Columns[nameof(Test.NeededTrueAnswers)].HeaderText = "Требуется правильных";
            dataGridView1.Columns["CountQuestion"].HeaderText = "Всего вопросов";

            // Растянуть последний столбце для визуала
            dataGridView1.Columns[nameof(Test.Id)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[nameof(Test.CountQuestion)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;
        }
    

    /// <summary>
    /// Создать новый тест
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void button1_Click(object sender, EventArgs e)
    {
        CurrentUser = textBox1.Text;
        var test = FindAndGetTestInDataGrid();
        if (test == null)
        {
            MessageBox.Show("Нормально строку выдели, Тест с таким ID не найден");
            return;
        }
        FindOrCreateUser(CurrentUser);
        NavigationService.ShowPassTestPage(this, CurrentUser, test);
    }

    private Test FindAndGetTestInDataGrid()
    {
        int testId = 0;
        if (dataGridView1.SelectedRows.Count > 0) // Проверяем, что есть выделенные строки
        {
            var firstSelectedRow = dataGridView1.SelectedRows[0]; // Берём первую выделенную строку
            testId = Convert.ToInt32(firstSelectedRow.Cells[0].Value); // Значение первой ячейки в этой строке
        }
        else if (dataGridView1.SelectedCells.Count == 1)
        {
            string cellValue = dataGridView1.SelectedCells[0].Value?.ToString();

            if (!int.TryParse(cellValue, out testId))
            {
                testId = 0;
            }
        }
        var test = _context.Tests.FirstOrDefault(x => x.Id == testId);
        if (test == null)
        {
            return null;
        }
        return test;
    }
    private void FindOrCreateUser(string userName)
    {
        var findudedUser = _context.Users.FirstOrDefault(x => x.UserName == userName);
        if (findudedUser != null)
        {
            return;
        }

        var newUser = new User()
        {
            UserName = userName
        };
        _context.Users.Add(newUser);
        _context.SaveChanges();
    }
    /// <summary>
    /// Создать новый тест
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void button2_Click(object sender, EventArgs e)
    {
        CurrentUser = textBox1.Text;
        FindOrCreateUser(CurrentUser);
        NavigationService.ShowCreatePage(this, CurrentUser);
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }

    private void MainMenu_Load_1(object sender, EventArgs e)
    {
        LoadTests();
        textBox1.Text = CurrentUser;
    }

    private void пользователяToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var userStatistic = _context.TestStatistic.Where(x => x.UserName == CurrentUser).ToList();
        NavigationService.ShowStatistcPage(this, userStatistic);
    }

    private void тестаToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var findedUser = _context.Users.FirstOrDefault(x => x.UserName == CurrentUser);
        if (findedUser == null)
        {
            MessageBox.Show($"Выбранный пользователь: {CurrentUser} не найден в БД - проходите или создавайте тесты");
            return;
        }
        var testStatistics = _context.TestStatistic.Where(x => x.UserId == findedUser.Id).ToList();
        if (testStatistics == null)
        {
            MessageBox.Show($"У выбранного пользователя: {findedUser.UserName} не найдено статистики - проходите или создавайте тесты");
            return;
        }
        NavigationService.ShowStatistcPage(this, testStatistics);
    }

    private void выбранныйТестToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var choosedTest = FindAndGetTestInDataGrid();
        var testStatistics = _context.TestStatistic.Where(x => x.TestId == choosedTest.Id).ToList();
        if (testStatistics == null)
        {
            MessageBox.Show($"По выбранному тесту: {choosedTest.Title} (Id = {choosedTest.Id}) ещё нет статистики прохождения");
        }
        NavigationService.ShowStatistcPage(this, testStatistics);
    }

    private void всеТестыToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var testStatistics = _context.TestStatistic.ToList();
        if (testStatistics == null)
        {
            MessageBox.Show($"Мы не создали ни одного теста");
        }
        NavigationService.ShowStatistcPage(this, testStatistics);
    }

    private void MainMenu_Shown(object sender, EventArgs e)
    {
        _context.ChangeTracker.Clear();
        LoadTests();
        textBox1.Text = CurrentUser;
    }
}
}
