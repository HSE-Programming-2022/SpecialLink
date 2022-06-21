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

        //public AddQuestionTestWindow()
        //{
        //    InitializeComponent();
        //    QuestionsListBox.ItemsSource = new List<Question>();
        //    //QuestionsListBox.ItemsSource = _questionBasedTest.Questions;
        //}

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
                if (q is QuestionBasedTest)
                {
                    listNamesOfTests.Add(q.Name);
                }
            }

            if (_questionBasedTest.Questions.Count >= 3 )
            {
                if (NameForTestTextBox.Text == "" | listNamesOfTests.Contains(NameForTestTextBox.Text))
                {
                    MessageBox.Show("Ой, неверное название теста (пустое / тест 'по вопросам' с таким названием уже создан)");
                }
                else
                {
                    if (DescriptionTextBox.Text != "" & DescriptionTextBox.Text.Length <= 130)
                    {
                        _questionBasedTest.Description = DescriptionTextBox.Text;
                        _questionBasedTest.Name = NameForTestTextBox.Text;
                        _questionBasedTest.ImageSource = "icon_2.jpg";
                        _storage.GetTests.Add(_questionBasedTest);
                        _storage.Save();
                        MessageBox.Show("Тест успешно добавлен!");
                        AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
                        adminMenuWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Упс, максимальная длина описания - 130 знаков");
                    }
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
            Answer_1_TextBlock.Text = "ответ №1: " + question.FirstAnswer;
        }

        private void Answer_2_TextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock Answer_2_TextBlock = sender as TextBlock;
            Question question = Answer_2_TextBlock.DataContext as Question;
            Answer_2_TextBlock.Text = "ответ №2: " + question.SecondAnswer;
        }

        private bool QuestionExist(string name)
        {
            bool questionIsAlreadyHere =false;
            foreach (var q in _questionBasedTest.Questions)
            {
                if (q.QuestionText == name)
                {
                    questionIsAlreadyHere = true;
                }
            }
            return questionIsAlreadyHere;
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            var children = AddQuestionGrid.Children;
            TextBox child2Name = children[2] as TextBox;
            TextBox child5Answer1 = children[5] as TextBox;
            TextBox child6Answer2 = children[6] as TextBox;
            TextBox child8Weight = children[8] as TextBox;


            if (child2Name.Text != "" & child5Answer1.Text != "" & child6Answer2.Text != "" & child8Weight.Text != "")
            {
                if (child5Answer1.Text != child6Answer2.Text)
                {
                    if (QuestionExist(child2Name.Text))
                    {
                        MessageBox.Show("Упс,такой вопрос уже есть в списке, попробуйте ещё раз");
                        child2Name.Clear();
                    }
                    else
                    {
                        try
                        {
                            int weightForQ = int.Parse(child8Weight.Text);

                            if (weightForQ > 0)
                            {
                                Question question = new Question();
                                question.QuestionText = child2Name.Text;
                                question.FirstAnswer = child5Answer1.Text;
                                question.SecondAnswer = child6Answer2.Text;
                                question.Weight = weightForQ;
                                _questionBasedTest.Questions.Add(question);

                                QuestionsListBox.ItemsSource = "";
                                QuestionsListBox.ItemsSource = _questionBasedTest.Questions;

                                child2Name.Clear();
                                child5Answer1.Clear();
                                child6Answer2.Clear();
                                child8Weight.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Упс,вес вопроса введён некорректно, попробуйте ещё раз");
                                child8Weight.Clear();
                            }

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Упс,вес вопроса введён некорректно, попробуйте ещё раз");
                            child8Weight.Clear();
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Упс, ответы должны различаться,  попробуйте ещё раз");
                    child6Answer2.Clear();
                }
            }
            else
            {
                MessageBox.Show("Упс, не все поля заполнены, попробуйте снова");
            }
        }

        private void QuestionNumberTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock QuestionNumberTextBlock = sender as TextBlock;
            Question question = QuestionNumberTextBlock.DataContext as Question;

            int index = _questionBasedTest.Questions.FindIndex(a => a.QuestionText == question.QuestionText);

            QuestionNumberTextBlock.Text = "Вопрос № " + (index + 1).ToString();
        }

        private void WeightTextBlock_Initialized(object sender, EventArgs e)
        {
            TextBlock WeightTextBlock = sender as TextBlock;
            Question question = WeightTextBlock.DataContext as Question;
            WeightTextBlock.Text = "Вес:  " + question.Weight.ToString();
        }
    }
}
