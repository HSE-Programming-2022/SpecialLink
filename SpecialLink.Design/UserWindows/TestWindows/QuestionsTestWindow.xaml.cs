using SpecialLink.Core;
using SpecialLink.Core.Models;
using SpecialLink.Core.Models.People;
using SpecialLink.Core.Models.Results;
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
    /// Логика взаимодействия для QuestionsTestWindow.xaml
    /// </summary>
    public partial class QuestionsTestWindow : Window
    {
        Test _test;
        User _user;
        List<int> _firstPersonAnswers = new List<int>();
        List<int> _secondPersonAnswers = new List<int>();
        IStorage _storage = Factory.GetInstance().Storage;

        public QuestionsTestWindow()
        {
            InitializeComponent();
        }

        public QuestionsTestWindow(Test test, User user)
        {
            InitializeComponent();
            _test = test;
            _user = user;
            QuestionsFirstListBox.ItemsSource = (_test as QuestionBasedTest).Questions;
            //QuestionsSecondListBox.ItemsSource = (_test as QuestionBasedTest).Questions;
            QuestionsSecondListBox.ItemsSource = new List<string>();
            SecondResultButton.IsEnabled = false;
            FillAnswersList();
        }

        private void FirstResultButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionsFirstListBox.IsEnabled = false;
            QuestionsFirstListBox.ItemsSource = new List<string>();
            QuestionsSecondListBox.ItemsSource = (_test as QuestionBasedTest).Questions;
            FirstResultButton.IsEnabled = false;
            SecondResultButton.IsEnabled = true;
        }

        private void FLQuestionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock FLQuestionTextBlock = sender as TextBlock;
            Question question = FLQuestionTextBlock.DataContext as Question;
            FLQuestionTextBlock.Text = question.QuestionText;
        }

        private void FLFirstAnswerRadioButton_Initialized(object sender, EventArgs e)
        {
            RadioButton FLFirstAnswerRadioButton = sender as RadioButton;
            Question question = FLFirstAnswerRadioButton.DataContext as Question;
            FLFirstAnswerRadioButton.Content = question.FirstAnswer;
        }

        private void FLSecondAnswerRadioButton_Initialized(object sender, EventArgs e)
        {
            RadioButton FLSecondAnswerRadioButton = sender as RadioButton;
            Question question = FLSecondAnswerRadioButton.DataContext as Question;
            FLSecondAnswerRadioButton.Content = question.SecondAnswer;
        }

        private void SLQuestionTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock SLQuestionTextBlock = sender as TextBlock;
            Question question = SLQuestionTextBlock.DataContext as Question;
            SLQuestionTextBlock.Text = question.QuestionText;
        }

        private void SLFirstAnswerRadioButton_Initialized(object sender, EventArgs e)
        {
            RadioButton SLFirstAnswerRadioButton = sender as RadioButton;
            Question question = SLFirstAnswerRadioButton.DataContext as Question;
            SLFirstAnswerRadioButton.Content = question.FirstAnswer;
        }

        private void SLSecondAnswerRadioButton_Initialized(object sender, EventArgs e)
        {
            RadioButton SLSecondAnswerRadioButton = sender as RadioButton;
            Question question = SLSecondAnswerRadioButton.DataContext as Question;
            SLSecondAnswerRadioButton.Content = question.SecondAnswer;
        }

        private void SecondResultButton_Click(object sender, RoutedEventArgs e)
        {
            QuestionsSecondListBox.IsEnabled = false;
            QuestionsSecondListBox.ItemsSource = new List<string>();
            SecondResultButton.IsEnabled = false;

            Result result = _test.GetResult(_firstPersonAnswers, _secondPersonAnswers);
            result.TestName = _test.Name;

            foreach (var person in _storage.GetPersons)
            {
                if (person.Login == _user.Login)
                {
                    (person as User).Results.Add(result);
                    _storage.Save();
                    _user = (person as User);
                    break;
                }
            }
            foreach (var test in _storage.GetTests)
            {
                if ((test.Name == _test.Name) && (test.Description == _test.Description))
                {
                    test.AmountOfTimesTaken += 1;
                    _storage.Save();
                    break;
                }
            }
            ResultsWindow resultsWindow = new ResultsWindow(result, _user);
            resultsWindow.Show();
            this.Close();
        }

        private void FillAnswersList()
        {
            foreach (var question in (_test as QuestionBasedTest).Questions)
            {
                _firstPersonAnswers.Add(0);
                _secondPersonAnswers.Add(0);
            }
        }

        private void FLFirstAnswerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton FLFirstAnswerRadioButton = sender as RadioButton;
            Question question = FLFirstAnswerRadioButton.DataContext as Question;
            for (int i = 0; i < (_test as QuestionBasedTest).Questions.Count; i++)
            {
                if ((_test as QuestionBasedTest).Questions[i].QuestionText == question.QuestionText)
                {
                    _firstPersonAnswers[i] = 0;
                    break;
                }
            }
        }

        private void FLSecondAnswerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton FLSecondAnswerRadioButton = sender as RadioButton;
            Question question = FLSecondAnswerRadioButton.DataContext as Question;
            for (int i = 0; i < (_test as QuestionBasedTest).Questions.Count; i++)
            {
                if ((_test as QuestionBasedTest).Questions[i].QuestionText == question.QuestionText)
                {
                    _firstPersonAnswers[i] = 1;
                    break;
                }
            }
        }

        private void SLFirstAnswerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton SLFirstAnswerRadioButton = sender as RadioButton;
            Question question = SLFirstAnswerRadioButton.DataContext as Question;
            for (int i = 0; i < (_test as QuestionBasedTest).Questions.Count; i++)
            {
                if ((_test as QuestionBasedTest).Questions[i].QuestionText == question.QuestionText)
                {
                    _secondPersonAnswers[i] = 0;
                    break;
                }
            }
        }

        private void SLSecondAnswerRadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton SLSecondAnswerRadioButton = sender as RadioButton;
            Question question = SLSecondAnswerRadioButton.DataContext as Question;
            for (int i = 0; i < (_test as QuestionBasedTest).Questions.Count; i++)
            {
                if ((_test as QuestionBasedTest).Questions[i].QuestionText == question.QuestionText)
                {
                    _secondPersonAnswers[i] = 1;
                    break;
                }
            }
        }
    }
}
