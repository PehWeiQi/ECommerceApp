﻿using ECommerceApp.Model;
using ECommerceApp.MVVM;
using ECommerceApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ECommerceApp.ViewModel
{
    public class CartViewModel: ViewModelBase
    {
        private readonly ProductService _productService;
        public CartViewModel(ProductService productService)
        {
            _productService = productService;
            ClearCartItemsCommand = new RelayCommand(ClearCartItems);
            RemoveCartItemCommand = new RelayCommand(RemoveCartItem);
        }
        public ObservableCollection<Product> CartItems => _productService.GetCartItems();
        public decimal CartTotalAmount => _productService.GetCartTotalAmount();
        public int CartTotalItems => _productService.GetCartTotalCount();

        public ICommand RemoveCartItemCommand { get; }

        private void RemoveCartItem(object parameter)
        {
            if (parameter is Product product)
            {
                product.UnitQuantity = 0; 
                product.Total = 0;
                OnPropertyChanged(nameof(CartItems));
                OnPropertyChanged(nameof(CartTotalAmount));
                OnPropertyChanged(nameof(CartTotalItems));
            }
        }

        public ICommand ClearCartItemsCommand { get; }

        private void ClearCartItems(object parameter)
        {
            _productService.ClearCartItems();
        }

    }
}
