using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBossCreater.Pages;

namespace TestBossCreater.Service.Navigation
{
    /// <summary>
    /// Класс навигации между страницами
    /// </summary>
    public static class Navigation
    {
        public static void ShowCreatePage(Form currentForm, string currentUser)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new CreateTest(currentUser);

            // Скрываем текущую главную форму (нельзя закрывать, потому что потушиться приложение)
            currentForm.Hide();

            // Показываем CreateTest как модальное окно
            createTestForm.ShowDialog();

            // После закрытия CreateTest возвращаем главную форму
            currentForm.Show();
        }

        public static void ShowMainMenu(Form currentForm)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new MainMenu();

            // Закрываем текущую главную форму
            currentForm.Close();

            // Показываем CreateTest как модальное окно
            createTestForm.ShowDialog();

            // После закрытия CreateTest возвращаем главную форму
            currentForm.Show();
        }

        public static void ShowPassTestPage(Form currentForm, int testId)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new PassTest(testId);

            // Закрываем текущую главную форму
            currentForm.Close();

            // Показываем CreateTest как модальное окно
            createTestForm.ShowDialog();

            // После закрытия CreateTest возвращаем главную форму
            currentForm.Show();
        }
    }
}
