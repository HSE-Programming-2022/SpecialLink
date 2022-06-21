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

namespace SpecialLink.Design.UserWindows.TestWindows
{
    /// <summary>
    /// Логика взаимодействия для ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        Result _result;
        User _user;
        public ResultsWindow()
        {
            InitializeComponent();
        }
        public ResultsWindow(Result result, User user)
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

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            UserMenuWindow userMenuWindow = new UserMenuWindow(_user);
            userMenuWindow.Show();
            this.Close();
        }
    }
}
