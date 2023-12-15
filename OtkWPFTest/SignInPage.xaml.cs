using Newtonsoft.Json.Linq;
using OTKApp;
using OTKLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
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

namespace OTKApp
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        public SignInPage()
        {
            InitializeComponent();
            ProductPage productPage = new ProductPage();
            productPage.InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var data = new JsonObject();
            data["login"] = tbLogin.Text;
            data["password"] = tbPassword.Password;
            if (Validate(tbLogin.Text, tbPassword.Password))
                _ = PostResponse(data);
        }

        private async Task PostResponse(object data)
        {
            var result = await Request.PostRequest(Server.SignInURL, data);
            if (result)
            {
                NavigationService.Navigate(PageControl.GetProductPage);
            }
        }

        private Boolean Validate(string login, string password)
        {
            if (String.IsNullOrEmpty(login)) return false;
            if (String.IsNullOrEmpty(password)) return false;
            return true;
        }
    }
}
