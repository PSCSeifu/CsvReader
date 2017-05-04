using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public class CsvLib
    {

        public static List<string> CsvSplit(string source, bool stripQuotes, bool trim, char separator, char quote)
        {
            string str1 = "\"\"";
            int startIndex = -1;
            int num = -1;
            if ((int)separator == 0)
                separator = ',';
            if ((int)quote == 0)
                quote = '"';
            else
                str1 = quote.ToString() + quote.ToString();
            List<string> stringList = new List<string>();
            if (string.IsNullOrEmpty(source))
                return stringList;
            source.Trim();
            if (source.Length < 3)
                return stringList;
            for (int length = 0; length < source.Length; ++length)
            {
                if ((int)source[length] == (int)separator && num == -1)
                {
                    if (startIndex == -1)
                    {
                        stringList.Add(source.Substring(0, length));
                        startIndex = length + 1;
                    }
                    else
                    {
                        if (stripQuotes)
                        {
                            string str2 = source.Substring(startIndex, length - startIndex);
                            if (str2 == str1)
                            {
                                stringList.Add("");
                            }
                            else
                            {
                                if (str2.Length > 2 && (int)str2[0] == (int)quote)
                                {
                                    string str3 = str2;
                                    int index = str3.Length - 1;
                                    if ((int)str3[index] == (int)quote)
                                        str2 = str2.Substring(1, str2.Length - 2);
                                }
                                if (trim)
                                    str2 = str2.Trim();
                                stringList.Add(str2);
                            }
                        }
                        else if (trim)
                            stringList.Add(source.Substring(startIndex, length - startIndex).Trim());
                        else
                            stringList.Add(source.Substring(startIndex, length - startIndex));
                        startIndex = length + 1;
                    }
                }
                if ((int)source[length] == (int)quote)
                {
                    if (num == -1)
                    {
                        num = length;
                        if (startIndex == -1)
                            startIndex = length;
                    }
                    else
                        num = -1;
                }
            }
            string str4 = source.Substring(startIndex, source.Length - startIndex);
            if (stripQuotes)
            {
                if (str4 == str1)
                {
                    stringList.Add("");
                }
                else
                {
                    if (str4.Length > 2 && (int)str4[0] == (int)quote)
                    {
                        string str2 = str4;
                        int index = str2.Length - 1;
                        if ((int)str2[index] == (int)quote)
                            str4 = str4.Substring(1, str4.Length - 2);
                    }
                    if (trim)
                        str4 = str4.Trim();
                    stringList.Add(str4);
                }
            }
            else if (trim)
                stringList.Add(str4.Trim());
            else
                stringList.Add(str4);
            return stringList;
        }

        /// <summary>
        /// Returns list of string. Splits the source string using provided seprator. Defaults to empty string list.
        /// Optionally removes provided quotes and trim each split item. 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="stripQuotes"></param>
        /// <param name="trim"></param>
        /// <param name="separator"></param>
        /// <param name="quote"></param>
        /// <returns>List<string></string></returns>
        public static List<string> CsvSplit2(string source, bool stripQuotes, bool trim, char separator, char quote)
        {
            if (string.IsNullOrEmpty(source)) return new List<string>();

            source.Trim();

            if(source.Length < 3) return new List<string>();

            var list = source.Split(separator);

            if (list != null && list.Length > 0)
            {               
                foreach (var item in list)
                {
                    if (stripQuotes) { item.Replace(quote.ToString(), ""); }
                    if (trim) { item.Trim(); }
                }
                return list.ToList();
            }

            return new List<string>();
        }

        /// <summary>
        /// Gets the Encoding of the file from the specified streamReader object. Defaults to ASCII if not found.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Encoding GetFileEncoding(string filePath)
        {
            var defaultEncodingIfNoBom = Encoding.ASCII;

            if (!string.IsNullOrWhiteSpace(filePath))
            {
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(file, true))
                    {
                        reader.Peek();
                        var encoding = reader.CurrentEncoding;
                        return (encoding != null) ? encoding : defaultEncodingIfNoBom;
                    }
                }
            }

            return defaultEncodingIfNoBom;
        }

        public static Encoding GetFileEncoding2(string fileName)
        {
            byte[] buffer = new byte[4];
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                fileStream.Read(buffer, 0, 4);
            if ((int)buffer[0] == 43 && (int)buffer[1] == 47 && (int)buffer[2] == 118)
                return Encoding.UTF7;
            if ((int)buffer[0] == 239 && (int)buffer[1] == 187 && (int)buffer[2] == 191)
                return Encoding.UTF8;
            if ((int)buffer[0] == (int)byte.MaxValue && (int)buffer[1] == 254)
                return Encoding.Unicode;
            if ((int)buffer[0] == 254 && (int)buffer[1] == (int)byte.MaxValue)
                return Encoding.BigEndianUnicode;
            if ((int)buffer[0] == 0 && (int)buffer[1] == 0 && ((int)buffer[2] == 254 && (int)buffer[3] == (int)byte.MaxValue))
                return Encoding.UTF32;
            return Encoding.Default;
        }

        public static object ValueAs (Type type, string input)
        {            
            if (type == typeof(string)) return input;
            if (type == typeof(int)) return Convert.ToInt32(input);
            if (type == typeof(bool)) return Convert.ToBoolean(input);            
            if (type == typeof(Double)) return Convert.ToDouble(input);
            if (type == typeof(DateTime))
            {
                try
                {
                    return Convert.ToDateTime(input);
                }
                catch (Exception)
                {
                    DateTime.TryParse(input, out DateTime date);
                    return date;
                }
            }

            if (type == typeof(char)) return Convert.ToChar(input);
            if (type == typeof(byte)) return Convert.ToByte(input);
            if (type == typeof(Decimal)) return Convert.ToDecimal(input);
            if (type == typeof(Single)) return Convert.ToSingle(input);
            if (type == typeof(long)) return Convert.ToInt64(input);
            
            if (type == typeof(float))
            {
                try
                {
                    return float.Parse(input, CultureInfo.InvariantCulture.NumberFormat);
                }
                catch (Exception)
                {
                     var r = float.TryParse(input, out float output);
                    return (r) ? output:  0.00;
                }                
            }

            return input;
        }
    }
}


