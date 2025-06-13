using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using TestBossCreater.Models;
using TestBossCreater.Pages;
using TestBossCreater.Service.Navigation;

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
            {
                dataGridView1.DataSource = allTests;
            }
        }

        /// <summary>
        /// Создать новый тест
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button1_Click(object sender, EventArgs e)
        {
            int testId = 0;
            CurrentUser = textBox1.Text;
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
                MessageBox.Show("Нормально строку выдели, Тест с таким ID не найден");
                return;
            }
            Navigation.ShowPassTestPage(this, CurrentUser, testId);
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
            Navigation.ShowCreatePage(this, CurrentUser);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainMenu_Load_1(object sender, EventArgs e)
        {
            textBox1.Text = CurrentUser;
        }
    }
}
