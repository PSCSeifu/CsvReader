using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public class CsvHeader
    {
        public CsvHeader()
        {
            this.FieldName = "";
            this.DefaultValue = "";
            this.DataType = 1;
            this.ForcedDefault = false;
            this.AllowNull = true;
        }

        #region Public Properties

        public int DataType { get; set; }
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
        public enum HeaderStatusType
        {
            NotValidated,
            Valid,
            IsNull,
            IncorrectData
        }

        #endregion

        #region Private Properties

        private HeaderStatusType status;

        #endregion
    }
}
