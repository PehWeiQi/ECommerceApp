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
    /// Interaction logic for CartItemDetailBox.xaml
    /// </summary>
    public partial class CartItemDetailBox : UserControl
    {
        public CartItemDetailBox()
        {
            InitializeComponent();
        }

        // Title DependencyProperty
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CartItemDetailBox), new PropertyMetadata(string.Empty));

        // Txt1 DependencyProperty
        public string Txt1
        {
            get { return (string)GetValue(Txt1Property); }
            set { SetValue(Txt1Property, value); }
        }

        public static readonly DependencyProperty Txt1Property =
            DependencyProperty.Register("Txt1", typeof(string), typeof(CartItemDetailBox), new PropertyMetadata(string.Empty));

        // Txt2 DependencyProperty
        public string Txt2
        {
            get { return (string)GetValue(Txt2Property); }
            set { SetValue(Txt2Property, value); }
        }

        public static readonly DependencyProperty Txt2Property =
            DependencyProperty.Register("Txt2", typeof(string), typeof(CartItemDetailBox), new PropertyMetadata(string.Empty));
        
        // BorderColor DependencyProperty (for the Rectangle's Fill)
        public Brush BorderColor
        {
            get { return (Brush)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static readonly DependencyProperty BorderColorProperty =
            DependencyProperty.Register("BorderColor", typeof(Brush), typeof(CartItemDetailBox), new PropertyMetadata(Brushes.Blue));
    }
}
