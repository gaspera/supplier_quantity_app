using Newtonsoft.Json;
using SupplierQuantityClientApp.Models;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SupplierQuantityClientApp
{
    /// <summary>
    /// Interaction logic for SendQuantity.xaml
    /// </summary>
    public partial class SendQuantity : Window
    {
        private readonly string _username;

        public SendQuantity(string username)
        {
            _username = username;
            InitializeComponent();
        }

        private async void sendQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxQuantity.Text.Length == 0)
            {
                message.Text = "Enter a quantity!";
                message.Foreground = Brushes.Red;
                textBoxQuantity.Focus();
            }
            else
            {
                if (!Decimal.TryParse(textBoxQuantity.Text, out decimal quantity))
                {
                    message.Text = "Invalid quantity!";
                    message.Foreground = Brushes.Red;
                    textBoxQuantity.Focus();
                    return;
                }

                var customer = new Customer()
                {
                    Name = _username,
                    Quantity = quantity
                };

                var contentPost = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");

                try
                {
                    using var client = new HttpClient();

                    //TODO: Add URL to a settings file
                    HttpResponseMessage response =
                        await client.PostAsync($"{ConfigurationSettings.AppSettings["ServerURL"]}Customers/AddQuantity", contentPost);

                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    message.Text = $"Error sending request to server: {ex.Message}!";
                    message.Foreground = Brushes.Red;
                    return;
                }

                message.Text = "Quantity successfully updated!";
                message.Foreground = Brushes.Green;
                textBoxQuantity.Text = string.Empty;
            }



        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9|,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
