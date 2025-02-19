using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Collections.ObjectModel;
using SistemaGestion.View;

namespace SistemaGestion.VistaModelo
{
    public class ProductosViewModel : ViewModelBase
    {
        private readonly IProductoRepository _productoRepository;

        // Lista privada con todos los productos (sin filtrar)
        private List<ProductoModel> _allProductos;

        // Propiedad que se enlaza al DataGrid (lista filtrada)
        private ObservableCollection<ProductoModel> _productos;
        public ObservableCollection<ProductoModel> Productos
        {
            get => _productos;
            set
            {
                _productos = value;
                OnPropertyChanged(nameof(Productos));
            }
        }

        // Propiedad para el texto de búsqueda
        private string _filtroBusqueda;
        public string FiltroBusqueda
        {
            get => _filtroBusqueda;
            set
            {
                _filtroBusqueda = value;
                OnPropertyChanged(nameof(FiltroBusqueda));
                // NO llamamos a FiltrarProductos() aquí, se hará al pulsar el botón
            }
        }

        // Comandos existentes
        public ICommand AbrirAgregarProductoCommand { get; set; }
        public ICommand CargarProductosCommand { get; set; }
        public ICommand EditarProductoCommand { get; set; }
        public ICommand EliminarProductoCommand { get; set; }
        public ICommand BuscarProductoCommand { get; set; }

        // Constructor
        public ProductosViewModel()
        {
            // Repositorio y colecciones
            _productoRepository = new ProductoRepository();
            Productos = new ObservableCollection<ProductoModel>();
            _allProductos = new List<ProductoModel>();

            // Comandos
            AbrirAgregarProductoCommand = new ViewModelCommand(AbrirAgregarProducto);
            CargarProductosCommand = new ViewModelCommand(CargarProductos);
            EliminarProductoCommand = new ViewModelCommand(EliminarProducto, PuedeEliminarProducto);
            EditarProductoCommand = new ViewModelCommand(EditarProducto, PuedeEditarProducto);
            BuscarProductoCommand = new ViewModelCommand(BuscarProducto);

            // Cargar productos al iniciar
            CargarProductos(null);
        }

        // Método que abre la ventana para agregar un nuevo producto
        private void AbrirAgregarProducto(object obj)
        {
            var ventana = new AgregarProductoView();
            ventana.DataContext = new AgregarProductoViewModel();
            ventana.ShowDialog();
            CargarProductos(null); // Recargar la lista después de agregar
        }

        // Método que carga los productos desde el repositorio
        private void CargarProductos(object obj)
        {
            var productosBD = _productoRepository.GetAll().ToList();
            _allProductos = productosBD; // Guarda la lista completa
            Productos = new ObservableCollection<ProductoModel>(_allProductos); // Muestra todos los productos
        }

        // Método para filtrar productos
        private void FiltrarProductos()
        {
            if (string.IsNullOrWhiteSpace(FiltroBusqueda))
            {
                // Sin filtro, mostrar todos los productos
                Productos = new ObservableCollection<ProductoModel>(_allProductos);
            }
            else
            {
                var texto = FiltroBusqueda.Trim().ToLower();
                var productosFiltrados = _allProductos
                    .Where(p => p.Nombre.ToLower().Contains(texto) ||
                                p.CodigoBarras.ToLower().Contains(texto))
                    .ToList();

                Productos = new ObservableCollection<ProductoModel>(productosFiltrados);
            }
        }

        // Método para ejecutar la búsqueda al pulsar el botón
        private void BuscarProducto(object obj)
        {
            FiltrarProductos();
        }

        // Lógica del comando Eliminar
        private void EliminarProducto(object parameter)
        {
            if (parameter is ProductoModel producto)
            {
                var resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el producto '{producto.Nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    _productoRepository.Remove(producto.Nombre);
                    Productos.Remove(producto);
                }
            }
        }

        // Determina si el botón "Eliminar" está habilitado
        private bool PuedeEliminarProducto(object parameter)
        {
            return parameter is ProductoModel;
        }

        // Lógica del comando Editar
        private void EditarProducto(object parameter)
        {
            if (parameter is ProductoModel producto)
            {
                var ventana = new AgregarProductoView();
                ventana.DataContext = new AgregarProductoViewModel(producto);
                ventana.ShowDialog();
                CargarProductos(null); // Recargar la lista después de editar
            }
        }

        // Determina si el botón "Editar" está habilitado
        private bool PuedeEditarProducto(object parameter)
        {
            return parameter is ProductoModel;
        }
    }
}
