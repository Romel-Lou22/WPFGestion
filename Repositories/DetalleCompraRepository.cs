using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class DetalleCompraRepository : RepositoryBase, IDetalleCompraRepository
    {
        public void Add(DetalleCompraModel detalle)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO DetalleCompras (CompraId, ProductoId, Cantidad, PrecioUnitario) VALUES (@CompraId, @ProductoId, @Cantidad, @PrecioUnitario)", connection))
            {
                command.Parameters.Add("@CompraId", SqlDbType.Int).Value = detalle.CompraId;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(DetalleCompraModel detalle)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE DetalleCompras SET CompraId=@CompraId, ProductoId=@ProductoId, Cantidad=@Cantidad, PrecioUnitario=@PrecioUnitario WHERE DetalleCompraId=@DetalleCompraId", connection))
            {
                command.Parameters.Add("@DetalleCompraId", SqlDbType.Int).Value = detalle.DetalleCompraId;
                command.Parameters.Add("@CompraId", SqlDbType.Int).Value = detalle.CompraId;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int detalleCompraId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM DetalleCompras WHERE DetalleCompraId=@DetalleCompraId", connection))
            {
                command.Parameters.Add("@DetalleCompraId", SqlDbType.Int).Value = detalleCompraId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DetalleCompraModel GetById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT DetalleCompraId, CompraId, ProductoId, Cantidad, PrecioUnitario FROM DetalleCompras WHERE DetalleCompraId=@DetalleCompraId", connection))
            {
                command.Parameters.Add("@DetalleCompraId", SqlDbType.Int).Value = id;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DetalleCompraModel
                        {
                            DetalleCompraId = reader.GetInt32(0),
                            CompraId = reader.GetInt32(1),
                            ProductoId = reader.GetInt32(2),
                            Cantidad = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<DetalleCompraModel> GetAll()
        {
            var detalles = new List<DetalleCompraModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT DetalleCompraId, CompraId, ProductoId, Cantidad, PrecioUnitario FROM DetalleCompras", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleCompraModel
                        {
                            DetalleCompraId = reader.GetInt32(0),
                            CompraId = reader.GetInt32(1),
                            ProductoId = reader.GetInt32(2),
                            Cantidad = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4)
                        });
                    }
                }
            }
            return detalles;
        }
    }
}
