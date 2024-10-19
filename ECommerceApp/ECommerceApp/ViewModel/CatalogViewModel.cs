using ECommerceApp.Model;
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
    public class CatalogViewModel: ViewModelBase
    {
        private readonly ProductService _productService;
        private INavigationService _navigationService;

        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();

            }
        }

        public RelayCommand NavigationToCartCommand { get; set; }

        public CatalogViewModel(ProductService productService, INavigationService navigationService)
        {
            _productService = productService;
            NavigationService = navigationService;
            IncrementQuantityCommand = new RelayCommand(IncrementQuantity);
            DecrementQuantityCommand = new RelayCommand(DecrementQuantity);
            NavigationToCartCommand = new RelayCommand(o => { NavigationService.NavigateTo<CartViewModel>(); }, o => true);
        }

        public ObservableCollection<Product> Products => _productService.Products;

        public ICommand IncrementQuantityCommand { get; }

        // Method to increment the quantity
        private void IncrementQuantity(object parameter)
        {
            if (parameter is Product product)
            {
                product.UnitQuantity += 1; // Increment the quantity
                product.Total = product.UnitQuantity * product.RegularPrice;
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
                    product.Total = product.UnitQuantity * product.RegularPrice;
                    OnPropertyChanged(nameof(Products)); // Notify UI of the change
                }

            }
        }
    }
}
