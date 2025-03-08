using System;
using System.ComponentModel;

namespace SistemaGestion.Models
{
    public class StockModel : INotifyPropertyChanged
    {
        private int _stockId;
        private int _productoId;
        private int _cantidadDisponible;
        private decimal _precioUnitario;
        private DateTime _fechaActualizacion;
        private string _nombreProducto; // Nueva propiedad

        public int StockId
        {
            get => _stockId;
            set { _stockId = value; OnPropertyChanged(nameof(StockId)); }
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

        public int CantidadDisponible
        {
            get => _cantidadDisponible;
            set { _cantidadDisponible = value; OnPropertyChanged(nameof(CantidadDisponible)); OnPropertyChanged(nameof(ValorTotal)); }
        }

        public decimal PrecioUnitario
        {
            get => _precioUnitario;
            set { _precioUnitario = value; OnPropertyChanged(nameof(PrecioUnitario)); OnPropertyChanged(nameof(ValorTotal)); }
        }

        // Campo calculado: Valor total invertido en ese producto
        public decimal ValorTotal => CantidadDisponible * PrecioUnitario;

        public DateTime FechaActualizacion
        {
            get => _fechaActualizacion;
            set { _fechaActualizacion = value; OnPropertyChanged(nameof(FechaActualizacion)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
