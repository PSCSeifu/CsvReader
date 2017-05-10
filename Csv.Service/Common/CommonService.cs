using CsvEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Service.Common
{
    public class CommonService
    {
        public static void WriteOut<T>(Csv.Type.Common.CommonCsvList<T> sourceObject)
        {
            //Create a Header string line - with quotes & delimiter
            var header = "";
            if (sourceObject.HeaderList.Count == 0)
            {
                CsvMaker.GetDefaultFields<T>(ref sourceObject.HeaderList);
                header = CsvMaker.CsvLine(sourceObject.HeaderList);
            }
            else
            {
                CsvMaker.CsvLine(sourceObject.HeaderList);
            }

            using (StreamWriter sw = new StreamWriter(sourceObject.OutputPath, true))
            {
                //Write header string to file
                sw.WriteLine(header);
                
                foreach (var item in sourceObject.Items)
                {                    
                    sw.WriteLine(CsvMaker.CsvItem<T>(item));
                }
                sw.WriteLine(EndofFileString());
            }
        }

        public static void WriteOut<T>(Csv.Type.Common.CommonCsvList<T> sourceObject, List<string> headerList, out int count)
        {
            count = 0;
            //Create a Header string line - with quotes & delimiter          
            if (headerList.Count == 0)
            {
                CsvMaker.GetDefaultFields<T>(ref headerList);
                sourceObject.HeaderString = CsvMaker.CsvLine(headerList);
            }
            else
            {
                sourceObject.HeaderString = CsvMaker.CsvLine(headerList);
            };
           
            using (StreamWriter sw = new StreamWriter(sourceObject.OutputPath, true))
            {
                //Write header string to file
                sw.WriteLine(sourceObject.HeaderString);

                foreach (var item in sourceObject.Items)
                {
                    //Create a csv line - with quotes and delimiter
                    sw.WriteLine(CsvMaker.CsvItem<T>(item));
                    sourceObject.Count++;
                }
                sw.WriteLine(EndofFileString());
            }
        }

        public static string FileName(string name, string payrollNo ="", string periodNo ="", string yearno ="", string payslipNo ="")
        {
            switch (name.ToLower())
            {
                case "payroll":
                    return $"Payroll{payrollNo.Trim()}.csv";
                case "contacts":
                    return $"Contacts{payrollNo.Trim()}.csv";
                case "datacodes":
                    return $"Datacodes{payrollNo.Trim()}.csv";
                case "pensions":
                    return $"Pensions{payrollNo.Trim()}.csv";
                case "sacrifice":
                    return $"Sacrifice{payrollNo.Trim()}.csv";
                default:
                    return "";
            }
            
        }

        private static string EndofFileString()
        {
            List<string> eof = new List<string> { "EOF", $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}" };
            CsvMaker.CsvLine(eof, ',', '"');
            return CsvMaker.CsvLine(eof, ',', '"'); 
        }
    }
}
