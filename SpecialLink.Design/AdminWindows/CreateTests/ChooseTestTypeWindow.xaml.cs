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
    /// Логика взаимодействия для ChooseTestTypeWindow.xaml
    /// </summary>
    public partial class ChooseTestTypeWindow : Window
    {
        Admin _admin;

        public ChooseTestTypeWindow(Admin admin)
        {
            _admin = admin;
            InitializeComponent();
        }

        private void wordTestButton_Click(object sender, RoutedEventArgs e)
        {
            AddWordTestWindow addWordTestWindow = new AddWordTestWindow(_admin);
            addWordTestWindow.Show();
            Close();
        }

        private void combinationTestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void questionTestButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionBasedTest questionBasedTest = new QuestionBasedTest();
            questionBasedTest.Questions = new List<Core.Models.Question>();

            AddQuestionTestWindow addQuestionTestWindow = new AddQuestionTestWindow(_admin, questionBasedTest);
            addQuestionTestWindow.Show();
            Close();

        }
    }
}
