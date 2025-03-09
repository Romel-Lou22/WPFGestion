using System;
using System.Collections.Specialized;
using System.Linq;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.VistaModelo
{
    public class HomeViewModel : ViewModelBase
    {
        public ClientesViewModel ClientesVM { get; set; }
        private int _totalClientes;
        public int TotalClientes
        {
            get => _totalClientes;
            set { _totalClientes = value; OnPropertyChanged(nameof(TotalClientes)); }
        }

        private decimal _ventasHoy;
        public decimal VentasHoy
        {
            get => _ventasHoy;
            set { _ventasHoy = value; OnPropertyChanged(nameof(VentasHoy)); }
        }

        private readonly IVentaRepository _ventaRepository;

        private decimal _ingresosMensuales;
        public decimal IngresosMensuales
        {
            get => _ingresosMensuales;
            set
            {
                if (_ingresosMensuales != value)
                {
                    _ingresosMensuales = value;
                    OnPropertyChanged(nameof(IngresosMensuales));
                }
            }
        }

       


        public HomeViewModel()
        {
            ClientesVM = new ClientesViewModel();
            ClientesVM.Clientes.CollectionChanged += Clientes_CollectionChanged;
            TotalClientes = ClientesVM.Clientes.Count;

            _ventaRepository = new VentaRepository();
            ConsultarVentasHoy();
            ConsultarIngresosMensuales();
        }

        private void Clientes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TotalClientes = ClientesVM.Clientes.Count;
        }

        private void ConsultarVentasHoy()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                var ventasHoy = _ventaRepository.GetReportes(hoy, hoy);
                VentasHoy = ventasHoy.Sum(v => v.Total);
            }
            catch (Exception)
            {
                // Manejo mínimo de errores
            }
        }

        private void ConsultarIngresosMensuales()
        {
            DateTime inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);
            var ventasMes = _ventaRepository.GetReportes(inicioMes, finMes);
            IngresosMensuales = ventasMes.Sum(v => v.Total);
        }
    }
}
