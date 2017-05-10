using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Csv.Test.CsvLib
{
    public class CsvLibTests
    {
        [Fact]
        public void CsvSplit_CorrectlyCommaDelimitedQuotedString_ReturnsStringList()
        {
            //Arrange
            string source = "\"AccountOfficeReference\",\"Address1\",\"Address2\",\"Address3\",\"Address4\",\"Address5\",\"BacsReferenceNo\",\"BankAccountName\",\"BankAccountNo\",\"BankBranch\",\"BankName\",\"BankSortCode\",\"CompanyName\",\"CompanyNo\",\"CompanyWeeks\",\"FourWeeklyDivisor\",\"HourlyDivisor\",\"MonthlyDivisor\",\"PayFrequency\",\"PeriodsPerYear\",\"QuarterlyDivisor\",\"TaxDistrict\",\"TaxOfficeNo\",\"TaxReference\",\"TwoWeeklyDivisor\",\"WeeklyDivisor\",\"WebSystemType\",\"AdditionalReports\",\"EmailPayslips\",\"EmailReports\",\"OutputMethod\",\"P11D\",\"PaidByBacs\",\"PayByDirectDebit\",\"PayDay\",\"PensionByWeb\",\"PostMethod\",\"SecondaryBacs\",\"PrintReports\",\"PrintEEsPayslip\",\"PrintERsPayslip\",\"NormalPayDay\",\"OrganisationName\",\"EmployeeNoFormat\",\"PayslipERsPension1\",\"PayslipERsPension2\",\"PrintPaymentDate\",\"OmniSlip\"";
           
            //Act
            //var result = CsvEngine.CsvLib.CsvSplit(source, true, true, ',', '"');

            //Assert

        }
        
    }
}
