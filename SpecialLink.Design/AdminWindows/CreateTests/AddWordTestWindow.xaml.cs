using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
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
using System.Windows.Shapes;

namespace SpecialLink.Design.AdminWindows.CreateTests
{
    /// <summary>
    /// Логика взаимодействия для AddWordTestWindow.xaml
    /// </summary>
    public partial class AddWordTestWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Admin _admin;
        public AddWordTestWindow(Admin admin)
        {
            _admin = admin;
            InitializeComponent();
        }

        private bool CheckName(string name)
        {
            // если false -> теста по словам с таким названием не существует
            bool check = false;
            foreach (var t in _storage.GetTests)
            {
                if ((t.Name == name) & (t is ComputationBasedTest))
                {
                    check = true;
                }
            }
            return check;
        }

        private void AddFullTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text != "")
            {
                string forCheckeing = "тест " + nameTextBox.Text;
                if (CheckName(forCheckeing))
                {
                    MessageBox.Show("Ой, тест 'по словам' с таким названием уже создан");
                    nameTextBox.Clear();
                }
                else
                {
                    ComputationBasedTest computationBasedTest = new ComputationBasedTest();
                    computationBasedTest.ImageSource = "icon_3.jpg";
                    computationBasedTest.Name = "тест " + nameTextBox.Text;
                    if (descriptionTextBox.Text != "")
                    {
                        computationBasedTest.Description = descriptionTextBox.Text;
                    }
                    _storage.GetTests.Add(computationBasedTest);
                    _storage.Save();
                    MessageBox.Show("Тест успешно добавлен!");

                    AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
                    adminMenuWindow.Show();
                    Close();
                }

            }
            else
            {
                MessageBox.Show("Упс, заполните поле, пожалуйста");
            }

        }

        private void ExitTestButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тест не был создан :(");
            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
            adminMenuWindow.Show();
            Close();
        }
    }
}
