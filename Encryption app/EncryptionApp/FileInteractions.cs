using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace EncryptionApp
{
    public class FileInteractions
    {
        //выбор файла для считывания
        public static void ChooseFile()
        {
            bool check = true;
            do
            {
                OpenFileDialog dialogFilePath = new OpenFileDialog();
                if (dialogFilePath.ShowDialog() == true)
                {
                    if(dialogFilePath.FileName.Substring(dialogFilePath.FileName.LastIndexOf('.')) == ".txt")
                    {
                        MainWindow.FilePath = dialogFilePath.FileName;
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            } while (check);
        }

        //считывание файла
        public static string ReadFile(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            string fileContent = streamReader.ReadToEnd();
            return fileContent;
        }


        //создание и сохранение файла
        public static void SaveFile(string content)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(saveFileDialog.FileName, content);
                    MessageBox.Show("Зашифрованный текст сохранён.");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }
    }
}
