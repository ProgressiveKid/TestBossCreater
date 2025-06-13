using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBossCreater.Pages;

namespace TestBossCreater.Service.Navigation
{
    /// <summary>
    /// Вспомогательный класс навигации между страницами
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
            createTestForm.Show();

        }

        public static void ShowMainMenu(Form currentForm)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new MainMenu();

            // Закрываем текущую главную форму
            currentForm.Close();

            // Показываем CreateTest как модальное окно
            createTestForm.Show();

        }

        public static void ShowPassTestPage(Form currentForm, string currentUser, int testId)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new PassTest(currentUser, testId);

            // Закрываем текущую главную форму
            currentForm.Hide();

            // Показываем CreateTest как модальное окно
            createTestForm.Show();
        }
    }
}
