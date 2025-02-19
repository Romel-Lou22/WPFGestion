using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Windows;
using System.Windows.Input;

namespace SistemaGestion.VistaModelo
{
    public class AgregarProductoViewModel : ViewModelBase
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoModel Producto { get; set; }

        public ICommand GuardarProductoCommand { get; }
        public ICommand CerrarVentanaCommand { get; }

        // Constructor para agregar un producto nuevo
        public AgregarProductoViewModel()
        {
            _productoRepository = new ProductoRepository(); // Instancia el repositorio de productos
            Producto = new ProductoModel(); // Nuevo producto vacío

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
            if (string.IsNullOrEmpty(Producto.Nombre) || Producto.Precio <= 0)
            {
                MessageBox.Show("Debe llenar todos los campos correctamente.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Producto.Id == 0)
            {
                _productoRepository.Add(Producto);
                MessageBox.Show("Producto agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _productoRepository.Edit(Producto);
                MessageBox.Show("Producto editado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            CerrarVentana(obj);
        }



        private void CerrarVentana(object obj)
        {
            Window ventana = obj as Window;
            ventana?.Close();
        }
    }

}
