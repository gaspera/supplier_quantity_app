using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using System.Windows;
using System.Windows.Media;
using SupplierQuantityClientApp.Models;
using System.Net;
using SupplierQuantityClientApp.Helpers;
using System.Configuration;

namespace SupplierQuantityClientApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUsername.Text.Length == 0)
            {
                errormessage.Text = "Enter a username!";
                textBoxUsername.Focus();
            }
            else if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "Enter a password!";
                passwordBox1.Focus();
            }
            else
            {
                var customer = new Customer()
                {
                    Name = textBoxUsername.Text,
                    Password = CryptographyHelper.SimpleEncrypt(passwordBox1.Password)
                };

                var contentPost = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                try
                {
                    using var client = new HttpClient();

                    //TODO: Add URL to a settings file
                    HttpResponseMessage response =
                        await client.PostAsync($"{ConfigurationSettings.AppSettings["ServerURL"]}Customers/Login", contentPost);

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        errormessage.Text = "Invalid username or password!";
                        return;
                    }

                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    errormessage.Text = $"Error sending request to server: {ex.Message}!";
                    return;
                }

                //IF LOGIN IS OK
                var sendQuantity = new SendQuantity(textBoxUsername.Text);
                sendQuantity.Show();
                this.Close();
            }
        }
    }
}
