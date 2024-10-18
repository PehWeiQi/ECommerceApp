using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ECommerceApp.MVVM;
using ECommerceApp.Dto;
using Newtonsoft.Json;
using ECommerceApp.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ECommerceApp.ViewModel
{
    internal class MainViewModel:ViewModelBase
    {
        private static readonly HttpClient client = new HttpClient();

        public MainViewModel() 
        {
            products = new ObservableCollection<Product>();
            get_data();
            IncrementQuantityCommand = new RelayCommand(IncrementQuantity);
            DecrementQuantityCommand = new RelayCommand(DecrementQuantity);
        }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        // Command to increment the quantity
        public ICommand IncrementQuantityCommand { get; }

        // Method to increment the quantity
        private void IncrementQuantity(object parameter)
        {
            if (parameter is Product product)
            {
                product.UnitQuantity += 1; // Increment the quantity
                OnPropertyChanged(nameof(Products)); // Notify UI of the change
            }
        }

        // Command to decrement the quantity
        public ICommand DecrementQuantityCommand { get; }

        // Method to decrement the quantity
        private void DecrementQuantity(object parameter)
        {
            if (parameter is Product product)
            {
                if (product.UnitQuantity > 0)
                {
                    product.UnitQuantity -= 1; // Decrement the quantity
                    OnPropertyChanged(nameof(Products)); // Notify UI of the change
                }
                
            }
        }

        private async void get_data()
        {
            string url = "https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/products";
            string username = "ck_b9e4e281dc7aa5595062207a479090a390304335";
            string password = "cs_95b5c4724a48737ed72daf8314dae9cbc83842ae";

            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                ProductsDto? productsDto = JsonConvert.DeserializeObject<ProductsDto>(responseBody);
                if (productsDto != null)
                {
                    var productsResponse = productsDto.Products;
                    System.Diagnostics.Debug.WriteLine(productsResponse.Count);
                    foreach (var product in productsResponse)
                    {
                        //System.Diagnostics.Debug.WriteLine($"ID: {product.Id}, Name: {product.Name}, SKU: {product.Sku}, Price: {product.RegularPrice}");
                        Products.Add(product);
                    }
                }

                //Products = responseBody;
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
