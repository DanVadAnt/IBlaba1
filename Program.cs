using System;
using CommandLine;
using Cryptor;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using System.IO;

namespace laba1
{

    public class Options
    {
        [Option('s', "shifr_type", Required = true, HelpText = "Тип шифра")]
        public string ShifrMode { get; set; }

        [Option('t', "text", Required = true, HelpText = "Открытый текст")]
        public string Text { get; set; }

        [Option('a', "alphabet", Required = true, HelpText = "алфавит")]
        public string Alphabet { get; set; }

        [Option('m', "mode", Required = true, HelpText = "mode: encrypt or decrypt")]
        public string Mode { get; set; }

        [Option('d', "delta", Required = false, HelpText = "delta")]
        public int Delta { get; set; }

        [Option('b', "table", Required = false, HelpText = "Таблица замен")]
        public string ReplacementTable { get; set; }
    }
  
    class Program
    {
        static public Dictionary<char,char> mixDict(string alphabet)
        {
            string values = String.Copy(alphabet);
            values = new string(alphabet.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray());
            Dictionary<char,char> replacementTable = alphabet.Zip(values, (s, i) => new { s, i })
                          .ToDictionary(item => item.s, item => item.i);
            File.WriteAllText("rTable.txt", replacementTable.ToString());
            return replacementTable;
        } 
        static public Dictionary<char, char> keyToValue(Dictionary<char,char> dict)
        {
            Dictionary<char, char> result = new Dictionary<char, char>();
            foreach (char key in dict.Keys)
            {;
                dict.TryGetValue(key, out char c);
                result.Add(c, key);
            }
            return result;
        }
        static public Dictionary<char, char> alphabetAndTableToDictionary(string alphabet, string replacementTable)
        {
            Dictionary<char, char> result = new Dictionary<char, char>();
            for (int i = 0; i < alphabet.Length-1; i++)
            {
                result.Add(alphabet[i], replacementTable[i]);
            }
            return result;
        }
        static void Main(string[] args)
        {
          var result = Parser.Default.ParseArguments<Options>(args)
        .WithParsed<Options>(o =>
       {
                
       });
            var options = result.Value;
            string table = options.ReplacementTable;
            string text = options.Text;
            string alphabet = options.Alphabet;
            int delta = options.Delta;
            string mode = options.Mode;
            string output = "";
            string shifrMode = options.ShifrMode;
            Dictionary<char, char> replacementTable = alphabetAndTableToDictionary(alphabet,table);
            switch (shifrMode)
            {
                case "Replacement":
                    {
                        ReplacementCryptor Cryptor = new ReplacementCryptor();
                        switch (mode)
                        {
                            case "encrypt":
                                output = Cryptor.encrypt(text, replacementTable);
                                Console.WriteLine(output);

                                break;
                            case "decrypt":
                                replacementTable = keyToValue(replacementTable);
                                output = Cryptor.decrypt(text, replacementTable);
                                Console.WriteLine(output);
                                break;
                        }
                        break;
                    }
                case "Shift":
                    {
                        ShiftCryptor Cryptor = new ShiftCryptor();
                        switch (mode)
                        {
                            case "encrypt":
                                output = Cryptor.encrypt(text, alphabet, delta);
                                Console.WriteLine(output);
                                break;
                            case "decrypt":
                                output = Cryptor.decrypt(text, alphabet, delta);
                                Console.WriteLine(output);
                                break;
                        }
                        break;
                    }
            }
            

            
        }
    }
}
