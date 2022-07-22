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
using System.Windows.Shapes;

namespace Laboratory2
{
    /// <summary>
    /// Логика взаимодействия для DetailedInfo.xaml
    /// </summary>
    public partial class DetailedInfo : Window
    {
        public DetailedInfo()
        {
            InitializeComponent();
            FullTable pickRow = ChoosenRow();
            textId.Text = pickRow.Id;
            textName.Text = pickRow.Name;
            textDesc.Text = pickRow.Desc;
            textSource.Text = pickRow.Source;
            textObj.Text = pickRow.Obj;
            textConf.Text = pickRow.Conf;
            textInteg.Text = pickRow.Integ;
            textAccess.Text = pickRow.Access;
        }

        static FullTable ChoosenRow()
        {
            FullTable fullTable = new FullTable("", "", "", "", "", "", "", "");
            MyTable selectedRow = ((MainWindow)System.Windows.Application.Current.MainWindow).grid.SelectedItem as MyTable;
            List<FullTable> myTablesFull = TableInteractions.FullTableFill();
            foreach(FullTable table in myTablesFull)
            {
                if (table.Id == selectedRow.Id) { fullTable = table; }
            }
            return fullTable;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
