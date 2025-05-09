using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    public interface IDetalleCompraRepository
    {
        void Add(DetalleCompraModel detalle);
        void Edit(DetalleCompraModel detalle);
        void Remove(int detalleCompraId);
        DetalleCompraModel GetById(int id);
        IEnumerable<DetalleCompraModel> GetAll();

        IEnumerable<DetalleCompraModel> GetDetalleCompra(int compraId);
    }
}
