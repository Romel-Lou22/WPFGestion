using System;
using System.ComponentModel;
using System.Diagnostics;

namespace SistemaGestion.Models
{
    public enum TipoTransaccion
    {
        Venta,
        Compra
    }

    public class ReporteModel : INotifyPropertyChanged
    {
        private int _id;
        private DateTime _fecha;
        private string _estado;
        private string _tipo;
        private decimal _total;
        private decimal _totalCompra;
        private string _nombre;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public DateTime Fecha
        {
            get => _fecha;
            set
            {
                if (_fecha != value)
                {
                    _fecha = value;
                    OnPropertyChanged(nameof(Fecha));
                }
            }
        }

        public string Estado
        {
            get => _estado;
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    OnPropertyChanged(nameof(Estado));
                }
            }
        }

        public string Tipo
        {
            get => _tipo;
            set
            {
                if (_tipo != value)
                {
                    _tipo = value;
                    Debug.WriteLine($"[ReporteModel] Tipo cambiado a: {_tipo}");
                    OnPropertyChanged(nameof(Tipo));
                    OnPropertyChanged(nameof(EsReporteVentas));
                    OnPropertyChanged(nameof(EsReporteCompras));
                    OnPropertyChanged(nameof(ValorTotal));
                }
            }
        }

        public decimal Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    Debug.WriteLine($"[ReporteModel] Total cambiado a: {_total}");
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(ValorTotal));
                }
            }
        }

        public decimal TotalCompra
        {
            get => _totalCompra;
            set
            {
                if (_totalCompra != value)
                {
                    _totalCompra = value;
                    Debug.WriteLine($"[ReporteModel] TotalCompra cambiado a: {_totalCompra}");
                    OnPropertyChanged(nameof(TotalCompra));
                    OnPropertyChanged(nameof(ValorTotal));
                }
            }
        }

        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public decimal ValorTotal => Tipo == "Venta" ? Total : TotalCompra;

        public bool EsReporteVentas => Tipo == "Venta";
        public bool EsReporteCompras => Tipo == "Compra";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en OnPropertyChanged para {propertyName}: {ex.Message}");
            }
        }
    }
}