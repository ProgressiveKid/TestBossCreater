using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;
using TestBossCreater.Pages;
using TestBossCreater.Pages.DialogePage;
using TestBossCreater.Service.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace TestBossCreater
{
    public partial class MainMenu : Form
    {
        private readonly AppDbContext _context;
        private List<User> _allUsers;
        private List<Test> _allTests;
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
            _allTests = allTests;
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
                var selectedCell = dataGridView1.SelectedCells[0];
                if (selectedCell.OwningColumn.Name == "Id")
                {
                    string cellValue = selectedCell.Value?.ToString();
                    if (!int.TryParse(cellValue, out testId))
                    {
                        testId = 0;
                    }
                }
            }
            var test = _context.Tests.FirstOrDefault(x => x.Id == testId);
            if (test == null || testId == 0)
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
            NavigationService.ShowCreatePage(this, CurrentUser, InputMode.Create);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            // ассинхронная задержка нужна, чтобы успеть прогрузить форму с таблице
            await Task.Delay(5);
            string searchText = textBox1.Text.Trim();
            var foundUsers = _allUsers
                .FirstOrDefault(u => u.UserName.Contains(searchText));
            if (foundUsers != null)
            {
                var testCreatedByUser = _allTests.Where(x => x.Creater == foundUsers.UserName).Select(x => x.Id);
                if (testCreatedByUser.Count() > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["Id"].Value != null)
                        {
                            int rowId = Convert.ToInt32(row.Cells["Id"].Value);
                            if (testCreatedByUser.Contains(rowId))
                            {
                                // выделяем цветом
                                row.DefaultCellStyle.BackColor = Color.AliceBlue;
                            }
                            else
                            {
                                // сбрасываем цвет если не в списке (на случай обновлений)
                                row.DefaultCellStyle.BackColor = Color.White;
                            }
                        }
                    }
                    манипуляцииСТестомToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                манипуляцииСТестомToolStripMenuItem.Visible = false;
            }
        }
        private void MainMenu_Load_1(object sender, EventArgs e)
        {
            LoadTests();
            // Загрузка всех пользоветелей
            _allUsers = _context.Users.AsNoTracking().ToList();
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
        private void MainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogePageAboutAuthor dialogePageAboutAuthor = new DialogePageAboutAuthor();
            dialogePageAboutAuthor.ShowDialog();
        }
        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testForUpdate = FindAndGetTestInDataGrid();
            if (testForUpdate == null)
            {
                MessageBox.Show("Нормально строку выдели, Тест с таким ID не найден");
                return;
            }
            CurrentUser = textBox1.Text;
            NavigationService.ShowCreatePage(this, CurrentUser, InputMode.Update, testForUpdate);
        }
        private void удалениеТестаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testForDelete = FindAndGetTestInDataGrid();
            if (testForDelete == null)
            {
                MessageBox.Show("Нормально строку выдели, Тест с таким ID не найден");
                return;
            }
            DialogResult result = MessageBox.Show(
                "Вы уверены, что хотите удалить тест?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                // Загружаем связанные сущности
                testForDelete = _context.Tests
                    .Include(t => t.Questions)
                    .FirstOrDefault(t => t.Id == testForDelete.Id);
                string titleDeletableTest = testForDelete.Title;
                // Удаляем связанные TestStatistic
                var statistics = _context.TestStatistic
                    .Where(s => s.TestId == testForDelete.Id);
                _context.TestStatistic.RemoveRange(statistics);
                // Удаляем сам тест
                _context.Tests.Remove(testForDelete);
                _context.SaveChanges();
                MessageBox.Show($"Тест с названием: {titleDeletableTest} удалён");
                LoadTests();
            }
            else
            {
                // Пользователь отказался от удаления — ничего не делаем
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Папка, где лежит справка — относительно папки запуска
            string helpDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            // Проверяем — существует ли эта папка
            if (!Directory.Exists(helpDirectory))
            {
                MessageBox.Show($"Папка справки (Resource) не найдена: {helpDirectory}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ищем первый chm файл
            string helpFile = Directory.GetFiles(helpDirectory, "*.chm").FirstOrDefault();

            if (helpFile == null)
            {
                MessageBox.Show($"Файл справки (*.chm) не найден в папке: {helpDirectory} - положи сюда .chm файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Запускаем справку
            Help.ShowHelp(null, helpFile);
        }
    }
}
