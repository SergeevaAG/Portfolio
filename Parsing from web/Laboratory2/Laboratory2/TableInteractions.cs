using ExcelLibrary.SpreadSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Laboratory2
{
    public class TableInteractions
    {
        public static List<MyTable> FillTable()
        {
            List<MyTable> table = new List<MyTable>();
            Workbook workbook = Workbook.Load(MainWindow.wayFile);
            Worksheet worksheet = workbook.Worksheets[0];
            for (int i = 1; i < worksheet.Cells.Rows.Count; i++)
            {
                table.Add(new MyTable(worksheet.Cells[i, 0].ToString(), worksheet.Cells[i, 1].ToString()));
            }
            return table;
        }
        public static List<FullTable> FullTableFill()
        {
            List<FullTable> table = new List<FullTable>();
            Workbook workbook = Workbook.Load(MainWindow.wayFile);
            Worksheet worksheet = workbook.Worksheets[0];
            for (int i = 1; i < worksheet.Cells.Rows.Count; i++)
            {
                table.Add(new FullTable(worksheet.Cells[i, 0].ToString(), worksheet.Cells[i, 1].ToString(), worksheet.Cells[i, 2].ToString(), worksheet.Cells[i, 3].ToString(), worksheet.Cells[i, 4].ToString(), worksheet.Cells[i, 5].ToString(), worksheet.Cells[i, 6].ToString(), worksheet.Cells[i, 7].ToString()));
            }
            return table;
        }
        public static void DetailedForm(object sender, MouseButtonEventArgs e)
        {
            DetailedInfo detailedInfo = new DetailedInfo();
            detailedInfo.Show();
        }
    }
}
