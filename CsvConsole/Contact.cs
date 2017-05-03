using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvConsole
{
    public class Contact
    {
        public int ContactType { get; set; }
        public string Email { get; set; }
        public string Forename1 { get; set; }
        public string PhoneNo { get; set; }
        public string Surname { get; set; }
        public int Title { get; set; }
        public string MobileNo { get; set; }

        public Contact(CsvParser cp)
        {
            if (cp.CsvLine != null && cp.CsvLine.Count > 0)
            {
                ContactType = cp.CsvItem<int>("ContactType");
                Email = cp.CsvItem("Email");
                Forename1 = cp.CsvItem("Forename1");
                PhoneNo = cp.CsvItem("PhoneNo");
                Surname = cp.CsvItem("Surname");
                Title = cp.CsvItem<int>("Title");
                MobileNo = cp.CsvItem("MobileNo");
            }
        }
    }
}
