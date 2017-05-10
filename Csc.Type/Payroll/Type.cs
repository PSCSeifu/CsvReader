using Csv.Type.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Type.Payroll
{
    public class Payroll 
    {
        public string AccountOfficeReference { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string BacsReferenceNo { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string BankSortCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNo { get; set; }
        public int CompanyWeeks { get; set; }
        public double FourWeeklyDivisor { get; set; }
        public double HourlyDivisor { get; set; }
        public int MonthlyDivisor { get; set; }
        public int PayFrequency { get; set; }
        public int PeriodsPerYear { get; set; }
        public double QuarterlyDivisor { get; set; }
        public string TaxDistrict { get; set; }
        public string TaxOfficeNo { get; set; }
        public string TaxReference { get; set; }
        public double TwoWeeklyDivisor { get; set; }
        public double WeeklyDivisor { get; set; }
        public int WebSystemType { get; set; }
        public bool AdditionalReports { get; set; }
        public bool EmailPayslips { get; set; }
        public bool EmailReports { get; set; }
        public string OutputMethod { get; set; }
        public bool P11D { get; set; }
        public bool PaidByBacs { get; set; }
        public bool PayByDirectDebit { get; set; }
        public string PayDay { get; set; }
        public bool PensionByWeb { get; set; }
        public int PostMethod { get; set; }
        public bool SecondaryBacs { get; set; }
        public bool PrintReports { get; set; }
        public bool PrintEEsPayslip { get; set; }
        public bool PrintERsPayslip { get; set; }
        public string NormalPayDay { get; set; }
        public string OrganisationName { get; set; }
        public string EmployeeNoFormat { get; set; }
        public bool PayslipERsPension1 { get; set; }
        public bool PayslipERsPension2 { get; set; }
        public bool PrintPaymentDate { get; set; }
        public bool OmniSlip { get; set; }

        public Payroll()
        {
            this.AccountOfficeReference = "";
            this.Address1 = "";
            this.Address2 = "";
            this.Address3 = "";
            this.Address4 = "";
            this.Address5 = "";
            this.BacsReferenceNo = "";
            this.BankAccountName = "";
            this.BankAccountNo = "";
            this.BankBranch = "";
            this.BankName = "";
            this.BankSortCode = "";
            this.CompanyName = "";
            this.CompanyNo = "";
            this.CompanyWeeks = 0;
            this.FourWeeklyDivisor = 0;
            this.HourlyDivisor = 0;
            this.MonthlyDivisor = 0;
            this.PayFrequency = 0;
            this.PeriodsPerYear = 0;
            this.QuarterlyDivisor = 0;
            this.TaxDistrict = "";
            this.TaxOfficeNo = "";
            this.TaxReference = "";
            this.TwoWeeklyDivisor = 0;
            this.WeeklyDivisor = 0;
            this.WebSystemType = 0;
            this.AdditionalReports = false;
            this.EmailPayslips = false;
            this.EmailReports = false;
            this.OutputMethod = "";
            this.P11D = false;
            this.PaidByBacs = false;
            this.PayByDirectDebit = false;
            this.PayDay = "";
            this.PensionByWeb = false;
            this.PostMethod = 0;
            this.SecondaryBacs = false;
            this.PrintReports = false;
            this.PrintEEsPayslip = false;
            this.PrintERsPayslip = false;
            this.NormalPayDay = "";
            this.OrganisationName = "";
            this.EmployeeNoFormat = "";
            this.PayslipERsPension1 = false;
            this.PayslipERsPension2 = false;
            this.PrintPaymentDate = false;
            this.OmniSlip = false;

        }
    }

    public class PayrollList  : Csv.Type.Common.CommonCsvList<Payroll>
    {
        //public List<Payroll> Items { get; set; } = new List<Payroll>();
        //public int Count { get { return Items.Count; }  set { Count = value; } }
        ////public string HeaderString { get; set; }

        //public string OutputPath { get; set; }

       
    }
}
