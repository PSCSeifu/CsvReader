using Csv.Data.Contact;
using Csv.Service.Common;
using CsvEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Service.Contact
{
    public static class ContactService
    {
        public static void ReadContactFile(string fileName)
        {
            var engine = new CsvParser(fileName, true);
            var contacts = new List<Csv.Type.Contact.Contact>();

            Console.WriteLine("**************************************");
            Console.WriteLine($"Total Row Count : {engine.CsvHeader.Count()}");
            Console.WriteLine("**************************************");

            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                Console.WriteLine($"Surname : {engine.CsvItem("Surname")}");
                Console.WriteLine($"Email : {engine.CsvItem("Email")}");
                Console.WriteLine("-----------------------------------------------");
                if (engine.CsvHeader.Count != engine.CsvLine.Count)
                {
                    Console.Write($"Error line : { engine.ErrorRow}");
                }
                else
                {
                    contacts.Add( ContactData.GetLine(engine));
                }

            }

            Console.WriteLine(contacts.Count());
            Console.WriteLine(engine.CsvItem("Surname"));
        }

        public static void ReadContactFileWithRequestedFields(string fileName, List<CsvHeader> requested)
        {
            var engine = new CsvParser(fileName, true);
            engine.SetOrderedFields(requested);

            var contacts = new List<Csv.Type.Contact.Contact>();

            Console.WriteLine("**************************************");
            Console.WriteLine($"Total Row Count : {engine.CsvHeader.Count()}");
            Console.WriteLine("**************************************");

            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                Console.WriteLine($"Surname : {engine.CsvItem("Surname")}");
                Console.WriteLine($"Email : {engine.CsvItem("Email")}");
                Console.WriteLine("-----------------------------------------------");
                if (engine.CsvHeader.Count != engine.CsvLine.Count)
                {
                    Console.Write($"Error line : { engine.ErrorRow}");
                }
                else
                {
                    contacts.Add(ContactData.GetLine(engine));
                }

            }

            Console.WriteLine(contacts.Count());
            Console.WriteLine(engine.CsvItem("Surname"));
        }

        public static void WriteCSVFile(string sourceFileName, string outputPath, string payrollNo)
        {
            /* Validate */
            var dir = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(dir)) { Directory.CreateDirectory(dir); }
            var fileName = CommonService.FileName("contacts", payrollNo);
            outputPath = Path.Combine(outputPath, fileName);
            if (File.Exists(outputPath)) { File.Delete(outputPath); }

            /* Read Source */
            var engine = new CsvParser(sourceFileName, true);
            var contactList = new Csv.Type.Common.CommonCsvList<Csv.Type.Contact.Contact>();
            while (!engine.EndOfStream)
            {
                engine.ReadLine();
                if (engine.CsvHeader.Count == engine.CsvLine.Count)
                    contactList.Items.Add(ContactData.GetLine(engine));
            }
            contactList.OutputPath = outputPath;

            /* Write CSV */
            CommonService.WriteOut<Csv.Type.Contact.Contact>(contactList);
            int count = contactList.Count;

        }
    }

    
}
