using ExcelServiceApp.Excel.Model;
using GemBox.Spreadsheet; 

namespace ExcelServiceApp.Excel.Service
{
    public class ExcelService : IExcelService
    {
        private readonly IConfiguration configuration;
        private readonly string key;
        public ExcelService(IConfiguration configuration)
        {
            this.configuration = configuration;
            key = this.configuration["Excel:secret-key"];
            SpreadsheetInfo.SetLicense(key);
        }
        public async Task<bool> CreateSpreadsheetFile(SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO)
        {
            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add(spreadsheetCreateRequestDTO.FileName);

            if(spreadsheetCreateRequestDTO.Colums != null && spreadsheetCreateRequestDTO.Rows != null)
            {
                await WriteFile(worksheet, spreadsheetCreateRequestDTO);
            } 
            var filePath = Path.Combine(spreadsheetCreateRequestDTO.FilePath ?? "output", $"{spreadsheetCreateRequestDTO.FileName}.xlsx");
            workbook.Save(filePath);

            return await Task.FromResult(true);
        }

        private async Task WriteFile(ExcelWorksheet worksheet,SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO)
        {
            var columns = spreadsheetCreateRequestDTO.Colums;
            var rows = spreadsheetCreateRequestDTO.Rows;

            for (var i = 0; i < columns.Count; i++)
            {
                worksheet.Cells[0, i].Value = columns[i];
            }

            for (var rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var rowData = rows[rowIndex];
                for (var colIndex = 0; colIndex < rowData.Count; colIndex++)
                {
                    worksheet.Cells[rowIndex + 1, colIndex].Value = rowData[colIndex];
                }
            }
        }

        public async Task<List<List<object>>> ReadExcelFile(string filePath)
        {
            var workbook = ExcelFile.Load(filePath);
            var wroksheet = workbook.Worksheets[0];
            var result = new List<List<object>>();

            var used = wroksheet.GetUsedCellRange();
            var firstRow = used.FirstRowIndex;
            var lastRow = used.LastRowIndex;
            var firstColumn = used.FirstColumnIndex;
            var lastColumn = used.LastColumnIndex;

            for(var rowIndex=firstRow; rowIndex< lastRow+1; rowIndex++)
            {
                var rowData = new List<object>();
                for(var colIndex = firstColumn; colIndex < lastColumn+1; colIndex++)
                {
                    rowData.Add(wroksheet.Cells[rowIndex, colIndex].Value);
                }
                result.Add(rowData);
            }
            return await Task.FromResult(result);
        }

        public async Task<bool> GeneratePdf(string filePath)
        {
            var workbook = ExcelFile.Load(filePath);
            workbook.Save(@"C:\\Users\\KraftWork\\Desktop\\GitWorkSpace\\Asp.Net_L\\ExcelServiceApp\\Output\\bulk.pdf",new PdfSaveOptions() { SelectionType = SelectionType.EntireFile});
            return await Task.FromResult(true);
        }

        public async Task<bool> GenerateImage(string filePath)
        {
            var workbook = ExcelFile.Load(filePath);
            workbook.Save(@"C:\\Users\\KraftWork\\Desktop\\GitWorkSpace\\Asp.Net_L\\ExcelServiceApp\\Output\\bulk.png", new ImageSaveOptions(ImageSaveFormat.Png) { PageNumber = 0, Width = 1080, CropToContent = true });
            return await Task.FromResult(true);
        }
    }
}
