using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaGestion.Models;
using System.Windows.Forms;

namespace SistemaGestion.Repositories
{
    public class ProveedorRepository : RepositoryBase, IProveedorRepository
    {
        public void add(ProveedorModel proveedor)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Proveedores (Nombre, Telefono, Email, Direccion) VALUES(@Nombre, @Telefono, @Email, @Direccion)", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = proveedor.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = (object)proveedor.Telefono ?? DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)proveedor.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)proveedor.Direccion ?? DBNull.Value;

                connection.Open();
                command.ExecuteNonQuery();
            }
            
        }

        public void edit(ProveedorModel proveedor)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand("UPDATE Proveedores SET Nombre=@Nombre, Telefono=@Telefono, Email=@Email, Direccion=@Direccion", connection))
                {
                    command.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = proveedor.Nombre;
                    command.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = (object)proveedor.Telefono ?? DBNull.Value;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)proveedor.Email ?? DBNull.Value;
                    command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = (object)proveedor.Direccion ?? DBNull.Value;

                    connection.Open();
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex )
            {
                MessageBox.Show($"Error al insertar Proveedor {ex.Message}");
            }

            
        }

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

        public ProveedorModel GetById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProveedorId, Nombre, Telefono, Email, Direccion FROM Proveedores WHERE ProveedorId=@ProveedorId", connection))
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
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<ProveedorModel> GetAll()
        {
            var proveedores = new List<ProveedorModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ProveedorId, Nombre, Telefono, Email, Direccion FROM Proveedores", connection))
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
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                }
            }
            return proveedores;
        }

        public void remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM Proveedores WHERE ProveedorId=@ProveedorId", connection))
            {
                command.Parameters.Add("@ProveedorId", SqlDbType.Int).Value = id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
