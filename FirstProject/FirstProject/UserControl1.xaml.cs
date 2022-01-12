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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {

        private new readonly Grid Parent;
        private int Counter = 0;
        public UserControl1(Grid parent)
        {
            InitializeComponent();
            Parent = parent;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parent.Children.Clear();
            Parent.Children.Add(new Page2(Parent));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Text.Content = ++Counter + " Klicks!";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (DBConnection dBConnection = new DBConnection())
            {
                DBText.Content = dBConnection.Texts.FirstOrDefault().Content;

              
            }
        }
    }
}
