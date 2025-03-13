using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SistemaGestion.VistaModelo
{
    public class AgregarProductoViewModel : ViewModelBase
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoModel Producto { get; set; }

        // Propiedad para la lista de categorías
        public ObservableCollection<string> ListaCategorias { get; set; }

        public ICommand GuardarProductoCommand { get; }
        public ICommand CerrarVentanaCommand { get; }

        // Constructor para agregar un producto nuevo
        public AgregarProductoViewModel()
        {
            _productoRepository = new ProductoRepository(); // Instancia el repositorio de productos
            Producto = new ProductoModel(); // Nuevo producto vacío

            // Inicializar la lista de categorías
            ListaCategorias = new ObservableCollection<string>
            {
                "Alimentos",
                "Bebidas",
                "Lácteos",
                "Carnes y Embutidos",
                "Productos de Limpieza",
                "Granos y Cereales",
                "Frutas y Verduras",
                "Productos de Papel",
                "Confitería",
                "Conservas",
                "Aceites y Grasas",
                "Snacks",
                "Productos de Aseo",
                "Agua y Bebidas No Alcohólicas",
                "Huevos"
            };

            GuardarProductoCommand = new ViewModelCommand(GuardarProducto);
            CerrarVentanaCommand = new ViewModelCommand(CerrarVentana);
        }

        // Constructor para editar un producto existente
        public AgregarProductoViewModel(ProductoModel productoExistente) : this()
        {
            Producto = productoExistente;
        }

        private void GuardarProducto(object obj)
        {
            // Validación: El nombre es obligatorio.
            if (string.IsNullOrEmpty(Producto.Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación: El precio debe ser mayor a cero (evitando negativos y cero).
            if (Producto.Precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a cero y no puede ser negativo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación: El stock no puede ser negativo (puede ser cero).
            if (Producto.Stock < 0)
            {
                MessageBox.Show("El stock no puede ser negativo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Producto.Id == 0)
            {
                _productoRepository.Add(Producto);
                MessageBox.Show("Producto agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpiar los campos reiniciando la propiedad Producto
                Producto = new ProductoModel();
                OnPropertyChanged(nameof(Producto));
            }
            else
            {
                _productoRepository.Edit(Producto);
                MessageBox.Show("Producto editado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CerrarVentana(obj);
            }
        }


        private void CerrarVentana(object obj)
        {
            Window ventana = obj as Window;
            ventana?.Close();
        }
    }
}
