using System.Windows.Forms;
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
        /// ���������� ������������ �����
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
        /// ������� ����� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button1_Click(object sender, EventArgs e)
        {
            CurrentUser = textBox1.Text;
            Navigation.ShowCreatePage(this, CurrentUser);
        }

        /// <summary>
        /// ������ ��������� ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button2_Click(object sender, EventArgs e)
        {
            CurrentUser = textBox1.Text;
            throw new NotImplementedException();
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
