using System.Collections.Generic;

namespace SistemaGestion.Models
{
    public interface IStockRepository
    {
        void Add(StockModel stock);
        void Edit(StockModel stock);
        void Remove(int stockId);
        StockModel GetById(int stockId);
        IEnumerable<StockModel> GetAll();

        StockModel GetByProductoId(int productoId);
    }
}
