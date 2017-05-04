using Csv.Data.Contact;
using Csv.Data.P60;
using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Service.P60
{
    public class P60Service
    {
        public static void ReadP60File(string fileName)
        {
            var engine = new CsvParser(fileName, true);
            var p60 = new List<Csv.Type.P60.P60>();

            int count = 0;
            int errorCount = 0;

            Console.WriteLine("**************************************");
            Console.WriteLine($"Total Row Count : {engine.CsvHeader.Count()}");
            Console.WriteLine("**************************************");

            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                count++;

                Console.WriteLine($"Surname : {engine.CsvItem("Surname")}");
                Console.WriteLine($"TaxCode : {engine.CsvItem("TaxCode")}");
                Console.WriteLine($"DateOfBirth : {engine.CsvItem("DateOfBirth")}");
                Console.WriteLine($"Address1 : {engine.CsvItem("Address1")}");
                Console.WriteLine($"PeriodsPerYear : {engine.CsvItem("PeriodsPerYear")}");
                Console.WriteLine("-----------------------------------------------");

                if (engine.CsvHeader.Count != engine.CsvLine.Count)
                {
                    Console.Write($"Error line : { engine.ErrorRow}");
                    errorCount++;
                }
                else
                {
                    p60.Add(P60Data.GetLine(engine));
                }
            }

            Console.WriteLine("**************************************");
            Console.WriteLine($"Parsed Count : {count}");
            Console.WriteLine($"Error  Count : {errorCount}");
            Console.WriteLine("**************************************");

        }
    }
}
