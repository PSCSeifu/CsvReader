using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public class CsvObjectMap
    {
        public string FieldName { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
        public CsvObjectMap()
        {

        }

        public CsvObjectMap(string fieldName, Type type, object value = null)
        {
            this.FieldName = fieldName;
            this.Type = type;
            this.Value = value;
        }
    }
}
