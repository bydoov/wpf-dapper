using Newtonsoft.Json;
using Ping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
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

namespace Ping.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:44329/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private void btnGetAllProduct_Click(object sender, RoutedEventArgs e)
        {
            this.GetAllProduct();
            UpdateLayout();
        }

        public async Task<List<Product>> ListOfProducts()
        {
            var response = await client.GetStringAsync("Product");
            var products = JsonConvert.DeserializeObject<List<Product>>(response);

            return products;
        }

        public async void GetAllProduct()
        {

            var products = await ListOfProducts();

            gdProduct.DataContext = products;
        }

        public async Task AddProduct(Product product)
        {
            var a = await client.PostAsJsonAsync("Product", product);
            if (!a.IsSuccessStatusCode)
            {
                throw new Exception("Name should be unique");
            }

            gdProduct.UpdateLayout();
        }

        private async Task UpdateProduct(Product product)
        {
            var a = await client.PutAsJsonAsync("Product/" + product.Id, product);
            if (!a.IsSuccessStatusCode)
            {
                throw new Exception("Name should be unique");
            }

            gdProduct.UpdateLayout();
        }

        private async void DeleteProduct(int id)
        {
            await client.DeleteAsync("Product/" + id);

            gdProduct.UpdateLayout();
        }


        private void btnEditProduct(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;
            txtId.Text = product.Id.ToString();
            txtName.Text = product.Name;
        }

        private void btnDeleteProduct(object sender, RoutedEventArgs e)
        {
            Product product = ((FrameworkElement)sender).DataContext as Product;
            DeleteProduct(product.Id);
            MessageBox.Show("Delete Successful");
            gdProduct.UpdateLayout();
            GetAllProduct();
        }

        private async void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = new Product()
            {
                Id = Convert.ToInt32(txtId.Text),
                Name = txtName.Text
            };

            if (product.Id == 0)
            {
                await AddProduct(product);
                MessageBox.Show("Insert Successful");
            }
            else
            {
                await UpdateProduct(product);
                MessageBox.Show("Update Successful");
            }

            txtId.Text = 0.ToString();
            txtName.Text = " ";
            GetAllProduct();
        }
    }
}
