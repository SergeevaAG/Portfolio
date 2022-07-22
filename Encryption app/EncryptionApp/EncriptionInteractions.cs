using System;
using System.Linq;

namespace EncryptionApp
{
    public class EncriptionInteractions
    {
        public static char[] alphabet = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };
        
        //расшифровка текста
        public static string Decipher(string cipheredText, string keyword)
        {
            string key = keyword.ToLower(), cipheredTextLow = cipheredText.ToLower(), deciphedText = "";
            int positionKey = 0;
            foreach (char symbol in cipheredTextLow)
            {
                if (alphabet.Contains(symbol))
                {

                    int p = (Array.IndexOf(alphabet, symbol) + alphabet.Length - Array.IndexOf(alphabet, key[positionKey])) % alphabet.Length;
                    deciphedText += alphabet[p];
                   
                    if ((positionKey + 1) == key.Length)
                    {
                        positionKey = 0;
                    }
                    else
                    {
                        positionKey++;
                    }
                }
                else
                {
                    deciphedText += symbol;
                }
            }
            return deciphedText;
        }

        //шифрование текста
        public static string Cipher(string text, string keyword)
        {
            string key = keyword.ToLower(), textLow = text.ToLower(), cipheredText = "";
            int positionKey = 0;
            foreach (char symbol in textLow)
            {
                if (alphabet.Contains(symbol))
                {
                    int c = (Array.IndexOf(alphabet, symbol) + Array.IndexOf(alphabet, key[positionKey])) % alphabet.Length;
                    cipheredText += alphabet[c];
                    if ((positionKey + 1) == key.Length)
                    {
                        positionKey = 0;
                    }
                    else
                    {
                        positionKey++;
                    }
                }
                else
                {
                    cipheredText += symbol;
                }
            }
            return cipheredText;
        }
    }
}
