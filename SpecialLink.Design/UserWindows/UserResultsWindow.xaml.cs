using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Results;
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

namespace SpecialLink.Design.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserResultsWindow.xaml
    /// </summary>
    public partial class UserResultsWindow : Window
    {
        User _user;
        IStorage _storage = Factory.GetInstance().Storage;
        public UserResultsWindow()
        {
            InitializeComponent();
        }

        public UserResultsWindow(User user)
        {
            InitializeComponent();
            _user = user;
            TestResultsListBox.ItemsSource = _user.Results;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            UserMenuWindow userMenuWindow = new UserMenuWindow(_user);
            userMenuWindow.Show();
            this.Close();
        }

        private void EraseButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var person in _storage.GetPersons)
            {
                if (person.Login == _user.Login)
                {
                    (person as User).Results = new List<Result>();
                    _storage.Save();
                    _user = (person as User);
                    MessageBox.Show("Вы успешно очистили историю прохождения тестов.");
                    break;
                }
            }
            UserMenuWindow userMenuWindow = new UserMenuWindow(_user);
            userMenuWindow.Show();
            this.Close();
        }

        private void TestNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock TestNameTextBlock = sender as TextBlock;
            Result result = TestNameTextBlock.DataContext as Result;
            TestNameTextBlock.Text = "♡ " + result.TestName;
        }

        private void ScoreTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ScoreTextBlock = sender as TextBlock;
            Result result = ScoreTextBlock.DataContext as Result;
            ScoreTextBlock.Text = "· Полученная совместимость: " + result.Score.ToString() + "%";
        }

        private void ExplanationTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ExplanationTextBlock = sender as TextBlock;
            Result result = ExplanationTextBlock.DataContext as Result;
            ExplanationTextBlock.Text = "· О совместимости: " + result.Explanation;
        }

        private void ParemeterTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock ParemeterTextBlock = sender as TextBlock;
            Result result = ParemeterTextBlock.DataContext as Result;
            if (result is ComputingOrAnswerBasedResult)
            {
                ParemeterTextBlock.Text = "Введенные параметры: " + (result as ComputingOrAnswerBasedResult).FirstValue + " & " + (result as ComputingOrAnswerBasedResult).SecondValue;
            }
            else
            {
                ParemeterTextBlock.Text = "Количество совпавших ответов: " + (result as QuestionBasedResult).NumberOfMatches.ToString();
            }
        }
    }
}
