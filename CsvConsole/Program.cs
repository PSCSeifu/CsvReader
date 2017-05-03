using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadFile(@"C:\Projects\PSC\CSVFiles\Contacts.csv");
            Console.ReadLine();
        }

        public static void ReadFile(string fileName)
        {
            var engine = new CsvEngine.CsvParser(fileName, true);
            var contacts = new List<Contact>();

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
                    contacts.Add(new Contact(engine.CsvLine));
                }
               
            }

            Console.WriteLine(contacts.Count());
            
            Console.WriteLine(engine.CsvItem("Surname"));
        }
        public  class Contact
        {
            public int ContactType { get; set; }
            public string Email { get; set; }
            public string Forename1 { get; set; }
            public string PhoneNo { get; set; }
            public string Surname { get; set; }
            public int Title { get; set; }
            public string MobileNo { get; set; }

            public Contact(List<string> line)
            {
                if(line != null && line.Count > 0)
                {
                    ContactType = Convert.ToInt16(line[0]);
                    Email = line[1];
                    Forename1 = line[2];
                    PhoneNo = line[3];
                    Surname = line[4];
                    Title = Convert.ToInt16(line[5]);
                    MobileNo = line[6];
                }
            }
        }
    }
}
