using System;
using System.ComponentModel;

namespace SistemaGestion.Models
{
    public class ProductoModel : INotifyPropertyChanged
    {
        private int _id;
        private string _nombre;
        private decimal _precio;
        private string _codigoBarras;
        private DateTime _fechaCaducidad;
        private string _categoria;
        private bool _estado;

        public ProductoModel()
        {
            // Inicializa FechaCaducidad con la fecha actual para evitar el error
            FechaCaducidad = DateTime.Now;
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

        public decimal Precio
        {
            get => _precio;
            set { _precio = value; OnPropertyChanged(nameof(Precio)); }
        }

        public string CodigoBarras
        {
            get => _codigoBarras;
            set { _codigoBarras = value; OnPropertyChanged(nameof(CodigoBarras)); }
        }

        public DateTime FechaCaducidad
        {
            get => _fechaCaducidad;
            set { _fechaCaducidad = value; OnPropertyChanged(nameof(FechaCaducidad)); }
        }

        public string Categoria
        {
            get => _categoria;
            set { _categoria = value; OnPropertyChanged(nameof(Categoria)); }
        }

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
