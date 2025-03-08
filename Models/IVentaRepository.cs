using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    public interface IVentaRepository
    {
        void Add(VentaModel venta);
        void Edit(VentaModel venta);
        void Remove(int ventaId);
        VentaModel GetById(int id);
        IEnumerable<VentaModel> GetAll();
        IEnumerable<VentaModel> GetReportes(DateTime fechaInicio, DateTime fechaFin);
    }
}
