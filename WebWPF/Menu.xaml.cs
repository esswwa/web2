using Microsoft.CodeAnalysis.Differencing;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using NuGet.Protocol.Plugins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using Web2API.Models;

namespace WebWPF
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        List<User> users2;
        public Menu()
        {
            InitializeComponent();
            Cl();
        }
        public async void Cl() {

            var apiClient = new ApiClient();
            var users = await apiClient.GetAsync<List<User>>("/api/User");
            users2 = users.ToList();
            listView.ItemsSource = users;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in users2) { 
            
                if(item == listView.SelectedItem)
                {
                    MessageBox.Show(item.Iduser.ToString());
                }
            
            }

            HttpClient httpClient = new HttpClient();
            UserDTO us = new UserDTO { Iduser = 1, Surname = "SDFDS", Name = "FSDFSD", Patronymic = "SDFSD", Post = "Дияз", Dateb = new DateOnly(2023, 11, 12), Salary = 34232312, Adress = "sdfsdfds", Number = 0, ChildId = 0, DivisionId = 0 };
            using var response = await httpClient.PostAsJsonAsync<UserDTO>("https://localhost:80/api/User/createUser", us);

            //us.Name = "РОМА";
            //us.Id = 3;
            //using var respinse2 = await httpClient.PutAsJsonAsync<User>("https://localhost:44378/api/Users/" + us.Id.ToString(), us);

            //User user = await httpClient.DeleteFromJsonAsync<User>("https://localhost:44378/api/Users/20");
        }
    }
}
