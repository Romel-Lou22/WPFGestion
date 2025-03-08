using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    interface IDetalleVentaRepository
    {
        void Add(DetalleVentaModel detalle);
        void Edit(DetalleVentaModel detalle);
        void Remove(int detalleVentaId);
        DetalleVentaModel GetById(int id);
        IEnumerable<DetalleVentaModel> GetAll();
    }
}
