using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ECommerceApp.Model;
using Newtonsoft.Json;

namespace ECommerceApp.Dto
{
    internal class ProductsDto
    {
        [JsonProperty("products")]
        public List<Product> Products;
    }
}
