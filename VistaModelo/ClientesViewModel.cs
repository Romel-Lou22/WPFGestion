using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using SistemaGestion.View;

namespace SistemaGestion.VistaModelo
{
    public class ClientesViewModel : ViewModelBase
    {
        private readonly IClienteRepository _clienteRepository;
        private List<ClienteModel> _allClientes;
        private ObservableCollection<ClienteModel> _clientes;
        private string _filtroBusqueda;

        public ObservableCollection<ClienteModel> Clientes
        {
            get => _clientes;
            set
            {
                _clientes = value;
                OnPropertyChanged(nameof(Clientes));
            }
        }

        public string FiltroBusqueda
        {
            get => _filtroBusqueda;
            set
            {
                _filtroBusqueda = value;
                OnPropertyChanged(nameof(FiltroBusqueda));
            }
        }

        public ICommand AbrirAgregarClienteCommand { get; }
        public ICommand CargarClientesCommand { get; }
        public ICommand EditarClienteCommand { get; }
        public ICommand EliminarClienteCommand { get; }
        public ICommand BuscarClienteCommand { get; }
        public ICommand DeshabilitarClienteCommand { get; }

        public ClientesViewModel()
        {
            _clienteRepository = new ClienteRepository();
            Clientes = new ObservableCollection<ClienteModel>();
            _allClientes = new List<ClienteModel>();

            _clienteRepository = new ClienteRepository();
            // Cargar lista de clientes...
            DeshabilitarClienteCommand = new ViewModelCommand(DeshabilitarCliente, PuedeDeshabilitarCliente);

            // Inicializar comandos
            AbrirAgregarClienteCommand = new ViewModelCommand(AbrirAgregarCliente);
            CargarClientesCommand = new ViewModelCommand(CargarClientes);
            EditarClienteCommand = new ViewModelCommand(EditarCliente, PuedeEditarCliente);
            EliminarClienteCommand = new ViewModelCommand(EliminarCliente, PuedeEliminarCliente);
            BuscarClienteCommand = new ViewModelCommand(BuscarCliente);

            // Cargar clientes al iniciar
            CargarClientes(null);
        }

        private void AbrirAgregarCliente(object obj)
        {
            var ventana = new AgregarClienteView();
            ventana.DataContext = new AgregarClienteViewModel();
            ventana.ShowDialog();
            CargarClientes(null);
        }

        private void CargarClientes(object obj)
        {
            var clientesBD = _clienteRepository.GetAll().ToList();
            _allClientes = clientesBD;
            Clientes = new ObservableCollection<ClienteModel>(_allClientes);
        }

        private void BuscarCliente(object obj)
        {
            if (string.IsNullOrWhiteSpace(FiltroBusqueda))
            {
                Clientes = new ObservableCollection<ClienteModel>(_allClientes);
            }
            else
            {
                var texto = FiltroBusqueda.Trim().ToLower();
                var clientesFiltrados = _allClientes
                    .Where(c => c.Nombre.ToLower().Contains(texto) ||
                               (c.Email != null && c.Email.ToLower().Contains(texto)) ||
                               (c.Telefono != null && c.Telefono.Contains(texto)) ||
                               (c.Cedula != null && c.Cedula.ToLower().Contains(texto)))
                    .ToList();

                Clientes = new ObservableCollection<ClienteModel>(clientesFiltrados);
            }
        }


        private void EditarCliente(object parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                var ventana = new AgregarClienteView();
                ventana.DataContext = new AgregarClienteViewModel(cliente);
                ventana.ShowDialog();
                CargarClientes(null);
            }
        }

        private bool PuedeEditarCliente(object parameter)
        {
            return parameter is ClienteModel;
        }

        private void EliminarCliente(object parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                var resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el cliente '{cliente.Nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    _clienteRepository.Remove(cliente.Id);
                    Clientes.Remove(cliente);
                }
            }
        }

        private bool PuedeEliminarCliente(object parameter)
        {
            return parameter is ClienteModel;
        }

        private void DeshabilitarCliente(object parameter)
        {
            if (parameter is ClienteModel cliente)
            {
                var resultado = MessageBox.Show($"¿Está seguro de que desea deshabilitar al cliente '{cliente.Nombre}'?",
                                                 "Confirmar Deshabilitación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultado == MessageBoxResult.Yes)
                {
                    _clienteRepository.ActualizarEstado(cliente.Id, false);
                    cliente.Activo = false;  // Actualiza el modelo para refrescar la UI.
                }
            }
        }

        private bool PuedeDeshabilitarCliente(object parameter)
        {
            return parameter is ClienteModel cliente && cliente.Activo;
        }
    }
}