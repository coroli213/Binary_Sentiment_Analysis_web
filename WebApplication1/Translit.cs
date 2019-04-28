using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAppAnalysis
{
    public static class Transliteration
    {
        private static Dictionary<string, string> iso = new Dictionary<string, string>();

        public static string Front(string text)
        {
            string output = text;

            output = output.Trim();

            Dictionary<string, string> tdict = iso;

            foreach (KeyValuePair<string, string> key in tdict)
            {
                output = output.Replace(key.Key, key.Value);
            }
            return output;
        }

        public static string Back (string text)
        {
            string output = text;

            Dictionary<string, string> tdict = iso;

            foreach (KeyValuePair<string, string> key in tdict)
            {
                output = output.Replace(key.Value, key.Key);
            }
            return output;
        }

        static Transliteration()
        {
            iso.Add("А", "A");
            iso.Add("Б", "B");
            iso.Add("В", "V");
            iso.Add("Г", "G");
            iso.Add("Д", "D");
            iso.Add("Е", "E");
            iso.Add("Ё", "YO");
            iso.Add("Ж", "ZH");
            iso.Add("З", "Z");
            iso.Add("И", "I");
            iso.Add("Й", "J");
            iso.Add("К", "K");
            iso.Add("Л", "L");
            iso.Add("М", "M");
            iso.Add("Н", "N");
            iso.Add("О", "O");
            iso.Add("П", "P");
            iso.Add("Р", "R");
            iso.Add("С", "S");
            iso.Add("Т", "T");
            iso.Add("У", "U");
            iso.Add("Ф", "F");
            iso.Add("Х", "X");
            iso.Add("Ц", "C");
            iso.Add("Ч", "CH");
            iso.Add("Ш", "SH");
            iso.Add("Щ", "SHH");
            iso.Add("Ъ", "'");
            iso.Add("Ы", "Y");
            iso.Add("Ь", "");
            iso.Add("Э", "E");
            iso.Add("Ю", "YU");
            iso.Add("Я", "YA");


            iso.Add("а", "a");
            iso.Add("б", "b");
            iso.Add("в", "v");
            iso.Add("г", "g");
            iso.Add("д", "d");
            iso.Add("е", "e");
            iso.Add("ё", "yo");
            iso.Add("ж", "zh");
            iso.Add("з", "z");
            iso.Add("и", "i");
            iso.Add("й", "j");
            iso.Add("к", "k");
            iso.Add("л", "l");
            iso.Add("м", "m");
            iso.Add("н", "n");
            iso.Add("о", "o");
            iso.Add("п", "p");
            iso.Add("р", "r");
            iso.Add("с", "s");
            iso.Add("т", "t");
            iso.Add("у", "u");
            iso.Add("ф", "f");
            iso.Add("х", "x");
            iso.Add("ц", "c");
            iso.Add("ч", "ch");
            iso.Add("ш", "sh");
            iso.Add("щ", "shh");
            iso.Add("ъ", "");
            iso.Add("ы", "y");
            iso.Add("ь", "");
            iso.Add("э", "e");
            iso.Add("ю", "yu");
            iso.Add("я", "ya");
        }
    }
}

