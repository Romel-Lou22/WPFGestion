using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestion.Repositories
{
    public class VentaRepository : RepositoryBase, IVentaRepository
    {
        public void Add(VentaModel venta)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insertar la cabecera
                        using (var command = new SqlCommand(
                            "INSERT INTO Ventas (ClienteId, FechaVenta, Total, Estado) " +
                            "VALUES (@ClienteId, @FechaVenta, @Total, @Estado); SELECT SCOPE_IDENTITY();",
                            connection, transaction))
                        {
                            command.Parameters.Add("@ClienteId", SqlDbType.Int).Value =
                                (object)venta.ClienteId ?? DBNull.Value; // null => Consumidor Final
                            command.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = venta.FechaVenta;
                            command.Parameters.Add("@Total", SqlDbType.Decimal).Value = venta.Total;
                            command.Parameters.Add("@Estado", SqlDbType.VarChar).Value = venta.Estado;

                            object result = command.ExecuteScalar();
                            venta.VentaId = Convert.ToInt32(result);
                        }

                        // Insertar detalles
                        foreach (var detalle in venta.Detalles)
                        {
                            using (var command = new SqlCommand(
                                "INSERT INTO DetalleVentas (VentaId, ProductoId, Cantidad, PrecioUnitario) " +
                                "VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario)",
                                connection, transaction))
                            {
                                command.Parameters.Add("@VentaId", SqlDbType.Int).Value = venta.VentaId;
                                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Edit(VentaModel venta)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Actualizar cabecera
                        using (var command = new SqlCommand(
                            "UPDATE Ventas SET ClienteId=@ClienteId, FechaVenta=@FechaVenta, " +
                            "Total=@Total, Estado=@Estado WHERE VentaId=@VentaId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@ClienteId", SqlDbType.Int).Value =
                                (object)venta.ClienteId ?? DBNull.Value;
                            command.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = venta.FechaVenta;
                            command.Parameters.Add("@Total", SqlDbType.Decimal).Value = venta.Total;
                            command.Parameters.Add("@Estado", SqlDbType.VarChar).Value = venta.Estado;
                            command.Parameters.Add("@VentaId", SqlDbType.Int).Value = venta.VentaId;
                            command.ExecuteNonQuery();
                        }

                        // Eliminar detalles existentes
                        using (var command = new SqlCommand(
                            "DELETE FROM DetalleVentas WHERE VentaId=@VentaId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@VentaId", SqlDbType.Int).Value = venta.VentaId;
                            command.ExecuteNonQuery();
                        }

                        // Insertar nuevamente los detalles
                        foreach (var detalle in venta.Detalles)
                        {
                            using (var command = new SqlCommand(
                                "INSERT INTO DetalleVentas (VentaId, ProductoId, Cantidad, PrecioUnitario) " +
                                "VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario)",
                                connection, transaction))
                            {
                                command.Parameters.Add("@VentaId", SqlDbType.Int).Value = venta.VentaId;
                                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = detalle.ProductoId;
                                command.Parameters.Add("@Cantidad", SqlDbType.Int).Value = detalle.Cantidad;
                                command.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal).Value = detalle.PrecioUnitario;
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Remove(int ventaId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Eliminar detalles
                        using (var command = new SqlCommand(
                            "DELETE FROM DetalleVentas WHERE VentaId=@VentaId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@VentaId", SqlDbType.Int).Value = ventaId;
                            command.ExecuteNonQuery();
                        }
                        // Eliminar cabecera
                        using (var command = new SqlCommand(
                            "DELETE FROM Ventas WHERE VentaId=@VentaId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@VentaId", SqlDbType.Int).Value = ventaId;
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public VentaModel GetById(int id)
        {
            VentaModel venta = null;
            using (var connection = GetConnection())
            {
                // 1. Obtener la cabecera
                using (var command = new SqlCommand(
                    "SELECT VentaId, ClienteId, FechaVenta, Total, Estado FROM Ventas WHERE VentaId=@VentaId",
                    connection))
                {
                    command.Parameters.Add("@VentaId", SqlDbType.Int).Value = id;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            venta = new VentaModel
                            {
                                VentaId = reader.GetInt32(0),
                                ClienteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                FechaVenta = reader.GetDateTime(2),
                                Total = reader.GetDecimal(3),
                                Estado = reader.GetString(4)
                            };
                        }
                    }
                    connection.Close();
                }
                if (venta != null)
                {
                    // 2. Obtener el detalle
                    using (var command = new SqlCommand(
                        "SELECT DetalleVentaId, ProductoId, Cantidad, PrecioUnitario FROM DetalleVentas WHERE VentaId=@VentaId",
                        connection))
                    {
                        command.Parameters.Add("@VentaId", SqlDbType.Int).Value = id;
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detalle = new DetalleVentaModel
                                {
                                    DetalleVentaId = reader.GetInt32(0),
                                    VentaId = id,
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                };
                                venta.Detalles.Add(detalle);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return venta;
        }

        public IEnumerable<VentaModel> GetAll()
        {
            var ventas = new List<VentaModel>();
            using (var connection = GetConnection())
            {
                // 1. Obtener cabeceras
                using (var command = new SqlCommand(
                    "SELECT VentaId, ClienteId, FechaVenta, Total, Estado FROM Ventas",
                    connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var venta = new VentaModel
                            {
                                VentaId = reader.GetInt32(0),
                                ClienteId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                                FechaVenta = reader.GetDateTime(2),
                                Total = reader.GetDecimal(3),
                                Estado = reader.GetString(4)
                            };
                            ventas.Add(venta);
                        }
                    }
                    connection.Close();
                }
                // 2. Para cada venta, obtener sus detalles
                foreach (var venta in ventas)
                {
                    using (var command = new SqlCommand(
                        "SELECT DetalleVentaId, ProductoId, Cantidad, PrecioUnitario FROM DetalleVentas WHERE VentaId=@VentaId",
                        connection))
                    {
                        command.Parameters.Add("@VentaId", SqlDbType.Int).Value = venta.VentaId;
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detalle = new DetalleVentaModel
                                {
                                    DetalleVentaId = reader.GetInt32(0),
                                    VentaId = venta.VentaId,
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                };
                                venta.Detalles.Add(detalle);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return ventas;
        }

        public IEnumerable<VentaModel> GetReportes(DateTime fechaInicio, DateTime fechaFin)
        {
            var ventas = new List<VentaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand(
                "SELECT VentaId, ClienteId, FechaVenta, Total, Estado FROM Ventas " +
                "WHERE FechaVenta BETWEEN @FechaInicio AND DATEADD(HOUR, 23, DATEADD(MINUTE, 59, DATEADD(SECOND, 59, @FechaFin)))", connection))
            {
                command.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                command.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime fechaVenta;
                        // Si el valor es DBNull o está fuera del rango, asigna un valor por defecto
                        if (reader.IsDBNull(2))
                        {
                            fechaVenta = new DateTime(1753, 1, 1);
                        }
                        else
                        {
                            fechaVenta = reader.GetDateTime(2);
                            // Si la fecha es menor que el mínimo permitido, la ajustamos
                            if (fechaVenta < new DateTime(1753, 1, 1))
                                fechaVenta = new DateTime(1753, 1, 1);
                        }

                        ventas.Add(new VentaModel
                        {
                            VentaId = reader.GetInt32(0),
                            // Puedes agregar el mapeo de ClienteId si lo necesitas
                            FechaVenta = fechaVenta,
                            Total = reader.GetDecimal(3),
                            Estado = reader.GetString(4)
                        });
                    }
                }
            }
            return ventas;
        }



    }
}
