using System.Net;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class DialogePageForAddImageInTest : Form
    {
        public DialogePageForAddImageInTest()
        {
            InitializeComponent();
        }

        public Image SelectedImage { get; set; }
        public string ImagePath { get; set; }

        /// <summary>
        /// Скачать из интернета картинку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string url = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("Введите ссылку на изображение.");
                    return;
                }

                // Папка для сохранения
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ImagesForTest");
                Directory.CreateDirectory(folderPath);

                // Получаем расширение из URL (если оно есть)
                string extension = Path.GetExtension(new Uri(url).AbsolutePath);
                if (string.IsNullOrEmpty(extension))
                    extension = ".jpg"; // По умолчанию, если в URL нет расширения

                // Уникальное имя файла
                string fileName = Guid.NewGuid().ToString() + extension;

                // Полный путь
                string fullPath = Path.Combine(folderPath, fileName);
                using (WebClient client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(url), fullPath);
                }

                // Загружаем изображение в PictureBox
                using (var img = Image.FromFile(fullPath))
                {
                    string relativePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, fullPath);
                    // Создаем копию картинки чтобы освободить файл
                    pictureBox1.Image = new Bitmap(img);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.ImageLocation = relativePath;
                }

                MessageBox.Show("Изображение успешно загружено и отображено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке: " + ex.Message);
            }
        }

        private void DialogePageForAddImageInTest_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectedImage = pictureBox1.Image;
            ImagePath = pictureBox1.ImageLocation;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите изображение";
                openFileDialog.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Путь выбранного файла
                        string sourcePath = openFileDialog.FileName;

                        // Папка для сохранения (та же папка что и для интернета)
                        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "ImagesForTest");
                        Directory.CreateDirectory(folderPath);

                        // Получаем расширение файла
                        string extension = Path.GetExtension(sourcePath);

                        // Генерируем уникальное имя
                        string fileName = Guid.NewGuid().ToString() + extension;

                        // Куда сохраняем
                        string fullPath = Path.Combine(folderPath, fileName);

                        // Копируем файл в хранилище
                        File.Copy(sourcePath, fullPath);

                        // Загружаем изображение в PictureBox
                        using (var img = Image.FromFile(fullPath))
                        {
                            string relativePath = Path.GetRelativePath(AppDomain.CurrentDomain.BaseDirectory, fullPath);
                            pictureBox1.Image = new Bitmap(img);
                            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                            pictureBox1.ImageLocation = relativePath;
                        }

                        MessageBox.Show("Изображение успешно добавлено из локального файла.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при добавлении изображения: " + ex.Message);
                    }
                }
            }
        }
    }
}
