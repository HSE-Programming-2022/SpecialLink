using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Tests;
using SpecialLink.Design.UserWindows.TestWindows;
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

namespace SpecialLink.Design.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserMenuWindow.xaml
    /// </summary>
    public partial class UserMenuWindow : Window
    {
        User _user;
        public UserMenuWindow()
        {
            InitializeComponent();
            TestsListBox.ItemsSource = Factory.GetInstance().Storage.GetTests;
        }

        public UserMenuWindow(User user)
        {
            _user = user;
            InitializeComponent();
            TestsListBox.ItemsSource = Factory.GetInstance().Storage.GetTests;
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
            TextBlock TestNameTextBlock = sender as TextBlock;

            Test test = TestNameTextBlock.DataContext as Test;

            TestNameTextBlock.Text = test.Name;
        }

        private void TimesTakenTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TimesTakenTextBlock = sender as TextBlock;

            Test test = TimesTakenTextBlock.DataContext as Test;

            TimesTakenTextBlock.Text = "Количество прохождений: " + test.AmountOfTimesTaken.ToString();
        }

        private void TakeTestButton_Initialized(object sender, EventArgs e)
        {
            Button TakeTestButton = sender as Button;
            Test test = TakeTestButton.DataContext as Test;
            TakeTestButton.Content = "Пройти";
        }

        private void TestDescriptionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TestDescriptionTextBlock = sender as TextBlock;

            Test test = TestDescriptionTextBlock.DataContext as Test;

            TestDescriptionTextBlock.Text = "Описание: " + test.Description;
        }

        private void TakeTestButton_Click(object sender, RoutedEventArgs e)
        {
            Button TakeTestButton = sender as Button;
            Test test = TakeTestButton.DataContext as Test;
            if (test is ComputationBasedTest)
            {
                ComputationTestWindow computationTestWindow = new ComputationTestWindow(test);
                computationTestWindow.Show();
                this.Close();
            }
            else if (test is AnswerBasedTest)
            {
                AnswerTestWindow answerTestWindow = new AnswerTestWindow(test);
            }
        }

        private void LoginTextBlock_Initialized(object sender, EventArgs e)
        {
            LoginTextBlock.Text = "Ваш логин: " + _user.Login;
        }

        private void NameTextBlock_Initialized(object sender, EventArgs e)
        {
            NameTextBlock.Text = "Ваше имя: " + _user.Name;
        }
    }
}
