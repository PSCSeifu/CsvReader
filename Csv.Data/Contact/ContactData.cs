using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Data.Contact
{
    public static class ContactData
    {
        public static Csv.Type.Contact.Contact GetLine(CsvEngine.CsvParser cp)
        {
            var contact = new Csv.Type.Contact.Contact();

            if (cp.CsvLine != null && cp.CsvLine.Count > 0)
            {
                contact.ContactType = cp.CsvItem<int>("ContactType");
                contact.Email = cp.CsvItem("Email");
                contact.Forename1 = cp.CsvItem("Forename1");
                contact.PhoneNo = cp.CsvItem("PhoneNo");
                contact.Surname = cp.CsvItem("Surname");
                contact.Title = cp.CsvItem<int>("Title");
                contact.MobileNo = cp.CsvItem("MobileNo");
            }
            return contact;
        }
    }
}
