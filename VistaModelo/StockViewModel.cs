using SistemaGestion.Models;
using SistemaGestion.Repositories;
using System.Collections.ObjectModel;
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
            set { _listaStock = value; OnPropertyChanged(nameof(ListaStock)); }
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

        public ICommand BuscarStockCommand { get; }
        public ICommand ActualizarStockCommand { get; }

        public StockViewModel()
        {
            _stockRepository = new StockRepository();
            _productoRepository = new ProductoRepository();

            // Cargar la lista completa de stock desde el repositorio
            ListaStock = new ObservableCollection<StockModel>(_stockRepository.GetAll());
            ListaStockFiltrada = new ObservableCollection<StockModel>(ListaStock);

            BuscarStockCommand = new ViewModelCommand(BuscarStock);
            ActualizarStockCommand = new ViewModelCommand(ActualizarStock);
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
        }

        private void ActualizarStock(object obj)
        {
            ListaStock = new ObservableCollection<StockModel>(_stockRepository.GetAll());
            ListaStockFiltrada = new ObservableCollection<StockModel>(ListaStock);
            MessageBox.Show("Stock actualizado");
        }
    }
}
