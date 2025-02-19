using SistemaGestion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace SistemaGestion.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            if (string.IsNullOrEmpty(credential?.UserName) || string.IsNullOrEmpty(credential?.Password))
            {
                throw new ArgumentException("Usuario y contraseña son requeridos");
            }

            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(1) FROM Users WHERE Username=@username AND PasswordHash=@password";
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de base de datos al autenticar usuario: {ex.Message} (Error: {ex.Number})");
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al autenticar usuario: " + ex.Message);
            }
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
