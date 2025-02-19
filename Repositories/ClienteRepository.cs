using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Repositories
{
    public class ClienteRepository : RepositoryBase, IClienteRepository
    {
        public void Add(ClienteModel cliente)
        {
            using (var conection = GetConnection())
            using (var command = new SqlCommand("INSERT INTO Clientes (Nombre,Telefono,Email,Direccion,NCedula)VALUES(@Nombre,@Telefono,@Email,@Direccion,@Cedula)", conection))
            {
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cliente.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = (object)cliente.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = (object)cliente.Direccion ?? DBNull.Value;
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cliente.Cedula;

                conection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ClienteModel cliente)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand("UPDATE Clientes SET Nombre=@Nombre, Telefono=@Telefono, Email=@Email, Direccion=@Direccion, NCedula=@Cedula, FechaModificacion=GETDATE() WHERE ID=@Id", connection))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = cliente.Id;
                command.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cliente.Nombre;
                command.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = cliente.Telefono;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = (object)cliente.Email ?? DBNull.Value;
                command.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = (object)cliente.Direccion ?? DBNull.Value;
                command.Parameters.Add("@Cedula", SqlDbType.VarChar).Value = cliente.Cedula;

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
            using (var command = new SqlCommand("SELECT ID, Nombre, Telefono, Email, Direccion, FechaCreacion, FechaModificacion, NCedula FROM Clientes WHERE ID=@Id", connection))
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
                            Cedula = reader.GetString(7)
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
            using (var command = new SqlCommand("SELECT ID, Nombre, Telefono, Email, Direccion, FechaCreacion, FechaModificacion, NCedula FROM Clientes", connection))
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
                            Cedula = reader.GetString(7)
                        });
                    }
                }
            }
            return clientes;
        }

        
    }
}
