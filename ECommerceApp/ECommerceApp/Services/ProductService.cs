using ECommerceApp.Dto;
using ECommerceApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public class ProductService
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private static readonly HttpClient client = new HttpClient();


        public ProductService()
        {
            // Initialize with data or fetch it from the API
            get_data();
        }

        public ObservableCollection<Product> GetCartItems()
        {
            var cartItems = Products.Where(p => p.UnitQuantity > 0);
            return new ObservableCollection<Product>(cartItems);
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
                        product.RegularPrice = Decimal.Parse("10.00");
                        System.Diagnostics.Debug.WriteLine($"ID: {product.Id}, Price: {product.RegularPrice}");
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
