using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    public interface ICompraRepository
    {
        void Add(CompraModel compra);
        void Edit(CompraModel compra);
        void Remove(int compraId);
        CompraModel GetById(int id);
        IEnumerable<CompraModel> GetAll();
        IEnumerable<CompraModel> GetReportes(DateTime fechaInicio, DateTime fechaFin);

    }
}
