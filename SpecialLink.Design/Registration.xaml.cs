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
using System.Net.Mail;
using System.Net;
using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using System.Security.Cryptography;

namespace SpecialLink.Design
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string _pass;
        string _log;
        string _name;
        string _email;
        int _salt;
        IStorage _storage = Factory.GetInstance().Storage;

        public Registration()
        {
            InitializeComponent();
        }

        public Registration(int salt)
        {
            _salt = salt;
            InitializeComponent();
            /*byte[] p = ComputePasswordHash("admin", _salt);
            _storage.GetPersons.Add(new Admin()
            {
                Login = "admin",
                Password = p,
                LastLogin = DateTime.Now
            }
            );
            _storage.Save(); */
        }

        public void GenerateLog()
        {
            int count = 0;
            foreach (var person in _storage.GetPersons)
            {
                if (person is User)
                {
                    count++;
                }
            }
            _log = "SLink" + count;
        }

        public void GeneratePass()
        {
            string[] arr = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "X", "Z", "b", "c", "d", "f", "g", "h", "j", "k", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "z", "A", "E", "U", "Y", "a", "e", "i", "o", "u", "y" };
            Random rnd = new Random();
            for (int i = 0; i < 8; i = i + 1)
            {
                _pass = _pass + arr[rnd.Next(0, 57)];
            }
        }

        public void EmailCreator()
        {
            _email = emailTextBox.Text;
            _name = NameTextBox.Text;
            MailAddress from = new MailAddress("SpecialLinkInfo@gmail.com", "Special Link");
            MailAddress to = new MailAddress(_email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Данные аккаунта на Special Link";
            m.Body = $"Здравствуй, {_name}!\n\nРады приветствовать тебя в нашем приложении!\nНиже указаны данные для твоего личного кабинета на Special Link:\n\tЛогин: {_log}\n\tПароль: {_pass}\n\nС тестами для тебя,\nКоманда разработчиков Special Link <3";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("SpecialLinkInfo@gmail.com", "ljjdqjbfpesjzsoq");
            smtp.EnableSsl = true;
            smtp.Send(m);
            MessageBox.Show("Вам на почту доставлено письмо. Обязательно проверьте спам!");
        }

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

        public void NewUserCreation()
        {
            byte[] password = ComputePasswordHash(_pass, _salt);
            _storage.GetPersons.Add(new User
            {
                Login = _log,
                Password = password,
                Name = _name,
                Email = _email
            }
            );
            _storage.Save();
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void NewRegistration_Click(object sender, RoutedEventArgs e)
        {
            GenerateLog();
            GeneratePass();
            try
            {
                EmailCreator();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат почты! Пожалйста, попробуйте снова!");
                return;
            }
            NewUserCreation();
        }
    }
}
