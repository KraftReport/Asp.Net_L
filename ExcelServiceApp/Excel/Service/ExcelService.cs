using ExcelServiceApp.Excel.Model;
using GemBox.Spreadsheet; 

namespace ExcelServiceApp.Excel.Service
{
    public class ExcelService : IExcelService
    {
        private readonly IConfiguration _configuration;
        private readonly string key;
        private readonly string output;
        public ExcelService(IConfiguration configuration)
        {
            IConfiguration configuration1;
            this._configuration = configuration;
            key = this._configuration["Excel:secret-key"];
            output = this._configuration["Excel:output-folder"];
            SpreadsheetInfo.SetLicense(key);
        }
        public async Task<bool> CreateSpreadsheetFile(SpreadsheetCreateRequestDTO spreadsheetCreateRequestDto)
        {
            var workbook = new ExcelFile();
            var worksheet = workbook.Worksheets.Add(spreadsheetCreateRequestDto.FileName);

            if(spreadsheetCreateRequestDto.Colums != null && spreadsheetCreateRequestDto.Rows != null)
            {
                await WriteFile(worksheet, spreadsheetCreateRequestDto);
            } 
            var filePath = Path.Combine(spreadsheetCreateRequestDto.FilePath ?? "output", $"{spreadsheetCreateRequestDto.FileName}.xlsx");
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

        public async Task<bool> GeneratePdf(string filePath,string fileName)
        {
            var workbook = ExcelFile.Load(filePath);
            workbook.Save(output+fileName,new PdfSaveOptions() { SelectionType = SelectionType.EntireFile});
            return await Task.FromResult(true);
        }

        public async Task<bool> GenerateImage(string filePath,string fileName)
        {
            var workbook = ExcelFile.Load(filePath);
            workbook.Save( output+fileName, new ImageSaveOptions(ImageSaveFormat.Png) { PageNumber = 0, Width = 1080, CropToContent = true });
            return await Task.FromResult(true);
        }
    }
}
