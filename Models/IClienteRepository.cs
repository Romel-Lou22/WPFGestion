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
    }
}
