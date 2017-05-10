using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csv.Type.Common
{
    public class CommonCsv<T>
    {
        
    }
    public class CommonCsvList<T>: IEnumerable<T>, IEnumerable
    {
        public List<T> Items { get; set; } = new List<T>();
        public int Count { get { return Items.Count; } set { Count = value; } }
        public string HeaderString { get; set; }
        public bool EndOfFile { get; set; }
        public string EndOfFileTimeStamp { get; set; }

        public List<string> HeaderList = new List<string>();

        public string OutputPath { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
