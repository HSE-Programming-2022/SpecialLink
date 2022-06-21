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

namespace SpecialLink.Design
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void NewRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("SpecialLinkInfo@gmail.com", "Special Link");
                MailAddress to = new MailAddress(emailTextBox.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Данные аккаунта на Special Link";
                m.Body = $"Здравствуй, {NameTextBox.Text}!\n\nРады приветствовать тебя в нашем приложении!\nНиже указаны данные для твоего личного кабинета на Special Link:\n\tЛогин:\n\tПароль:\n\nС предсказанием для тебя,\nКоманда разработчиков Special Link <3";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("SpecialLinkInfo@gmail.com", "ljjdqjbfpesjzsoq");
                smtp.EnableSsl = true;
                smtp.Send(m);
                Console.Read();
                MessageBox.Show("Вам на почту доставлено письмо. Обязательно проверьте спам!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат почты! Пожалйста, попробуйте снова!");
            }
        }
    }
}
