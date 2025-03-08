using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class StockRepository : RepositoryBase, IStockRepository
    {
        public void Add(StockModel stock)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Stock (ProductoId, CantidadDisponible, PrecioUnitario, FechaActualizacion) VALUES (@ProductoId, @CantidadDisponible, @PrecioUnitario, @FechaActualizacion)", connection))
            {
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = stock.ProductoId;
                command.Parameters.Add("@CantidadDisponible", SqlDbType.Int).Value = stock.CantidadDisponible;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = stock.PrecioUnitario;
                command.Parameters.Add("@FechaActualizacion", SqlDbType.DateTime).Value = stock.FechaActualizacion;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(StockModel stock)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Stock SET ProductoId=@ProductoId, CantidadDisponible=@CantidadDisponible, PrecioUnitario=@PrecioUnitario, FechaActualizacion=@FechaActualizacion WHERE StockId=@StockId", connection))
            {
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = stock.ProductoId;
                command.Parameters.Add("@CantidadDisponible", SqlDbType.Int).Value = stock.CantidadDisponible;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = stock.PrecioUnitario;
                command.Parameters.Add("@FechaActualizacion", SqlDbType.DateTime).Value = stock.FechaActualizacion;
                command.Parameters.Add("@StockId", SqlDbType.Int).Value = stock.StockId;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int stockId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM Stock WHERE StockId=@StockId", connection))
            {
                command.Parameters.Add("@StockId", SqlDbType.Int).Value = stockId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public StockModel GetById(int stockId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT StockId, ProductoId, CantidadDisponible, PrecioUnitario, FechaActualizacion FROM Stock WHERE StockId=@StockId", connection))
            {
                command.Parameters.Add("@StockId", SqlDbType.Int).Value = stockId;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new StockModel
                        {
                            StockId = reader.GetInt32(0),
                            ProductoId = reader.GetInt32(1),
                            CantidadDisponible = reader.GetInt32(2),
                            PrecioUnitario = reader.GetDecimal(3),
                            FechaActualizacion = reader.GetDateTime(4)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<StockModel> GetAll()
        {
            var lista = new List<StockModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                @"SELECT s.StockId, s.ProductoId, p.Nombre AS NombreProducto, 
                 s.CantidadDisponible, s.PrecioUnitario, s.FechaActualizacion 
          FROM Stock s
          INNER JOIN Productos p ON s.ProductoId = p.ProductoId", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new StockModel
                        {
                            StockId = reader.GetInt32(0),
                            ProductoId = reader.GetInt32(1),
                            NombreProducto = reader.GetString(2), // Se obtiene el nombre del producto
                            CantidadDisponible = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4),
                            FechaActualizacion = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return lista;
        }
        // Implementación del método GetByProductoId:
        public StockModel GetByProductoId(int productoId)
        {
            StockModel stock = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                @"SELECT s.StockId, s.ProductoId, p.Nombre AS NombreProducto, 
                         s.CantidadDisponible, s.PrecioUnitario, s.FechaActualizacion 
                  FROM Stock s
                  INNER JOIN Productos p ON s.ProductoId = p.ProductoId
                  WHERE s.ProductoId = @ProductoId", connection))
            {
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stock = new StockModel
                        {
                            StockId = reader.GetInt32(0),
                            ProductoId = reader.GetInt32(1),
                            NombreProducto = reader.GetString(2),
                            CantidadDisponible = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4),
                            FechaActualizacion = reader.GetDateTime(5)
                        };
                    }
                }
            }
            return stock;
        }

    }
}
