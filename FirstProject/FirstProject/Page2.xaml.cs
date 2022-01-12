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

namespace FirstProject
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    /// 

    public partial class Page2 : UserControl
    {
        private new readonly Grid Parent;

        public Page2(Grid parent)
        {
            InitializeComponent();
            Parent = parent;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parent.Children.Clear();
            Parent.Children.Add(new UserControl1(Parent));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Random r = new Random();
            Grid.Background = new SolidColorBrush(Color.FromRgb(rnd(r), rnd(r), rnd(r)));
        }

        private byte rnd(Random r)
        {
            return (byte)r.Next(1, 255);
        }
    }
}
