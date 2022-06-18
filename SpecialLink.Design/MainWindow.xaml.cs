using SpecialLink.Core;
using SpecialLink.Core.Models.People;
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
        }

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            //UserMenuWindow userMenuWindow = new UserMenuWindow();
            //userMenuWindow.Show();
            //this.Close();

            Admin admin = _storage.GetPersons[1] as Admin;

            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(admin);
            adminMenuWindow.Show();
            Close();

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
