using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using EduSafe.IO.Excel.Records;

namespace EduSafe.Core.Reporting
{
    public class PaymentHistoryReport
    {
        private const string _reportTabName = "Payment History";
        private const string _fontName = "Calibri";
        private const string _scenarioDescription = "Scenario";

        private const string _shortDateFormat = "m/d/yyyy";
        private const string _accountingNumberFormat = @"_(* #,##0.00_);_(* (#,##0.00);_(* "" - ""??_);_(@_)";

        private const int _baseFontSize = 10;
        private const int _blankColumnsOffset = 2;
        private const int _blankRowsOffset = 2;
        private const int _leftAlignedHeaderIndex = 0;

        private static int _dataRowOffsetCounter = 1;

        private static List<string> _headersRow = new List<string>
        {
            "Date",
            "Amount ($)",
            "Status",
            "Comments",
        };

        private static void AddReportTab(XLWorkbook excelWorkbook, List<PaymentHistoryRecord> paymentHistoryRecords)
        {
            var excelWorksheet = excelWorkbook.Worksheets.Add(_reportTabName);
            AddReportHeadersLine(excelWorksheet);

            foreach (var paymentHistoryRecord in paymentHistoryRecords)
            {
                AddReportLine(excelWorksheet, paymentHistoryRecord, _dataRowOffsetCounter);
                _dataRowOffsetCounter++;
            }

            SetSheetFormatting(excelWorksheet);
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
            PaymentHistoryRecord paymentHistoryRecord,
            int dataRowOffsetCounter)
        {
            var dataColumnOffsetCounter = 0;

            // Date
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = paymentHistoryRecord.PaymentDate;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _shortDateFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Amount ($)
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = paymentHistoryRecord.PaymentAmount;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.NumberFormat.Format = _accountingNumberFormat;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            dataColumnOffsetCounter++;

            // Status
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = paymentHistoryRecord.PaymentStatus;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;

            // Comments
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Value = paymentHistoryRecord.PaymentComments;
            excelWorksheet.Cell(_blankRowsOffset + dataRowOffsetCounter, dataColumnOffsetCounter + _blankColumnsOffset).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            dataColumnOffsetCounter++;
        }
    }
}
