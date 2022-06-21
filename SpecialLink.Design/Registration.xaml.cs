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

namespace SpecialLink.Design
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string _pass;
        string _log;
        IStorage _storage = Factory.GetInstance().Storage;

        public Registration()
        {
            InitializeComponent();
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
            MailAddress from = new MailAddress("SpecialLinkInfo@gmail.com", "Special Link");
            MailAddress to = new MailAddress(emailTextBox.Text);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Данные аккаунта на Special Link";
            m.Body = $"Здравствуй, {NameTextBox.Text}!\n\nРады приветствовать тебя в нашем приложении!\nНиже указаны данные для твоего личного кабинета на Special Link:\n\tЛогин: {_log}\n\tПароль: {_pass}\n\nС тестами для тебя,\nКоманда разработчиков Special Link <3";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("SpecialLinkInfo@gmail.com", "ljjdqjbfpesjzsoq");
            smtp.EnableSsl = true;
            smtp.Send(m);
            MessageBox.Show("Вам на почту доставлено письмо. Обязательно проверьте спам!");
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
            }
        }
    }
}
