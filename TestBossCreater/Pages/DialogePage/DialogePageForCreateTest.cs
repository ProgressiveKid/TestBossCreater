using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class DialogePageForCreateTest : Form
    {
        public string Title { get; set; }
        public string Description { get; private set; }
        public int NeededTrueAnswers { get; private set; }

        public DialogePageForCreateTest()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Проверяем введённые данные
            if (string.IsNullOrEmpty(richTextBox1.Text) ||
                string.IsNullOrEmpty(richTextBox2.Text) ||
                !int.TryParse(textBox1.Text, out int countTrueAnswers))
            {
                MessageBox.Show("Заполните поля корректно");
                return;
            }

            // Сохраняем данные
            Title = richTextBox1.Text;
            Description = richTextBox2.Text;
            NeededTrueAnswers = countTrueAnswers;

            // Закрываем диалоговое окно и возвращаем DialogResult.OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешить цифры и управляющие символы (Backspace, Delete и т.п.)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отменить ввод
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
