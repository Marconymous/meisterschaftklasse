using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
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
    /// Interaction logic for QueryEditorPage.xaml
    /// </summary>
    public partial class QueryEditorPage : UserControl
    {


        private readonly Grid Parent;
        private readonly MainWindow Window;
        private readonly SqlConnection SqlConnection;
        private readonly Credentials Creds;
        public QueryEditorPage(Grid parent, MainWindow window, Credentials creds)
        {
            InitializeComponent();

            Parent = parent;
            Window = window;

            Creds = creds;

            SqlConnection = new SqlConnection($"Server={Creds.Server};Database={Creds.Database};User Id={Creds.User};Password={Creds.Password}");
            Connect_DB(null, null);
        }

        private void Connect_DB(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection.Open();
                displayConnectionSettings(true);
            } catch
            {
                MessageBox.Show("Couldn't connect to Database!");
            }
        }

        private void Disconnect_DB(object sender, RoutedEventArgs e)
        {
            if (SqlConnection.State.Equals(System.Data.ConnectionState.Closed))
            {
                MessageBox.Show("Connection already closed!");
                return;
            }

            SqlConnection.Close();
            MessageBox.Show("Connection closed!");
            displayConnectionSettings(false);
        }

        private void displayConnectionSettings(bool con)
        {
            ConnectionSettings.Content = $"{Creds.Server}/{Creds.Database} as {Creds.User} : " + (con ? "Connected" : "Disconnected");
        }

        private void Execute_Statement(object sender, RoutedEventArgs e)
        {
            var sql = QueryInput.Text;
            var command = SqlConnection.CreateCommand();

            command.CommandText = sql;

            var elements = new List<Dictionary<string, object>>();

            try
            {
                SqlDataReader reader;
                try
                {
                    reader = command.ExecuteReader();
                } catch (Exception ex)
                {
                    ResponseText.Text = ex.Message;
                    return;
                }
                 
                var recordsAffected = reader.RecordsAffected;

                while (reader.Read())
                {
                    var fc = reader.FieldCount;
                    var dict = new Dictionary<string, object>();

                    for (int i = 0; i < fc; i++)
                    {
                        dict.Add(reader.GetName(i), reader.GetValue(i));
                    }

                    elements.Add(dict);

                    Console.WriteLine(dict.Count);
                }

                Set_ResponseText(true, recordsAffected);

                if (elements.Count > 0)
                {
                    DataGrid.Columns.Clear();
                    DataGrid.Items.Clear();

                    foreach (var item in elements[0])
                    {
                        DataGridTextColumn textColumn = new DataGridTextColumn();
                        textColumn.Header = item.Key;
                        textColumn.Binding = new Binding(item.Key);

                        DataGrid.Columns.Add(textColumn);
                    }
                    
                    foreach (var row in elements)
                    {
                        var eo = new ExpandoObject() as IDictionary<string, Object>; ;
                        
                        foreach (var ting in row)
                        {
                            eo.Add(ting.Key, ting.Value);
                        }

                        DataGrid.Items.Add(eo);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Set_ResponseText(false, 0);
            }
        }

        private void Set_ResponseText(bool success, int affectedRows)
        {
            var response = success ? $"{DateTime.Now} -> Success; Number of Rows: {affectedRows}" : "Error";

            ResponseText.Text = response;
        }
    }
}
