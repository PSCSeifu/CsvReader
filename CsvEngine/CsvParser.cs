using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvEngine
{
    public class CsvParser
    {

        #region Public Properties

        public List<string> CsvHeader
        {
            get
            {
                return (this.fields == null) ? new List<string>() : this.fields;
            }
        }

        public List<string> CsvLine
        {
            get
            {
                return (this.items == null) ? new List<string>() : this.items;
            }
        }

        public List<string> CsvOrderedLine
        {
            get
            {
                return (this.orderedItems == null) ? new List<string>() : this.orderedItems;
            }
        }

        public CsvOrderedLineStatustype CsvOrderedLineStatus
        {
            get
            {
                return this.parserLineStatus;
            }
        }
        public string CurrentLine { get; set; }

        public bool EndOfStream
        {
            get
            {
                return this.sr == null || this.sr.EndOfStream;
            }
        }

        public string ErrorMessage { get; set; }

        public string ErrorRow
        {
            get
            {
                return this.errorRow;
            }
        }

        public int FieldCount { get; set; }

        public Encoding FileEncoding
        {
            get
            {
                return this.encoding;
            }
        }
        public int LineNumber
        {
            get
            {
                return this.lineNumber;
            }
        }
        public char Quote { get; set; }
        public int RowIndex { get; set; }
        public string RowTerminator { get; set; }
        public char Separator { get; set; }
        public bool StripQuotes { get; set; }
        public bool Trim { get; set; }
        public enum CsvOrderedLineStatustype
        {
            Valid,
            Error
        }
        #endregion

        #region Public Methods

        public CsvParser(string fileName, bool hasHeader)
        {
            this.StripQuotes = true;
            this.Trim = true;
            this.Separator = char.MinValue;
            this.Quote = '"';
            this.FieldCount = 0;
            this.fields = new List<string>();
            this.SetupCsvParser(fileName, hasHeader);
        }
        public CsvParser(string fileName, List<string> fields)
        {
            this.StripQuotes = true;
            this.Trim = true;
            this.Separator = char.MinValue;
            this.Quote = '"';
            this.FieldCount = 0;
            this.fields = fields;
            this.SetupCsvParser(fileName, false);
        }
        public CsvParser(string fileName, bool hasHeader, bool stripQuotes, bool trim)
        {
            this.StripQuotes = stripQuotes;
            this.Trim = trim;
            this.Separator = char.MinValue;
            this.Quote = '"';
            this.FieldCount = 0;
            this.fields = new List<string>();
            this.SetupCsvParser(fileName, hasHeader);
        }
        public CsvParser(string fileName, bool hasHeader, bool stripQuotes, bool trim, char separator, char quote)
        {
            this.RowTerminator = "\r\n";
            this.StripQuotes = stripQuotes;
            this.Trim = trim;
            this.Separator = separator;
            this.Quote = quote;
            this.FieldCount = 0;
            this.fields = new List<string>();
            this.SetupCsvParser(fileName, hasHeader);
        }

        public string CsvItem(int index) => (this.items == null || index <= -1 || index >= this.items.Count) ? "" : this.items[index];
        public string CsvItem(string name)
        {
            if (this.items == null || this.fields == null || (this.fields.Count == 0 || this.fields.Count != this.items.Count))
                return "";
            for (int index = 0; index < this.fields.Count; ++index)
            {
                if (this.fields[index].Trim().ToLower() == name.Trim().ToLower())
                    return this.items[index];
            }
            return "";
        }
        public T CsvItem<T>(int index) => this.CsvItem<T>(index, default(T));        
        public T CsvItem<T>(int index, T defaultValue)
        {
            string str = this.CsvItem(index);          
            try
            {
                if (string.IsNullOrEmpty(str)) return defaultValue;
                Type type = typeof(T);
                try
                {
                    return (T) CsvLib.ValueAs(type, str);
                }
                catch
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public T CsvItem<T>(string name) => this.CsvItem<T>(name, default(T));
        public T CsvItem<T>(string name, T defaultValue)
        {
            string str = this.CsvItem(name);
            try
            {
                if (string.IsNullOrEmpty(str)) return defaultValue;
                Type type = typeof(T);
                try
                {
                    return (T)CsvLib.ValueAs(type, str);
                }
                catch
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public string CsvLineToString()
        {
            if (this.items == null || this.items.Count == 0)
                return "";
            return string.Join(this.Separator.ToString(), this.items.ToArray());
        }
        public string CsvOrderedLineToString()
        {
            if (this.orderedItems == null || this.orderedItems.Count == 0)
                return "";
            string str1 = "";
            for (int index = 0; index < this.requestedFields.Count; ++index)
            {
                if (str1.Length > 0)
                    str1 += ", ";
                string str2 = str1 + this.orderedItems[index];
                switch (this.requestedFields[index].Status)
                {
                    case CsvEngine.CsvHeader.HeaderStatusType.NotValidated:
                        break;
                    case CsvEngine.CsvHeader.HeaderStatusType.Valid:
                        break;
                    case CsvEngine.CsvHeader.HeaderStatusType.IsNull:
                        str1 = str2 + " ( Is Null ) ";
                        break;
                    case CsvEngine.CsvHeader.HeaderStatusType.IncorrectData:
                        str1 = str2 + " ( Invalid data ) ";
                        break;
                    default:
                        str1 = str2 + " ( OK ) ";
                        break;
                }
            }
            return str1;
        }
        public bool IsValidLine()
        {
            return this.items != null
                    && this.items.Count > 0
                    && (this.fields == null || this.fields.Count == this.items.Count);
        }
        public void Dispose()
        {
            if (this.sr == null) return;
            this.sr.Dispose();
            this.fields = (List<string>)null;
            this.items = (List<string>)null;
        }
        public void SetOrderedFields(List<CsvHeader> fields)
        {
            this.requestedFields = fields;
        }


        public void MoveToPositionInFile(long position = 0)
        {
            this.RowIndex = 0;
            this.sr.BaseStream.Position = position;
            this.sr.DiscardBufferedData();
        }
        public void MoveToPositionStartOfData()
        {
            this.RowIndex = 0;
            this.sr.BaseStream.Position = this.dataStartPosition;
            this.sr.DiscardBufferedData();
        }
        public void ReadLine()
        {
            this.parserLineStatus = CsvOrderedLineStatustype.Valid;
            if (this.sr != null && !this.sr.EndOfStream)
            {
                string source = this.sr.ReadLine();
                this.errorRow = "";
                this.lineNumber = this.lineNumber + 1;
                while (true)
                {
                    do
                    {
                        this.orderedItems = new List<string>();
                        if (string.IsNullOrEmpty(source))
                        {
                            if (this.sr.EndOfStream)
                            {
                                this.items = (List<string>)null; return;
                            }
                            source = this.sr.ReadLine();
                        }

                        this.errorRow = source;
                        this.items = CsvLib.CsvSplit(source, this.StripQuotes, this.Trim, this.Separator, this.Quote);
                        if (this.requestedFields != null && this.requestedFields.Count > 0)
                        {
                            for (int index = 0; index < this.requestedFields.Count; ++index)
                            {
                                this.orderedItems.Add(this.CsvItem(this.requestedFields[index].FieldName));
                            }
                        }

                        if (!this.IsValidLine())
                        {
                            if (this.items.Count > this.fields.Count)
                            {
                                this.errorRow = source;
                                this.items = (List<string>)null;
                                return;
                            }
                        }
                        else { return; }

                        if (!this.sr.EndOfStream)
                            source += this.sr.ReadLine();
                        else
                            return;
                    }
                    while (this.items.Count >= this.fields.Count);
                }
            }
            else
                this.items = (List<string>)null;
        }


        public int FindDelimiter(int numberOfRowsToCheck = 100)
        {
            this.MoveToPositionInFile(0L);
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            int num1 = 0;
            while (!this.sr.EndOfStream)
            {
                ++num1;
                if (num1 <= numberOfRowsToCheck)
                {
                    this.CurrentLine = this.sr.ReadLine();
                    if (string.IsNullOrEmpty(this.CurrentLine))
                    {
                        --num1;
                    }
                    else
                    {
                        for (int index = 0; index < this.CurrentLine.Length; ++index)
                        {
                            char ch = this.CurrentLine[index];
                            if ((int)ch != 32 && (int)ch != 34 && ((int)ch < 48 || (int)ch > 57) && (((int)ch < 65 || (int)ch > 90) && ((int)ch < 97 || (int)ch > 122)))
                            {
                                if (!dictionary.ContainsKey((int)ch))
                                    dictionary.Add((int)ch, 1);
                                else
                                    dictionary[(int)ch] = dictionary[(int)ch] + 1;
                            }
                        }
                    }
                }
                else
                    break;
            }
            int num2 = dictionary.Values.Max();
            int num3 = 44;
            foreach (int key in dictionary.Keys)
            {
                if (dictionary[key] == num2)
                {
                    num3 = key;
                    break;
                }
            }
            this.MoveToPositionInFile(0L);
            return num3;
        }
        #endregion

        #region Private Methods

        private void SetupCsvParser(string fileName, bool hasHeader)
        {
            string separator = ",";
            SetupCsvParser( fileName, hasHeader, separator);
        }
        private void SetupCsvParser(string fileName, bool hasHeader, string separator)
        {
            this.fields = new List<string>();          
            try
            {
                this.encoding = CsvLib.GetFileEncoding(fileName);
                this.sr = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read), this.encoding);
                this.Separator = (string.IsNullOrEmpty(separator)) ? (char)FindDelimiter(100) : Convert.ToChar(separator);
                this.CurrentLine = this.sr.ReadLine();
                this.RowIndex = this.RowIndex + 1;
                this.dataStartPosition = (long)this.sr.CurrentEncoding.GetByteCount(this.CurrentLine);
                this.fields = CsvLib.CsvSplit(this.CurrentLine, this.StripQuotes, this.Trim, this.Separator, this.Quote);
                this.FieldCount = this.fields.Count;
                if (hasHeader) return;
                this.dataStartPosition = 0L;
                this.fields = new List<string>();
                for (int index = 0; index < this.FieldCount; ++index)
                    this.fields.Add("Field" + (object)index);
                this.MoveToPositionInFile(this.dataStartPosition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private Properties

        private List<string> fields;
        private List<string> items;
        private List<string> orderedItems;
        private CsvOrderedLineStatustype parserLineStatus;
        private StreamReader sr;
        private string errorRow;
        private Encoding encoding;
        private int lineNumber;
        private List<CsvHeader> requestedFields;
        private long dataStartPosition;

        #endregion
    }
}
