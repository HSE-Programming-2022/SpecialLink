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

namespace SpecialLink.Design.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для ChangeAdminListWindow.xaml
    /// </summary>
    public partial class ChangeAdminListWindow : Window
    {
        IStorage _storage = Factory.GetInstance().Storage;
        Admin _admin;
        public ChangeAdminListWindow(Admin admin)
        {
            _admin = admin;
            InitializeComponent();
            AdminsListBox.ItemsSource = GetLoginsAdmins();
        }

        private List<string> GetLoginsAdmins()
        {
            List<string> logins = new List<string>();

            foreach (var p in _storage.GetPersons)
            {
                if (p is Admin)
                {
                    logins.Add(p.Login);
                }
            }
            return logins;
        }

        private List<string> GetLogins()
        {
            List<string> logins = new List<string>();

            foreach (var p in _storage.GetPersons)
            {
                logins.Add(p.Login);
            }
            return logins;
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

        private void AddAdminButton_Click(object sender, RoutedEventArgs e)
        {

            var children = AddAdminInfoGrid.Children;
            TextBox child2Login = children[2] as TextBox;
            PasswordBox child4Password = children[4] as PasswordBox;

            List<string> logins = GetLogins();

            if (child2Login.Text != "" & child4Password.Password != "")
            {
                if (logins.Contains(child2Login.Text))
                {
                    MessageBox.Show("Ой, такой логин уже зарегистрирован");
                    child2Login.Clear();
                }
                else
                {
                    // Маша, тут тоже хешировать пароль надо :)
                    Admin admin = new Admin();
                    admin.Login = child2Login.Text;
                    admin.Password = ComputePasswordHash(child4Password.Password, salt);
                    admin.Salt = salt;

                    _storage.GetPersons.Add(admin);
                    _storage.Save();

                    AdminsListBox.ItemsSource = "";
                    AdminsListBox.ItemsSource = GetLoginsAdmins();
                    MessageBox.Show("Админ успешно добавлен");

                    child4Password.Clear();
                    child2Login.Clear();

                }
            }
            else
            {
                MessageBox.Show("Упс, не все обязательные поля заполнены");
            }
        }

        private Person FindByLogin(string loginPerson)
        {
            Person person = null;
            foreach (var p in _storage.GetPersons)
            {
                if (p.Login == loginPerson)
                {
                    person = p;
                }
            }
            return person;
        }

        private void DeleteAdminButton_Click(object sender, RoutedEventArgs e)
        {
            int indexForDelete = AdminsListBox.SelectedIndex;

            if (indexForDelete != -1)
            {
                if (AdminsListBox.Items.Count > 1)
                {
                    Person person = FindByLogin(AdminsListBox.SelectedItem.ToString());
                    if (person is null)
                    {
                        MessageBox.Show("Невозможно удалить");
                    }
                    else
                    {
                        int index = 0;

                        for (int i = 0; i < _storage.GetPersons.Count; i++)
                        {
                            if (_storage.GetPersons[i].Login == person.Login)
                            {
                                index = i;
                            };
                        }

                        _storage.GetPersons.RemoveAt(index);
                        _storage.Save();
                        AdminsListBox.ItemsSource = "";
                        AdminsListBox.ItemsSource = GetLoginsAdmins();
                        MessageBox.Show("Админ успешно удалён");

                    }
                }
                else
                {
                    MessageBox.Show("Вы не можете удалить единственного админа");
                }
            }
            else
            {
                MessageBox.Show("Упс, никакой админ не был выбран");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMenuWindow adminMenuWindow = new AdminMenuWindow(_admin);
            adminMenuWindow.Show();
            Close();
        }
    }
}
