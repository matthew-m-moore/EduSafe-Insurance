using EduSafe.IO.Excel;
using System.IO;

namespace EduSafe.Core.Repositories.Excel
{
    public abstract class ExcelDataRepository
    {
        protected ExcelFileReader _ExcelFileReader;

        public ExcelDataRepository(string pathToExcelFile)
        {
            _ExcelFileReader = new ExcelFileReader(pathToExcelFile);
        }

        public ExcelDataRepository(Stream fileStream)
        {
            _ExcelFileReader = new ExcelFileReader(fileStream);
        }

        public ExcelDataRepository(ExcelFileReader excelFileReader)
        {
            _ExcelFileReader = excelFileReader;
        }

        public void Dispose()
        {
            _ExcelFileReader.Dispose();
        }
    }
}
