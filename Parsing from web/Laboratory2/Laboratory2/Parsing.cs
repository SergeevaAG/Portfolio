using System;
using System.Text;
using System.Net;
using System.Windows;
using System.IO;
using ExcelLibrary.SpreadSheet;
using HtmlAgilityPack;

namespace Laboratory2
{
    public class Parsing
    {
        public static void CheckOnStart()
        {
            if(FileExist(out string pathToFile))
            {
                MainWindow.wayFile = pathToFile;
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Отсутствует файл с сохраненными сведениями. Провести первичную загрузку?", "Предупреждение", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes) 
                { 
                    Download();
                    FileExist(out string way);
                    MainWindow.wayFile = way;
                }
            }
        }
        static bool FileExist(out string filePath)
        {
            string catalog = @"C:";
            string fileName = "informationSecurityThreatsDatabank.xls";
            bool checkResult = false;
            filePath = "";
            foreach (string findedFile in Directory.EnumerateFiles(catalog, fileName, SearchOption.AllDirectories))
            {
                FileInfo fileInfo;
                fileInfo = new FileInfo(findedFile);
                checkResult = true;
                filePath = fileInfo.FullName;
            }
            return checkResult;
        }
        static void Download()
        {
            try 
            {
                MessageBox.Show("Загрузка началась. Это может занять несколько минут.");
                HtmlDocument htmlDocument = new HtmlDocument();
                string filePath = "informationSecurityThreatsDatabank.xls";
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("БД");
                worksheet.Cells[0, 0] = new Cell("Идентификатор угрозы");
                worksheet.Cells[0, 1] = new Cell("Наименование угрозы");
                worksheet.Cells[0, 2] = new Cell("Описание угрозы");
                worksheet.Cells[0, 3] = new Cell("Источник угрозы");
                worksheet.Cells[0, 4] = new Cell("Объект воздействия угрозы");
                worksheet.Cells[0, 5] = new Cell("Нарушение конфиденциальности");
                worksheet.Cells[0, 6] = new Cell("Нарушение целостности");
                worksheet.Cells[0, 7] = new Cell("Нарушение доступности");
                for (int i = 0; i < 222; i++)
                {
                    string id = "";
                    switch ((i+1).ToString().Length) 
                    {
                        case 1:
                            id = "00" + (i+1).ToString();
                            break;
                        case 2:
                            id = "0" + (i + 1).ToString();
                            break;
                        case 3:
                            id = (i + 1).ToString();
                            break;
                    }
                    string pathWeb = @"https://bdu.fstec.ru/threat/ubi." + id + "?viewtype=list";
                    htmlDocument.LoadHtml(LoadPage(pathWeb));
                    string name = htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class,'col-sm-11')]").SelectSingleNode(".//h4").InnerText;
                    string description = htmlDocument.DocumentNode.SelectSingleNode("//table[contains(@class,'table table-striped attr-view-table')]").SelectNodes(".//td")[1].InnerText;
                    string source = htmlDocument.DocumentNode.SelectSingleNode("//table[contains(@class,'table table-striped attr-view-table')]").SelectNodes(".//td")[3].InnerText;
                    string impact = htmlDocument.DocumentNode.SelectSingleNode("//table[contains(@class,'table table-striped attr-view-table')]").SelectNodes(".//td")[5].InnerText;
                    int position;
                    if (i == 141 || i == 147) { position = 0; }
                    else { position = 1; }
                    int quantity = htmlDocument.DocumentNode.SelectNodes("//ul[contains(@class,'list-unstyled')]")[position].SelectNodes(".//li").Count;
                    string str1 = "нет", str2 = "нет", str3 = "нет";
                    for (int j = 0; j < quantity; j++)
                    {
                        if (htmlDocument.DocumentNode.SelectNodes("//ul[contains(@class,'list-unstyled')]")[position].SelectNodes(".//li")[j].InnerText.Contains("Нарушение конфиденциальности")) { str1 = "да"; }
                        if (htmlDocument.DocumentNode.SelectNodes("//ul[contains(@class,'list-unstyled')]")[position].SelectNodes(".//li")[j].InnerText.Contains("Нарушение целостности")) { str2 = "да"; }
                        if (htmlDocument.DocumentNode.SelectNodes("//ul[contains(@class,'list-unstyled')]")[position].SelectNodes(".//li")[j].InnerText.Contains("Нарушение доступности")) { str3 = "да"; }
                    }
                    worksheet.Cells[i + 1, 0] = new Cell(name.Split(':')[0]);
                    worksheet.Cells[i + 1, 1] = new Cell(name.Substring(9));
                    worksheet.Cells[i + 1, 2] = new Cell(description);
                    worksheet.Cells[i + 1, 3] = new Cell(source);
                    worksheet.Cells[i + 1, 4] = new Cell(impact);
                    worksheet.Cells[i + 1, 5] = new Cell(str1);
                    worksheet.Cells[i + 1, 6] = new Cell(str2);
                    worksheet.Cells[i + 1, 7] = new Cell(str3);
                }
                workbook.Worksheets.Add(worksheet);
                workbook.Save(filePath);
                MessageBox.Show("Загрузка завершена.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        static string LoadPage(string url)
        {
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    StreamReader readStream;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    result = readStream.ReadToEnd();
                    readStream.Close();
                }
                response.Close();
            }
            return result;
        } 
    }
}
