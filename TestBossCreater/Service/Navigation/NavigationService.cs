using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBossCreater.Models;
using TestBossCreater.Models.Consts;
using TestBossCreater.Pages;
using TestBossCreater.Pages.DialogePage;
using static System.Net.Mime.MediaTypeNames;
namespace TestBossCreater.Service.Navigation
{
    /// <summary>
    /// Вспомогательный класс навигации между страницами
    /// </summary>
    public static class NavigationService
    {
        /// <summary>
        ///  Это свойство тут нужно чтобы не создавать лишние экземпляра главной формы
        /// </summary>
        public static Form MainMenu { get; set;  }
        /// <summary>
        /// Показать форму создания теста
        /// </summary>
        /// <param name="currentForm">форма на которой вызываем код</param>
        /// <param name="currentUser">текущий пользователь</param>
        /// <param name="inputMode">мод пользователя (создания/редактирование)</param>
        /// <param name="updatableTest">передаваймый для редактирования тест (если мод - редактирование)</param>
        public static void ShowCreatePage(Form currentForm, string currentUser, InputMode inputMode, Test updatableTest = null)
        {
            // Создаём экземпляр формы CreateTest
            var createTestForm = new CreateTest(currentUser, inputMode, updatableTest);
            // Скрываем текущую главную форму (нельзя закрывать, потому что потушиться приложение)
            MainMenu = currentForm;
            MainMenu.Hide();
            createTestForm.StartPosition = FormStartPosition.CenterScreen;
            createTestForm.Show();
        }
        public static void ShowMainMenu(Form currentForm)
        {
            currentForm.Close();
            currentForm.Dispose();
            MainMenu = new MainMenu();
            MainMenu.StartPosition = FormStartPosition.CenterScreen;
            MainMenu.Show();
        }
        public static void ShowPassTestPage(Form currentForm, string currentUser, Test test)
        {
            // Создаём экземпляр формы CreateTest
            var PassForm = new PassTest(currentUser, test);
            MainMenu = currentForm;
            MainMenu.Hide();
            PassForm.StartPosition = FormStartPosition.CenterScreen;
            PassForm.Show();
        }
        public static void ShowStatistcPage(Form currentForm, List<TestStatistic> testStatistics)
        {
            // Создаём экземпляр формы CreateTest
            var statisticForm = new DialogePageForStatistic(testStatistics);
            MainMenu = currentForm;
            MainMenu.Hide();
            statisticForm.StartPosition = FormStartPosition.CenterScreen;
            statisticForm.Show();
        }
    }
}
