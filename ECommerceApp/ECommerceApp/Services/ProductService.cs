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

namespace ECommerceApp.Services
{
    public class ProductService
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        private static readonly HttpClient client = new HttpClient();

        public ProductService()
        {
            // Initialize with data or fetch it from the API
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
            System.Diagnostics.Debug.WriteLine(dbProducts.Count);
            foreach (var product in dbProducts)
            {
                System.Diagnostics.Debug.WriteLine($"ID: {product.Id}, Price: {product.RegularPrice}");
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
            string url = $"https://cloud.boostorder.com/bo-mart/api/v1/wp-json/wc/v1/bo/products?page={pageNum}";
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
                        System.Diagnostics.Debug.WriteLine($"ID: {product.Id}, Price: {product.RegularPrice}");
                        product.RegularPrice = Decimal.Parse("10.00");                       
                        Products.Add(product);
                    }
                }

                IEnumerable<string> headerValues = response.Headers.GetValues("X-WC-TotalPages");
                int totalPages = Int32.Parse(headerValues.FirstOrDefault()!);
                System.Diagnostics.Debug.WriteLine(totalPages);
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
