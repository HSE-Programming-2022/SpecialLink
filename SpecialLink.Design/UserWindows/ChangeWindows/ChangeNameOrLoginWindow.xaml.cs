using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для ChangeNameOrLoginWindow.xaml
    /// </summary>
    public partial class ChangeNameOrLoginWindow : Window
    {
        User _user;
        string _type;
        IStorage _storage = Factory.GetInstance().Storage;
        public ChangeNameOrLoginWindow()
        {
            InitializeComponent();
        }

        public ChangeNameOrLoginWindow(User user, string parameter)
        {
            InitializeComponent();
            _user = user;
            _type = parameter;
            if (_type == "name")
            {
                ParameterTextBlock.Text = "Ваше имя: " + _user.Name;
                NewParameterTextBlock.Text = "Новое имя: ";
            }
            else if (_type == "login")
            {
                ParameterTextBlock.Text = "Ваш логин: " + _user.Login;
                NewParameterTextBlock.Text = "Новый логин: ";
            }
            else if (_type == "pass")
            {
                ParameterTextBlock.Text = "";
                NewParameterTextBlock.Text = "Новый пароль: ";
            }
        }

        private static int GenerateSaltForPassword()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[4];
            rng.GetNonZeroBytes(saltBytes);
            return (((int)saltBytes[0]) << 24) + (((int)saltBytes[1]) << 16) + (((int)saltBytes[2]) << 8) + ((int)saltBytes[3]);
        }

        int salt = GenerateSaltForPassword();

        private byte[] ComputePasswordHash(string password, int salt)
        {
            byte[] saltBytes = new byte[4];
            saltBytes[0] = (byte)(salt >> 24);
            saltBytes[1] = (byte)(salt >> 16);
            saltBytes[2] = (byte)(salt >> 8);
            saltBytes[3] = (byte)(salt);

            byte[] passwordBytes = UTF8Encoding.UTF8.GetBytes(password);

            byte[] preHashed = new byte[saltBytes.Length + passwordBytes.Length];
            System.Buffer.BlockCopy(passwordBytes, 0, preHashed, 0, passwordBytes.Length);
            System.Buffer.BlockCopy(saltBytes, 0, preHashed, passwordBytes.Length, saltBytes.Length);

            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(preHashed);
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            int flag = 0;
            if (_type == "name")
            {
                string name = NewParameterTextBox.Text;
                if (name.Length <= 30)
                {
                    foreach (var person in _storage.GetPersons)
                    {
                        if (person.Login == _user.Login)
                        {
                            (person as User).Name = name;
                            _storage.Save();
                            _user = person as User;
                            flag += 1;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выбранное имя слишком длинное (превышает 30 символов).");
                }
            }
            else if (_type == "login")
            {
                string login = NewParameterTextBox.Text;
                if (CheckLogin(login))
                {
                    foreach (var person in _storage.GetPersons)
                    {
                        if (person.Login == _user.Login)
                        {
                            (person as User).Login = login;
                            _storage.Save();
                            _user = person as User;
                            flag += 1;
                            break;
                        }
                    }
                }
            }
            else if (_type == "pass")
            {
                byte[] password = ComputePasswordHash(NewParameterTextBox.Text, salt);
                foreach (var person in _storage.GetPersons)
                {
                    if (person.Login == _user.Login)
                    {
                        (person as User).Password = password;
                        (person as User).Salt = salt;
                        _storage.Save();
                        _user = person as User;
                        flag += 1;
                        break;
                    }
                }
            }
            if (flag > 0)
            {
                ChoosingChangeWindow choosingChangeWindow = new ChoosingChangeWindow(_user);
                choosingChangeWindow.Show();
                this.Close();
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosingChangeWindow choosingChangeWindow = new ChoosingChangeWindow(_user);
            choosingChangeWindow.Show();
            this.Close();
        }

        private bool CheckLogin(string login)
        {
            bool flag = true;
            foreach (var person in _storage.GetPersons)
            {
                if (person.Login == login)
                {
                    flag = false;
                    MessageBox.Show("Логин уже существует в системе.");
                }
            }
            if (login == "")
            {
                flag = false;
                MessageBox.Show("Нельзя задать пустой логин.");
            }
            if (login.Substring(0,5) == "SLink")
            {
                flag = false;
                MessageBox.Show("Нельзя использовать стандартный паттерн для генерирования логинов. Пожалуйста, используйте другой тип логина.");
            }
            else if (login.Length > 30)
            {
                MessageBox.Show("Слишком длинный логин.");
            }

            return flag;

        }
    }
}
