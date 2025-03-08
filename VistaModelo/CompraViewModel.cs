using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Linq;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using SistemaGestion.Services;  // Agregar esta referencia

namespace SistemaGestion.VistaModelo
{
    public class CompraViewModel : ViewModelBase
    {
        private readonly ICompraRepository _compraRepository;
        private readonly InventarioService _inventarioService;  // Añadir servicio de inventario

        public CompraModel Compra { get; set; }

        public ObservableCollection<ProveedorModel> ListaProveedores { get; set; }
        public ObservableCollection<ProductoModel> ListaProductos { get; set; }

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

            // Inicializar el servicio de inventario
            var stockRepository = new StockRepository(); // Asumiendo que tienes esta implementación
            _inventarioService = new InventarioService(stockRepository);

            Compra = new CompraModel();

            RegistrarCompraCommand = new ViewModelCommand(RegistrarCompra);
            AgregarDetalleCommand = new ViewModelCommand(AgregarDetalle);
            EliminarDetalleCommand = new ViewModelCommand(EliminarDetalle, CanEliminarDetalle);

            // Cargar proveedores
            var proveedorRepository = new ProveedorRepository();
            ListaProveedores = new ObservableCollection<ProveedorModel>(proveedorRepository.GetAll());

            // Cargar productos
            var productoRepository = new ProductoRepository();
            ListaProductos = new ObservableCollection<ProductoModel>(productoRepository.GetAll());
        }

        private void RegistrarCompra(object obj)
        {
            try
            {
                // Registrar la compra primero
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

        // El resto de métodos se mantienen igual...
        private void AgregarDetalle(object obj)
        {
            if (SelectedProducto != null)
            {
                // Agrega un nuevo detalle usando el producto seleccionado
                Compra.Detalles.Add(new DetalleCompraModel
                {
                    ProductoId = SelectedProducto.Id,
                    Cantidad = 1,
                    PrecioUnitario = 0
                });
                // Opcional: Resetear la selección
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