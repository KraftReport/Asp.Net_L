namespace StockAPI.Helper
{
    public class QueryObject
    {
        public string? CompanyName { get; set; } = null;
        public string? Symbol {  get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
