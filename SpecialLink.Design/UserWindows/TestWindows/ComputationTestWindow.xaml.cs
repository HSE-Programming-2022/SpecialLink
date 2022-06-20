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
    /// Логика взаимодействия для ComputationTestWindow.xaml
    /// </summary>
    public partial class ComputationTestWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Test _test;
        User _user;
        public ComputationTestWindow()
        {
            InitializeComponent();
        }

        public ComputationTestWindow(Test test, User user)
        {
            InitializeComponent();
            _test = test;
            _user = user;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if ((FirstValueTextBox.Text.Length != 0) && (SecondValueTextBox.Text.Length != 0))
            {
                Result result = _test.GetResult(FirstValueTextBox.Text, SecondValueTextBox.Text);
                result.TestName = _test.Name;
                foreach (var person in _storage.GetPersons)
                {
                    if (person.Login == _user.Login)
                    {
                        (person as User).Results.Add(result);
                        _storage.Save();
                    }
                }
            }
            else
            {
                MessageBox.Show("К сожалению, мы не маги и не можем прочитать ваши мысли :) Для рассчета совместимости введите какие-то значения.");
            }
        }
    }
}
