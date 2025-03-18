using System.Collections.Generic;

namespace SistemaGestion.Models
{
    interface IProveedorRepository
    {
        void Add(ProveedorModel proveedor);
        void Edit(ProveedorModel proveedor);
        void Remove(int id);
        ProveedorModel GetById(int id);
        IEnumerable<ProveedorModel> GetAll();
        // Nuevo método para actualizar el estado (activo/inactivo)
        void ActualizarEstado(int id, bool estado);
        // Nuevo método para obtener solo proveedores activos
        IEnumerable<ProveedorModel> GetProveedoresActivos();

        bool ExisteProveedorNombre(string nombre);
        bool ExisteProveedorNombre(string nombre, int proveedorId);

    }
}
