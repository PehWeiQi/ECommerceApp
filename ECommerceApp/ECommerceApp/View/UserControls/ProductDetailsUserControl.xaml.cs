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
    /// Interaction logic for ProductDetailsUserControl.xaml
    /// </summary>
    public partial class ProductDetailsUserControl : UserControl
    {
        public static readonly DependencyProperty IndexVisibilityProperty =
        DependencyProperty.Register("IndexVisibility", typeof(Visibility), typeof(ProductDetailsUserControl), new PropertyMetadata(Visibility.Visible));

        public Visibility IndexVisibility
        {
            get { return (Visibility)GetValue(IndexVisibilityProperty); }
            set { SetValue(IndexVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IndexProperty =
            DependencyProperty.Register("Index", typeof(int), typeof(ProductDetailsUserControl), new PropertyMetadata(0));

        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        public ProductDetailsUserControl()
        {
            InitializeComponent();
        }
    }
}
