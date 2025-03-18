using System;
using System.ComponentModel;

namespace SistemaGestion.Models
{
    public class ProveedorModel : INotifyPropertyChanged
    {
        private int _proveedorId;
        private string _nombre;
        private string _telefono;
        private string _email;
        private string _direccion;
        private bool _estado;  // Usamos Estado

        public ProveedorModel()
        {
            // Por defecto, se crea activo
            Estado = true;
        }

        public int ProveedorId
        {
            get => _proveedorId;
            set { _proveedorId = value; OnPropertyChanged(nameof(ProveedorId)); }
        }

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string Telefono
        {
            get => _telefono;
            set { _telefono = value; OnPropertyChanged(nameof(Telefono)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Direccion
        {
            get => _direccion;
            set { _direccion = value; OnPropertyChanged(nameof(Direccion)); }
        }

        // Propiedad que indica si el proveedor está activo o inactivo.
        public bool Estado
        {
            get => _estado;
            set { _estado = value; OnPropertyChanged(nameof(Estado)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
