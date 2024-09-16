namespace ExcelServiceApp.Excel.Model
{
    public class SpreadsheetCreateRequestDTO
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public List<string>? Colums { get; set; }
        public List<List<string>>? Rows { get; set; }
    }
}
