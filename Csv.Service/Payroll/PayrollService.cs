using Csv.Data.Payroll;
using CsvEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Service.Payroll
{
    public static class PayrollService
    {
        public static void ReadPayrollFile(string fileName)
        {
            var engine = new CsvParser(fileName, true);
            var payrollList = new List<Csv.Type.Payroll.Payroll>();

            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                Console.WriteLine($"AccountOfficeReference  : {engine.CsvItem("AccountOfficeReference")}");
                Console.WriteLine($"Address1  : {engine.CsvItem("Address1")}");
                Console.WriteLine($"CompanyName   : {engine.CsvItem("CompanyName")}");
                Console.WriteLine($"FourWeeklyDivisor   : {engine.CsvItem("FourWeeklyDivisor")}");
                Console.WriteLine($"TaxDistrict   : {engine.CsvItem("TaxDistrict")}");
                Console.WriteLine($"NormalPayDay   : {engine.CsvItem("NormalPayDay")}");
                Console.WriteLine($"EmployeeNoFormat   : {engine.CsvItem("EmployeeNoFormat")}");
                Console.WriteLine($"OmniSlip  : {engine.CsvItem("OmniSlip")}");
                Console.WriteLine($"PrintPaymentDate  : {engine.CsvItem("PrintPaymentDate")}");

                Console.WriteLine("-----------------------------------------------");
                if (engine.CsvHeader.Count != engine.CsvLine.Count)
                {
                    Console.Write($"Error line : { engine.ErrorRow}");
                }
                else
                {
                    payrollList.Add(PayrollData.GetLine(engine));
                }

                var x = new Csv.Type.Payroll.Payroll();
                CsvLib.WireupCSV<Csv.Type.Payroll.Payroll>(ref x, engine.CsvHeader, engine.CsvLine);
                var y = x.Address1;
            }

            Console.WriteLine($"Total Parsed Count : {payrollList.Count()}");
            Console.WriteLine(engine.CsvItem("Address2"));

        }

        public static void WritePayrollCSVFile(string sourceFileName, string outputPath)
        {
            //Validate output path directory exists
            var dir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

            //Delete file if exists
            if (File.Exists(outputPath)) { File.Delete(outputPath); }

            /* Read Source */
            var engine = new CsvParser(sourceFileName, true);
            var payrollList = new List<Csv.Type.Payroll.Payroll>();
            while (!engine.EndOfStream)
            {
                engine.ReadLine();               
                if (engine.CsvHeader.Count == engine.CsvLine.Count) 
                    payrollList.Add(PayrollData.GetLine(engine));
            }

            /* Write CSV */
            if(payrollList !=null && payrollList.Count > 0)
            {
                //Get List<String> of Headers from class
                List<string> headerList =new List<string>();
                CsvMaker.GetDefaultFields<Csv.Type.Payroll.Payroll>(ref headerList);

                //Create a Header string line - with quotes & delimiter
                var header = CsvMaker.CsvLine(headerList);

               
                using (StreamWriter sw = new StreamWriter(outputPath, true))
                {
                    //Write header string to file
                    sw.WriteLine(header);

                    foreach (var payroll in payrollList)
                    {
                        //Create a csv line - with quotes and delimiter
                       sw.WriteLine(CsvMaker.CsvItem<Csv.Type.Payroll.Payroll>(payroll));
                    }
                }
            }
        }
    }
}
