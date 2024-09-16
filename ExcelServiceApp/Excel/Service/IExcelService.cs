using ExcelServiceApp.Excel.Model;

namespace ExcelServiceApp.Excel.Service
{
    public interface IExcelService
    {
        public Task<bool> CreateSpreadsheetFile(SpreadsheetCreateRequestDTO spreadsheetCreateRequestDTO);

        public Task<List<List<object>>> ReadExcelFile(string filePath);
        public Task<bool> GeneratePdf(string filePath);
        public Task<bool> GenerateImage(string filePath);
    }
}
