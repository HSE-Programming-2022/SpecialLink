using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace SpecialLink.Design.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для AdminMenuWindow.xaml
    /// </summary>
    public partial class AdminMenuWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Admin _admin;
        public AdminMenuWindow(Admin admin)
        {
            _admin = admin;
            InitializeComponent();
            TestsListBox.ItemsSource = _storage.GetTests;
        }

        private void TestImage_Initialized(object sender, EventArgs e)
        {
            Image TestImage = sender as Image;

            Test test = TestImage.DataContext as Test;

            BitmapImage bitmapImage = new BitmapImage();

            using (var fileStream = new FileStream("../../Pictures/Icons_CMYK/" + test.ImageSource, FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            TestImage.Source = bitmapImage;

        }

        private void TestNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TestName = sender as TextBlock;
            Test test = TestName.DataContext as Test;
            TestName.Text = test.Name;
        }

        private void TestDescriptionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TestDescription = sender as TextBlock;
            Test test = TestDescription.DataContext as Test;
            TestDescription.Text = "Описание: " + test.Description;
        }

        private void TimesTakenTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TimesTaken = sender as TextBlock;
            Test test = TimesTaken.DataContext as Test;
            TimesTaken.Text = "Количество прохождений: " + test.AmountOfTimesTaken.ToString();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            int index = _storage.GetPersons.FindIndex(a => a.Login == _admin.Login);
            Admin admin = _storage.GetPersons[index] as Admin;
            admin.LastLogin = DateTime.Now;
            _storage.Save();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Addtest_Click(object sender, RoutedEventArgs e)
        {
            QuestionBasedTest questionBasedTest = new QuestionBasedTest();
            questionBasedTest.Questions = new List<Core.Models.Question>();

            AddQuestionTestWindow addQuestionTestWindow = new AddQuestionTestWindow(_admin, questionBasedTest);
            addQuestionTestWindow.Show();
            Close();
        }

        private void Deletetest_Click(object sender, RoutedEventArgs e)
        {
            int indexForDelete = TestsListBox.SelectedIndex;

            if (indexForDelete != -1)
            {
                if (TestsListBox.Items.Count > 1)
                {
                    _storage.GetTests.RemoveAt(indexForDelete);
                    _storage.Save();
                    MessageBox.Show("Тест успешно удалён! ");
                    TestsListBox.ItemsSource = "";
                    TestsListBox.ItemsSource = _storage.GetTests;
                }
                else
                {
                    MessageBox.Show("Нельзя удалить единсвенный тест");
                }
            }
            else
            {
                MessageBox.Show("Упс, никакой тест не был выбран");
            }

        }

        private void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            ChangeAdminListWindow changeAdminListWindow = new ChangeAdminListWindow(_admin);
            changeAdminListWindow.Show();
            Close();
        }

        private void AdminLoginTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock AdminLogin = sender as TextBlock;
            AdminLogin.Text = "Ваш логин: " + _admin.Login;

        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var children = ChangePasswordGrid.Children;
            PasswordBox child2 = children[2] as PasswordBox;


            // Маша, тут нужно захешировать пароль 
            if (NewPasswordPasswordBox.Password != "")
            {
                //_admin.Password = child2.Password;

                int index = _storage.GetPersons.FindIndex(a => a.Login == _admin.Login);
                _storage.GetPersons[index].Password = child2.Password;
                _storage.Save();

                MessageBox.Show("Пароль успешно изменён");
                child2.Clear();
            }
            else
            {
                MessageBox.Show("Нельзя заменить на пустой пароль");
            }

        }
    }
}
