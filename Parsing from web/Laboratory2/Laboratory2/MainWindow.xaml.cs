using ExcelLibrary.SpreadSheet;
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

namespace Laboratory2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string wayFile = "";
        public MainWindow()
        {
            InitializeComponent();
            Parsing.CheckOnStart();
            List<MyTable> myTables = TableInteractions.FillTable();
            grid.ItemsSource = myTables;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TableInteractions.DetailedForm(sender, e);
        }
    }
}
