using CsvEngine;

namespace Csv.Data.Payroll
{
    public static class PayrollData
    {
        public static Csv.Type.Payroll.Payroll GetLine(CsvParser cp)
        {
            var payroll = new Csv.Type.Payroll.Payroll();

            if(cp.CsvLine != null && cp.CsvLine.Count > 0)
            {
                payroll.AccountOfficeReference = cp.CsvItem("AccountOfficeReference");
                payroll.Address1 = cp.CsvItem("Address1");
                payroll.Address2 = cp.CsvItem("Address2");
                payroll.Address3 = cp.CsvItem("Address3");
                payroll.Address4 = cp.CsvItem("Address4");
                payroll.Address5 = cp.CsvItem("Address5");
                payroll.BacsReferenceNo = cp.CsvItem("BacsReferenceNo");
                payroll.BankAccountName = cp.CsvItem("BankAccountName");
                payroll.BankAccountNo = cp.CsvItem("BankAccountNo");
                payroll.BankBranch = cp.CsvItem("BankBranch");
                payroll.BankName = cp.CsvItem("BankName");
                payroll.BankSortCode = cp.CsvItem("BankSortCode");
                payroll.CompanyName = cp.CsvItem("CompanyName");
                payroll.CompanyNo = cp.CsvItem("CompanyNo");
                payroll.CompanyWeeks = cp.CsvItem<int>("CompanyWeeks");
                payroll.FourWeeklyDivisor = cp.CsvItem<double>("FourWeeklyDivisor");
                payroll.HourlyDivisor = cp.CsvItem<double>("HourlyDivisor");
                payroll.MonthlyDivisor = cp.CsvItem<int>("MonthlyDivisor");
                payroll.PayFrequency = cp.CsvItem<int>("PayFrequency");
                payroll.PeriodsPerYear = cp.CsvItem<int>("PeriodsPerYear");
                payroll.QuarterlyDivisor = cp.CsvItem<double>("QuarterlyDivisor");
                payroll.TaxDistrict = cp.CsvItem("TaxDistrict");
                payroll.TaxOfficeNo = cp.CsvItem("TaxOfficeNo");
                payroll.TaxReference = cp.CsvItem("TaxReference");
                payroll.TwoWeeklyDivisor = cp.CsvItem<double>("TwoWeeklyDivisor");
                payroll.WeeklyDivisor = cp.CsvItem<double>("WeeklyDivisor");
                payroll.WebSystemType = cp.CsvItem<int>("WebSystemType");
                payroll.AdditionalReports = cp.CsvItem<bool>("AdditionalReports");
                payroll.EmailPayslips = cp.CsvItem<bool>("EmailPayslips");
                payroll.EmailReports = cp.CsvItem<bool>("EmailReports");
                payroll.OutputMethod = cp.CsvItem("OutputMethod");
                payroll.P11D = cp.CsvItem<bool>("P11D");
                payroll.PaidByBacs = cp.CsvItem<bool>("PaidByBacs");
                payroll.PayByDirectDebit = cp.CsvItem<bool>("PayByDirectDebit");
                payroll.PayDay = cp.CsvItem("PayDay");
                payroll.PensionByWeb = cp.CsvItem<bool>("PensionByWeb");
                payroll.PostMethod = cp.CsvItem<int>("PostMethod");
                payroll.SecondaryBacs = cp.CsvItem<bool>("SecondaryBacs");
                payroll.PrintReports = cp.CsvItem<bool>("PrintReports");
                payroll.PrintEEsPayslip = cp.CsvItem<bool>("PrintEEsPayslip");
                payroll.PrintERsPayslip = cp.CsvItem<bool>("PrintERsPayslip");
                payroll.NormalPayDay = cp.CsvItem("NormalPayDay");
                payroll.OrganisationName = cp.CsvItem("OrganisationName");
                payroll.EmployeeNoFormat = cp.CsvItem("EmployeeNoFormat");
                payroll.PayslipERsPension1 = cp.CsvItem<bool>("PayslipERsPension1");
                payroll.PayslipERsPension2 = cp.CsvItem<bool>("PayslipERsPension2");
                payroll.PrintPaymentDate = cp.CsvItem<bool>("PrintPaymentDate");
                payroll.OmniSlip = cp.CsvItem<bool>("OmniSlip");

            }

            return payroll;
        }
    }
}
