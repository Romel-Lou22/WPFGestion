using System.ComponentModel;
using System.Windows;

namespace SistemaGestion.Models
{
    public class DetalleVentaModel : INotifyPropertyChanged
    {
        private int _detalleVentaId;
        private int _ventaId;
        private int _productoId;
        private string _nombreProducto;  // Muestra el nombre en la fila
        private int _cantidad;
        private decimal _precioUnitario;

        public int DetalleVentaId
        {
            get => _detalleVentaId;
            set { _detalleVentaId = value; OnPropertyChanged(nameof(DetalleVentaId)); }
        }

        public int VentaId
        {
            get => _ventaId;
            set { _ventaId = value; OnPropertyChanged(nameof(VentaId)); }
        }

        public int ProductoId
        {
            get => _productoId;
            set { _productoId = value; OnPropertyChanged(nameof(ProductoId)); }
        }

        public string NombreProducto
        {
            get => _nombreProducto;
            set { _nombreProducto = value; OnPropertyChanged(nameof(NombreProducto)); }
        }

        public int Cantidad
        {
            get => _cantidad;
            set
            {
                // Verificamos si el valor es menor o igual a 0
                if (value <= 0)
                {
                    MessageBox.Show("La cantidad no puede ser 0 o negativa.",
                                    "Cantidad inválida",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    // Opcional: revertir el cambio (mantener la cantidad anterior)
                    return;
                }

                _cantidad = value;
                OnPropertyChanged(nameof(Cantidad));
                OnPropertyChanged(nameof(ImporteTotal));
            }
        }


        public decimal PrecioUnitario
        {
            get => _precioUnitario;
            set
            {
                _precioUnitario = value;
                OnPropertyChanged(nameof(PrecioUnitario));
                OnPropertyChanged(nameof(ImporteTotal));
            }
        }

        // Se calcula en tiempo real (y en la BD está como columna calculada)
        public decimal ImporteTotal => Cantidad * PrecioUnitario;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
