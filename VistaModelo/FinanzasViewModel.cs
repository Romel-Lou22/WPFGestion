using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SistemaGestion.VistaModelo
{
    public class FinanzasViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        // Repositorios
        private readonly IVentaRepository ventaRepository;
        private readonly ICompraRepository compraRepository;
        private readonly IStockRepository stockRepository;

        // Diccionario para almacenar errores de validación
        private readonly Dictionary<string, List<string>> _errores = new Dictionary<string, List<string>>();

        public FinanzasViewModel()
        {
            // Inicializamos los repositorios
            ventaRepository = new VentaRepository();
            compraRepository = new CompraRepository();
            stockRepository = new StockRepository();

            // Inicializamos las colecciones
            Ingresos = new ObservableCollection<MovimientoFinanciero>();
            Egresos = new ObservableCollection<MovimientoFinanciero>();

            // Establecemos fechas iniciales
            FechaInicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            FechaFin = FechaInicio.AddMonths(1).AddDays(-1);

            // Inicializamos el comando
            ConsultarFinanzasCommand = new ViewModelCommand(ConsultarFinanzas, PuedeConsultarFinanzas);

            // Cargamos datos iniciales
            ConsultarFinanzas(null);
        }

        #region INotifyDataErrorInfo Implementation
        public bool HasErrors => _errores.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_errores.ContainsKey(propertyName))
                return null;

            return _errores[propertyName];
        }

        private void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AgregarError(string propertyName, string error)
        {
            if (!_errores.ContainsKey(propertyName))
                _errores[propertyName] = new List<string>();

            if (!_errores[propertyName].Contains(error))
            {
                _errores[propertyName].Add(error);
                OnErrorsChanged(propertyName);
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void LimpiarErrores(string propertyName)
        {
            if (_errores.ContainsKey(propertyName))
            {
                _errores.Remove(propertyName);
                OnErrorsChanged(propertyName);
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private bool ValidarFechas()
        {
            bool esValido = true;

            // Validación de fecha mínima
            var fechaMinima = new DateTime(1753, 1, 1);

            // Validar FechaInicio
            if (FechaInicio < fechaMinima)
            {
                AgregarError(nameof(FechaInicio), $"La fecha inicial no puede ser anterior a {fechaMinima.ToShortDateString()}");
                esValido = false;
            }
            else
            {
                LimpiarErrores(nameof(FechaInicio));
            }

            // Validar FechaFin
            if (FechaFin < fechaMinima)
            {
                AgregarError(nameof(FechaFin), $"La fecha final no puede ser anterior a {fechaMinima.ToShortDateString()}");
                esValido = false;
            }
            else
            {
                LimpiarErrores(nameof(FechaFin));
            }

            // Validación específica de relación entre fechas
            if (FechaInicio > FechaFin)
            {
                AgregarError(nameof(FechaInicio), "La fecha inicial no puede ser posterior a la fecha final");
                AgregarError(nameof(FechaFin), "La fecha final no puede ser anterior a la fecha inicial");
                esValido = false;
            }

            // Validar rango máximo razonable (por ejemplo, máximo 5 años)
            TimeSpan rango = FechaFin - FechaInicio;
            if (rango.TotalDays > 1825) // 5 años aproximadamente
            {
                AgregarError(nameof(FechaFin), "El rango de fechas no puede superar los 5 años");
                esValido = false;
            }

            return esValido;
        }

        private bool PuedeConsultarFinanzas(object parametro)
        {
            return !HasErrors;
        }
        #endregion

        #region Propiedades de Fechas
        private DateTime _fechaInicio;
        public DateTime FechaInicio
        {
            get => _fechaInicio;
            set
            {
                if (_fechaInicio != value)
                {
                    _fechaInicio = value;
                    OnPropertyChanged(nameof(FechaInicio));

                    // Validar después de establecer el valor
                    ValidarFechas();

                    // Solo consultar si no hay errores
                    if (!HasErrors)
                    {
                        ConsultarFinanzas(null);
                    }
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
                    _fechaFin = value;
                    OnPropertyChanged(nameof(FechaFin));

                    // Validar después de establecer el valor
                    ValidarFechas();

                    // Solo consultar si no hay errores
                    if (!HasErrors)
                    {
                        ConsultarFinanzas(null);
                    }
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
        public ObservableCollection<MovimientoFinanciero> Ingresos { get; set; }
        public ObservableCollection<MovimientoFinanciero> Egresos { get; set; }
        #endregion

        #region Comando
        public ICommand ConsultarFinanzasCommand { get; set; }
        #endregion

        #region Métodos para Cargar y Calcular Datos
        private void CargarDatos()
        {
            try
            {
                Ingresos.Clear();
                Egresos.Clear();

                // Obtener ventas
                var ventas = ventaRepository.GetReportes(FechaInicio, FechaFin);
                foreach (var venta in ventas)
                {
                    Ingresos.Add(new MovimientoFinanciero
                    {
                        Fecha = venta.FechaVenta,
                        Descripcion = "Venta #" + venta.VentaId,
                        Monto = venta.Total
                    });
                }

                // Obtener compras
                var compras = compraRepository.GetReportes(FechaInicio, FechaFin);
                foreach (var compra in compras)
                {
                    Egresos.Add(new MovimientoFinanciero
                    {
                        Fecha = compra.FechaCompra,
                        Descripcion = "Compra #" + compra.CompraId,
                        Monto = compra.TotalCompra
                    });
                }

                // Calcular valor del inventario
                ValorInventario = stockRepository.GetAll().Sum(s => s.ValorTotal);
            }
            catch (Exception ex)
            {
                // Manejar la excepción apropiadamente
                System.Diagnostics.Debug.WriteLine($"Error al cargar datos: {ex.Message}");

                // Opcionalmente, puedes mostrar un mensaje de error
                AgregarError("General", $"Error al cargar datos: {ex.Message}");
            }
        }

        private void CalcularResumen()
        {
            VentasTotales = Ingresos.Sum(i => i.Monto);
            EgresosTotales = Egresos.Sum(e => e.Monto);
            GananciaBruta = VentasTotales - EgresosTotales;
        }

        private void ConsultarFinanzas(object parameter)
        {
            if (ValidarFechas())
            {
                CargarDatos();
                CalcularResumen();
            }
        }
        #endregion
    }

    public class MovimientoFinanciero
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}