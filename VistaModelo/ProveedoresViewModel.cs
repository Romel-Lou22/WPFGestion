using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SistemaGestion.Models;
using SistemaGestion.Repositories;
using SistemaGestion.View;

namespace SistemaGestion.VistaModelo
{
    public class ProveedoresViewModel : ViewModelBase
    {
        private readonly IProveedorRepository _proveedorRepository;
        private List<ProveedorModel> _allProveedores;
        private ObservableCollection<ProveedorModel> _proveedores;
        private string _filtroBusqueda;

        public ObservableCollection<ProveedorModel> Proveedores
        {
            get => _proveedores;
            set
            {
                _proveedores = value;
                OnPropertyChanged(nameof(Proveedores));
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

        // Comandos
        public ICommand AbrirAgregarProveedorCommand { get; }
        public ICommand CargarProveedoresCommand { get; }
        public ICommand EditarProveedorCommand { get; }
        public ICommand EliminarProveedorCommand { get; }
        public ICommand BuscarProveedorCommand { get; }

        public ProveedoresViewModel()
        {
            _proveedorRepository = new ProveedorRepository();
            Proveedores = new ObservableCollection<ProveedorModel>();
            _allProveedores = new List<ProveedorModel>();

            // Inicializamos comandos
            AbrirAgregarProveedorCommand = new ViewModelCommand(AbrirAgregarProveedor);
            CargarProveedoresCommand = new ViewModelCommand(CargarProveedores);
            EditarProveedorCommand = new ViewModelCommand(EditarProveedor, PuedeEditarProveedor);
            EliminarProveedorCommand = new ViewModelCommand(EliminarProveedor, PuedeEliminarProveedor);
            BuscarProveedorCommand = new ViewModelCommand(BuscarProveedor);

            // Cargar proveedores al iniciar
            CargarProveedores(null);
        }

        private void AbrirAgregarProveedor(object obj)
        {
            // Se abre la ventana para agregar un proveedor nuevo
            var ventana = new AgregarProveedorView(); // Asegúrate de tener esta vista
            ventana.DataContext = new AgregarProveedorViewModel();
            ventana.ShowDialog();
            CargarProveedores(null);
        }

        private void CargarProveedores(object obj)
        {
            var proveedoresBD = _proveedorRepository.GetAll().ToList();
            _allProveedores = proveedoresBD;
            Proveedores = new ObservableCollection<ProveedorModel>(_allProveedores);
        }

        private void BuscarProveedor(object obj)
        {
            if (string.IsNullOrWhiteSpace(FiltroBusqueda))
            {
                Proveedores = new ObservableCollection<ProveedorModel>(_allProveedores);
            }
            else
            {
                var texto = FiltroBusqueda.Trim().ToLower();
                var proveedoresFiltrados = _allProveedores
                    .Where(p => p.Nombre.ToLower().Contains(texto) ||
                                (!string.IsNullOrEmpty(p.Email) && p.Email.ToLower().Contains(texto)) ||
                                (!string.IsNullOrEmpty(p.Telefono) && p.Telefono.Contains(texto)))
                    .ToList();
                Proveedores = new ObservableCollection<ProveedorModel>(proveedoresFiltrados);
            }
        }

        private void EditarProveedor(object parameter)
        {
            if (parameter is ProveedorModel proveedor)
            {
                // Se abre la ventana de agregar/editar pasando el proveedor seleccionado
                var ventana = new AgregarProveedorView();
                ventana.DataContext = new AgregarProveedorViewModel(proveedor);
                ventana.ShowDialog();
                CargarProveedores(null);
            }
        }

        private bool PuedeEditarProveedor(object parameter)
        {
            return parameter is ProveedorModel;
        }

        private void EliminarProveedor(object parameter)
        {
            if (parameter is ProveedorModel proveedor)
            {
                var resultado = MessageBox.Show(
                    $"¿Está seguro de que desea eliminar el proveedor '{proveedor.Nombre}'?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    _proveedorRepository.Remove(proveedor.ProveedorId);
                    Proveedores.Remove(proveedor);
                }
            }
        }

        private bool PuedeEliminarProveedor(object parameter)
        {
            return parameter is ProveedorModel;
        }
    }
}
