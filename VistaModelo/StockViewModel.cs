using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SistemaGestion.VistaModelo
{
    public class StockViewModel : ViewModelBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductoRepository _productoRepository; // para obtener info adicional si es necesario

        private ObservableCollection<StockModel> _listaStock;
        public ObservableCollection<StockModel> ListaStock
        {
            get => _listaStock;
            set
            {
                if (_listaStock != value)
                {
                    if (_listaStock != null)
                        _listaStock.CollectionChanged -= ListaStock_CollectionChanged;
                    _listaStock = value;
                    _listaStock.CollectionChanged += ListaStock_CollectionChanged;
                    SubscribeToStockChanges();
                    OnPropertyChanged(nameof(ListaStock));
                    OnPropertyChanged(nameof(ValorTotalInventario));
                }
            }
        }

        private ObservableCollection<StockModel> _listaStockFiltrada;
        public ObservableCollection<StockModel> ListaStockFiltrada
        {
            get => _listaStockFiltrada;
            set { _listaStockFiltrada = value; OnPropertyChanged(nameof(ListaStockFiltrada)); }
        }

        private string _filtroProducto;
        public string FiltroProducto
        {
            get => _filtroProducto;
            set { _filtroProducto = value; OnPropertyChanged(nameof(FiltroProducto)); }
        }

        // Propiedad calculada que suma el ValorTotal de cada StockModel
        public decimal ValorTotalInventario => ListaStock?.Sum(s => s.ValorTotal) ?? 0;

        public ICommand BuscarStockCommand { get; }
        public ICommand ActualizarStockCommand { get; }

        public StockViewModel()
        {
            _stockRepository = new StockRepository();
            _productoRepository = new ProductoRepository();

            // Cargar la lista completa de stock desde el repositorio
            ListaStock = new ObservableCollection<StockModel>(_stockRepository.GetAll());
            ListaStock.CollectionChanged += ListaStock_CollectionChanged;
            SubscribeToStockChanges();
            ListaStockFiltrada = new ObservableCollection<StockModel>(ListaStock);

            BuscarStockCommand = new ViewModelCommand(BuscarStock);
            ActualizarStockCommand = new ViewModelCommand(ActualizarStock);
        }

        // Suscribirse al PropertyChanged de cada StockModel para actualizar el total
        private void SubscribeToStockChanges()
        {
            foreach (var stock in ListaStock)
            {
                stock.PropertyChanged -= Stock_PropertyChanged;
                stock.PropertyChanged += Stock_PropertyChanged;
            }
        }

        // Maneja cambios en la colección (nuevos o eliminados elementos)
        private void ListaStock_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    if (newItem is StockModel stock)
                        stock.PropertyChanged += Stock_PropertyChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    if (oldItem is StockModel stock)
                        stock.PropertyChanged -= Stock_PropertyChanged;
                }
            }
            OnPropertyChanged(nameof(ValorTotalInventario));
        }

        // Cuando cambia alguna propiedad relevante en StockModel, actualiza el total
        private void Stock_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(StockModel.CantidadDisponible) ||
                e.PropertyName == nameof(StockModel.PrecioUnitario) ||
                e.PropertyName == "ValorTotal")
            {
                OnPropertyChanged(nameof(ValorTotalInventario));
            }
        }

        private void BuscarStock(object obj)
        {
            if (string.IsNullOrWhiteSpace(FiltroProducto))
            {
                ListaStockFiltrada = new ObservableCollection<StockModel>(ListaStock);
            }
            else
            {
                var filtro = FiltroProducto.Trim().ToLower();
                // Filtrar usando la propiedad NombreProducto en lugar de ProductoId
                var filtrados = ListaStock.Where(s => s.NombreProducto.ToLower().Contains(filtro)).ToList();
                ListaStockFiltrada = new ObservableCollection<StockModel>(filtrados);
            }
            OnPropertyChanged(nameof(ValorTotalInventario));
        }

        private void ActualizarStock(object obj)
        {
            ListaStock = new ObservableCollection<StockModel>(_stockRepository.GetAll());
            ListaStockFiltrada = new ObservableCollection<StockModel>(ListaStock);
            MessageBox.Show("Stock actualizado");
        }
    }
}
