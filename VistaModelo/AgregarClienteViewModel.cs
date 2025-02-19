using System.Windows;
using System.Windows.Input;
using SistemaGestion.Models;
using SistemaGestion.Repositories;

namespace SistemaGestion.VistaModelo
{
    public class AgregarClienteViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteModel Cliente { get; set; }
        public ICommand GuardarClienteCommand { get; }
        public ICommand CerrarVentanaCommand { get; }

        // Constructor para nuevo cliente
        public AgregarClienteViewModel()
        {
            _clienteRepository = new ClienteRepository();
            Cliente = new ClienteModel { Activo = true }; // Por defecto activo
            GuardarClienteCommand = new ViewModelCommand(GuardarCliente);
            CerrarVentanaCommand = new ViewModelCommand(CerrarVentana);
        }

        // Constructor para editar cliente existente
        public AgregarClienteViewModel(ClienteModel clienteExistente) : this()
        {
            Cliente = clienteExistente;
        }

        private void GuardarCliente(object obj)
        {
            if (string.IsNullOrEmpty(Cliente.Nombre) || string.IsNullOrEmpty(Cliente.Telefono))
            {
                MessageBox.Show("El nombre y teléfono son obligatorios.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            // Validación básica de email
            if (!string.IsNullOrEmpty(Cliente.Email) && !Cliente.Email.Contains("@"))
            {
                MessageBox.Show("Por favor, ingrese un email válido.",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (Cliente.Id == 0)
                {
                    _clienteRepository.Add(Cliente);
                    MessageBox.Show("Cliente agregado con éxito.",
                                  "Éxito",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                else
                {
                    _clienteRepository.Edit(Cliente);
                    MessageBox.Show("Cliente actualizado con éxito.",
                                  "Éxito",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                CerrarVentana(obj);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente: {ex.Message}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }

            CerrarVentana(obj);
        }

        public void CerrarVentana(object obj)
        {
            Window ventana = obj as Window;
            ventana?.Close();
        }
    }
}