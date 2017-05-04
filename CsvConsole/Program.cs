using Csv.Service.Contact;
using Csv.Service.P60;
using Csv.Service.Payroll;
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
            //ContactService.ReadContactFile(@"C:\Projects\PSC\CSVFiles\Contacts.csv");
            //P60Service.ReadP60File(@"C:\Projects\PSC\CSVFiles\P60s-2013.csv");
            // PayrollService.ReadPayrollFile(@"C:\Projects\PSC\CSVFiles\Payroll.csv");

            //Filtered Columns
            //List<CsvHeader> requested = new List<CsvHeader>();            
            //requested.Add(new CsvHeader("MobileNo",typeof(string)));
            //requested.Add(new CsvHeader("Title",typeof(int)));
            //ContactService.ReadContactFileWithRequestedFields(@"C:\Projects\PSC\CSVFiles\Contacts.csv", requested);


            /*Write CSV*/
            PayrollService.WritePayrollCSVFile(@"C:\Projects\PSC\CSVFiles\Payroll.csv", @"C:\Projects\PSC\CsvOut2\OutPayroll.csv");

            Console.ReadLine();
        }
      
    }
}
