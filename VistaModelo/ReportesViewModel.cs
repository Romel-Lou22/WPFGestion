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

        // Colección de resultados del reporte
        private ObservableCollection<ReporteModel> _reportes;
        public ObservableCollection<ReporteModel> Reportes
        {
            get => _reportes;
            set { _reportes = value; OnPropertyChanged(nameof(Reportes)); }
        }

        // Comandos
        public ICommand ConsultarCommand { get; }
        public ICommand ExportarPDFCommand { get; }

        // Referencias a repositorios
        private readonly IVentaRepository _ventaRepository;
        private readonly ICompraRepository _compraRepository;

        public ReportesViewModel()
        {
            // Asume que ya tienes implementados los repositorios para ventas y compras.
            _ventaRepository = new VentaRepository();
            _compraRepository = new CompraRepository();

            Reportes = new ObservableCollection<ReporteModel>();

            ConsultarCommand = new ViewModelCommand(ConsultarReportes);
            ExportarPDFCommand = new ViewModelCommand(ExportarPDF);
        }

        private void ConsultarReportes(object obj)
        {
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
                            Tipo = "Venta"
                        });
                    }
                }
                else // Compras
                {
                    var listaCompras = _compraRepository.GetReportes(FechaInicio, FechaFin);
                    foreach (var compra in listaCompras)
                    {
                        try
                        {
                            Debug.WriteLine($"[ViewModel] Procesando CompraId: {compra.CompraId}, TotalCompra: {compra.TotalCompra}");
                            var reporteModel = new ReporteModel
                            {
                                Id = compra.CompraId,
                                Fecha = compra.FechaCompra,
                                Total = 0, // Valor por defecto para Compras
                                TotalCompra = compra.TotalCompra,
                                Estado = compra.Estado,
                                Tipo = "Compra"
                            };
                            Debug.WriteLine($"[ViewModel] Creado ReporteModel - Id: {reporteModel.Id}, Tipo: {reporteModel.Tipo}");
                            Reportes.Add(reporteModel);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"[ViewModel] Error procesando CompraId {compra.CompraId}: {ex.Message}");
                            // Para mayor visibilidad, se podría utilizar MessageBox:
                            // MessageBox.Show($"Error procesando CompraId {compra.CompraId}: {ex.Message}");
                        }
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

        }
    }
}