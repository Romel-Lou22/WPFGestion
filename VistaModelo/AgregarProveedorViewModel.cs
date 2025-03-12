using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.VistaModelo
{
    public class AgregarProveedorViewModel : ViewModelBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        public ProveedorModel Proveedor { get; set; }

        public ICommand GuardarProveedorCommand { get; }
        public ICommand CerrarVentanaCommand { get; }

        // Constructor para agregar un proveedor nuevo
        public AgregarProveedorViewModel()
        {
            _proveedorRepository = new ProveedorRepository();
            Proveedor = new ProveedorModel(); // Nuevo proveedor; ProveedorId será 0
            GuardarProveedorCommand = new ViewModelCommand(GuardarProveedor);
            CerrarVentanaCommand = new ViewModelCommand(CerrarVentana);
        }

        // Constructor para editar un proveedor existente
        public AgregarProveedorViewModel(ProveedorModel proveedorExistente) : this()
        {
            Proveedor = proveedorExistente;
        }

        private void GuardarProveedor(object obj)
        {
            // Validación básica (por ejemplo, el nombre es obligatorio)
            if (string.IsNullOrEmpty(Proveedor.Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validación de email si está presente
            if (!string.IsNullOrEmpty(Proveedor.Email) && !Regex.IsMatch(Proveedor.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Por favor, ingrese un email válido.",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Si ProveedorId es 0, es un proveedor nuevo; si no, se actualiza
                if (Proveedor.ProveedorId == 0)
                {
                    _proveedorRepository.add(Proveedor);
                    MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    _proveedorRepository.edit(Proveedor);
                    MessageBox.Show("Proveedor actualizado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                CerrarVentana(obj);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al guardar el proveedor: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void CerrarVentana(object obj)
        {
            if (obj is Window ventana)
            {
                ventana.Close();
            }
        }
    }
}
