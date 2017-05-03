using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public class CsvHeader
    {
        public Type DataType { get; set; }
        public string DefaultValue { get; set; }
        public string FieldName { get; set; }
        public bool ForcedDefault { get; set; }
        public bool AllowNull { get; set; }
        public HeaderStatusType Status
        {
            get
            {
                return this.status;
            }
        }
             
        public CsvHeader()
        {
            this.FieldName = "";
            this.DefaultValue = "";
            this.DataType = typeof(string);
            this.ForcedDefault = false;
            this.AllowNull = true;
        }

        public CsvHeader(string fieldName)
        {
            this.FieldName = fieldName;
            this.DefaultValue = "";
            this.DataType = typeof(string);
            this.ForcedDefault = false;
            this.AllowNull = true;
        }

        public CsvHeader(string fieldName, Type dataType)
        {
            this.FieldName = fieldName;
            this.DefaultValue = "";
            this.DataType = dataType;
            this.ForcedDefault = false;
            this.AllowNull = true;
        }

        private HeaderStatusType status;

        public enum HeaderStatusType
        {
            NotValidated,
            Valid,
            IsNull,
            IncorrectData
        }
    }

    
}
