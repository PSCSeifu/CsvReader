using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Type.P60
{
    public class P60
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string ASPP { get; set; }
        public string CoAddress1 { get; set; }
        public string CoAddress2 { get; set; }
        public string CoAddress3 { get; set; }
        public string CoAddress4 { get; set; }
        public string CoAddress5 { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public DateTime EdiCreatedDate { get; set; }
        public string EmployeeNo { get; set; }
        public string Forename1 { get; set; }
        public string Forename2 { get; set; }
        public string Gender { get; set; }
        public string Initials { get; set; }
        public string IsActive { get; set; }
        public string IsAmended { get; set; }
        public string IsEdiCreated { get; set; }
        public string IsPrinted { get; set; }
        public string IsStudentLoan { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LeftDate { get; set; }
        public string NINumber { get; set; }
        public int P35ASPP { get; set; }        
        public double P35NIC { get; set; }
        public int P35SAP { get; set; }
        public double P35SMP { get; set; }
        public int P35SPP { get; set; }
        public double P35SSP { get; set; }
        public double P35StudentLoan { get; set; }
        public double P35Tax { get; set; }
        public int PayFrequency { get; set; }
        public int PayPrevious { get; set; }
        public double PayThis { get; set; }
        public int PeriodsPerYear { get; set; }
        public DateTime PrintedDate { get; set; }
        public double SAP { get; set; }
        public double Site { get; set; }
        public double SMP { get; set; }
        public double SPP { get; set; }
        public double SSP { get; set; }
        public double StudentLoan { get; set; }
        public string Surname { get; set; }
        public string TaxBasis { get; set; }
        public string TaxCode { get; set; }
        public string TaxDistrict { get; set; }
        public int TaxOfficeNo { get; set; }
        public int TaxPrevious { get; set; }
        public string TaxReference { get; set; }
        public double TaxThis { get; set; }
        public string Title { get; set; }
        public string WidowOrphan { get; set; }
        public int YearNumber { get; set; }

        public P60()
        {

        }       
    }
}
