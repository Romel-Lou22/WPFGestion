using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class ProductoRepository : RepositoryBase, IProductoRepository
    {
        // Método para agregar un producto a la base de datos
        public void Add(ProductoModel producto)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Productos (Nombre, Precio, CodigoBarras, Stock, Categoria, Estado) VALUES (@Nombre, @Precio, @CodigoBarras, @Stock, @Categoria, @Estado)", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = producto.Nombre;
                command.Parameters.Add("@Precio", SqlDbType.Decimal).Value = producto.Precio;
                command.Parameters.Add("@CodigoBarras", SqlDbType.NVarChar).Value = producto.CodigoBarras;
                command.Parameters.Add("@Stock", SqlDbType.Int).Value = producto.Stock;
                command.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = producto.Categoria;
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = producto.Estado;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Método para actualizar un producto existente
        public void Edit(ProductoModel producto)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Productos SET Nombre=@Nombre, Precio=@Precio, CodigoBarras=@CodigoBarras, Stock=@Stock, Categoria=@Categoria, Estado=@Estado WHERE ProductoId=@ProductoId", connection))
            {
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = producto.Id;
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = producto.Nombre;
                command.Parameters.Add("@Precio", SqlDbType.Decimal).Value = producto.Precio;
                command.Parameters.Add("@CodigoBarras", SqlDbType.NVarChar).Value = producto.CodigoBarras;
                command.Parameters.Add("@Stock", SqlDbType.Int).Value = producto.Stock;
                command.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = producto.Categoria;
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = producto.Estado;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Método para eliminar un producto por nombre
        public void Remove(string nombre)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM Productos WHERE Nombre=@Nombre", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = nombre;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Método para obtener un producto por su ID
        public ProductoModel GetById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProductoId, Nombre, Precio, CodigoBarras, Stock, Categoria, Estado FROM Productos WHERE ProductoId=@Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductoModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            CodigoBarras = reader.GetString(3),
                            Stock = reader.GetInt32(4),
                            Categoria = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            Estado = reader.GetBoolean(6)
                        };
                    }
                }
            }
            return null;
        }


        // Método para obtener todos los productos de la base de datos
        public IEnumerable<ProductoModel> GetAll()
        {
            var productos = new List<ProductoModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProductoId, Nombre, Precio, CodigoBarras, Stock, Categoria, Estado FROM Productos", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new ProductoModel
                        {
                            Id = reader.GetInt32(0),  // Se asigna el Id
                            Nombre = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            CodigoBarras = reader.GetString(3),
                            Stock = reader.GetInt32(4),
                            Categoria = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            Estado = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return productos;
        }

        public bool ExisteProducto(string nombre)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Productos WHERE Nombre = @Nombre", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = nombre;
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteProducto(string nombre, int productoId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Productos WHERE Nombre = @Nombre AND ProductoId <> @ProductoId", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = nombre;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public void ActualizarEstado(int productoId, bool estado)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Productos SET Estado = @Estado WHERE ProductoId = @ProductoId", connection))
            {
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
                command.Parameters.Add("@ProductoId", SqlDbType.Int).Value = productoId;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ProductoModel> GetProductosActivos()
        {
            var productos = new List<ProductoModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProductoId, Nombre, Precio, CodigoBarras, Stock, Categoria, Estado FROM Productos WHERE Estado = 1", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new ProductoModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Precio = reader.GetDecimal(2),
                            CodigoBarras = reader.GetString(3),
                            Stock = reader.GetInt32(4),
                            Categoria = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                            Estado = reader.GetBoolean(6)
                        });
                    }
                }
            }
            return productos;
        }




    }
}
