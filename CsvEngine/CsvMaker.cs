using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public static class CsvMaker
    {

        public static string CsvItem<T>(T sourceObject )
        {
            List<string> fields = new List<string>();
            GetDefaultFields<T>(ref fields);

            var data = new List<string>();
            WireUpStringList<T>(sourceObject, fields, ref data);
            return CsvLine(data);
        }

        public static string CsvItem<T>(T sourceObject, List<string> fields)
        {
            var data = new List<string>();
            WireUpStringList<T>(sourceObject, fields, ref data);
            return CsvLine(data);
        }


        public static string CsvLine(List<string> source, char separator, char quote)
            => CsvLine(source, true, true, separator, quote);        

        public static string CsvLine(List<string> source)        
            => CsvLine(source, true, true, ',', '"');
        
        public static string CsvLine(List<string> source, bool addQuotes, bool trim, char separator, char quote)
        {
            if (source == null || source.Count == 0)
                return "";

            StringBuilder line = new StringBuilder();
            int loopCount = 0;

            foreach (var item in source)
            {
                loopCount++;
                if (trim) item.Trim();

                if (source.Count() > loopCount)
                {
                    if (addQuotes)
                        line.Append(quote.ToString())
                            .Append(item)
                            .Append(quote.ToString())
                            .Append(separator);
                    else
                        line.Append(item).Append(separator);
                }
                else
                {
                    if (addQuotes)
                        line.Append(quote.ToString())
                            .Append(item)
                            .Append(quote.ToString());
                    else
                        line.Append(item);
                }
            }

            var csvLine = line.ToString();
            if (csvLine.Length > 2 && (int)csvLine[csvLine.Length - 1] == (int)separator)
                csvLine.Substring(0, csvLine.Length - 2);

            return csvLine;
        }


        public static void WireUpStringList<T>(T sourceObject, List<string> fields, ref List<string> data)
        {
            if (fields == null || fields.Count == 0 || sourceObject == null)
                return;

            List<CsvObjectMap> ledger = new List<CsvObjectMap>();
            ledger = GetObjectMap<T>(sourceObject, fields);

            if (ledger == null && ledger.Count > 0) return;          

            foreach (var item in ledger)
            {
                var value = sourceObject.GetType()
                                        .GetProperty(item.FieldName)
                                        .GetValue(sourceObject, null);

                if (item.Type.Equals(typeof(string)) || item.Type.Equals(typeof(int)) || (item.Type.Equals(typeof(DateTime))
                    || item.Type.Equals(typeof(bool)) || item.Type.Equals(typeof(short))
                    || item.Type.Equals(typeof(float)) || item.Type.Equals(typeof(double))
                    || item.Type.Equals(typeof(long))))
                    data.Add(value.ToString());

                else if (item.Type.Equals(typeof(int?)) || (item.Type.Equals(typeof(DateTime?))
                    || item.Type.Equals(typeof(bool?)) || item.Type.Equals(typeof(short?))
                    || item.Type.Equals(typeof(float?)) || item.Type.Equals(typeof(double?))
                    || item.Type.Equals(typeof(long?))))
                    data.Add(value?.ToString() ?? "");
                else
                    data.Add("");
            }                      
        }
        
        public static List<CsvObjectMap> GetObjectMap<T>(T sourceObject, List<string> fields)
        {           
            List<CsvObjectMap> objectMapList = new List<CsvObjectMap>();

            if (fields != null && fields.Count > 0)
            {
                foreach (var field in fields)
                {
                    var property = sourceObject.GetType().GetProperties()
                                                .Where(x => x.Name == field)
                                                .SingleOrDefault();

                   // var value = sourceObject.GetType().GetProperty(field).GetValue(sourceObject, null);

                    objectMapList.Add(new CsvObjectMap(field, property?.PropertyType ?? typeof(string)/*, value*/));
                }
                
            }

            return objectMapList;
        }

        public static void GetDefaultFields<T>( ref List<string> fields)
        {
            T sourceObject = Activator.CreateInstance<T>();            
            foreach (PropertyInfo property in sourceObject.GetType().GetProperties())            
                fields.Add(property?.Name ?? "");            
        }

    }
}
