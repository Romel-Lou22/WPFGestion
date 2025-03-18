using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    interface IClienteRepository
    {
        void Add(ClienteModel cliente);
        void Edit(ClienteModel cliente);
        void Remove(int id);
        ClienteModel GetById(int id);
        IEnumerable<ClienteModel> GetAll();

        bool ExisteClienteNombre(string nombre);
        bool ExisteClienteNombre(string nombre, int id);
        bool ExisteClienteCedula(string cedula);
        bool ExisteClienteCedula(string cedula, int id);

        void ActualizarEstado(int id, bool estado);
        IEnumerable<ClienteModel> GetClientesActivos();
    }
}
