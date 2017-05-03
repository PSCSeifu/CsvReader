using Csv.Service.Contact;
using Csv.Service.P60;
using CsvEngine;
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
            ContactService.ReadContactFile(@"C:\Projects\PSC\CSVFiles\Contacts.csv");
            P60Service.ReadP60File(@"C:\Projects\PSC\CSVFiles\P60s-2013.csv");
            
            Console.ReadLine();
        }
      
    }
}
