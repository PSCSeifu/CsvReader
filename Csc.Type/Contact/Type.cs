﻿using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csc.Type.Contact
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

        public Contact()
        {

        }
    }
}
