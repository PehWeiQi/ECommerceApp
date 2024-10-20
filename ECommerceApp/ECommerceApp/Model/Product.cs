using ECommerceApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerceApp.Model
{
    public class Product: ObservableObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("regular_price")]
        public decimal RegularPrice { get; set; }
        [JsonPropertyName("stock_quantity")]
        public int StockQuantity { get; set; }

        private int unitQuantity;

        public int UnitQuantity
        {
            get { return unitQuantity; }
            set
            {
                if (unitQuantity != value)
                {
                    unitQuantity = value;
                    OnPropertyChanged(nameof(UnitQuantity)); 
                }
            }
        }

        private decimal total;

        public decimal Total
        {
            get { return total; }
            set
            {
                if (total != value)
                {
                    total = value;
                    OnPropertyChanged(nameof(Total));
                }
            }
        }
    }
}
