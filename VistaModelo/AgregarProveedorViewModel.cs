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
            // 1. Validaciones previas
            if (!ValidarCampos())
                return;

            try
            {
                if (Proveedor.ProveedorId == 0)
                {
                    // Agregar proveedor nuevo
                    _proveedorRepository.add(Proveedor);
                    MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Limpiar los campos para agregar uno nuevo
                    Proveedor = new ProveedorModel();
                    OnPropertyChanged(nameof(Proveedor));
                }
                else
                {
                    // Actualizar proveedor existente y cerrar la ventana
                    _proveedorRepository.edit(Proveedor);
                    MessageBox.Show("Proveedor actualizado con éxito.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CerrarVentana(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el proveedor: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
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
