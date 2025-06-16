using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestBossCreater.Models;
using TestBossCreater.Service.Navigation;

namespace TestBossCreater.Pages.DialogePage
{
    public partial class DialogePageForStatistic : Form
    {
        public DialogePageForStatistic()
        {
            InitializeComponent();
        }

        public DialogePageForStatistic(List<TestStatistic> testStatistics)
        {
            InitializeComponent();
            // Выборка только по информативным полям
            dataGridView1.DataSource = testStatistics.
                Select(x => new
                {
                    x.UserName,
                    x.Test.Title,
                    x.Test.Description,
                    x.TrueAnswer,
                    x.TestQuestion,
                    x.Grade,
                    x.Comment
                })
                .ToList();

            // Названия колонок
            dataGridView1.Columns[nameof(TestStatistic.UserName)].HeaderText = "Пользователь";
            dataGridView1.Columns[nameof(Test.Title)].HeaderText = "Название теста";
            dataGridView1.Columns[nameof(Test.Description)].HeaderText = "Описание теста";
            dataGridView1.Columns[nameof(TestStatistic.TrueAnswer)].HeaderText = "Кол-во правильных вопросов";
            dataGridView1.Columns[nameof(TestStatistic.TestQuestion)].HeaderText = "Всего вопросов";
            dataGridView1.Columns[nameof(TestStatistic.Grade)].HeaderText = "Прошёл?";
            dataGridView1.Columns[nameof(TestStatistic.Comment)].HeaderText = "Комментарий";
            // Растянуть последний столбце для визуала
            dataGridView1.Columns[nameof(TestStatistic.Comment)].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.ReadOnly = true;
        }

        private void DialogePageForStatistic_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NavigationService.ShowMainMenu(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
