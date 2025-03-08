using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace SistemaGestion.Models
{
    public class CompraModel : INotifyPropertyChanged
    {
        private int _compraId;
        private int _proveedorId;
        private DateTime _fechaCompra;
        private decimal _totalCompra;
        private string _estado;
        private ObservableCollection<DetalleCompraModel> _detalles;

        public CompraModel()
        {
            // Inicializa los valores por defecto
            FechaCompra = DateTime.Now;
            Estado = "COMPLETADA";
            Detalles = new ObservableCollection<DetalleCompraModel>();
            // Suscribirse al cambio de la colección para recalcular el total
            Detalles.CollectionChanged += OnDetallesCollectionChanged;
        }

        public int CompraId
        {
            get => _compraId;
            set { _compraId = value; OnPropertyChanged(nameof(CompraId)); }
        }

        public int ProveedorId
        {
            get => _proveedorId;
            set { _proveedorId = value; OnPropertyChanged(nameof(ProveedorId)); }
        }

        public DateTime FechaCompra
        {
            get => _fechaCompra;
            set { _fechaCompra = value; OnPropertyChanged(nameof(FechaCompra)); }
        }

        public decimal TotalCompra
        {
            get => (Detalles != null && Detalles.Any()) ? Detalles.Sum(d => d.TotalDetalle) : _totalCompra;
            set
            {
                _totalCompra = value;
                OnPropertyChanged(nameof(TotalCompra));
            }
        }

        public string Estado
        {
            get => _estado;
            set { _estado = value; OnPropertyChanged(nameof(Estado)); }
        }

        public ObservableCollection<DetalleCompraModel> Detalles
        {
            get => _detalles;
            set { _detalles = value; OnPropertyChanged(nameof(Detalles)); }
        }

        // Evento que se dispara cuando se agregan o quitan detalles
        private void OnDetallesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Cuando se agregan nuevos detalles, enganchar el PropertyChanged de cada uno
            if (e.NewItems != null)
            {
                foreach (DetalleCompraModel nuevoDetalle in e.NewItems)
                    nuevoDetalle.PropertyChanged += Detalle_PropertyChanged;
            }
            // Cuando se quitan detalles, desenganchar
            if (e.OldItems != null)
            {
                foreach (DetalleCompraModel detalleEliminado in e.OldItems)
                    detalleEliminado.PropertyChanged -= Detalle_PropertyChanged;
            }
            // Notificar que cambió el TotalCompra
            OnPropertyChanged(nameof(TotalCompra));
        }

        // Evento que se dispara cuando cambia Cantidad o PrecioUnitario de un detalle
        private void Detalle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DetalleCompraModel.Cantidad) ||
                e.PropertyName == nameof(DetalleCompraModel.PrecioUnitario))
            {
                // Recalcular total
                OnPropertyChanged(nameof(TotalCompra));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
