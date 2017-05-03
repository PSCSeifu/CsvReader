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
        public static List<string> CsvSplit(string source, bool stripQuotes, bool trim, char separator, char quote)
        {
            if (string.IsNullOrEmpty(source)) return new List<string>();

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


