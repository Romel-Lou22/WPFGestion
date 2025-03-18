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
        public ObservableCollection<string> ListaCategorias { get; set; }
        public ICommand GuardarProductoCommand { get; }
        public ICommand CerrarVentanaCommand { get; }

        // Constructor para agregar un producto nuevo
        public AgregarProductoViewModel()
        {
            _productoRepository = new ProductoRepository();
            Producto = new ProductoModel
            {
                // Asignamos el stock por defecto a 1 ya que el campo estará oculto
                Stock = 1
            };

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
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Id del producto en edición: {Producto.Id}");
        }

        // Valida solo los campos obligatorios (Nombre y Precio)
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(Producto.Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (Producto.Precio <= 0)
            {
                MessageBox.Show("El precio debe ser mayor a cero y no puede ser negativo.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Producto.CodigoBarras))
            {
                MessageBox.Show("El código de barras es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(Producto.Categoria))
            {
                MessageBox.Show("La categoría es obligatoria.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }



        private void GuardarProducto(object obj)
        {
            if (!ValidarCampos())
                return;

            // Si por alguna razón el stock no es mayor a cero, asignar valor por defecto 1.
            if (Producto.Stock <= 0)
            {
                Producto.Stock = 1;
            }

            if (Producto.Id == 0)
            {
                // Validar duplicado al agregar un nuevo producto
                if (_productoRepository.ExisteProducto(Producto.Nombre))
                {
                    MessageBox.Show("Ya existe un producto con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _productoRepository.Add(Producto);
                MessageBox.Show("Producto agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reinicia el modelo para agregar un nuevo producto, manteniendo el stock por defecto.
                Producto = new ProductoModel { Stock = 1 };
                OnPropertyChanged(nameof(Producto));
            }
            else
            {
                // Al editar, se valida excluyendo el producto actual
                if (_productoRepository.ExisteProducto(Producto.Nombre, Producto.Id))
                {
                    MessageBox.Show("Ya existe otro producto con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _productoRepository.Edit(Producto);
                MessageBox.Show("Producto editado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                CerrarVentana(obj);
            }
        }

        private void CerrarVentana(object obj)
        {
            if (obj is Window ventana)
                ventana.Close();
        }
    }
}
