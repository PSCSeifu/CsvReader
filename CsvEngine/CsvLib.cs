using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        /// <returns>List<string></returns>
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

        public static bool IsNumeric(string value)
        {
            double result;
            return !string.IsNullOrEmpty(value) && double.TryParse(value, out result);
        }
        public static void WireupCSV<T>(ref T sourceObject, List<string> fields, List<string> data)
        {
            if (fields == null || fields.Count == 0 || (data == null || data.Count == 0) || data.Count < fields.Count)
                return;
            foreach (PropertyInfo property in sourceObject.GetType().GetProperties())
            {
                if (property.CanRead && property.CanWrite)
                {
                    DisplayNameAttribute[] customAttributes = (DisplayNameAttribute[])property.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                    int index1 = -1;
                    for (int index2 = 0; index2 <= fields.Count - 1; ++index2)
                    {
                        if (fields[index2].Trim().ToLower() == property.Name.Trim().ToLower())
                            index1 = index2;
                        else if (customAttributes != null && customAttributes.Length != 0 && fields[index2].Trim().ToLower() == customAttributes[0].DisplayName.Trim().ToLower())
                            index1 = index2;
                    }
                    if (index1 > -1)
                    {
                        if (index1 < data.Count)
                        {
                            try
                            {
                                object obj = (object)null;
                                Type type = property.GetValue((object)sourceObject, (object[])null)?.GetType();
                                if ((object)type == (object)typeof(string))
                                {
                                    try
                                    {
                                        obj = (object)data[index1].Trim();
                                    }
                                    catch
                                    {
                                        obj = (object)"";
                                    }
                                }
                                else if ((object)type == (object)typeof(int) || (object)type == (object)typeof(int))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToInt32(data[index1].Trim());
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToInt32(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToInt32(0);
                                }
                                else if ((object)type == (object)typeof(short))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToInt16(data[index1].Trim());
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToInt16(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToInt16(0);
                                }
                                else if ((object)type == (object)typeof(long))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToInt64(data[index1].Trim());
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToInt64(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToInt64(0);
                                }
                                else if ((object)type == (object)typeof(float))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToSingle(data[index1]);
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToSingle(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToSingle(0);
                                }
                                else if ((object)type == (object)typeof(double))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToDouble(data[index1].Trim());
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToDouble(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToDouble(0);
                                }
                                else if ((object)type == (object)typeof(Decimal))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        if (IsNumeric(data[index1]))
                                        {
                                            try
                                            {
                                                obj = (object)Convert.ToDecimal(data[index1].Trim());
                                                goto label_62;
                                            }
                                            catch
                                            {
                                                obj = (object)Convert.ToDecimal(0);
                                                goto label_62;
                                            }
                                        }
                                    }
                                    obj = (object)Convert.ToDecimal(0);
                                }
                                else if ((object)type == (object)typeof(bool))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        try
                                        {
                                            obj = (object)Convert.ToBoolean(data[index1].Trim());
                                        }
                                        catch
                                        {
                                            obj = (object)false;
                                        }
                                    }
                                    else
                                        obj = (object)Convert.ToDouble(0);
                                }
                                else if ((object)type == (object)typeof(DateTime))
                                {
                                    if (!string.IsNullOrEmpty(data[index1]))
                                    {
                                        try
                                        {
                                            obj = (object)ToDate(data[index1]);
                                        }
                                        catch
                                        {
                                            obj = (object)new DateTime(1900, 1, 1);
                                        }
                                    }
                                    else
                                        obj = (object)new DateTime(1900, 1, 1);
                                }
                                label_62:
                                if (obj != null)
                                    property.SetValue((object)sourceObject, obj, (object[])null);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        public static DateTime ToDate(string value)
        {
            DateTime defaultValue = new DateTime(1900, 1, 1);
            return ToDate(value, defaultValue);
        }

        public static DateTime ToDate(string value, DateTime defaultValue)
        {
            return ToDate(value, defaultValue, false);
        }

        public static DateTime ToDate(string value, DateTime defaultValue, bool isUs)
        {
            int day = 0;
            int year = 0;
            int minute = 0;
            int hour = 0;
            int second = 0;
            bool flag = false;
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            value = value.ToLower();
            string[] strArray1;
            if (value.Contains("t"))
            {
                string[] strArray2 = value.Split('t');
                strArray1 = strArray2[0].Split('-');
                flag = true;
                if (strArray2.Length == 2)
                {
                    string[] strArray3 = strArray2[1].Split(':');
                    if (strArray3.Length == 3)
                    {
                        hour = Convert.ToInt32(strArray3[0]);
                        minute = Convert.ToInt32(strArray3[1]);
                        second = Convert.ToInt32(Convert.ToDecimal(strArray3[2]));
                    }
                }
            }
            else if (value.Contains(":"))
            {
                value = value.Replace("pm", "");
                value = value.Replace("am", "");
                value = value.Trim();
                string[] strArray2 = value.Split(' ');
                if (value.Contains("/"))
                    strArray1 = strArray2[0].Split('/');
                else
                    strArray1 = strArray2[0].Split('-');
                if (strArray2.Length == 2)
                {
                    string[] strArray3 = strArray2[1].Split(':');
                    if (strArray3.Length == 3)
                    {
                        hour = Convert.ToInt32(strArray3[0]);
                        minute = Convert.ToInt32(strArray3[1]);
                        second = Convert.ToInt32(Convert.ToDecimal(strArray3[2]));
                    }
                }
            }
            else if (value.Contains("/"))
                strArray1 = value.Split('/');
            else if (value.Contains("-"))
                strArray1 = value.Split('-');
            else
                strArray1 = value.Split(' ');
            if (strArray1.Length < 3 || strArray1.Length > 4)
                return defaultValue;
            int num = strArray1.Length != 4 ? 0 : 1;
            if (strArray1[0].Length == 4 && strArray1[1].Length == 2 && strArray1[2].Length == 2)
                flag = true;
            else if (strArray1[0].Length == 2 && strArray1[1].Length == 2 && strArray1[2].Length == 4)
                flag = false;
            int month;
            if (flag)
            {
                if (IsNumeric(strArray1[2 + num]))
                    day = Convert.ToInt32(strArray1[2 + num]);
                month = !IsNumeric(strArray1[1 + num]) ? GetMonthFromString(strArray1[1 + num]) : Convert.ToInt32(strArray1[1 + num]);
                if (IsNumeric(strArray1[0 + num]))
                    year = Convert.ToInt32(strArray1[0 + num]);
            }
            else if (isUs)
            {
                if (IsNumeric(strArray1[1 + num]))
                    day = Convert.ToInt32(strArray1[1 + num]);
                month = !IsNumeric(strArray1[0 + num]) ? GetMonthFromString(strArray1[1 + num]) : Convert.ToInt32(strArray1[0 + num]);
                if (IsNumeric(strArray1[2 + num]))
                    year = Convert.ToInt32(strArray1[2 + num]);
            }
            else
            {
                if (IsNumeric(strArray1[0 + num]))
                    day = Convert.ToInt32(strArray1[0 + num]);
                month = !IsNumeric(strArray1[1 + num]) ? GetMonthFromString(strArray1[1 + num]) : Convert.ToInt32(strArray1[1 + num]);
                if (IsNumeric(strArray1[2 + num]))
                    year = Convert.ToInt32(strArray1[2 + num]);
            }
            if (day > 0 && month > 0)
            {
                if (year > 0)
                {
                    try
                    {
                        return new DateTime(year, month, day, hour, minute, second);
                    }
                    catch
                    {
                        try
                        {
                            return Convert.ToDateTime(value);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return ToDateVerbose(value, defaultValue);
        }


        public static DateTime ToDateVerbose(string value, string separator = "/", DateFormat format = DateFormat.UK)
        {
            return ToDateVerbose(value, new DateTime(1900, 1, 1), separator, format);
        }

        public static DateTime ToDateVerbose(string value, DateTime defaultValue, string separator = "/", DateFormat format = DateFormat.UK)
        {
            string[] strArray1 = new string[14]
            {
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sun",
        "Mon",
        "Tue",
        "Wed",
        "Thu",
        "Fri",
        "Sat"
            };
            int day = 0;
            int year = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            string[] strArray2;
            if (value.Contains("/"))
                strArray2 = value.Split('/');
            else if (value.Contains("-"))
                strArray2 = value.Split('-');
            else if (value.Contains("|"))
                strArray2 = value.Split('|');
            else if (value.Contains(separator))
                strArray2 = value.Split(separator.ToCharArray());
            else
                strArray2 = value.Split(' ');
            if (strArray2.Length < 3)
                return defaultValue;
            if (strArray2.Length > 3)
            {
                string[] strArray3 = strArray2[4].Trim().Split(':');
                if (strArray3.Length == 3)
                {
                    try
                    {
                        hour = Convert.ToInt32(strArray3[0]);
                        minute = Convert.ToInt32(strArray3[1]);
                        second = Convert.ToInt32(strArray3[2]);
                    }
                    catch
                    {
                    }
                }
            }
            int month;
            if (format == DateFormat.UK)
            {
                int num = strArray2.Length != 4 ? 0 : 1;
                if (IsNumeric(strArray2[0 + num]))
                    day = Convert.ToInt32(strArray2[0 + num]);
                month = !IsNumeric(strArray2[1 + num]) ? GetMonthFromString(strArray2[1 + num]) : Convert.ToInt32(strArray2[1 + num]);
                if (IsNumeric(strArray2[2 + num]))
                    year = Convert.ToInt32(strArray2[2 + num]);
            }
            else if (format == DateFormat.US)
            {
                if (IsNumeric(strArray2[1]))
                    day = Convert.ToInt32(strArray2[1]);
                month = !IsNumeric(strArray2[0]) ? GetMonthFromString(strArray2[0]) : Convert.ToInt32(strArray2[0]);
                if (IsNumeric(strArray2[2]))
                    year = Convert.ToInt32(strArray2[2]);
            }
            else
            {
                if (IsNumeric(strArray2[2]))
                    day = Convert.ToInt32(strArray2[2]);
                month = !IsNumeric(strArray2[1]) ? GetMonthFromString(strArray2[1]) : Convert.ToInt32(strArray2[1]);
                if (IsNumeric(strArray2[0]))
                    year = Convert.ToInt32(strArray2[0]);
            }
            if (day > 0 && month > 0)
            {
                if (year > 0)
                {
                    try
                    {
                        return new DateTime(year, month, day, hour, minute, second);
                    }
                    catch
                    {
                    }
                }
            }
            return defaultValue;
        }

        public static DateTime ToDateVerbose(string value, DateTime defaultValue)
        {
            string[] strArray1 = new string[14]
            {
        "Sunday",
        "Monday",
        "Tuesday",
        "Wednesday",
        "Thursday",
        "Friday",
        "Saturday",
        "Sun",
        "Mon",
        "Tue",
        "Wed",
        "Thu",
        "Fri",
        "Sat"
            };
            int day = 0;
            int year = 0;
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            string[] strArray2;
            if (value.Contains("/"))
                strArray2 = value.Split('/');
            else
                strArray2 = value.Split(' ');
            if (strArray2.Length < 3 || strArray2.Length > 4)
                return defaultValue;
            int num = strArray2.Length != 4 ? 0 : 1;
            if (IsNumeric(strArray2[0 + num]))
                day = Convert.ToInt32(strArray2[0 + num]);
            int month = !IsNumeric(strArray2[1 + num]) ? GetMonthFromString(strArray2[1 + num]) : Convert.ToInt32(strArray2[1 + num]);
            if (IsNumeric(strArray2[2 + num]))
                year = Convert.ToInt32(strArray2[2 + num]);
            if (day > 0 && month > 0)
            {
                if (year > 0)
                {
                    try
                    {
                        return new DateTime(year, month, day);
                    }
                    catch
                    {
                    }
                }
            }
            return defaultValue;
        }

        public static string ToCsvString(DateTime value)
        {
            return value.Year.ToString() + ":" + (object)value.Month + ":" + (object)value.Day + ":" + (object)value.Hour + ":" + (object)value.Minute + ":" + (object)value.Second;
        }

        private static int GetMonthFromString(string value)
        {
            string[] strArray = new string[24]
            {
        "january",
        "febuary",
        "march",
        "april",
        "may",
        "june",
        "july",
        "august",
        "september",
        "october",
        "november",
        "december",
        "jan",
        "feb",
        "mar",
        "apr",
        "may",
        "jun",
        "jul",
        "aug",
        "sep",
        "oct",
        "nov",
        "dec"
            };
            if (string.IsNullOrEmpty(value))
                return 0;
            value = StripNonAlphaChars(value.ToLower());
            for (int index = 0; index <= strArray.Length - 1; ++index)
            {
                if (strArray[index].ToLower() == value)
                {
                    if (index > 11)
                        return index - 11;
                    return index + 1;
                }
            }
            return 0;
        }

        public static string StripNonAlphaChars(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            char[] charArray = value.ToCharArray();
            for (int index = 0; index <= charArray.Length - 1; ++index)
            {
                if ((int)charArray[index] > 96 && (int)charArray[index] < 122)
                    stringBuilder.Append(charArray[index]);
            }
            return stringBuilder.ToString();
        }

        public enum DateFormat
        {
            UK,
            US,
            ISO,
        }
    }
}


