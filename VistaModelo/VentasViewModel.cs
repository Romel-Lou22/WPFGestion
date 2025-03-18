using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SistemaGestion.Comprobante;
using SistemaGestion.Services;
using MessageBox = System.Windows.MessageBox;

namespace SistemaGestion.VistaModelo
{
    public class VentasViewModel : ViewModelBase
    {
        #region Repositorios y Servicios
        private readonly IVentaRepository _ventaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IStockRepository _stockRepository;
        private readonly InventarioService _inventarioService;
        #endregion

        #region Propiedades Privadas
        private VentaModel _venta;
        private ClienteModel _selectedCliente;
        private ProductoModel _productoSeleccionado;
        private DetalleVentaModel _detalleSeleccionado;
        private ObservableCollection<ClienteModel> _listaClientes;
        private ObservableCollection<ProductoModel> _listaProductos;
        private string _filtroCliente;
        private string _filtroProducto;
        private decimal _ivaPorcentaje = 0.15m; // Se asume 15%
        private decimal _pagoCliente;           // Monto que el cliente entrega
        private bool _efectivoExacto;
        private bool _esConsumidorFinal;
        private List<ClienteModel> _allClientes;  // Lista completa de clientes
        private List<ProductoModel> _allProductos; // Lista completa de productos
        #endregion

        #region Constructor
        public VentasViewModel()
        {
            // Instanciar repositorios
            _ventaRepository = new VentaRepository();
            _clienteRepository = new ClienteRepository();
            _productoRepository = new ProductoRepository();
            _stockRepository = new StockRepository();

            // Instanciar el servicio de inventario
            _inventarioService = new InventarioService(_stockRepository);

            // Inicializar la venta
            Venta = new VentaModel();
            // Suscribirse a los cambios de la colección de detalles de Venta
            Venta.Detalles.CollectionChanged += Detalles_CollectionChanged;

            // Cargar la lista completa de clientes
            // Cargar la lista de clientes activos
            _allClientes = _clienteRepository.GetClientesActivos().ToList();
            ListaClientes = new ObservableCollection<ClienteModel>(_allClientes);



            // Cargar la lista de productos activos
            _allProductos = _productoRepository.GetProductosActivos().ToList();
            ListaProductos = new ObservableCollection<ProductoModel>(_allProductos);


            // Inicializar comandos
            RegistrarVentaCommand = new ViewModelCommand(RegistrarVenta, PuedeRegistrarVenta);
            EliminarDetalleCommand = new ViewModelCommand(EliminarDetalle, PuedeEliminarDetalle);
            BuscarClienteCommand = new ViewModelCommand(BuscarCliente);
            BuscarProductoCommand = new ViewModelCommand(BuscarProducto);
            AgregarProductoCommand = new ViewModelCommand(AgregarProducto);
            CancelarVentaCommand = new ViewModelCommand(CancelarVenta);
        }
        #endregion

        #region Propiedades Públicas
        // Objeto principal de la venta
        public VentaModel Venta
        {
            get => _venta;
            set { _venta = value; OnPropertyChanged(nameof(Venta)); }
        }

        // Cliente seleccionado
        public ClienteModel SelectedCliente
        {
            get => _selectedCliente;
            set { _selectedCliente = value; OnPropertyChanged(nameof(SelectedCliente)); }
        }

        // Producto seleccionado en la lista de búsqueda
        public ProductoModel ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                OnPropertyChanged(nameof(ProductoSeleccionado));
            }
        }

        // Detalle seleccionado para eliminar
        public DetalleVentaModel DetalleSeleccionado
        {
            get => _detalleSeleccionado;
            set { _detalleSeleccionado = value; OnPropertyChanged(nameof(DetalleSeleccionado)); }
        }

        // Listas para UI
        public ObservableCollection<ClienteModel> ListaClientes
        {
            get => _listaClientes;
            set { _listaClientes = value; OnPropertyChanged(nameof(ListaClientes)); }
        }

        public ObservableCollection<ProductoModel> ListaProductos
        {
            get => _listaProductos;
            set { _listaProductos = value; OnPropertyChanged(nameof(ListaProductos)); }
        }

        // Filtros de búsqueda
        public string FiltroCliente
        {
            get => _filtroCliente;
            set { _filtroCliente = value; OnPropertyChanged(nameof(FiltroCliente)); }
        }

        public string FiltroProducto
        {
            get => _filtroProducto;
            set { _filtroProducto = value; OnPropertyChanged(nameof(FiltroProducto)); }
        }

        // Propiedades de cálculo
        public decimal IVAPorcentaje
        {
            get => _ivaPorcentaje;
            set
            {
                _ivaPorcentaje = value;
                OnPropertyChanged(nameof(IVAPorcentaje));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(Vuelto));
            }
        }

        public decimal Subtotal => Venta.Detalles.Sum(d => d.ImporteTotal);
        public decimal IVA => Subtotal * IVAPorcentaje;
        public decimal Total => Subtotal + IVA;

        public decimal PagoCliente
        {
            get => _pagoCliente;
            set
            {
                if (value < 0)
                {
                    MessageBox.Show("El efectivo recibido no puede ser negativo.",
                        "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _pagoCliente = value;
                OnPropertyChanged(nameof(PagoCliente));
                OnPropertyChanged(nameof(Vuelto));
            }
        }


        public bool EfectivoExacto
        {
            get => _efectivoExacto;
            set
            {
                _efectivoExacto = value;
                OnPropertyChanged(nameof(EfectivoExacto));
                OnPropertyChanged(nameof(Vuelto));
            }
        }

        public decimal Vuelto => EfectivoExacto ? 0 : (PagoCliente - Total);

        public bool EsConsumidorFinal
        {
            get => _esConsumidorFinal;
            set
            {
                _esConsumidorFinal = value;
                OnPropertyChanged(nameof(EsConsumidorFinal));
                if (_esConsumidorFinal)
                {
                    Venta.ClienteId = null;
                }
                else if (SelectedCliente != null)
                {
                    Venta.ClienteId = SelectedCliente.Id;
                }
            }
        }
        #endregion

        #region Comandos
        public ICommand RegistrarVentaCommand { get; }
        public ICommand EliminarDetalleCommand { get; }
        public ICommand BuscarClienteCommand { get; }
        public ICommand BuscarProductoCommand { get; }
        public ICommand AgregarProductoCommand { get; }
        public ICommand CancelarVentaCommand { get; }
        #endregion

        #region Métodos de Validación
        // Método para validar si hay stock disponible
        private bool ValidarStockDisponible(DetalleVentaModel detalle)
        {
            StockModel stock = _stockRepository.GetByProductoId(detalle.ProductoId);

            if (stock == null || stock.CantidadDisponible < detalle.Cantidad)
            {
                decimal cantidadDisponible = stock?.CantidadDisponible ?? 0;
                MessageBox.Show($"Stock insuficiente para el producto '{detalle.NombreProducto}'. " +
                    $"Stock disponible: {cantidadDisponible}, Cantidad solicitada: {detalle.Cantidad}",
                    "Stock no disponible", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        // Validar si se puede registrar la venta
        private bool PuedeRegistrarVenta(object obj)
        {
            // Verificar que haya al menos un producto en la venta
            return Venta.Detalles.Count > 0;
        }
        #endregion

        #region Métodos de Negocio
        private void RegistrarVenta(object obj)
        {
            try
            {
                // Mostrar mensaje de confirmación
                var resultado = MessageBox.Show("¿Está seguro de realizar la venta?", "Confirmación de Venta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado != MessageBoxResult.Yes)
                {
                    return;
                }

                // Verificar que haya al menos un producto
                if (Venta.Detalles.Count == 0)
                {
                    MessageBox.Show("Debe agregar al menos un producto para registrar la venta.",
                        "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Verificar stock para todos los productos antes de proceder
                foreach (var detalle in Venta.Detalles)
                {
                    if (!ValidarStockDisponible(detalle))
                    {
                        // Si algún producto no tiene stock suficiente, cancelar la operación
                        return;
                    }
                }

                // Asignar el total calculado a la venta
                Venta.Total = Total;
                if (!EsConsumidorFinal && SelectedCliente != null)
                    Venta.ClienteId = SelectedCliente.Id;

                // Registrar la venta en la base de datos
                _ventaRepository.Add(Venta);

                // Actualizar el inventario para cada detalle de venta
                foreach (var detalle in Venta.Detalles)
                {
                    _inventarioService.ActualizarStockVenta(detalle);
                }

                // Generar comprobante
                ComprobanteVenta.GenerarComprobantePDF(Venta, SelectedCliente, "Efectivo", Subtotal, IVA, Total);

                MessageBox.Show("Venta registrada exitosamente");

                // Reiniciar para nueva venta
                Venta = new VentaModel();
                Venta.Detalles.CollectionChanged += Detalles_CollectionChanged;
                SelectedCliente = null;
                EsConsumidorFinal = false;
                PagoCliente = 0;
                EfectivoExacto = false;

                NotificarCambiosCalculos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la venta: " + ex.Message);
            }
        }


        private void EliminarDetalle(object obj)
        {
            if (DetalleSeleccionado != null)
            {
                Venta.Detalles.Remove(DetalleSeleccionado);
                // Actualizar cálculos
                NotificarCambiosCalculos();
            }
        }

        private bool PuedeEliminarDetalle(object obj) => DetalleSeleccionado != null;

        private void BuscarCliente(object obj)
        {
            if (string.IsNullOrWhiteSpace(FiltroCliente))
            {
                ListaClientes = new ObservableCollection<ClienteModel>(_allClientes);
            }
            else
            {
                var filtro = FiltroCliente.Trim().ToLower();
                var filtrados = _allClientes
                    .Where(c => c.Nombre.ToLower().Contains(filtro))
                    .ToList();
                ListaClientes = new ObservableCollection<ClienteModel>(filtrados);
            }
        }

        private void BuscarProducto(object obj)
        {
            if (string.IsNullOrWhiteSpace(FiltroProducto))
            {
                ListaProductos = new ObservableCollection<ProductoModel>(_allProductos);
            }
            else
            {
                var filtro = FiltroProducto.Trim().ToLower();
                var filtrados = _allProductos
                    .Where(p => p.Nombre.ToLower().Contains(filtro)
                             || p.CodigoBarras.ToLower().Contains(filtro))
                    .ToList();
                ListaProductos = new ObservableCollection<ProductoModel>(filtrados);
            }
        }

        private void AgregarProducto(object obj)
        {
            if (ProductoSeleccionado == null)
            {
                MessageBox.Show("Seleccione un producto de la lista");
                return;
            }

            // Buscar si el producto ya está agregado en el detalle
            var detalleExistente = Venta.Detalles.FirstOrDefault(d => d.ProductoId == ProductoSeleccionado.Id);
            if (detalleExistente != null)
            {
                // Calcular la nueva cantidad
                int nuevaCantidad = detalleExistente.Cantidad + 1;
                // Verificar stock para la nueva cantidad
                StockModel stock = _stockRepository.GetByProductoId(ProductoSeleccionado.Id);
                if (stock == null || stock.CantidadDisponible < nuevaCantidad)
                {
                    decimal cantidadDisponible = stock?.CantidadDisponible ?? 0;
                    MessageBox.Show($"No hay suficiente stock para incrementar la cantidad del producto '{ProductoSeleccionado.Nombre}'. Stock disponible: {cantidadDisponible}.",
                        "Stock no disponible", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                // Actualizar la cantidad
                detalleExistente.Cantidad = nuevaCantidad;
                NotificarCambiosCalculos();
            }
            else
            {
                // Crear un nuevo detalle si el producto no existe aún
                var detalle = new DetalleVentaModel
                {
                    DetalleVentaId = 0, // Se asigna en la BD al guardar
                    ProductoId = ProductoSeleccionado.Id,
                    NombreProducto = ProductoSeleccionado.Nombre,
                    Cantidad = 1,
                    PrecioUnitario = ProductoSeleccionado.Precio
                };

                // Validar stock para el nuevo detalle
                if (!ValidarStockDisponible(detalle))
                {
                    return; // No agregar si no hay stock suficiente
                }

                Venta.Detalles.Add(detalle);
                NotificarCambiosCalculos();
            }
        }


        private void CancelarVenta(object obj)
        {
            // Restablecer la venta a un nuevo objeto
            Venta = new VentaModel();
            Venta.Detalles.CollectionChanged += Detalles_CollectionChanged;

            // Limpiar la selección de cliente
            SelectedCliente = null;
            EsConsumidorFinal = false;
            PagoCliente = 0;
            EfectivoExacto = false;

            // Notificar cambios de propiedades calculadas
            NotificarCambiosCalculos();

            // Mensaje opcional para confirmar que se canceló
            MessageBox.Show("La venta ha sido cancelada.", "Cancelar", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion

        #region Métodos de Eventos
        // Método que se llama cuando la colección cambia
        private void Detalles_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (DetalleVentaModel nuevo in e.NewItems)
                {
                    // Suscribirse a los cambios de cada nuevo detalle
                    nuevo.PropertyChanged += Detalle_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (DetalleVentaModel viejo in e.OldItems)
                {
                    // Desuscribirse de los cambios de cada detalle eliminado
                    viejo.PropertyChanged -= Detalle_PropertyChanged;
                }
            }

            // Notificar a la UI que la colección y los totales han cambiado
            NotificarCambiosCalculos();

            // Notificar para actualizar el estado de los comandos
            CommandManager.InvalidateRequerySuggested();
        }

        // Método que se llama cuando cambian propiedades de un detalle
        private void Detalle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DetalleVentaModel.Cantidad))
            {
                // Validar stock cuando cambia la cantidad
                DetalleVentaModel detalle = sender as DetalleVentaModel;
                if (detalle != null && !ValidarStockDisponible(detalle))
                {
                    // Si no hay stock suficiente, revertir al valor anterior o ajustar
                    StockModel stock = _stockRepository.GetByProductoId(detalle.ProductoId);
                    decimal disponible = stock?.CantidadDisponible ?? 0;
                    if (disponible > 0)
                    {
                        // Establecer la cantidad al máximo disponible
                        detalle.Cantidad = (int)disponible;
                        MessageBox.Show($"Se ajustó la cantidad al máximo disponible: {disponible}");
                    }
                    else
                    {
                        // Si no hay nada disponible, remover el detalle
                        Venta.Detalles.Remove(detalle);
                        MessageBox.Show("El producto se ha eliminado del carrito por falta de stock");
                    }
                }
            }

            if (e.PropertyName == nameof(DetalleVentaModel.Cantidad) ||
                e.PropertyName == nameof(DetalleVentaModel.PrecioUnitario))
            {
                // Notificar que se deben recalcular las propiedades dependientes
                NotificarCambiosCalculos();
            }
        }

        // Método auxiliar para notificar cambios en los cálculos
        private void NotificarCambiosCalculos()
        {
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(IVA));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Vuelto));
        }
        #endregion
    }
}