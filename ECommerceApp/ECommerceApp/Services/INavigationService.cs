﻿using ECommerceApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Services
{
    public interface INavigationService
    {
        ViewModelBase CurrentView { get; }

        void NavigateTo<T>() where T :ViewModelBase;
    }
}
