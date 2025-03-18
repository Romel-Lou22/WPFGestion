using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaGestion.Repositories
{
    public class ProveedorRepository : RepositoryBase, IProveedorRepository
    {
        // Agregar un proveedor incluyendo el campo Estado
        public void Add(ProveedorModel proveedor)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Proveedores (Nombre, Telefono, Email, Direccion, Estado) VALUES(@Nombre, @Telefono, @Email, @Direccion, @Estado)", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = proveedor.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = (object)proveedor.Telefono ?? DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)proveedor.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)proveedor.Direccion ?? DBNull.Value;
                // Se agrega el parámetro Estado
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = proveedor.Estado;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Editar proveedor incluyendo el campo Estado y actualizando FechaModificacion (si se requiriera)
        public void Edit(ProveedorModel proveedor)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand(
                    "UPDATE Proveedores " +
                    "SET Nombre=@Nombre, Telefono=@Telefono, Email=@Email, Direccion=@Direccion, Estado=@Estado " +
                    "WHERE ProveedorId=@ProveedorId", connection))
                {
                    command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = proveedor.Nombre;
                    command.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = (object)proveedor.Telefono ?? DBNull.Value;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)proveedor.Email ?? DBNull.Value;
                    command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)proveedor.Direccion ?? DBNull.Value;
                    command.Parameters.Add("@Estado", SqlDbType.Bit).Value = proveedor.Estado; // Campo Estado
                    command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = proveedor.ProveedorId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar Proveedor: {ex.Message}");
            }
        }

        // Actualizar solo el estado del proveedor (soft delete o reactivar)
        public void ActualizarEstado(int id, bool estado)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Proveedores SET Estado=@Estado WHERE ProveedorId=@ProveedorId", connection))
            {
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
                command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Obtener proveedor por ID, incluyendo el campo Estado
        public ProveedorModel GetById(int id)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("SELECT ProveedorId, Nombre, Telefono, Email, Direccion, Estado FROM Proveedores WHERE ProveedorId=@ProveedorId", connection))
                {
                    command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = id;
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProveedorModel
                            {
                                ProveedorId = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Telefono = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Estado = reader.GetBoolean(5)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el proveedor: {ex.Message}");
            }
            return null;
        }

        // Obtener todos los proveedores, incluyendo el campo Estado
        public IEnumerable<ProveedorModel> GetAll()
        {
            var proveedores = new List<ProveedorModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProveedorId, Nombre, Telefono, Email, Direccion, Estado FROM Proveedores", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new ProveedorModel
                        {
                            ProveedorId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Estado = reader.GetBoolean(5)
                        });
                    }
                }
            }
            return proveedores;
        }

       
        // Eliminar un proveedor de forma física (si lo deseas)
        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM Proveedores WHERE ProveedorId=@ProveedorId", connection))
            {
                command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public IEnumerable<ProveedorModel> GetProveedoresActivos()
        {
            var proveedores = new List<ProveedorModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProveedorId, Nombre, Telefono, Email, Direccion, Estado FROM Proveedores WHERE Estado = 1", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new ProveedorModel
                        {
                            ProveedorId = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Estado = reader.GetBoolean(5)
                        });
                    }
                }
            }
            return proveedores;
        }

        public bool ExisteProveedorNombre(string nombre)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Proveedores WHERE Nombre = @Nombre", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = nombre.Trim();
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteProveedorNombre(string nombre, int proveedorId)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Proveedores WHERE Nombre = @Nombre AND ProveedorId <> @ProveedorId", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = nombre.Trim();
                command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = proveedorId;
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }


    }
}
