using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Cryptor
{
    /// <summary>
    /// Класс содержащий методы encrypt decrypt
    /// </summary>
    public class Crypt {
        public string CryptInfo()
        {
            string info = "Шифр замены";
            return info;
        }
    }
    public class ShiftCryptor:Crypt 
    {
        /// <summary>
        /// Шифрует текст
        /// </summary>
        /// <param name="openText">Открытый текст</param>
        /// <param name="alphabet">Алфавит</param>
        /// <param name="delta">Сдвиг</param>
        /// <returns>Возвращает зашифрованный текст</returns>
        public string encrypt(string openText,string alphabet,int delta) 
        {
            StringBuilder shifrText = new StringBuilder(openText.Length);
            for(int i = 0; i < openText.Length; i++)
            {
                shifrText.Append(alphabet[(alphabet.IndexOf(openText[i]) + delta) % alphabet.Length]);
            }
            File.WriteAllText("text.txt", shifrText.ToString());
            return shifrText.ToString();
        }
        /// <summary>
        /// Расшифровывает текст
        /// </summary>
        /// <param name="shifrText">Шифротекст</param>
        /// <param name="alphabet">Алфавит</param>
        /// <param name="delta">Сдвиг</param>
        /// <returns>Возвращает рассшифрованный текст</returns>
        public string decrypt(string shifrText, string alphabet, int delta)
        {
            StringBuilder openText = new StringBuilder(shifrText.Length);
            for (int i = 0; i < shifrText.Length; i++)
            {
                openText.Append(alphabet[(alphabet.IndexOf(shifrText[i]) - delta+alphabet.Length) % alphabet.Length]);
            }
            File.WriteAllText("text.txt", openText.ToString());
            return openText.ToString();
        }
    }
    public class ReplacementCryptor : Crypt
    {
        /// <summary>
        /// Шифрует текст
        /// </summary>
        /// <param name="openText">Открытый текст</param>
        /// <param name="alphabet">Алфавит</param>
        /// <param name="delta">Сдвиг</param>
        /// <returns>Возвращает зашифрованный текст</returns>
        public string encrypt(string openText, Dictionary<char,char> replacementTable)
        {
            StringBuilder shifrText = new StringBuilder(openText.Length);
            for (int i = 0; i < openText.Length; i++)
            {
                replacementTable.TryGetValue(openText[i], out char c);
                shifrText.Append(c);
            }
            File.WriteAllText("text.txt", shifrText.ToString());
            return shifrText.ToString();
        }
        /// <summary>
        /// Расшифровывает текст
        /// </summary>
        /// <param name="shifrText">Шифротекст</param>
        /// <param name="alphabet">Алфавит</param>
        /// <param name="delta">Сдвиг</param>
        /// <returns>Возвращает рассшифрованный текст</returns>
        public string decrypt(string shifrText, Dictionary<char,char> replacementTable)
        {
            StringBuilder openText = new StringBuilder(shifrText.Length);
            for (int i = 0; i < shifrText.Length; i++)
            {

                replacementTable.TryGetValue(shifrText[i], out char c);
                openText.Append(c);
            }
            File.WriteAllText("text.txt", openText.ToString());
            return openText.ToString();
        }
        
    }
}
