using System.Windows.Forms;
using TestBossCreater.Pages;
using TestBossCreater.Service.Navigation;

namespace TestBossCreater
{
    public partial class MainMenu : Form
    {
        private readonly AppDbContext _context;
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
            var products = _context.Tests.ToList();
            if (products.Count == 0)
            {
                pictureBoxDataGrid.BringToFront();
                pictureBoxDataGrid.Visible = true;
            }
            else
            {
                dataGridView1.DataSource = products;
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
            Navigation.ShowCreatePage(this);
        }

        /// <summary>
        /// Пройти выбранный
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
