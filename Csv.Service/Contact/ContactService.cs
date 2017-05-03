using Csv.Data.Contact;
using CsvEngine;
using System;
using System.Collections.Generic;
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
            var contacts = new List<Csc.Type.Contact.Contact>();

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
    }
}
