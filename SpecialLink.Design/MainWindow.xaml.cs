using SpecialLink.Core;
using SpecialLink.Core.Models;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
using SpecialLink.Design.AdminWindows;
using SpecialLink.Design.UserWindows;
using SpecialLink.Design.UserWindows.TestWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpecialLink.Design
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;

        public MainWindow()
        {
            InitializeComponent();

            //_storage.GetPersons.Add(new Admin
            //{
            //    Login = "admin200",
            //    Password = "qwirqwbicw",
            //    LastLogin = DateTime.Now
            //}
            //);
            //_storage.Save();

            //List<string> check = new List<string>();

            //List<string> namesTest = new List<string>();
            //foreach (var t in Factory.GetInstance().Storage.GetTests)
            //{
            //    namesTest.Add(t.Name);
            //    check.Add(t.Name);
            //}

            //List<string> LoginPerson = new List<string>();
            //foreach (var p in Factory.GetInstance().Storage.GetPersons)
            //{
            //    LoginPerson.Add(p.Login);
            //    check.Add(p.Login);
            //}

            //TestingListBox.ItemsSource = check;

            /*AnswerBasedTest newTest = new AnswerBasedTest()
            {
                AmountOfTimesTaken = 0,
                Name = "Тест по гендерной совместимости",
                Description = "Гейские приколы для тестинга окна для этого типа тестов",
                Combinations = new List<Combination>()
                {
                    new Combination()
                    {
                        FirstValue = "мужской пол",
                        SecondValue = "мужской пол",
                        Score = 100,
                        Explanation = "10/10, вы прекрасны, ВШЭ хочет вас в студенты."
                    },
                    new Combination()
                    {
                        FirstValue = "мужской пол",
                        SecondValue = "женский пол",
                        Score = 78,
                        Explanation = "Не ужасно, но могло бы быть и лучше."
                    },
                    new Combination()
                    {
                        FirstValue = "женский пол",
                        SecondValue = "женский пол",
                        Score = 100,
                        Explanation = "О-о, у вас хороший вкус. Так держать, вы шикарны!"
                    }
                }
            };
            _storage.GetTests.Add(newTest);
            _storage.Save(); */
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            User user = _storage.GetPersons[0] as User;
            UserMenuWindow userMenuWindow = new UserMenuWindow(user);
            userMenuWindow.Show();
            this.Close();

            /*Admin admin = _storage.GetPersons[1] as Admin;

            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(admin);
            adminMenuWindow.Show();
            Close(); */

            // Маша тебе нужно здесь сделать авторизацию тк я хз как там с хешированием паролей проверка работает
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
