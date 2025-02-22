using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    interface IProveedorRepository
    {
        void add(ProveedorModel proveedor);
        void edit(ProveedorModel proveedor);
        void remove(int id);
        ProveedorModel GetById(int id);
        IEnumerable<ProveedorModel> GetAll();
    }
}
