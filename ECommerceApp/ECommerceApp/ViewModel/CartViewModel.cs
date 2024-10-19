using ECommerceApp.Model;
using ECommerceApp.MVVM;
using ECommerceApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.ViewModel
{
    public class CartViewModel: ViewModelBase
    {
        private readonly ProductService _productService;
        public CartViewModel(ProductService productService)
        {
            _productService = productService;
        }

        public ObservableCollection<Product> CartItems => _productService.GetCartItems();

    }
}
