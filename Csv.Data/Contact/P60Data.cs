using Csv.Type.P60;
using CsvEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Data.Contact
{
    public static class P60Data
    {
        public static P60 GetLine(CsvParser cp)
        {
            var p60 = new Csv.Type.P60.P60();
            if (cp.CsvLine != null && cp.CsvLine.Count > 0)
            {
                p60.Address1 = cp.CsvItem("Address1");
                p60.Address2 = cp.CsvItem("Address2");
                p60.Address3 = cp.CsvItem("Address3");
                p60.Address4 = cp.CsvItem("Address4");
                p60.Address5 = cp.CsvItem("Address5");
                p60.ASPP = cp.CsvItem("ASPP");
                p60.CoAddress1 = cp.CsvItem("CoAddress1");
                p60.CoAddress2 = cp.CsvItem("CoAddress2");
                p60.CoAddress3 = cp.CsvItem("CoAddress3");
                p60.CoAddress4 = cp.CsvItem("CoAddress4");
                p60.CoAddress5 = cp.CsvItem("CoAddress5");
                p60.CompanyName = cp.CsvItem("CompanyName");
                p60.CompanyNo = cp.CsvItem("CompanyNo");
                p60.DateOfBirth = cp.CsvItem<DateTime>("DateOfBirth");
                p60.Department = cp.CsvItem("Department");
                p60.EdiCreatedDate = cp.CsvItem<DateTime>("EdiCreatedDate");
                p60.EmployeeNo = cp.CsvItem("EmployeeNo");
                p60.Forename1 = cp.CsvItem("Forename1");
                p60.Forename2 = cp.CsvItem("Forename2");
                p60.Gender = cp.CsvItem("Gender");
                p60.Initials = cp.CsvItem("Initials");
                p60.IsActive = cp.CsvItem("IsActive");
                p60.IsAmended = cp.CsvItem("IsAmended");
                p60.IsEdiCreated = cp.CsvItem("IsEdiCreated");
                p60.IsPrinted = cp.CsvItem("IsPrinted");
                p60.IsStudentLoan = cp.CsvItem("IsStudentLoan");
                p60.JoinDate = cp.CsvItem<DateTime>("JoinDate");
                p60.LeftDate = cp.CsvItem<DateTime>("LeftDate");
                p60.NINumber = cp.CsvItem("NINumber");
                p60.P35ASPP = cp.CsvItem<int>("P35ASPP");
                p60.P35NIC = cp.CsvItem<double>("P35NIC");
                p60.P35SAP = cp.CsvItem<int>("P35SAP");
                p60.P35SMP = cp.CsvItem<double>("P35SMP");
                p60.P35SPP = cp.CsvItem<int>("P35SPP");
                p60.P35SSP = cp.CsvItem<double>("P35SSP");
                p60.P35StudentLoan = cp.CsvItem<double>("P35StudentLoan");
                p60.P35Tax = cp.CsvItem<double>("P35Tax");
                p60.PayFrequency = cp.CsvItem<int>("PayFrequency");
                p60.PayPrevious = cp.CsvItem<int>("PayPrevious");
                p60.PayThis = cp.CsvItem<double>("PayThis");
                p60.PeriodsPerYear = cp.CsvItem<int>("PeriodsPerYear");
                p60.PrintedDate = cp.CsvItem<DateTime>("PrintedDate");
                p60.SAP = cp.CsvItem<double>("SAP");
                p60.Site = cp.CsvItem<double>("Site");
                p60.SMP = cp.CsvItem<double>("SMP");
                p60.SPP = cp.CsvItem<double>("SPP");
                p60.SSP = cp.CsvItem<double>("SSP");
                p60.StudentLoan = cp.CsvItem<double>("StudentLoan");
                p60.Surname = cp.CsvItem("Surname");
                p60.TaxBasis = cp.CsvItem("TaxBasis");
                p60.TaxCode = cp.CsvItem("TaxCode");
                p60.TaxDistrict = cp.CsvItem("TaxDistrict");
                p60.TaxOfficeNo = cp.CsvItem<int>("TaxOfficeNo");
                p60.TaxPrevious = cp.CsvItem<int>("TaxPrevious");
                p60.TaxReference = cp.CsvItem("TaxReference");
                p60.TaxThis = cp.CsvItem<double>("TaxThis");
                p60.Title = cp.CsvItem("Title");
                p60.WidowOrphan = cp.CsvItem("WidowOrphan");
                p60.YearNumber = cp.CsvItem<int>("YearNumber");
            }
            return p60;
        }
    }
}
