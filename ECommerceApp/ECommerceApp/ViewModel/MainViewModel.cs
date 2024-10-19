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
using ECommerceApp.Services;
using ECommerceApp.View.Pages;

namespace ECommerceApp.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
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

        public MainViewModel(INavigationService navigationService) 
        {
            NavigationService = navigationService;
            NavigationService.NavigateTo<CatalogViewModel>();

        }
    }
}
