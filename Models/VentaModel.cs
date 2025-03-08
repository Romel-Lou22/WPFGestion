using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SistemaGestion.Models
{
    public class VentaModel : INotifyPropertyChanged
    {
        private int _ventaId;
        private int? _clienteId; // Puede ser null para “Consumidor Final”
        private DateTime _fechaVenta;
        private decimal _total;  // Se actualiza al final de la operación o en tiempo real
        private string _estado;
        private ObservableCollection<DetalleVentaModel> _detalles;

        public VentaModel()
        {
            FechaVenta = DateTime.Now;
            Estado = "PAGADO";
            Detalles = new ObservableCollection<DetalleVentaModel>();
            // Si quieres recalcular Total en tiempo real, suscríbete a la colección
            Detalles.CollectionChanged += OnDetallesCollectionChanged;
        }

        public int VentaId
        {
            get => _ventaId;
            set { _ventaId = value; OnPropertyChanged(nameof(VentaId)); }
        }

        public int? ClienteId
        {
            get => _clienteId;
            set { _clienteId = value; OnPropertyChanged(nameof(ClienteId)); }
        }

        public DateTime FechaVenta
        {
            get => _fechaVenta;
            set { _fechaVenta = value; OnPropertyChanged(nameof(FechaVenta)); }
        }

        // Este campo en la BD es DECIMAL(10,2). Aquí se puede recalcular con la suma de los detalles.
        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public string Estado
        {
            get => _estado;
            set { _estado = value; OnPropertyChanged(nameof(Estado)); }
        }

        public ObservableCollection<DetalleVentaModel> Detalles
        {
            get => _detalles;
            set
            {
                _detalles = value;
                OnPropertyChanged(nameof(Detalles));
            }
        }

        // (Opcional) Recalcular el total sumando ImporteTotal de cada detalle
        private void OnDetallesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DetalleVentaModel nuevo in e.NewItems)
                    nuevo.PropertyChanged += Detalle_PropertyChanged;
            }
            if (e.OldItems != null)
            {
                foreach (DetalleVentaModel viejo in e.OldItems)
                    viejo.PropertyChanged -= Detalle_PropertyChanged;
            }
            // Recalcular
            CalcularTotal();
        }

        private void Detalle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DetalleVentaModel.Cantidad) ||
                e.PropertyName == nameof(DetalleVentaModel.PrecioUnitario))
            {
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            // Suma de ImporteTotal de cada línea
            Total = Detalles.Sum(d => d.ImporteTotal);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
