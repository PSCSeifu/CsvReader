using Csv.Data.Payroll;
using Csv.Service.Common;
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
            var payrollList = new Csv.Type.Common.CommonCsvList<Csv.Type.Payroll.Payroll>();

            while (!engine.EndOfStream)
            {
                engine.ReadLine();                

                if (engine.CsvHeader.Count == engine.CsvLine.Count)
                {
                    payrollList.Items.Add(PayrollData.GetLine(engine));
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
                }
                else if (engine.CsvLine.Count > 0 && engine.CsvLine[0].Contains("EOF"))
                {
                    payrollList.EndOfFile = true;
                    payrollList.EndOfFileTimeStamp = (engine.CsvLine.Count < 2 ) ? "" : engine.CsvLine[1]?.ToString();
                    Console.WriteLine($"End of CSV File : Create TimeStamp @ {payrollList.EndOfFileTimeStamp}");
                }
                else
                {
                    Console.WriteLine($"Error line : { engine.ErrorRow}");
                }


                var x = new Csv.Type.Payroll.Payroll();
                CsvLib.WireupCSV<Csv.Type.Payroll.Payroll>(ref x, engine.CsvHeader, engine.CsvLine);
                var y = x.Address1;
            }

            Console.WriteLine($"Total Parsed Count : {payrollList.Count()}");
            Console.WriteLine(engine.CsvItem("Address2"));

        }

        public static void WritePayrollCSVFile(string sourceFileName, string outputPath, string payrollNo)
        {
            /* Validate/Prepare Path */
            outputPath = PrepareOutputPath(payrollNo, "payroll", outputPath);

            /* Read Source */
            var payrollList = ReadFromSource(sourceFileName, outputPath);

            /* Write CSV */
            CommonService.WriteOut<Csv.Type.Payroll.Payroll>(payrollList);
            int count = payrollList.Count;
        }

        public static string PrepareOutputPath(string payrollNo, string fileNameString, string outputPath)
        {
            var dir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }

            var fileName = CommonService.FileName(fileNameString, payrollNo);
            outputPath = Path.Combine(outputPath, fileName);

            if (File.Exists(outputPath)) { File.Delete(outputPath); }

            return outputPath;
        }

        public static Csv.Type.Common.CommonCsvList<Csv.Type.Payroll.Payroll> ReadFromSource(string sourceFileName, string outputPath)
        {
            var engine = new CsvParser(sourceFileName, true);
            var payrollList = new Csv.Type.Common.CommonCsvList<Csv.Type.Payroll.Payroll>();
            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                if (engine.CsvHeader.Count == engine.CsvLine.Count)
                    payrollList.Items.Add(PayrollData.GetLine(engine));
                else if (engine.CsvLine.Count > 0 && engine.CsvLine[0].Contains("EOF"))
                    payrollList.EndOfFile = true;
                    payrollList.EndOfFileTimeStamp = (engine.CsvLine.Count < 2) ? "" : engine.CsvLine[1]?.ToString();
            }
            payrollList.OutputPath = outputPath;

            return payrollList;
        }
    }
}
