﻿using ECommerceApp.Model;
using ECommerceApp.MVVM;
using ECommerceApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECommerceApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for MenuBar.xaml
    /// </summary>
    public partial class MenuBar : UserControl
    {
        public static readonly DependencyProperty PageTitleProperty =
        DependencyProperty.Register("PageTitle", typeof(string), typeof(MenuBar), new PropertyMetadata(string.Empty));

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(MenuBar), new PropertyMetadata(null));

        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        public static readonly DependencyProperty ClearCommandProperty =
            DependencyProperty.Register("ClearCommand", typeof(ICommand), typeof(MenuBar), new PropertyMetadata(null));

        public ICommand ClearCommand
        {
            get { return (ICommand)GetValue(ClearCommandProperty); }
            set { SetValue(ClearCommandProperty, value); }
        }

        public MenuBar()
        {
            InitializeComponent();
        }

    }
}
