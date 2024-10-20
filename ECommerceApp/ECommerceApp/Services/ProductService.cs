using ECommerceApp.Dto;
using ECommerceApp.Model;
using Newtonsoft.Json;
using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerceApp.Repos;
using DotNetEnv;

namespace ECommerceApp.Services
{
    public class ProductService
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private static readonly HttpClient client = new HttpClient();

        public ProductService()
        {
            IntializeProducts();
        }

        public ObservableCollection<Product> GetCartItems()
        {
            var cartItems = Products.Where(p => p.UnitQuantity > 0);
            return new ObservableCollection<Product>(cartItems);
        }

        public decimal GetCartTotalAmount()
        {
            var cartItems = GetCartItems();
            return cartItems.Sum(ci => ci.Total);
        }

        public int GetCartTotalCount()
        {
            var cartItems = GetCartItems();
            return cartItems.Count;
        }

        public void ClearCartItems()
        {
            foreach (var product in Products)
            {
                product.UnitQuantity = 0;
                product.Total = 0;
            }
        }

        private async void IntializeProducts()
        {
            var totalPages = await FetchProductsApi();
            if (totalPages is not null)
            {
                for (int i = 2; i <= totalPages; i++)
                {
                    await FetchProductsApi(i);
                }

                ProductRepository.InitializeDatabase();
                StoreProductsLocal();
            }
            else
            {
                FetchProductsLocal();
            }
        }

        private void FetchProductsLocal()
        {
            var dbProducts = ProductRepository.GetProductsFromDatabase();
            foreach (var product in dbProducts)
            {
                Products.Add(product);
            }
        } 

        private void StoreProductsLocal()
        {
            if (Products.Count > 0)
            {
                foreach (Product product in Products)
                {
                    ProductRepository.AddProduct(product);
                }
            }
        }

        private async Task<int?> FetchProductsApi(int pageNum = 1)
        {
            Env.Load("C:\\Users\\Owner\\source\\repos\\ECommerceApp\\ECommerceApp\\ECommerceApp\\.env");
            string url = $"https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/products?page={pageNum}";
            string username = Env.GetString("API_USERNAME");
            string password = Env.GetString("API_PASSWORD");

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

                    foreach (var product in productsResponse)
                    {
                        product.RegularPrice = Decimal.Parse("10.00");                       
                        Products.Add(product);
                    }
                }

                IEnumerable<string> headerValues = response.Headers.GetValues("X-WC-TotalPages");
                int totalPages = Int32.Parse(headerValues.FirstOrDefault()!);
                return totalPages;

            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }
    }
}
