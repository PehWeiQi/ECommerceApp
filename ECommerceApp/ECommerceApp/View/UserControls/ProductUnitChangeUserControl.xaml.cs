using ECommerceApp.ViewModel;
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
    /// Interaction logic for ProductUnitChangeUserControl.xaml
    /// </summary>
    public partial class ProductUnitChangeUserControl : UserControl
    {
        public CatalogViewModel ParentViewModel
        {
            get { return (CatalogViewModel)GetValue(ParentViewModelProperty); }
            set { SetValue(ParentViewModelProperty, value); }
        }

        public static readonly DependencyProperty ParentViewModelProperty =
            DependencyProperty.Register("ParentViewModel", typeof(CatalogViewModel), typeof(ProductUnitChangeUserControl), new PropertyMetadata(null));
        public ProductUnitChangeUserControl()
        {
            InitializeComponent();
        }
    }
}
