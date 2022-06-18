using SpecialLink.Core;
using SpecialLink.Core.Models;
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

namespace SpecialLink.Design.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для AddQuestionTestWindow.xaml
    /// </summary>
    public partial class AddQuestionTestWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Admin _admin;
        QuestionBasedTest _questionBasedTest;
        public AddQuestionTestWindow(Admin admin, QuestionBasedTest questionBasedTest)
        {
            _admin = admin;
            _questionBasedTest = questionBasedTest;
            InitializeComponent();
            QuestionsListBox.ItemsSource = _questionBasedTest.Questions;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
            adminMenuWindow.Show();
            Close();
        }

        private void AddFullTestButton_Click(object sender, RoutedEventArgs e)
        {

            List<string> listNamesOfTests = new List<string>();
            foreach (var q in _storage.GetTests)
            {
                listNamesOfTests.Add(q.Name);
            }

            if (_questionBasedTest.Questions.Count >= 3 )
            {
                if (NameForTestTextBox.Text == "" | listNamesOfTests.Contains(NameForTestTextBox.Text))
                {
                    MessageBox.Show("Ой, неверное название теста (пустое / тест с таким названием уже создан)");
                }
                else
                {
                    if (DescriptionTextBox.Text != "")
                    {
                        _questionBasedTest.Description = DescriptionTextBox.Text;
                    }
                    _questionBasedTest.Name = NameForTestTextBox.Text;
                    _questionBasedTest.ImageSource = "icon_2.jpg";
                    _storage.GetTests.Add(_questionBasedTest);
                    _storage.Save();
                    MessageBox.Show("Тест успешно добавлен!");
                    AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
                    adminMenuWindow.Show();
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Ой, недостаточное количество вопросов(минимум = 3)");
            }

        }

        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            int indexForDelete = QuestionsListBox.SelectedIndex;

            if (indexForDelete != -1)
            {
                _questionBasedTest.Questions.RemoveAt(indexForDelete);
                QuestionsListBox.ItemsSource = "";
                QuestionsListBox.ItemsSource = _questionBasedTest.Questions;
            }
            else
            {
                MessageBox.Show("Упс, никакой вопрос не был выбран");
            }
        }

        private void QuestionNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock QuestionName = sender as TextBlock;
            Question question = QuestionName.DataContext as Question;
            QuestionName.Text = question.QuestionText;
        }

        private void Answer_1_TextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock Answer_1_TextBlock = sender as TextBlock;
            Question question = Answer_1_TextBlock.DataContext as Question;
            Answer_1_TextBlock.Text = question.FirstAnswer;
        }

        private void Answer_2_TextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock Answer_2_TextBlock = sender as TextBlock;
            Question question = Answer_2_TextBlock.DataContext as Question;
            Answer_2_TextBlock.Text = question.SecondAnswer;
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var children = AddQuestionGrid.Children;
            TextBox child2Name = children[2] as TextBox;
            TextBox child5Answer1 = children[5] as TextBox;
            TextBox child6Answer2 = children[6] as TextBox;


            if (child2Name.Text != "" & child5Answer1.Text != "" & child6Answer2.Text != "")
            {
                if (child5Answer1.Text != child6Answer2.Text)
                {
                    Question question = new Question();
                    question.QuestionText = child2Name.Text;
                    question.FirstAnswer = child5Answer1.Text;
                    question.SecondAnswer = child6Answer2.Text;
                    _questionBasedTest.Questions.Add(question);

                    QuestionsListBox.ItemsSource = "";
                    QuestionsListBox.ItemsSource = _questionBasedTest.Questions;

                    child2Name.Clear();
                    child5Answer1.Clear();
                    child6Answer2.Clear();
                }
                else
                {
                    MessageBox.Show("Упс, ответы должны различаться");
                    child6Answer2.Clear();
                }
            }
            else
            {
                MessageBox.Show("Упс, не все поля заполнены, попробуйте снова");
            }
        }
    }
}
