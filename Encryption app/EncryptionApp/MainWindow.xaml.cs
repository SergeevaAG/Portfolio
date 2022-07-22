using Microsoft.VisualBasic;
using System;
using System.Windows;

namespace EncryptionApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDecision_Click(object sender, RoutedEventArgs e)
        {
            FileInteractions.ChooseFile();
            lb.Content = FilePath;
        }

        private void btnDecipher_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FilePath))
            {
                MessageBox.Show("Файл для дешифровки не выбран.");
            }
            else
            {
                string key = Interaction.InputBox("Введите ключ (если хотите использовать ключ по умолчанию, оставьте пустым):");
                if (String.IsNullOrWhiteSpace(key))
                {
                    txtOut.Text = EncriptionInteractions.Decipher(FileInteractions.ReadFile(FilePath), "скорпион");
                }
                else
                {
                    txtOut.Text = EncriptionInteractions.Decipher(FileInteractions.ReadFile(FilePath), key);
                }
            }
        }

        private void btnCipher_Click(object sender, RoutedEventArgs e)
        {
            FileInteractions.SaveFile(EncriptionInteractions.Cipher(txtOut.Text, Interaction.InputBox("Введите ключ:")));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FileInteractions.SaveFile(txtOut.Text);
        }
    }
}
