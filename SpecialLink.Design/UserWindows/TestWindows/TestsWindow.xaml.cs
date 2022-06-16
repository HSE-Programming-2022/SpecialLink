using SpecialLink.Core;
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

namespace SpecialLink.Design.UserWindows.TestWindows
{
    /// <summary>
    /// Логика взаимодействия для TestsWindow.xaml
    /// </summary>
    public partial class TestsWindow : Window
    {
        public TestsWindow()
        {
            InitializeComponent();
            TestsListBox.ItemsSource = Factory.GetInstance().Storage.GetTests;
        }

        private void TestImage_Initialized(object sender, EventArgs e)
        {

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
    }
}
