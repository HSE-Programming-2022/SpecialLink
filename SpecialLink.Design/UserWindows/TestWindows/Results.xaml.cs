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
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        Result _result;
        User _user;
        public Results()
        {
            InitializeComponent();
        }
        public Results(Result result, User user)
        {
            _result = result;
            _user = user;
            InitializeComponent();
        }

        private void TestNameTextBlock_Initialized(object sender, EventArgs e)
        {
            TestNameTextBlock.Text = _result.TestName;
        }

        private void ScoreTextBlock_Initialized(object sender, EventArgs e)
        {
            ScoreTextBlock.Text = "Ваша сочетаемость: " + _result.Score.ToString() + "%";
        }

        private void ExplanationTextBlock_Initialized(object sender, EventArgs e)
        {
            ExplanationTextBlock.Text = _result.Explanation;
        }
    }
}
