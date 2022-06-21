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

namespace SpecialLink.Design.AdminWindows.CreateTests
{
    /// <summary>
    /// Логика взаимодействия для AddCombinationTestWindow.xaml
    /// </summary>
    public partial class AddCombinationTestWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Admin _admin;
        AnswerBasedTest _answerBasedTest;
        List<string> _elements = new List<string>();
        int numOfCorrectFillingCombinations = 0;
        public AddCombinationTestWindow(Admin admin, AnswerBasedTest answerBasedTest)
        {
            _admin = admin;
            _answerBasedTest = answerBasedTest;
            InitializeComponent();
            AddFullTestButton.IsEnabled = false;
            combinationsListBox.ItemsSource = _answerBasedTest.Combinations;
            elementsListBox.ItemsSource = _elements;
        }

        private void CreateCombinations()
        {
            List<string> secondListElements = new List<string>();
            secondListElements.AddRange(_elements);

            for (int i = 0; i < _elements.Count; i++)
            {
                for (int j = 0; j < secondListElements.Count; j++)
                {
                    _answerBasedTest.Combinations.Add(new Combination
                    {
                        FirstValue = _elements[i],
                        SecondValue = secondListElements[j]
                    });
                }
                secondListElements.RemoveAt(0);
            }
        }

        private void twoCombinationsTextBlock_Initialized_1(object sender, EventArgs e)
        {
            TextBlock twoCombinations = sender as TextBlock;
            Combination combination = twoCombinations.DataContext as Combination;
            twoCombinations.Text = combination.FirstValue + " + " + combination.SecondValue;
        }

        private void endElementsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_elements.Count > 0)
            {
                MessageBox.Show("Элементы успешно созданы, теперь заполните правую часть окна");
                AddFullTestButton.IsEnabled = true;
                AddElementButton.IsEnabled = false;
                endElementsButton.IsEnabled = false;

                CreateCombinations();

                combinationsListBox.ItemsSource = "";
                combinationsListBox.ItemsSource = _answerBasedTest.Combinations;

            }
            else
            {
                MessageBox.Show("Количество элементов для комбинаций должно быть не менее 1");
            }

        }

        private bool CheckName(string name)
        {
            // если false -> теста по комбинациям с таким названием не существует
            bool check = false;
            foreach (var t in _storage.GetTests)
            {
                if ((t.Name == name) & (t is AnswerBasedTest))
                {
                    check = true;
                }
            }
            return check;
        }

        private void AddFullTestButton_Click(object sender, RoutedEventArgs e)
        {
            if (numOfCorrectFillingCombinations == _answerBasedTest.Combinations.Count)
            {
                if (NameForTestTextBox.Text != "")
                {
                    if (CheckName(NameForTestTextBox.Text))
                    {
                        MessageBox.Show("Упс, тест 'по комбинациям' с таким названием уже создан");

                        _answerBasedTest.Name = NameForTestTextBox.Text;
                        if (descriptionTextBox.Text != "")
                        {
                            _answerBasedTest.Description = descriptionTextBox.Text;
                        }
                        _storage.GetTests.Add(_answerBasedTest);
                        _storage.Save();
                        MessageBox.Show("Тест успешно добавлен!");
                        AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
                        adminMenuWindow.Show();
                        Close();
                    }
                    else
                    {
                        _answerBasedTest.Name = NameForTestTextBox.Text;
                        if (descriptionTextBox.Text != "")
                        {
                            _answerBasedTest.Description = descriptionTextBox.Text;
                        }
                        _storage.GetTests.Add(_answerBasedTest);
                        _storage.Save();
                        MessageBox.Show("Тест успешно добавлен!");
                        AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
                        adminMenuWindow.Show();
                        Close();
                    }
                }
                else
                {
                    MessageBox.Show("Упс, вы забыли ввести название теста");
                }
                
            }
            else
            {
                MessageBox.Show("Упс, не все комбинации заполнены");
            }

        }

        private void AddElementButton_Click(object sender, RoutedEventArgs e)
        {
            if (ElementTextBox.Text != "")
            {
                if (_elements.Contains(ElementTextBox.Text))
                {
                    MessageBox.Show("Такой элемент уже есть в списке");
                    ElementTextBox.Clear();
                }
                else
                {
                    _elements.Add(ElementTextBox.Text);
                    ElementTextBox.Clear();

                    elementsListBox.ItemsSource = "";
                    elementsListBox.ItemsSource = _elements;
                }
            }
            else
            {
                MessageBox.Show("Нельзя добавить пустой элемент");
            }

        }

        private int FindIndex(string first, string second)
        {
            int index = 0;
            for (int i = 0; i < _answerBasedTest.Combinations.Count; i++)
            {
                Combination combination = _answerBasedTest.Combinations[i];
                if (combination.FirstValue == first & combination.SecondValue == second)
                {
                    index = i;
                }
            }
            return index;
        }

        private int TryParse(string num)
        {
            int number = -1;
            try
            {
                number = int.Parse(num);
            }
            catch (Exception)
            {
                number = -1;
            }

            if (number > 0 & number < 101)
            {
                return number;
            }
            else
            {
                return -1;
            }

        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Grid parent = button.Parent as Grid;

            TextBlock child0 = parent.Children[0] as TextBlock;
            var twoCombinationsList = child0.Text.Split();

            int indexOfThisCombination = FindIndex(twoCombinationsList[0], twoCombinationsList[2]);

            TextBox child3Percentage = parent.Children[3] as TextBox;
            TextBox child4Explanation = parent.Children[4] as TextBox;

            if (child3Percentage.Text != "")
            {
                if (child4Explanation.Text != "")
                {
                    int score = TryParse(child3Percentage.Text);
                    if (score != -1)
                    {
                        _answerBasedTest.Combinations[indexOfThisCombination].Score = score;
                        _answerBasedTest.Combinations[indexOfThisCombination].Explanation = child4Explanation.Text;
                        numOfCorrectFillingCombinations += 1;
                        button.IsEnabled = false;
                        MessageBox.Show("Комбинация заполнена верна, изменению не подлежит :) ");
                    }
                    else
                    {
                        MessageBox.Show("Заполните процент совместимости корректно, пожалуйста");
                        child3Percentage.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Заполните описание совместимости, пожалуйста");
                }
            }
            else
            {
                MessageBox.Show("Заполните процент совместимости, пожалуйста");
            }

        }
    }
}
