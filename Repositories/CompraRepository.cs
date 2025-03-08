using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaGestion.Repositories
{
    public class CompraRepository : RepositoryBase, ICompraRepository
    {
        public void Add(CompraModel compra)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insertar la cabecera de la compra
                        using (var command = new SqlCommand(
                            "INSERT INTO Compras (ProveedorId, FechaCompra, TotalCompra, Estado) " +
                            "VALUES (@ProveedorId, @FechaCompra, @TotalCompra, @Estado); SELECT SCOPE_IDENTITY();",
                            connection, transaction))
                        {
                            command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = compra.ProveedorId;
                            command.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = compra.FechaCompra;

                            // Calcular el total a partir de los detalles
                            decimal totalCompra = compra.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
                            command.Parameters.Add("@TotalCompra", SqlDbType.Decimal).Value = totalCompra;
                            command.Parameters.Add("@Estado", SqlDbType.VarChar).Value = compra.Estado;

                            object result = command.ExecuteScalar();
                            compra.CompraId = Convert.ToInt32(result);
                        }

                        // Insertar cada detalle de compra
                        foreach (var detalle in compra.Detalles)
                        {
                            using (var command = new SqlCommand(
                                "INSERT INTO DetalleCompras (CompraId, ProductoId, Cantidad, PrecioUnitario) " +
                                "VALUES (@CompraId, @ProductoId, @Cantidad, @PrecioUnitario)",
                                connection, transaction))
                            {
                                command.Parameters.Add("@CompraId", SqlDbType.Int).Value = compra.CompraId;
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

        public void Edit(CompraModel compra)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Actualizar la cabecera de la compra
                        using (var command = new SqlCommand(
                            "UPDATE Compras SET ProveedorId=@ProveedorId, FechaCompra=@FechaCompra, " +
                            "TotalCompra=@TotalCompra, Estado=@Estado WHERE CompraId=@CompraId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = compra.ProveedorId;
                            command.Parameters.Add("@FechaCompra", SqlDbType.DateTime).Value = compra.FechaCompra;

                            decimal totalCompra = compra.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
                            command.Parameters.Add("@TotalCompra", SqlDbType.Decimal).Value = totalCompra;
                            command.Parameters.Add("@Estado", SqlDbType.VarChar).Value = compra.Estado;
                            command.Parameters.Add("@CompraId", SqlDbType.Int).Value = compra.CompraId;
                            command.ExecuteNonQuery();
                        }

                        // Eliminar los detalles existentes de esta compra
                        using (var command = new SqlCommand(
                            "DELETE FROM DetalleCompras WHERE CompraId=@CompraId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@CompraId", SqlDbType.Int).Value = compra.CompraId;
                            command.ExecuteNonQuery();
                        }

                        // Insertar los detalles actualizados
                        foreach (var detalle in compra.Detalles)
                        {
                            using (var command = new SqlCommand(
                                "INSERT INTO DetalleCompras (CompraId, ProductoId, Cantidad, PrecioUnitario) " +
                                "VALUES (@CompraId, @ProductoId, @Cantidad, @PrecioUnitario)",
                                connection, transaction))
                            {
                                command.Parameters.Add("@CompraId", SqlDbType.Int).Value = compra.CompraId;
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

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Eliminar primero los detalles asociados
                        using (var command = new SqlCommand(
                            "DELETE FROM DetalleCompras WHERE CompraId=@CompraId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@CompraId", SqlDbType.Int).Value = id;
                            command.ExecuteNonQuery();
                        }
                        // Luego eliminar la cabecera de la compra
                        using (var command = new SqlCommand(
                            "DELETE FROM Compras WHERE CompraId=@CompraId",
                            connection, transaction))
                        {
                            command.Parameters.Add("@CompraId", SqlDbType.Int).Value = id;
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

        public CompraModel GetById(int id)
        {
            CompraModel compra = null;
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(
                    "SELECT CompraId, ProveedorId, FechaCompra, TotalCompra, Estado FROM Compras WHERE CompraId=@CompraId",
                    connection))
                {
                    command.Parameters.Add("@CompraId", SqlDbType.Int).Value = id;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            compra = new CompraModel
                            {
                                CompraId = reader.GetInt32(0),
                                ProveedorId = reader.GetInt32(1),
                                FechaCompra = reader.GetDateTime(2),
                                TotalCompra = reader.GetDecimal(3),
                                Estado = reader.GetString(4)
                            };
                        }
                    }
                    connection.Close();
                }
                if (compra != null)
                {
                    // Obtener los detalles asociados a la compra
                    using (var command = new SqlCommand(
                        "SELECT DetalleCompraId, ProductoId, Cantidad, PrecioUnitario FROM DetalleCompras WHERE CompraId=@CompraId",
                        connection))
                    {
                        command.Parameters.Add("@CompraId", SqlDbType.Int).Value = id;
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detalle = new DetalleCompraModel
                                {
                                    DetalleCompraId = reader.GetInt32(0),
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                };
                                compra.Detalles.Add(detalle);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return compra;
        }

        public IEnumerable<CompraModel> GetAll()
        {
            var compras = new List<CompraModel>();
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(
                    "SELECT CompraId, ProveedorId, FechaCompra, TotalCompra, Estado FROM Compras",
                    connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var compra = new CompraModel
                            {
                                CompraId = reader.GetInt32(0),
                                ProveedorId = reader.GetInt32(1),
                                FechaCompra = reader.GetDateTime(2),
                                TotalCompra = reader.GetDecimal(3),
                                Estado = reader.GetString(4)
                            };
                            compras.Add(compra);
                        }
                    }
                    connection.Close();
                }
                // Para cada compra, cargar sus detalles
                foreach (var compra in compras)
                {
                    using (var command = new SqlCommand(
                        "SELECT DetalleCompraId, ProductoId, Cantidad, PrecioUnitario FROM DetalleCompras WHERE CompraId=@CompraId",
                        connection))
                    {
                        command.Parameters.Add("@CompraId", SqlDbType.Int).Value = compra.CompraId;
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detalle = new DetalleCompraModel
                                {
                                    DetalleCompraId = reader.GetInt32(0),
                                    ProductoId = reader.GetInt32(1),
                                    Cantidad = reader.GetInt32(2),
                                    PrecioUnitario = reader.GetDecimal(3)
                                };
                                compra.Detalles.Add(detalle);
                            }
                        }
                        connection.Close();
                    }
                }
            }
            return compras;
        }

        public IEnumerable<CompraModel> GetReportes(DateTime fechaInicio, DateTime fechaFin)
        {
            var compras = new List<CompraModel>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "SELECT CompraId, ProveedorId, FechaCompra, TotalCompra, Estado FROM Compras " +
                    "WHERE FechaCompra BETWEEN @FechaInicio AND DATEADD(HOUR, 23, DATEADD(MINUTE, 59, DATEADD(SECOND, 59, @FechaFin)))",
                    connection))
                {
                    command.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                    command.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = fechaFin;

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var totalCompra = reader.GetDecimal(3);
                            Debug.WriteLine($"CompraId {reader.GetInt32(0)}: TotalCompra = {totalCompra}");

                            compras.Add(new CompraModel
                            {
                                CompraId = reader.GetInt32(0),
                                ProveedorId = reader.GetInt32(1),
                                FechaCompra = reader.GetDateTime(2),
                                TotalCompra = totalCompra,
                                Estado = reader.GetString(4)
                            });
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                // Registra el error o lanza una excepción personalizada
                Console.WriteLine($"Error en GetReportes: {ex.Message}");
                //Opcional: throw;
            }
            return compras;
        }



    }
}
