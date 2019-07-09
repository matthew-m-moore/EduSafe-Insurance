using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Reporting
{
    public class StudentInformationReport
    {
        private const string _reportTabName = "Student Details";
        private const string _fontName = "Calibri";
        private const string _scenarioDescription = "Scenario";

        private const string _shortDateFormat = "m/d/yyyy";
        private const string _noDecimalsNumberFormat = "##########0";
        private const string _accountingNumberFormat = @"_(* #,##0.00_);_(* (#,##0.00);_(* "" - ""??_);_(@_)";

        private const string _yes = "Yes";
        private const string _no = "No";

        private const int _baseFontSize = 10;
        private const int _blankColumnsOffset = 2;
        private const int _blankRowsOffset = 2;
        private const int _leftAlignedHeaderIndex = 0;

        private static int _dataRowOffsetCounter = 1;

        private static List<string> _headersRow = new List<string>
        {
            "School ID",
            "Policy ID",
            "Name",
            "Major",
            "Start Date",
            "Grad. Date",
            "Payment ($)",
            "Total Paid-In ($)",
            "Coverage ($)",
            "Remaining ($)",
            "Enrolled",
            "Graduated",
            "Has Claims",
        };

        public static void AddReportTab(XLWorkbook excelWorkbook, List<StudentInformationRecord> studentInformationRecords)
        {
            var excelWorksheet = excelWorkbook.Worksheets.Add(_reportTabName);
            AddReportHeadersLine(excelWorksheet);

            foreach (var studentInformaitonRecord in studentInformationRecords)
            {
                AddReportLine(excelWorksheet, studentInformaitonRecord,  _dataRowOffsetCounter);
                _dataRowOffsetCounter++;
            }

            SetSheetFormatting(excelWorksheet);
            _dataRowOffsetCounter = 1;
        }

        private static void AddReportHeadersLine(IXLWorksheet excelWorksheet)
        {
            foreach (var headerIndex in Enumerable.Range(0, _headersRow.Count))
            {
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Value = _headersRow[headerIndex];
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Font.Bold = true;
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Border.TopBorderColor = XLColor.Black;
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Border.BottomBorderColor = XLColor.Black;
                excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                if (headerIndex > _leftAlignedHeaderIndex)
                {
                    excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
                else
                {
                    excelWorksheet.Cell(_blankRowsOffset, headerIndex + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                }
            }
        }

        private static void SetSheetFormatting(IXLWorksheet excelWorksheet)
        {
            // Setup font name and size
            excelWorksheet.Cells().Style.Font.FontName = _fontName;
            excelWorksheet.Cells().Style.Font.FontSize = _baseFontSize + 1;
            excelWorksheet.Row(_blankRowsOffset).Style.Font.FontSize = _baseFontSize;
            excelWorksheet.Row(_blankRowsOffset).Height = 15;
            excelWorksheet.SheetView.Freeze(_blankRowsOffset, _blankColumnsOffset);
            excelWorksheet.SetTabColor(XLColor.White);

            // Auto-fit and adjust column-widths
            excelWorksheet.Columns().AdjustToContents();
            foreach (var columnIndex in Enumerable.Range(1, _blankColumnsOffset - 1))
            {
                excelWorksheet.Column(columnIndex).Width = 2;
            }

            // Freeze panes and set tab color
            excelWorksheet.SheetView.Freeze(_blankRowsOffset, _blankColumnsOffset);
            excelWorksheet.SetTabColor(XLColor.White);
        }

        private static void AddReportLine(
            IXLWorksheet excelWorksheet,
            StudentInformationRecord studentInformationRecord,
            int dataRowOffsetCounter)
        {
            var dataColumnOffsetCounter = 0;

            // School ID
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.StudentSchoolId;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;

            // Policy ID
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.StudentPolicyId;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _noDecimalsNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;

            // Name
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.StudentName;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;

            // Major
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.StudentMajor;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;

            // Start Date
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.CollegeStartDate;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _shortDateFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Grad. Date
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.CollegeGraduationDate;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _shortDateFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Payment ($)
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.MonthlyPayment;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _accountingNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Total Paid-In ($)
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.TotalPaidInPremiums;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _accountingNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Coverage ($)
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.TotalCoverage;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _accountingNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Remaining ($)
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = studentInformationRecord.RemainingCoverage;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _accountingNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Enrolled
            var enrollmentIndicator = studentInformationRecord.IsEnrolled ? _yes : _no;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = enrollmentIndicator;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataColumnOffsetCounter++;

            // Graduated
            var graduationIndicator = studentInformationRecord.HasGraduated ? _yes : _no;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = graduationIndicator;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataColumnOffsetCounter++;

            // Has Claims
            var claimsIndicator = studentInformationRecord.HasClaims ? _yes : _no;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = claimsIndicator;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }
    }
}
