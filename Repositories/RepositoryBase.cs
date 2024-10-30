using System;
using System.Data.SqlClient;
using System.Configuration;

namespace SistemaGestion.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            try
            {
                // Obtener la cadena de conexión del archivo de configuración
                _connectionString = ConfigurationManager.ConnectionStrings["miConexion"].ConnectionString;

                // Validar la conexión al instanciar
                using (var connection = GetConnection())
                {
                    connection.Open();
                    
                }
            }
            catch (ConfigurationErrorsException ex)
            {
                throw new Exception("Error al leer la configuración de conexión: " + ex.Message);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error de conexión a la base de datos: {ex.Message} (Error: {ex.Number})");
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al inicializar la conexión: " + ex.Message);
            }
        }

        protected SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la conexión: " + ex.Message);
            }
        }
    }
}