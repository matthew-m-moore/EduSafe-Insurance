using System.Linq;
using ClosedXML.Excel;

namespace EduSafe.IO.Excel
{
    public class ExcelHeadersRow
    {
        public int FirstCellUsedColumnNumber { get; }

        private string[] _excelHeaders { get; }

        public ExcelHeadersRow(IXLRangeRow excelHeadersRow)
        {
            FirstCellUsedColumnNumber = excelHeadersRow.FirstCell().Address.ColumnNumber;
            _excelHeaders = excelHeadersRow.Cells().Select(c => c.GetValue<string>()).ToArray();
        }

        private ExcelHeadersRow(int firstCellUsedColumnNumber, string[] excelHeaders)
        {
            FirstCellUsedColumnNumber = firstCellUsedColumnNumber;
            _excelHeaders = excelHeaders;
        }

        public ExcelHeadersRow Copy()
        {
            return new ExcelHeadersRow(FirstCellUsedColumnNumber, _excelHeaders.ToArray());
        }

        public string this[int index]
        {
            get { return _excelHeaders[index]; }
        }
    }
}
