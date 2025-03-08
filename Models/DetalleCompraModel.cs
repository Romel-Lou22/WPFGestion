using SistemaGestion.Repositories;
using System;
using System.ComponentModel;

namespace SistemaGestion.Models
{
    public class DetalleCompraModel : INotifyPropertyChanged
    {
        private int _detalleCompraId;
        private int _compraId;
        private int _productoId;
        private int _cantidad;
        private decimal _precioUnitario;

        public int DetalleCompraId
        {
            get => _detalleCompraId;
            set { _detalleCompraId = value; OnPropertyChanged(nameof(DetalleCompraId)); }
        }

        public int CompraId
        {
            get => _compraId;
            set { _compraId = value; OnPropertyChanged(nameof(CompraId)); }
        }

        public int ProductoId
        {
            get => _productoId;
            set { _productoId = value; OnPropertyChanged(nameof(ProductoId)); }
        }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                _cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
                OnPropertyChanged(nameof(TotalDetalle));
            }
        }

        public decimal PrecioUnitario
        {
            get => _precioUnitario;
            set
            {
                _precioUnitario = value;
                OnPropertyChanged(nameof(PrecioUnitario));
                OnPropertyChanged(nameof(TotalDetalle));
            }
        }

        // La columna calculada TotalDetalle (no se almacena, se calcula en tiempo real)
        public decimal TotalDetalle => Cantidad * PrecioUnitario;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        // Nueva propiedad para mostrar el nombre del producto
        public string NombreProducto
        {
            get
            {
                if (ProductoId == 0)
                    return string.Empty;

                // Consulta rápida al repositorio para obtener el nombre
                // (En un proyecto grande, lo ideal es tener un servicio/cache para evitar hits repetidos)
                var repo = new ProductoRepository();
                var producto = repo.GetById(ProductoId);
                return producto?.Nombre ?? string.Empty;
            }
        }
    }
}
