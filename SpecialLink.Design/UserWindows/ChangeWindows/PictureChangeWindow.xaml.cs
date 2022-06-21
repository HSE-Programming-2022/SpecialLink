using SpecialLink.Core;
using SpecialLink.Core.Models.People;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PictureChangeWindow.xaml
    /// </summary>
    public partial class PictureChangeWindow : Window
    {
        User _user;
        IStorage _storage = Factory.GetInstance().Storage;
        public PictureChangeWindow()
        {
            InitializeComponent();
        }

        public PictureChangeWindow(User user)
        {
            InitializeComponent();
            _user = user;
            List<string> source = new List<string>()
            {
                "profile_1.jpg",
                "profile_2.jpg",
                "profile_3.jpg",
                "profile_4.jpg",
                "profile_5.jpg",
                "profile_6.jpg",
                "profile_7.jpg",
                "profile_8.jpg",
                "profile_9.jpg",
                "profile_10.jpg",
                "profile_11.jpg",
                "profile_12.jpg"
            };
            PicturesListBox.ItemsSource = source;
        }


        private void ProfileImage_Initialized(object sender, EventArgs e)
        {
            Image ProfileImage = sender as Image;
            string picture = ProfileImage.DataContext as string;
            BitmapImage bitmapImage = new BitmapImage();

            using (var fileStream = new FileStream("../../Pictures/Profiles_CMYK/" + picture, FileMode.Open))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = fileStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            ProfileImage.Source = bitmapImage;
        }

        private void PickImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button PickImageButton = sender as Button;
            string picture = PickImageButton.DataContext as string;
            foreach (var person in _storage.GetPersons)
            {
                if (person.Login == _user.Login)
                {
                    (person as User).ImageSource = picture;
                    _storage.Save();
                    _user = person as User;
                    break;
                }
            }
            ChoosingChangeWindow choosingChangeWindow = new ChoosingChangeWindow(_user);
            choosingChangeWindow.Show();
            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ChoosingChangeWindow choosingChangeWindow = new ChoosingChangeWindow(_user);
            choosingChangeWindow.Show();
            this.Close();
        }
    }
}
