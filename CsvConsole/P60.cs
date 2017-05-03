using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvConsole
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

        public P60(CsvParser cp)
        {
            if (cp.CsvLine != null && cp.CsvLine.Count > 0)
            {
                Address1 = cp.CsvItem("Address1");
                Address2 = cp.CsvItem("Address2");
                Address3 = cp.CsvItem("Address3");
                Address4 = cp.CsvItem("Address4");
                Address5 = cp.CsvItem("Address5");
                ASPP = cp.CsvItem("ASPP");
                CoAddress1 = cp.CsvItem("CoAddress1");
                CoAddress2 = cp.CsvItem("CoAddress2");
                CoAddress3 = cp.CsvItem("CoAddress3");
                CoAddress4 = cp.CsvItem("CoAddress4");
                CoAddress5 = cp.CsvItem("CoAddress5");
                CompanyName = cp.CsvItem("CompanyName");
                CompanyNo = cp.CsvItem("CompanyNo");
                DateOfBirth = cp.CsvItem<DateTime>("DateOfBirth");
                Department = cp.CsvItem("Department");
                EdiCreatedDate = cp.CsvItem<DateTime>("EdiCreatedDate");
                EmployeeNo = cp.CsvItem("EmployeeNo");
                Forename1 = cp.CsvItem("Forename1");
                Forename2 = cp.CsvItem("Forename2");
                Gender = cp.CsvItem("Gender");
                Initials = cp.CsvItem("Initials");
                IsActive = cp.CsvItem("IsActive");
                IsAmended = cp.CsvItem("IsAmended");
                IsEdiCreated = cp.CsvItem("IsEdiCreated");
                IsPrinted = cp.CsvItem("IsPrinted");
                IsStudentLoan = cp.CsvItem("IsStudentLoan");
                JoinDate = cp.CsvItem<DateTime>("JoinDate");
                LeftDate = cp.CsvItem<DateTime>("LeftDate");
                NINumber = cp.CsvItem("NINumber");
                P35ASPP = cp.CsvItem<int>("P35ASPP");
                P35NIC = cp.CsvItem<double>("P35NIC");
                P35SAP = cp.CsvItem<int>("P35SAP");
                P35SMP = cp.CsvItem<double>("P35SMP");
                P35SPP = cp.CsvItem<int>("P35SPP");
                P35SSP = cp.CsvItem<double>("P35SSP");
                P35StudentLoan = cp.CsvItem<double>("P35StudentLoan");
                P35Tax = cp.CsvItem<double>("P35Tax");
                PayFrequency = cp.CsvItem<int>("PayFrequency");
                PayPrevious = cp.CsvItem<int>("PayPrevious");
                PayThis = cp.CsvItem<double>("PayThis");
                PeriodsPerYear = cp.CsvItem<int>("PeriodsPerYear");
                PrintedDate = cp.CsvItem<DateTime>("PrintedDate");
                SAP = cp.CsvItem<double>("SAP");
                Site = cp.CsvItem<double>("Site");
                SMP = cp.CsvItem<double>("SMP");
                SPP = cp.CsvItem<double>("SPP");
                SSP = cp.CsvItem<double>("SSP");
                StudentLoan = cp.CsvItem<double>("StudentLoan");
                Surname = cp.CsvItem("Surname");
                TaxBasis = cp.CsvItem("TaxBasis");
                TaxCode = cp.CsvItem("TaxCode");
                TaxDistrict = cp.CsvItem("TaxDistrict");
                TaxOfficeNo = cp.CsvItem<int>("TaxOfficeNo");
                TaxPrevious = cp.CsvItem<int>("TaxPrevious");
                TaxReference = cp.CsvItem("TaxReference");
                TaxThis = cp.CsvItem<double>("TaxThis");
                Title = cp.CsvItem("Title");
                WidowOrphan = cp.CsvItem("WidowOrphan");
                YearNumber = cp.CsvItem<int>("YearNumber");
            }
        }
    }
}
