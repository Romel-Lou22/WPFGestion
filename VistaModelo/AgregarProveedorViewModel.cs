using System;
using System.Linq;
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
            if (!ValidarCampos())
                return;

            string nombre = Proveedor.Nombre?.Trim();

            if (Proveedor.ProveedorId == 0)
            {
                // Validación para nuevo proveedor
                if (_proveedorRepository.ExisteProveedorNombre(nombre))
                {
                    MessageBox.Show("Ya existe un proveedor con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _proveedorRepository.Add(Proveedor);
                MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Reiniciar campos si es necesario.
            }
            else
            {
                // Validación para edición, excluyendo el proveedor actual
                if (_proveedorRepository.ExisteProveedorNombre(nombre, Proveedor.ProveedorId))
                {
                    MessageBox.Show("Ya existe otro proveedor con ese nombre.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _proveedorRepository.Edit(Proveedor);
                MessageBox.Show("Proveedor editado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Cerrar ventana o actualizar la UI según corresponda.
            }
        }

        private bool ValidarCampos()
        {
            // Validación de nombre obligatorio
            if (string.IsNullOrWhiteSpace(Proveedor.Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Validación de formato de email (si no está vacío)
            if (!string.IsNullOrWhiteSpace(Proveedor.Email))
            {
                // Patrón básico de email: [texto]@[texto].[texto]
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(Proveedor.Email, emailPattern))
                {
                    MessageBox.Show("El formato del correo no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Validación de teléfono (solo dígitos, si no está vacío)
            if (!string.IsNullOrWhiteSpace(Proveedor.Telefono))
            {
                if (!Proveedor.Telefono.All(char.IsDigit))
                {
                    MessageBox.Show("El teléfono solo debe contener dígitos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }

            // Validación de dirección (ejemplo: obligatorio)
            if (string.IsNullOrWhiteSpace(Proveedor.Direccion))
            {
                MessageBox.Show("La dirección es obligatoria.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true; // Si todas las validaciones pasaron
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
