using SpecialLink.Core.Models.People;
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

namespace SpecialLink.Design.UserWindows.ChangeWindows
{
    /// <summary>
    /// Логика взаимодействия для ChoosingChangeWindow.xaml
    /// </summary>
    public partial class ChoosingChangeWindow : Window
    {
        User _user;
        public ChoosingChangeWindow()
        {
            InitializeComponent();
        }

        public ChoosingChangeWindow(User user)
        {
            _user = user;
            InitializeComponent();
        }

        private void ChangeNameButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeNameOrLoginWindow changeNameOrLoginWindow = new ChangeNameOrLoginWindow(_user, "name");
            changeNameOrLoginWindow.Show();
            this.Close();
        }

        private void ChangeLoginButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeNameOrLoginWindow changeNameOrLoginWindow = new ChangeNameOrLoginWindow(_user, "login");
            changeNameOrLoginWindow.Show();
            this.Close();
        }

        private void ChangePictureButton_Click(object sender, RoutedEventArgs e)
        {
            PictureChangeWindow pictureChangeWindow = new PictureChangeWindow(_user);
            pictureChangeWindow.Show();
            this.Close();
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            UserMenuWindow userMenuWindow = new UserMenuWindow(_user);
            userMenuWindow.Show();
            this.Close();
        }

        private void ChangePassButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeNameOrLoginWindow changeNameOrLoginWindow = new ChangeNameOrLoginWindow(_user, "pass");
            changeNameOrLoginWindow.Show();
            this.Close();
        }
    }
}
