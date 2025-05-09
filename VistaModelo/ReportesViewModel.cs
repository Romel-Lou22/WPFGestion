using SistemaGestion.Models;
using SistemaGestion.Repositories;
using SistemaGestion.Comprobante; // si usas un helper para exportar a PDF
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace SistemaGestion.VistaModelo
{
    public enum ReporteTipo
    {
        Ventas,
        Compras
    }
    public class ReportesViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProveedorRepository _proveedorRepository;
        // Repositorios de Ventas y Compras
        private readonly IVentaRepository _ventaRepository;
        private readonly ICompraRepository _compraRepository;
        // Repositorios para los detalles
        private readonly IDetalleVentaRepository _detalleVentaRepository;
        private readonly IDetalleCompraRepository _detalleCompraRepository;

        // Propiedades de fechas
        private DateTime _fechaInicio = DateTime.Today.AddDays(-7); // por defecto, últimos 7 días
        public DateTime FechaInicio
        {
            get => _fechaInicio;
            set { _fechaInicio = value; OnPropertyChanged(nameof(FechaInicio)); }
        }

        private DateTime _fechaFin = DateTime.Today;
        public DateTime FechaFin
        {
            get => _fechaFin;
            set { _fechaFin = value; OnPropertyChanged(nameof(FechaFin)); }
        }

        // Tipo de reporte seleccionado (Ventas o Compras)
        private ReporteTipo _reporteSeleccionado = ReporteTipo.Ventas;
        public ReporteTipo ReporteSeleccionado
        {
            get => _reporteSeleccionado;
            set
            {
                _reporteSeleccionado = value;
                OnPropertyChanged(nameof(ReporteSeleccionado));
                // Notificar cambios en las propiedades dependientes
                OnPropertyChanged(nameof(EsReporteVentas));
                OnPropertyChanged(nameof(EsReporteCompras));
            }
        }

        // Propiedades para controlar la visibilidad de columnas
        public bool EsReporteVentas => ReporteSeleccionado == ReporteTipo.Ventas;
        public bool EsReporteCompras => ReporteSeleccionado == ReporteTipo.Compras;

        // Colección de resultados del reporte (maestro)
        private ObservableCollection<ReporteModel> _reportes;
        public ObservableCollection<ReporteModel> Reportes
        {
            get => _reportes;
            set { _reportes = value; OnPropertyChanged(nameof(Reportes)); }
        }

        // Propiedad para el elemento seleccionado en el DataGrid maestro
        private ReporteModel _reporteItemSeleccionado;
        public ReporteModel ReporteItemSeleccionado
        {
            get => _reporteItemSeleccionado;
            set
            {
                _reporteItemSeleccionado = value;
                OnPropertyChanged(nameof(ReporteItemSeleccionado));
                // Al cambiar el seleccionado, se cargan los detalles
                CargarDetalle(_reporteItemSeleccionado);
            }
        }

        // Colección para los detalles (detalle)
        private ObservableCollection<object> _detalles;
        public ObservableCollection<object> Detalles
        {
            get => _detalles;
            set { _detalles = value; OnPropertyChanged(nameof(Detalles)); }
        }

        // Comandos
        public ICommand ConsultarCommand { get; }
        public ICommand ExportarPDFCommand { get; }
        public ICommand ImprimirDetalleCommand { get; }

        public ReportesViewModel()
        {
            // Inicializar repositorios
            _ventaRepository = new VentaRepository();
            _compraRepository = new CompraRepository();
            _clienteRepository = new ClienteRepository();
            _proveedorRepository = new ProveedorRepository();
            _detalleVentaRepository = new DetalleVentaRepository();
            _detalleCompraRepository = new DetalleCompraRepository();

            Reportes = new ObservableCollection<ReporteModel>();
            Detalles = new ObservableCollection<object>();

            ConsultarCommand = new ViewModelCommand(ConsultarReportes);
            ExportarPDFCommand = new ViewModelCommand(ExportarPDF);

            ImprimirDetalleCommand = new ViewModelCommand(ImprimirDetalle);
        }

        private void ConsultarReportes(object obj)
        {
            // Validar que la fecha de inicio no sea mayor que la fecha de fin.
            if (FechaInicio > FechaFin)
            {
                MessageBox.Show("La fecha de inicio no puede ser mayor que la fecha de fin.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Reportes.Clear();
                if (ReporteSeleccionado == ReporteTipo.Ventas)
                {
                    var listaVentas = _ventaRepository.GetReportes(FechaInicio, FechaFin);
                    foreach (var venta in listaVentas)
                    {
                        Reportes.Add(new ReporteModel
                        {
                            Id = venta.VentaId,
                            Fecha = venta.FechaVenta,
                            Total = venta.Total,
                            Estado = venta.Estado,
                            Tipo = "Venta",
                            Nombre = ObtenerNombre(venta.ClienteId, ReporteTipo.Ventas)
                        });
                    }
                }
                else // Compras
                {
                    var listaCompras = _compraRepository.GetReportes(FechaInicio, FechaFin);
                    foreach (var compra in listaCompras)
                    {
                        Reportes.Add(new ReporteModel
                        {
                            Id = compra.CompraId,
                            Fecha = compra.FechaCompra,
                            TotalCompra = compra.TotalCompra,
                            Estado = compra.Estado,
                            Tipo = "Compra",
                            Nombre = ObtenerNombre(compra.ProveedorId, ReporteTipo.Compras)
                        });
                    }
                }

                OnPropertyChanged(nameof(Reportes));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar reportes: " + ex.Message);
            }
        }

        private void ExportarPDF(object obj)
        {
            if (Reportes == null || Reportes.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ComprobanteReportes.GenerarComprobantePDF(Reportes, FechaInicio, FechaFin);
        }

        private string ObtenerNombre(int? id, ReporteTipo tipo)
        {
            if (tipo == ReporteTipo.Ventas)
            {
                // Si es venta y el ClienteId es nulo, se interpreta como Consumidor Final
                if (id.HasValue)
                {
                    var cliente = _clienteRepository.GetById(id.Value);
                    return cliente != null ? cliente.Nombre : $"Cliente {id.Value}";
                }
                else
                {
                    return "Consumidor Final";
                }
            }
            else // Compras
            {
                // Para compras, el ProveedorId no debe ser nulo
                var proveedor = _proveedorRepository.GetById(id ?? 0);
                return proveedor != null ? proveedor.Nombre : $"Proveedor {id}";
            }
        }

        // Método para cargar el detalle del reporte seleccionado
        private void CargarDetalle(ReporteModel reporte)
        {
            Detalles.Clear();

            if (reporte == null)
                return;

            if (reporte.Tipo.Equals("Venta", StringComparison.OrdinalIgnoreCase))
            {
                // Obtener detalles de venta para el ID de venta
                var detallesVenta = _detalleVentaRepository.GetDetalleVenta(reporte.Id);
                foreach (var det in detallesVenta)
                {
                    Detalles.Add(det);
                }
            }
            else if (reporte.Tipo.Equals("Compra", StringComparison.OrdinalIgnoreCase))
            {
                // Obtener detalles de compra para el ID de compra
                var detallesCompra = _detalleCompraRepository.GetDetalleCompra(reporte.Id);
                foreach (var det in detallesCompra)
                {
                    Detalles.Add(det);
                }
            }
        }

        // Agrega en la sección de comandos:
     

        // En el constructor, inicializa el comando:
      

        // Método que invoca la generación del PDF individual
        private void ImprimirDetalle(object obj)
        {
            if (ReporteItemSeleccionado == null)
            {
                MessageBox.Show("Seleccione una transacción.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Aquí se asume que la colección Detalles contiene los detalles correspondientes al reporte seleccionado
            DetalleComprobantePDF.GenerarPDFDetalle(ReporteItemSeleccionado, Detalles);
        }

    }
}
