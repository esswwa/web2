using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;
using Web2API.Models;
using NuGet.Protocol.Plugins;

namespace WebWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Авторизация

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var apiClient = new ApiClient();
            var users = await apiClient.GetAsync<List<User>>("/api/User");

            foreach (var user in users)
            {
                if (Login.Text == user.Name && BCrypt.Net.BCrypt.Verify(Password.Text, user.Patronymic))
                {

                    MessageBox.Show("Авторизация прошла успешно");
                    Frame frame = new Frame();
                    frame.Content = new Menu(); // Замените YourPage на название вашей страницы
                    this.Content = frame;
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame frame = new Frame();
            frame.Content = new Registration(); // Замените YourPage на название вашей страницы
            this.Content = frame;
        }
    }
}
