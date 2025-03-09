using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SistemaGestion.VistaModelo
{
    public class FinanzasViewModel : ViewModelBase
    {
        // Instancias de los repositorios para ventas, compras y stock.
        private readonly IVentaRepository ventaRepository;
        private readonly ICompraRepository compraRepository;
        private readonly IStockRepository stockRepository; // Agregado para el inventario

        public FinanzasViewModel()
        {
            // Inicializamos los repositorios.
            ventaRepository = new VentaRepository();
            compraRepository = new CompraRepository();
            stockRepository = new StockRepository();

            // Inicializamos las colecciones antes de establecer las fechas.
            Ingresos = new ObservableCollection<MovimientoFinanciero>();
            Egresos = new ObservableCollection<MovimientoFinanciero>();

            // Establecemos el rango de fechas válido para el mes actual.
            FechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            // Inicializamos el comando para consultar los datos.
            ConsultarFinanzasCommand = new ViewModelCommand(ConsultarFinanzas);

            // Cargamos los datos inicialmente.
            ConsultarFinanzas(null);
        }

        #region Propiedades de Fechas
        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get => _fechaInicio;
            set
            {
                if (_fechaInicio != value)
                {
                    // Aseguramos que la fecha no sea menor a 1/1/1753.
                    _fechaInicio = value < new DateTime(1753, 1, 1) ? new DateTime(1753, 1, 1) : value;
                    OnPropertyChanged(nameof(FechaInicio));
                    ConsultarFinanzas(null);
                }
            }
        }

        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get => _fechaFin;
            set
            {
                if (_fechaFin != value)
                {
                    // Aseguramos que la fecha no sea menor a 1/1/1753.
                    _fechaFin = value < new DateTime(1753, 1, 1) ? new DateTime(1753, 1, 1) : value;
                    OnPropertyChanged(nameof(FechaFin));
                    ConsultarFinanzas(null);
                }
            }
        }
        #endregion

        #region Resumen Financiero
        private decimal _valorInventario;
        public decimal ValorInventario
        {
            get => _valorInventario;
            set
            {
                if (_valorInventario != value)
                {
                    _valorInventario = value;
                    OnPropertyChanged(nameof(ValorInventario));
                }
            }
        }

        private decimal _ventasTotales;
        public decimal VentasTotales
        {
            get => _ventasTotales;
            set
            {
                if (_ventasTotales != value)
                {
                    _ventasTotales = value;
                    OnPropertyChanged(nameof(VentasTotales));
                }
            }
        }

        private decimal _gananciaBruta;
        public decimal GananciaBruta
        {
            get => _gananciaBruta;
            set
            {
                if (_gananciaBruta != value)
                {
                    _gananciaBruta = value;
                    OnPropertyChanged(nameof(GananciaBruta));
                }
            }
        }
        #endregion

        #region Colecciones de Movimientos Financieros
        // Ingresos provienen de las ventas.
        public ObservableCollection<MovimientoFinanciero> Ingresos { get; set; }

        // Egresos provienen de las compras (u otros gastos).
        public ObservableCollection<MovimientoFinanciero> Egresos { get; set; }
        #endregion

        #region Comando
        public ICommand ConsultarFinanzasCommand { get; set; }
        #endregion

        #region Métodos para Cargar y Calcular Datos
        /// <summary>
        /// Carga los datos reales de ventas, compras y calcula el valor del inventario.
        /// </summary>
        private void CargarDatos()
        {
            // Aseguramos que los parámetros sean válidos.
            DateTime fechaInicioValida = FechaInicio < new DateTime(1753, 1, 1) ? new DateTime(1753, 1, 1) : FechaInicio;
            DateTime fechaFinValida = FechaFin < new DateTime(1753, 1, 1) ? new DateTime(1753, 1, 1) : FechaFin;

            // Obtener ventas reales filtradas por fecha.
            var ventas = ventaRepository.GetReportes(fechaInicioValida, fechaFinValida);
            Ingresos.Clear();
            foreach (var venta in ventas)
            {
                Ingresos.Add(new MovimientoFinanciero
                {
                    Fecha = venta.FechaVenta,
                    Descripcion = "Venta #" + venta.VentaId,
                    Monto = venta.Total
                });
            }

            // Obtener compras reales filtradas por fecha, considerándolas como egresos.
            var compras = compraRepository.GetReportes(fechaInicioValida, fechaFinValida);
            Egresos.Clear();
            foreach (var compra in compras)
            {
                Egresos.Add(new MovimientoFinanciero
                {
                    Fecha = compra.FechaCompra,
                    Descripcion = "Compra #" + compra.CompraId,
                    Monto = compra.TotalCompra
                });
            }

            // Calcular el valor del inventario obteniendo la suma del ValorTotal de cada stock.
            ValorInventario = stockRepository.GetAll().Sum(s => s.ValorTotal);
        }

        /// <summary>
        /// Calcula el resumen financiero a partir de los movimientos de ingresos y egresos.
        /// </summary>
        private void CalcularResumen()
        {
            VentasTotales = Ingresos.Sum(i => i.Monto);
            EgresosTotales = Egresos.Sum(e => e.Monto);
            GananciaBruta = VentasTotales - Egresos.Sum(e => e.Monto);
        }

        /// <summary>
        /// Método que consulta y actualiza la información financiera según el rango de fechas.
        /// </summary>
        /// <param name="parameter"></param>
        private void ConsultarFinanzas(object parameter)
        {
            CargarDatos();
            CalcularResumen();
        }
        #endregion
        private decimal _egresosTotales;
        public decimal EgresosTotales
        {
            get => _egresosTotales;
            set
            {
                if (_egresosTotales != value)
                {
                    _egresosTotales = value;
                    OnPropertyChanged(nameof(EgresosTotales));
                }
            }
        }

    }

    /// <summary>
    /// Modelo para representar un movimiento financiero (ingreso o egreso).
    /// </summary>
    public class MovimientoFinanciero
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }


}
