using CsvEngine;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Csv.Test.CsvEngine
{
    public class CsvParserTests
    {
        #region " SETUP "
        public class MockedValues
        {         
            public string MockFileName { get; set; }
            public string MockCurrentLine { get; set; }
            public int MockFieldCount { get; set; }
            public List<String> MockCsvHeader { get; set; }
            public List<string> MockCsvLine { get; set; }
        }

        //private  string GetTestDataFolder(string testDataFolder)
        //{
        //    string startupPath = Directory.GetCurrentDirectory();//ApplicationEnvironment.ApplicationBasePath;
        //    var pathItems = startupPath.Split(Path.DirectorySeparatorChar);
        //    var pos = pathItems.Reverse().ToList().FindIndex(x => string.Equals("bin", x));
        //    string projectPath = String.Join(Path.DirectorySeparatorChar.ToString(), pathItems.Take(pathItems.Length - pos - 1));
        //    return Path.Combine(projectPath, "CsvEngineTestData", testDataFolder);
        //}

        private MockedValues GetMockedValues()
        {
            var mockFileName = @"C:\Projects\PSC\CsvReader\Csv.Test\TestData\CsvEngineTestData.csv";
            var mockCurrentLine = "";
            var mockFieldCount = 11;
            var mockCsvHeader = new List<string>() {
                "AccountOfficeReference","Address1","Address2","Address3","Address4","Address5","BacsReferenceNo","BankAccountName","BankAccountNo","BankBranch","FourWeeklyDivisor"
            };
            var mockCsvLine = new List<string> {
                "126PT00104404","5 Cambridge Technopark","Newmarket Road","Cambridge","Cambridgeshire","CB5 8PB","","","","","13.035"
            };

            return new MockedValues
            {
                MockFileName = mockFileName,
                MockCurrentLine = mockCurrentLine,
                MockFieldCount = mockFieldCount,
                MockCsvHeader = mockCsvHeader,
                MockCsvLine= mockCsvLine
            };
        }
        #endregion


       
        [Fact]
        public void SetCsvParser_WithCorrectFieldCount_ReturnsCount()
        {
            //Arrange                  
            var mockedValues = GetMockedValues();
            var sut = new CsvParser(mockedValues.MockFileName, true, true, true);

            //Act
            var result = sut.FieldCount;

            //Assert            
            result.Should().Be(mockedValues.MockFieldCount);
        }

        [Fact]
        public void SetCsvParser_WithCorrectCsvHeader_ReturnsCsvHeader()
        {
            //Arrange                  
            var mockedValues = GetMockedValues();
            var sut = new CsvParser(mockedValues.MockFileName, true, true, true);

            //Act
            var result = sut.CsvHeader;

            //Assert            
            result.Should().BeEquivalentTo(mockedValues.MockCsvHeader);
        }
        
        [Fact]
        public void SetCsvParser_WithCorrectCsvLine_ReturnsCsvLine()
        {
            //Arrange                  
            var mockedValues = GetMockedValues();
            var sut = new CsvParser(mockedValues.MockFileName, true, true, true);
            sut.ReadLine();

            //Act
            var result = sut.CsvLine;
               

            //Assert            
            result.Should().BeEquivalentTo(mockedValues.MockCsvLine);
        }

        [Fact]
        public void GetCsvItem_WithCorrectName_ReturnsItem()
        {
            //Arrange      
            var mockedValues = GetMockedValues();
            var sut = new CsvParser(mockedValues.MockFileName, true, true, true);
            sut.ReadLine();

            //Act
            var result = sut.CsvItem<string>("Address1");            

            //Assert
            result.Should().Be("5 Cambridge Technopark");
        }


        [Fact]
        public void GetCsvItem_WithDoubleType_ReturnsCorrectItem()
        {
            //Arrange      
            var mockedValues = GetMockedValues();
            var sut = new CsvParser(mockedValues.MockFileName, true, true, true);
            sut.ReadLine();

            //Act
            var result = sut.CsvItem<string>("FourWeeklyDivisor");

            //Assert
            result.Should().Be("13.035");
        }

    }
}
