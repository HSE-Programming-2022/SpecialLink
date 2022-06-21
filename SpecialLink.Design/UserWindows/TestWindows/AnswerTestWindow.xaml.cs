using SpecialLink.Core;
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
    /// Логика взаимодействия для AnswerTestWindow.xaml
    /// </summary>
    public partial class AnswerTestWindow : Window
    {
        Test _test;
        User _user;
        IStorage _storage = Factory.GetInstance().Storage;
        public AnswerTestWindow()
        {
            InitializeComponent();
        }

        public AnswerTestWindow(Test test, User user)
        {
            InitializeComponent();
            _test = test;
            _user = user;
            foreach (var value in GetSource())
            {
                FirstValueComboBox.Items.Add(value);
                SecondValueComboBox.Items.Add(value);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(FirstValueComboBox.SelectedItem is null) && !(SecondValueComboBox.SelectedItem is null))
            {
                Result result = _test.GetResult(FirstValueComboBox.Text, SecondValueComboBox.Text);
                result.TestName = _test.Name;
                foreach (var person in _storage.GetPersons)
                {
                    if (person.Login == _user.Login)
                    {
                        (person as User).Results.Add(result);
                        _storage.Save();
                        _user = (person as User);
                    }
                }
                _test.AmountOfTimesTaken += 1;
                foreach (var test in _storage.GetTests)
                {
                    if ((test.Name == _test.Name) && (test.Description == _test.Description))
                    {
                        test.AmountOfTimesTaken += 1;
                        _storage.Save();
                    }
                }
                ResultsWindow resultsWindow = new ResultsWindow(result, _user);
                resultsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("К сожалению, мы не маги и не можем прочитать ваши мысли :) Для рассчета совместимости нужно выбрать два элемента.");
            }
        }

        private List<string> GetSource()
        {
            List<string> parts = new List<string>();
            foreach (var combo in (_test as AnswerBasedTest).Combinations)
            {
                if (!parts.Contains(combo.FirstValue))
                {
                    parts.Add(combo.FirstValue);
                }
                if (!parts.Contains(combo.SecondValue))
                {
                    parts.Add(combo.SecondValue);
                }
            }
            return parts;
        }
    }
}
