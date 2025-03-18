using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.Repositories
{
    public class ClienteRepository : RepositoryBase, IClienteRepository
    {
        public void Add(ClienteModel cliente)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Clientes (Nombre, Telefono, Email, Direccion, NCedula, Estado) VALUES(@Nombre, @Telefono, @Email, @Direccion, @Cedula, @Estado)", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cliente.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = (object)cliente.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = (object)cliente.Direccion ?? DBNull.Value;
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cliente.Cedula;
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = cliente.Activo; // Nuevo parámetro

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ClienteModel cliente)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Clientes SET Nombre=@Nombre, Telefono=@Telefono, Email=@Email, Direccion=@Direccion, NCedula=@Cedula, Estado=@Estado, FechaModificacion=GETDATE() WHERE ID=@Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cliente.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = (object)cliente.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = (object)cliente.Direccion ?? DBNull.Value;
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cliente.Cedula;
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = cliente.Activo; // Nuevo parámetro

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("DELETE FROM Clientes WHERE ID=@Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ClienteModel GetById(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ID, Nombre, Telefono, Email, Direccion, FechaCreacion, FechaModificacion, Estado, NCedula FROM Clientes WHERE ID=@Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ClienteModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            FechaCreacion = reader.GetDateTime(5),
                            FechaModificacion = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            Activo = reader.GetBoolean(7), // Se asigna el valor de Estado al modelo
                            Cedula = reader.GetString(8)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            var clientes = new List<ClienteModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ID, Nombre, Telefono, Email, Direccion, FechaCreacion, FechaModificacion, Estado, NCedula FROM Clientes", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new ClienteModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            FechaCreacion = reader.GetDateTime(5),
                            FechaModificacion = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            Activo = reader.GetBoolean(7),
                            Cedula = reader.GetString(8)
                        });
                    }
                }
            }
            return clientes;
        }
        public void ActualizarEstado(int id, bool estado)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Clientes SET Estado=@Estado, FechaModificacion=GETDATE() WHERE ID=@Id", connection))
            {
                command.Parameters.Add("@Estado", SqlDbType.Bit).Value = estado;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<ClienteModel> GetClientesActivos()
        {
            var clientes = new List<ClienteModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT ID, Nombre, Telefono, Email, Direccion, FechaCreacion, FechaModificacion, Estado, NCedula FROM Clientes WHERE Estado = 1", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new ClienteModel
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Telefono = reader.GetString(2),
                            Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Direccion = reader.IsDBNull(4) ? null : reader.GetString(4),
                            FechaCreacion = reader.GetDateTime(5),
                            FechaModificacion = reader.IsDBNull(6) ? null : (DateTime?)reader.GetDateTime(6),
                            Activo = reader.GetBoolean(7),
                            Cedula = reader.GetString(8)
                        });
                    }
                }
            }
            return clientes;
        }

        public bool ExisteClienteNombre(string nombre)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE Nombre = @Nombre", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre.Trim();
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteClienteCedula(string cedula)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE NCedula = @Cedula", connection))
            {
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cedula.Trim();
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteClienteNombre(string nombre, int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE Nombre = @Nombre AND ID <> @ID", connection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = nombre.Trim();
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        public bool ExisteClienteCedula(string cedula, int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE NCedula = @Cedula AND ID <> @ID", connection))
            {
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cedula.Trim();
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }





    }
}
