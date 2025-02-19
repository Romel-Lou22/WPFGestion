using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Models
{
    public class ClienteModel:INotifyPropertyChanged
    {
        private int _id;
        private string _nombre;
        private string _cedula;
        private string _telefono;
        private string _email;
        private string _direccion;
        private DateTime _fechaCreacion;
        private DateTime? _fechaModificacion;
        private bool _activo;

        public ClienteModel()
        {
            FechaCreacion = DateTime.Now;
        }


        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(nameof(Nombre)); }
        }

        public string Cedula
        {
            get => _cedula;
            set { _cedula = value; OnPropertyChanged(nameof(Cedula)); }

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

        public DateTime FechaCreacion
        {
            get => _fechaCreacion;
            set { _fechaCreacion = value; OnPropertyChanged(nameof(FechaCreacion)); }
        }

        public DateTime? FechaModificacion
        {
            get => _fechaModificacion;
            set { _fechaModificacion = value; OnPropertyChanged(nameof(FechaModificacion)); }
        }

        public bool Activo
        {
            get => _activo;
            set { _activo = value; OnPropertyChanged(nameof(Activo)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
