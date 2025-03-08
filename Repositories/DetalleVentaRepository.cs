using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class DetalleVentaRepository : RepositoryBase, IDetalleVentaRepository
    {
        public void Add(DetalleVentaModel detalle)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "INSERT INTO DetalleVentas (VentaId, ProductoId, Cantidad, PrecioUnitario) " +
                "VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario)", connection))
            {
                command.Parameters.Add("@VentaId", SqlDbType.Int).Value = detalle.VentaId;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(DetalleVentaModel detalle)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "UPDATE DetalleVentas SET VentaId=@VentaId, ProductoId=@ProductoId, " +
                "Cantidad=@Cantidad, PrecioUnitario=@PrecioUnitario WHERE DetalleVentaId=@DetalleVentaId", connection))
            {
                command.Parameters.Add("@DetalleVentaId", SqlDbType.Int).Value = detalle.DetalleVentaId;
                command.Parameters.Add("@VentaId", SqlDbType.Int).Value = detalle.VentaId;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int detalleVentaId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "DELETE FROM DetalleVentas WHERE DetalleVentaId=@DetalleVentaId", connection))
            {
                command.Parameters.Add("@DetalleVentaId", SqlDbType.Int).Value = detalleVentaId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DetalleVentaModel GetById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "SELECT DetalleVentaId, VentaId, ProductoId, Cantidad, PrecioUnitario " +
                "FROM DetalleVentas WHERE DetalleVentaId=@DetalleVentaId", connection))
            {
                command.Parameters.Add("@DetalleVentaId", SqlDbType.Int).Value = id;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new DetalleVentaModel
                        {
                            DetalleVentaId = reader.GetInt32(0),
                            VentaId = reader.GetInt32(1),
                            ProductoId = reader.GetInt32(2),
                            Cantidad = reader.GetInt32(3),
                            PrecioUnitario = reader.GetDecimal(4)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<DetalleVentaModel> GetAll()
        {
            var detalles = new List<DetalleVentaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "SELECT DetalleVentaId, VentaId, ProductoId, Cantidad, PrecioUnitario FROM DetalleVentas",
                connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        detalles.Add(new DetalleVentaModel
                        {
                            DetalleVentaId = reader.GetInt32(0),
                            VentaId = reader.GetInt32(1),
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
