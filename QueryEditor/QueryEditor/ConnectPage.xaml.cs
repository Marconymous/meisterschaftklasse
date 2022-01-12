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

namespace QueryEditor
{
    /// <summary>
    /// Interaction logic for ConnectPage.xaml
    /// </summary>
    public partial class ConnectPage : UserControl
    {
        private new readonly Grid Parent;
        private readonly MainWindow Window;
        public ConnectPage(Grid parent, MainWindow window)
        {
            InitializeComponent();
            Window = window;
            Parent = parent;

            Window.Name = "Connect";
        }

        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ConnectToDB(object sender, RoutedEventArgs e)
        {
            var server = ServerInput.Text;
            var user = UserInput.Text;
            var password = PasswordInput.Password;
            var database = DatabaseInput.Text;

            var connectionString = $"Server={server};Database={database};User Id={user};Password={password}";

            Console.WriteLine(connectionString);

            Parent.Children.Clear();
            Parent.Children.Add(new QueryEditorPage(Parent, Window, new Credentials(server, user, password, database)));
        }
    }
}
