using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace SistemaGestion.VistaModelo
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IVentaRepository _ventaRepository;

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

        private decimal _ingresosMensuales;
        public decimal IngresosMensuales
        {
            get => _ingresosMensuales;
            set { _ingresosMensuales = value; OnPropertyChanged(nameof(IngresosMensuales)); }
        }

        private string _notificacionVenta;
        public string NotificacionVenta
        {
            get => _notificacionVenta;
            set { _notificacionVenta = value; OnPropertyChanged(nameof(NotificacionVenta)); }
        }

        private decimal _ultimaVentaMonto;
        public decimal UltimaVentaMonto
        {
            get => _ultimaVentaMonto;
            set { _ultimaVentaMonto = value; OnPropertyChanged(nameof(UltimaVentaMonto)); }
        }

        private string _ultimaVentaCliente;
        public string UltimaVentaCliente
        {
            get => _ultimaVentaCliente;
            set { _ultimaVentaCliente = value; OnPropertyChanged(nameof(UltimaVentaCliente)); }
        }


        public HomeViewModel()
        {
            _clienteRepository = new ClienteRepository();
            _ventaRepository = new VentaRepository();

            CargarClientes();
            ConsultarVentasHoy();
            ConsultarIngresosMensuales();
            ConsultarNotificacionVenta();
        }

        private void CargarClientes()
        {
            var clientes = _clienteRepository.GetAll();
            TotalClientes = clientes.Count();
        }

        private void ConsultarVentasHoy()
        {
            try
            {
                DateTime hoy = DateTime.Today;
                var ventasHoy = _ventaRepository.GetReportes(hoy, hoy);
                VentasHoy = ventasHoy.Sum(v => v.Total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar ventas de hoy: " + ex.Message);
            }
        }

        private void ConsultarIngresosMensuales()
        {
            try
            {
                DateTime inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);
                var ventasMes = _ventaRepository.GetReportes(inicioMes, finMes);
                IngresosMensuales = ventasMes.Sum(v => v.Total);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar ingresos mensuales: " + ex.Message);
            }
        }

        private void ConsultarNotificacionVenta()
        {
            DateTime hoy = DateTime.Today;
            var ventasHoy = _ventaRepository.GetReportes(hoy, hoy)
                                            .OrderByDescending(v => v.FechaVenta)
                                            .ToList();
            if (ventasHoy.Any())
            {
                var ultimaVenta = ventasHoy.First();
                if (ultimaVenta.ClienteId.HasValue)
                {
                    var cliente = _clienteRepository.GetById(ultimaVenta.ClienteId.Value);
                    if (cliente != null)
                    {
                        Debug.WriteLine($"[ConsultarNotificacionVenta] Cliente encontrado: {cliente.Nombre}");
                        UltimaVentaCliente = cliente.Nombre;
                    }
                    else
                    {
                        Debug.WriteLine($"[ConsultarNotificacionVenta] No se encontró cliente para ID: {ultimaVenta.ClienteId.Value}");
                        UltimaVentaCliente = "Desconocido";
                    }
                }
                else
                {
                    Debug.WriteLine("[ConsultarNotificacionVenta] ClienteId es null, se usa 'Consumidor Final'");
                    UltimaVentaCliente = "Consumidor Final";
                }
                UltimaVentaMonto = ultimaVenta.Total;
            }
            else
            {
                UltimaVentaMonto = 0;
                UltimaVentaCliente = "";
            }
        }



    }
}
