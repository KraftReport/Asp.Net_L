using StockAPI.DTO.Stock;
using StockAPI.Helper;
using StockAPI.Model;

namespace StockAPI.Interface
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllStocks(QueryObject queryObject);
        public Task<Stock> GetStockById(int Id);
        public Task<Stock> GetStockBySymobl(string symobl);
        public Task<Stock> CreateAStock(Stock stock);
        public Task<Stock> UpdateAStock(StockCreateRequest stockCreateRequest, int id);
        public Task<Stock> DeleteAStock(int id);
        public Task<bool> isStockExist(int id);
    }
}
