using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Table.Services.Services
{
    public class ExcelExportService<T> where T : class 
    {
        private const int maxRowPerSheet = 1048576;
        public byte[] ExportExcelFile(List<T> dataList,string sheetName)
        {
            using(var package = new ExcelPackage())
            {
                var sheetCount = (dataList.Count/maxRowPerSheet) + 1;

                Enumerable.Range(0, sheetCount).ToList().ForEach(sheetIndex =>
                {
                    var currentSheetName = $"{sheetName}_Part-{sheetIndex + 1}";

                    var workSheet = package.Workbook.Worksheets.Add(currentSheetName);

                    var currentSheetData = dataList.Skip(sheetIndex*maxRowPerSheet).Take(maxRowPerSheet).ToList();

                    if (currentSheetData.Any())
                    {
                        AddHeaders(workSheet, typeof(T));

                        currentSheetData
                       .Select((item, rowIndex) => new { RowIndex = rowIndex + 2, Item = item })
                       .ToList()
                       .ForEach(rowData => AddRowData(workSheet, rowData.RowIndex, rowData.Item));
                    }
                });

                return package.GetAsByteArray();
            }
        }




        private void AddHeaders(ExcelWorksheet excelWorksheet,Type type)
        {
            type.GetProperties()
                .Select((prop, index) => new { ColumnIndex = index + 1, PropName = prop.Name })
                .ToList()
                .ForEach(headerData =>
                {
                    excelWorksheet.Cells[1, headerData.ColumnIndex].Value = headerData.PropName;
                });
        }

        private void AddRowData(ExcelWorksheet excelWorksheet,int row,T item)
        {
            typeof(T).GetProperties()
                .Select((prop, index) => new { CloumnIndex = index + 1, Value = prop.GetValue(item)?.ToString() ?? string.Empty })
                .ToList()
                .ForEach(rowData =>
                {
                    excelWorksheet.Cells[row, rowData.CloumnIndex].Value = rowData.Value;
                });
        }

    }
}
