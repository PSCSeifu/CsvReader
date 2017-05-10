using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Csv.Test
{
    public  class CsvTestDataAttribute : DataAttribute
    {
        private readonly string _csvFileName;

        public CsvTestDataAttribute(string csvFileName)
        {
            _csvFileName = csvFileName;
        }
                        

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var filePath = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                _csvFileName);            

            return new List<object[]>();
        }

    }
}
