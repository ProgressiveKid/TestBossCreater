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
        /// ���������� ������������ �����
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
            // ��������� �����������
            _allTests = allTests;
            dataGridView1.Columns[nameof(Test.Id)].HeaderText = "Id";
            dataGridView1.Columns[nameof(Test.Title)].HeaderText = "�������� �����";
            dataGridView1.Columns[nameof(Test.Description)].HeaderText = "��������";
            dataGridView1.Columns[nameof(Test.Creater)].HeaderText = "�����";
            dataGridView1.Columns[nameof(Test.NeededTrueAnswers)].HeaderText = "��������� ����������";
            dataGridView1.Columns["CountQuestion"].HeaderText = "����� ��������";
            // ��������� ��������� ������� ��� �������
            dataGridView1.Columns[nameof(Test.Id)].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[nameof(Test.CountQuestion)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;
        }
        /// <summary>
        /// ������� ����� ����
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
                MessageBox.Show("��������� ������ ������, ���� � ����� ID �� ������");
                return;
            }
            FindOrCreateUser(CurrentUser);
            NavigationService.ShowPassTestPage(this, CurrentUser, test);
        }
        private Test FindAndGetTestInDataGrid()
        {
            int testId = 0;
            if (dataGridView1.SelectedRows.Count > 0) // ���������, ��� ���� ���������� ������
            {
                var firstSelectedRow = dataGridView1.SelectedRows[0]; // ���� ������ ���������� ������
                testId = Convert.ToInt32(firstSelectedRow.Cells[0].Value); // �������� ������ ������ � ���� ������
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
        /// ������� ����� ����
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
            // ������������ �������� �����, ����� ������ ���������� ����� � �������
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
                                // �������� ������
                                row.DefaultCellStyle.BackColor = Color.AliceBlue;
                            }
                            else
                            {
                                // ���������� ���� ���� �� � ������ (�� ������ ����������)
                                row.DefaultCellStyle.BackColor = Color.White;
                            }
                        }
                    }
                    ������������������ToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
                ������������������ToolStripMenuItem.Visible = false;
            }
        }
        private void MainMenu_Load_1(object sender, EventArgs e)
        {
            LoadTests();
            // �������� ���� �������������
            _allUsers = _context.Users.AsNoTracking().ToList();
            textBox1.Text = CurrentUser;
        }
        private void ������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userStatistic = _context.TestStatistic.Where(x => x.UserName == CurrentUser).ToList();
            NavigationService.ShowStatistcPage(this, userStatistic);
        }
        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var findedUser = _context.Users.FirstOrDefault(x => x.UserName == CurrentUser);
            if (findedUser == null)
            {
                MessageBox.Show($"��������� ������������: {CurrentUser} �� ������ � �� - ��������� ��� ���������� �����");
                return;
            }
            var testStatistics = _context.TestStatistic.Where(x => x.UserId == findedUser.Id).ToList();
            if (testStatistics == null)
            {
                MessageBox.Show($"� ���������� ������������: {findedUser.UserName} �� ������� ���������� - ��������� ��� ���������� �����");
                return;
            }
            NavigationService.ShowStatistcPage(this, testStatistics);
        }
        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var choosedTest = FindAndGetTestInDataGrid();
            var testStatistics = _context.TestStatistic.Where(x => x.TestId == choosedTest.Id).ToList();
            if (testStatistics == null)
            {
                MessageBox.Show($"�� ���������� �����: {choosedTest.Title} (Id = {choosedTest.Id}) ��� ��� ���������� �����������");
            }
            NavigationService.ShowStatistcPage(this, testStatistics);
        }
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testStatistics = _context.TestStatistic.ToList();
            if (testStatistics == null)
            {
                MessageBox.Show($"�� �� ������� �� ������ �����");
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
        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogePageAboutAuthor dialogePageAboutAuthor = new DialogePageAboutAuthor();
            dialogePageAboutAuthor.ShowDialog();
        }
        private void ��������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testForUpdate = FindAndGetTestInDataGrid();
            if (testForUpdate == null)
            {
                MessageBox.Show("��������� ������ ������, ���� � ����� ID �� ������");
                return;
            }
            CurrentUser = textBox1.Text;
            NavigationService.ShowCreatePage(this, CurrentUser, InputMode.Update, testForUpdate);
        }
        private void �������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var testForDelete = FindAndGetTestInDataGrid();
            if (testForDelete == null)
            {
                MessageBox.Show("��������� ������ ������, ���� � ����� ID �� ������");
                return;
            }
            DialogResult result = MessageBox.Show(
                "�� �������, ��� ������ ������� ����?",
                "������������� ��������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            if (result == DialogResult.Yes)
            {
                // ��������� ��������� ��������
                testForDelete = _context.Tests
                    .Include(t => t.Questions)
                    .FirstOrDefault(t => t.Id == testForDelete.Id);
                string titleDeletableTest = testForDelete.Title;
                // ������� ��������� TestStatistic
                var statistics = _context.TestStatistic
                    .Where(s => s.TestId == testForDelete.Id);
                _context.TestStatistic.RemoveRange(statistics);
                // ������� ��� ����
                _context.Tests.Remove(testForDelete);
                _context.SaveChanges();
                MessageBox.Show($"���� � ���������: {titleDeletableTest} �����");
                LoadTests();
            }
            else
            {
                // ������������ ��������� �� �������� � ������ �� ������
            }
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // �����, ��� ����� ������� � ������������ ����� �������
            string helpDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            // ��������� � ���������� �� ��� �����
            if (!Directory.Exists(helpDirectory))
            {
                MessageBox.Show($"����� ������� (Resource) �� �������: {helpDirectory}", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ���� ������ chm ����
            string helpFile = Directory.GetFiles(helpDirectory, "*.chm").FirstOrDefault();

            if (helpFile == null)
            {
                MessageBox.Show($"���� ������� (*.chm) �� ������ � �����: {helpDirectory} - ������ ���� .chm ����", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ��������� �������
            Help.ShowHelp(null, helpFile);
        }
    }
}
