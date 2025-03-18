using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using SistemaGestion.Services;

namespace SistemaGestion.VistaModelo
{
    public class CompraViewModel : ViewModelBase
    {
        private readonly ICompraRepository _compraRepository;
        private readonly InventarioService _inventarioService;
        private DetalleCompraRepository _detalleCompraRepository;

        public CompraModel Compra { get; set; }

        public ObservableCollection<ProveedorModel> ListaProveedores { get; set; }
        public ObservableCollection<ProductoModel> ListaProductos { get; set; }

        // Propiedad para el proveedor seleccionado
        private ProveedorModel _selectedProveedor;
        public ProveedorModel SelectedProveedor
        {
            get => _selectedProveedor;
            set { _selectedProveedor = value; OnPropertyChanged(nameof(SelectedProveedor)); }
        }

        // Propiedad para el producto seleccionado en el ComboBox
        private ProductoModel _selectedProducto;
        public ProductoModel SelectedProducto
        {
            get => _selectedProducto;
            set { _selectedProducto = value; OnPropertyChanged(nameof(SelectedProducto)); }
        }

        private DetalleCompraModel _detalleSeleccionado;
        public DetalleCompraModel DetalleSeleccionado
        {
            get => _detalleSeleccionado;
            set { _detalleSeleccionado = value; OnPropertyChanged(nameof(DetalleSeleccionado)); }
        }

        public ICommand RegistrarCompraCommand { get; }
        public ICommand AgregarDetalleCommand { get; }
        public ICommand EliminarDetalleCommand { get; }

        public CompraViewModel()
        {
            _compraRepository = new CompraRepository();
            _detalleCompraRepository = new DetalleCompraRepository();

            // Inicializar el servicio de inventario
            var stockRepository = new StockRepository(); // Se asume que existe esta implementación
            _inventarioService = new InventarioService(stockRepository);

            Compra = new CompraModel();

            RegistrarCompraCommand = new ViewModelCommand(RegistrarCompra);
            AgregarDetalleCommand = new ViewModelCommand(AgregarDetalle);
            EliminarDetalleCommand = new ViewModelCommand(EliminarDetalle, CanEliminarDetalle);

            // Cargar proveedores
            var proveedorRepository = new ProveedorRepository();
            ListaProveedores = new ObservableCollection<ProveedorModel>(proveedorRepository.GetProveedoresActivos());

            // Cargar productos
            var productoRepository = new ProductoRepository();
            ListaProductos = new ObservableCollection<ProductoModel>(productoRepository.GetProductosActivos());
        }

        private void RegistrarCompra(object obj)
        {
            // Validación: Se debe seleccionar un proveedor.
            if (SelectedProveedor == null)
            {
                MessageBox.Show("Debe seleccionar un proveedor.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación: Se debe agregar al menos un producto en los detalles.
            if (Compra.Detalles == null || Compra.Detalles.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto a la compra.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación: No se permiten valores negativos en cantidad y precio unitario.
            foreach (var detalle in Compra.Detalles)
            {
                if (detalle.Cantidad <= 0)
                {
                    MessageBox.Show("Ingrese cantidad mayor a 0.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (detalle.PrecioUnitario <= 0)
                {
                    MessageBox.Show("Ingrese precio unitario mayor a 0.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                // Asignar el proveedor a la compra (asumiendo que CompraModel tiene una propiedad ProveedorId)
                Compra.ProveedorId = SelectedProveedor.ProveedorId;

                // Registrar la compra
                _compraRepository.Add(Compra);

                // Actualizar el inventario para cada detalle de la compra
                foreach (var detalle in Compra.Detalles)
                {
                    _inventarioService.ActualizarStockCompra(detalle);
                }

                MessageBox.Show("Compra registrada exitosamente y stock actualizado", "Información",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Reiniciar para una nueva compra
                Compra = new CompraModel();
                OnPropertyChanged(nameof(Compra));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error al registrar la compra: " + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AgregarDetalle(object obj)
        {
            if (SelectedProducto != null)
            {
                // Obtener el último precio registrado para el producto.
                // Se asume que tienes acceso a una instancia del repository que implementa GetUltimoPrecioCompra.
                decimal precioCompra = 0;
                decimal? ultimoPrecio = _detalleCompraRepository.GetUltimoPrecioCompra(SelectedProducto.Id);
                if (ultimoPrecio.HasValue)
                    precioCompra = ultimoPrecio.Value;
                else
                    precioCompra = 0;  // Si nunca se ha comprado, asigna 0.

                // Verificar si el producto ya existe en la compra para actualizar el detalle existente.
                var detalleExistente = Compra.Detalles.FirstOrDefault(d => d.ProductoId == SelectedProducto.Id);
                if (detalleExistente != null)
                {
                    detalleExistente.PrecioUnitario = precioCompra;
                    detalleExistente.Cantidad += 1;
                }
                else
                {
                    Compra.Detalles.Add(new DetalleCompraModel
                    {
                        ProductoId = SelectedProducto.Id,
                        Cantidad = 1,
                        PrecioUnitario = precioCompra
                    });
                }
                // Reiniciar la selección del producto
                SelectedProducto = null;
                OnPropertyChanged(nameof(Compra));
            }
            else
            {
                MessageBox.Show("Seleccione un producto para agregar.", "Información",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EliminarDetalle(object obj)
        {
            if (DetalleSeleccionado != null)
            {
                // Elimina el detalle seleccionado de la colección
                Compra.Detalles.Remove(DetalleSeleccionado);
                OnPropertyChanged(nameof(Compra));
            }
            else
            {
                MessageBox.Show("Seleccione un detalle para eliminar.", "Información",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool CanEliminarDetalle(object obj)
        {
            return DetalleSeleccionado != null;
        }
    }
}
