using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduSafe.Common.Utilities;
using EduSafe.IO.Excel;
using EduSafe.IO.Excel.Records.TestRecords;

namespace EduSafe.IO.Tests.Excel
{
    [TestClass]
    public class ExcelFileWriterTests
    {
        private const string _defaultListTabName = "ListOfData";
        private const string _defaultTableTabName = "DataTable";
        private const string _testTabName = "TestTabName";

        private const string _testTabOne = "TestTabOne";
        private const string _testTabTwo = "TestTabTwo";

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForListOfData_AddTwoWorksheetsWithDifferentTabNames_WorkbookHasTwoTabs()
        {
            var listOfTestData = GetListOfTestData();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfTestData, _testTabOne);
            excelFileWriter.AddWorksheetForListOfData(listOfTestData, _testTabTwo);

            Assert.AreEqual(2, excelFileWriter.ExcelWorkbook.Worksheets.Count);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(ArgumentException))]
        public void AddWorksheetForListOfData_AddTwoWorksheetsWithSameTabName_ThrowsException()
        {
            var listOfTestData = GetListOfTestData();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfTestData);
            excelFileWriter.AddWorksheetForListOfData(listOfTestData);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForListOfData_SupplyListOfDateWithoutWorksheetName_DataIsAddedWithDefaultTabName()
        {
            var listOfTestData = GetListOfTestData();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfTestData);

            var excelTabName = excelFileWriter.ExcelWorkbook.Worksheets.First().Name;
            Assert.AreEqual(_defaultListTabName, excelTabName);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForListOfData_SupplyListOfDateWithWorksheetName_DataIsAddedWithProvidedTabName()
        {
            var listOfTestData = GetListOfTestData();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForListOfData(listOfTestData, _testTabName);

            var excelTabName = excelFileWriter.ExcelWorkbook.Worksheets.First().Name;
            Assert.AreEqual(_testTabName, excelTabName);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForDataTable_AddTwoWorksheetsWithDifferentTabNames_WorkbookHasTwoTabs()
        {
            var testDataTable = GetTestDataTable();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForDataTable(testDataTable, _testTabOne);
            excelFileWriter.AddWorksheetForDataTable(testDataTable, _testTabTwo);

            Assert.AreEqual(2, excelFileWriter.ExcelWorkbook.Worksheets.Count);
        }

        [TestMethod, Owner("Matthew Moore")]
        [ExpectedException(typeof(ArgumentException))]
        public void AddWorksheetForDataTable_AddTwoWorksheetsWithSameTabName_ThrowsException()
        {
            var testDataTable = GetTestDataTable();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForDataTable(testDataTable);
            excelFileWriter.AddWorksheetForDataTable(testDataTable);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForDataTable_SupplyListOfDateWithoutWorksheetName_DataIsAddedWithDefaultTabName()
        {
            var testDataTable = GetTestDataTable();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForDataTable(testDataTable);

            var excelTabName = excelFileWriter.ExcelWorkbook.Worksheets.First().Name;
            Assert.AreEqual(_defaultTableTabName, excelTabName);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void AddWorksheetForDataTable_SupplyListOfDateWithWorksheetName_DataIsAddedWithProvidedTabName()
        {
            var testDataTable = GetTestDataTable();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForDataTable(testDataTable, _testTabName);

            var excelTabName = excelFileWriter.ExcelWorkbook.Worksheets.First().Name;
            Assert.AreEqual(_testTabName, excelTabName);
        }

        [TestMethod, Owner("Matthew Moore")]
        public void ClearWorkbook_AddTwoWorksheetsThenRemoveThem_WorkbookHasZeroTabs()
        {
            var testDataTable = GetTestDataTable();

            var excelFileWriter = new ExcelFileWriter();
            excelFileWriter.AddWorksheetForDataTable(testDataTable, _testTabOne);
            excelFileWriter.AddWorksheetForDataTable(testDataTable, _testTabTwo);
            excelFileWriter.ClearWorkbook();

            Assert.AreEqual(0, excelFileWriter.ExcelWorkbook.Worksheets.Count);
        }

        private List<TestRecordA> GetListOfTestData()
        {
            var testDataOne = new TestRecordA
            {
                TestDateA = new DateTime(2017, 1, 1),
                TestDoubleA = 99.99,
                TestIntA = 5,
                TestStringA = "Banana"
            };

            var testDataTwo = new TestRecordA
            {
                TestDateA = new DateTime(2016, 5, 10),
                TestDoubleA = 8.34,
                TestIntA = 2,
                TestStringA = "James"
            };

            var testDataThree = new TestRecordA
            {
                TestDateA = new DateTime(2013, 1, 3),
                TestDoubleA = 6.53,
                TestIntA = 1,
                TestStringA = "Colin"
            };

            var listOfTestData = new List<TestRecordA>
            {
                testDataOne,
                testDataTwo,
                testDataThree
            };

            return listOfTestData;
        }

        private DataTable GetTestDataTable()
        {
            var listOfTestData = GetListOfTestData();
            var testDataTable = DataTableUtility.ConvertListToDataTable(listOfTestData);
            return testDataTable;
        }
    }
}
